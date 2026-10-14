using System.Diagnostics;
using System.Management;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;

namespace Nextcalibur.Cli;

/// <summary>
/// <c>nextcalibur selftest</c>: every place the application meets Windows or
/// the hardware, called for real, one line each - PASS, FAIL or SKIP with
/// what was seen. Written after a program-start watch failed silently for a
/// whole session (14 October 2026): unit tests prove the logic, this proves
/// the machine answers. Reads only, except two things it undoes at once: a
/// short-lived copy of ping is started (and put into Efficiency mode) to see
/// that a program start is noticed. Run elevated, as the application runs.
/// </summary>
internal static class SelfTest
{
    private static int _fails;

    public static int Run()
    {
        Console.WriteLine($"Nextcalibur self-test, {DateTimeOffset.Now:yyyy-MM-dd'T'HH:mm:sszzz}, elevated {Elevation.IsElevated()}");

        // ---- power plans
        foreach (var mode in SystemModes.InOrder)
            Check($"plan for {mode}", () => SystemModeService.PlanFor(mode) is { } p ? (true, $"{p.Plan:D} ours={p.Ours}") : (false, "no plan"));
        Check("mode profile baseline", () => { var b = ModeProfile.ReadBaseline(); return (b.Balanced.Count > 0, $"hybrid={b.Hybrid}, balanced settings {b.Balanced.Count}"); });
        foreach (var mode in SystemModes.InOrder)
            Check($"processor ceiling readable ({mode})", () =>
                SystemModeService.PlanFor(mode) is { } p && PlanTuning.Read(p.Plan, PlanTuning.MaxState) is { } v ? (true, $"{v.Ac}/{v.Dc}%") : (false, "unreadable"));
        Check("power mode (overlay)", () => (true, PowerOverlayService.GetActiveOverlay().ToString("D")));
        foreach (var mode in SystemModes.InOrder)
            Check($"sleep and screen times ({mode})", () =>
                SystemModeService.PlanFor(mode) is { } p && IdleTimeouts.Read(p.Plan) is { } t
                    ? (true, $"screen {t.Display.Ac / 60}/{t.Display.Dc / 60} min, sleep {t.Sleep.Ac / 60}/{t.Sleep.Dc / 60} min") : (false, "unreadable"));
        foreach (var kind in new[] { DeviceSaving.Usb, DeviceSaving.Wifi })
            Check($"{kind} power saving readable", () =>
                SystemModeService.PlanFor(SystemMode.Balanced) is { } p && DevicePowerSaving.Read(p.Plan, kind) is { } v ? (true, $"{v.Ac}/{v.Dc}") : (false, "unreadable"));

        // ---- hardware readings
        Check("firmware mailbox present", () => (EcMailbox.IsSupported(), ""));
        Check("embedded controller thermal read", () =>
        {
            using var mailbox = new EcMailbox();
            return new ThermalReader(mailbox).TryRead(out var s) ? (true, $"CPU {s.CpuTemperatureC} °C, GPU {s.GpuTemperatureC} °C, fans {s.CpuFanRpm}/{s.GpuFanRpm}") : (false, "no answer");
        });
        Check("PawnIO processor reads (MSR)", () =>
        {
            if (!CpuPowerReader.DriverInstalled) return (null, "PawnIO not installed");
            using var msr = new CpuPowerReader();
            msr.ReadWatts(); Thread.Sleep(300);
            return msr.ReadPackageTemperatureC() is { } c ? (true, $"{c} °C, {msr.ReadWatts():0.0} W") : (false, "no reading");
        });
        Check("processor clock", () => { using var c = new CpuClockReader(); c.ReadGhz(); Thread.Sleep(300); return c.ReadGhz() is { } g ? (true, $"{g:0.00} GHz") : (false, "no reading"); });
        Check("processor use (all cores)", () =>
        {
            var load = new CoreLoad(); load.Read(); Thread.Sleep(500);
            var cores = load.Read();
            return cores.Length > 0 ? (true, $"{cores.Average():0}% over {cores.Length} threads") : (false, "no reading");
        });
        Check("graphics card (NVML)", () =>
        {
            using var gpu = new GpuClockReader();
            return gpu.ReadTemperatureC() is { } t ? (true, $"{t} °C, {gpu.ReadLoad()?.UtilisationPercent}% used") : (null, "asleep or no NVIDIA driver");
        });
        Check("NVIDIA power management mode (NVAPI)", () => NvidiaPowerMode.Read() is { } m ? (true, $"value {m.Value}, set {m.Set}") : (null, "not readable"));
        Check("firmware tables (SMBIOS)", () => { var f = FirmwareInventory.Read(); return (f.BiosVersion is { Length: > 0 }, $"BIOS {f.BiosVersion}, top clock {f.ProcessorMaxMhz} MHz"); });

        // ---- the person and the screen
        Check("notification state", () => (true, UserPresence.Describe()));
        Check("time since last input", () => (true, UserPresence.SinceLastInput().ToString(@"hh\:mm\:ss")));
        Check("full-screen program in front", () => (true, UserPresence.ForegroundFullScreenProcessId()?.ToString() ?? "none"));

        // ---- programs: what the game rules and Efficiency mode rest on
        Check("game scan", () => { var g = GameLibrary.Scan(); return (true, $"{g.Count} game(s)"); });
        Check("background scan (preflight)", () =>
        {
            var watch = Stopwatch.StartNew();
            var p = TestPreflight.Scan(TimeSpan.FromSeconds(1));
            return (true, $"{p.ToClose.Count} listed, {p.CriticalWork.Count} critical, {watch.ElapsedMilliseconds} ms");
        });
        ProgramStarts();

        // ---- notifications: a toast needs a Start-menu shortcut carrying the application's identity
        Check("Start-menu shortcut (for notifications)", () =>
        {
            var links = new[] { Environment.SpecialFolder.Programs, Environment.SpecialFolder.CommonPrograms }
                .Select(Environment.GetFolderPath).Where(Directory.Exists)
                .SelectMany(f => Directory.EnumerateFiles(f, "Nextcalibur*.lnk", SearchOption.AllDirectories)).ToList();
            return (links.Count > 0, links.Count > 0 ? string.Join(", ", links.Select(Path.GetFileName)) : "none: toasts fall back to the tray balloon");
        });

        // ---- the application's own files
        Check("protected settings folder writable", () =>
        {
            var probe = Path.Combine(Nextcalibur.Core.Security.ProtectedStore.Folder, "selftest.tmp");
            File.WriteAllText(probe, "x"); File.Delete(probe);
            return (true, Nextcalibur.Core.Security.ProtectedStore.Folder);
        });
        Check("power report (powercfg /batteryreport)", () =>
        {
            var file = Path.Combine(Path.GetTempPath(), "nc-selftest-battery.html");
            using var p = Process.Start(new ProcessStartInfo(Nextcalibur.Core.Security.SystemTools.Powercfg, $"/batteryreport /output \"{file}\"") { CreateNoWindow = true, UseShellExecute = false, RedirectStandardOutput = true })!;
            p.StandardOutput.ReadToEnd(); p.WaitForExit(30_000);
            var ok = File.Exists(file); if (ok) File.Delete(file);
            return (ok, $"exit {p.ExitCode}");
        });

        Console.WriteLine(_fails == 0 ? "RESULT: all passed" : $"RESULT: {_fails} failed");
        return _fails;
    }

    /// <summary>
    /// The program-start watch the rules use, and its fallback: Windows'
    /// process-start event if it is delivered here, and in any case the process
    /// list, which must see a program within seconds. Also Efficiency mode on
    /// that program.
    /// </summary>
    private static void ProgramStarts()
    {
        var copy = Path.Combine(Path.GetTempPath(), "ncselftest.exe");
        File.Copy(Path.Combine(Environment.SystemDirectory, "PING.EXE"), copy, overwrite: true);
        ManagementEventWatcher? watcher = null;
        var seen = new ManualResetEventSlim();
        string? watchFailed = null;
        try
        {
            watcher = new ManagementEventWatcher(new WqlEventQuery("SELECT ProcessName FROM Win32_ProcessStartTrace"));
            watcher.EventArrived += (_, e) => { if (ProgramRules.IsWatched(new HashSet<string>(["ncselftest"], StringComparer.OrdinalIgnoreCase), e.NewEvent["ProcessName"] as string)) seen.Set(); };
            watcher.Start();
        }
        catch (Exception ex) when (ex is ManagementException or UnauthorizedAccessException or System.Runtime.InteropServices.COMException)
        {
            watchFailed = ex.Message.Trim();
            watcher?.Dispose(); watcher = null;
        }

        using var program = Process.Start(new ProcessStartInfo(copy, "-n 8 127.0.0.1") { CreateNoWindow = true, UseShellExecute = false, RedirectStandardOutput = true })!;
        try
        {
            if (watchFailed is not null) Report(null, "program-start event (WMI)", $"not delivered on this Windows: {watchFailed}; the application looks at the process list instead");
            else Report(seen.Wait(TimeSpan.FromSeconds(5)), "program-start event (WMI)", "a started program was reported");

            var listed = false;
            var watch = Stopwatch.StartNew();
            while (watch.Elapsed < TimeSpan.FromSeconds(4) && !listed)
            {
                foreach (var p in Process.GetProcesses()) using (p) if (string.Equals(p.ProcessName, "ncselftest", StringComparison.OrdinalIgnoreCase)) listed = true;
                if (!listed) Thread.Sleep(200);
            }
            Report(listed, "program seen in the process list (rules fallback)", $"{watch.ElapsedMilliseconds} ms");
            Report(EfficiencyMode.Set(program.Id, true), "Efficiency mode on a program", $"pid {program.Id}");
        }
        finally
        {
            try { program.Kill(); program.WaitForExit(3000); } catch (Exception ex) when (ex is InvalidOperationException or System.ComponentModel.Win32Exception) { }
            watcher?.Stop(); watcher?.Dispose();
            try { File.Delete(copy); } catch (IOException) { } catch (UnauthorizedAccessException) { }
        }
    }

    private static void Check(string name, Func<(bool? Ok, string Detail)> probe)
    {
        try
        {
            var (ok, detail) = probe();
            Report(ok, name, detail);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Report(false, name, $"{ex.GetType().Name}: {ex.Message.Trim()}");
        }
    }

    private static void Report(bool? ok, string name, string detail)
    {
        if (ok == false) _fails++;
        Console.WriteLine($"{(ok switch { true => "PASS", false => "FAIL", null => "SKIP" }),-5} {name}{(detail.Length > 0 ? "  -- " + detail : "")}");
    }
}
