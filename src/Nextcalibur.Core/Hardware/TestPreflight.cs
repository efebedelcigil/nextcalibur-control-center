using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Hardware;

/// <summary>What a running thing is, for the ceiling test's list.</summary>
public enum RunningKind
{
    /// <summary>A program with a window.</summary>
    App,
    /// <summary>A program without one, using the processor or the disk.</summary>
    Background,
    /// <summary>A service that is not Windows', using the processor or the disk: stopped, and started again after.</summary>
    Service,
    /// <summary>Moving data - a download, an update, a copy: shown apart, since these are easy to forget.</summary>
    Transfer,
}

/// <summary>One thing running, as the ceiling test lists it.</summary>
public sealed record RunningItem(RunningKind Kind, string Name, int Pid, string? Path, double CpuPercent, double MegabytesPerSecond, string? ServiceName);

/// <summary>What was found: work that must not be interrupted (the test does not start), and what would be closed.</summary>
public sealed record Preflight(IReadOnlyList<string> CriticalWork, IReadOnlyList<RunningItem> ToClose);

/// <summary>
/// Before a ceiling test: what is running (the owner's rules of 14 October
/// 2026). Anything that could leave Windows, a driver or the firmware half
/// done - an update downloading or installing, a driver or BIOS install,
/// disk maintenance, a Defender scan, indexing, a restore point being made -
/// stops the test before it starts, and nothing is closed. Otherwise the
/// programs, the background work, the services that are not Windows' and
/// the transfers in progress are listed for closing.
///
/// Never on the list, whatever they do: Windows itself (anything under the
/// Windows folder), Windows Defender, security software, the drivers'
/// helpers (NVIDIA, Intel, Realtek, AMD), input devices' helpers, and
/// Nextcalibur. A program whose file cannot be read is left alone too - what
/// cannot be told apart is not closed.
/// </summary>
public static class TestPreflight
{
    private static readonly string[] CriticalNames =
    [
        "TiWorker", "TrustedInstaller", "MoUsoCoreWorker", "usocoreworker", "wuauclt", "WaaSMedicAgent", "SIHClient",
        "DrvInst", "pnputil", "defrag", "dfrgui", "chkdsk", "autochk", "MRT", "MpCmdRun", "srtasks",
        "AFUWIN", "AFUWINx64", "FWUpdLcl", "FWUpdLcl64", "BIOSUpdate", "WinFlash", "H2OFFT-W", "Flash",
    ];

    // Busy enough to count as work in progress over the sample.
    private const double BusyCpuPercent = 1.0, BusyMegabytesPerSecond = 0.5, TransferMegabytesPerSecond = 1.0;

    private static readonly string[] NeverCloseCompanies =
    [
        "NVIDIA", "Intel", "Realtek", "Advanced Micro Devices", "AMD", "Logitech", "Razer", "Corsair", "SteelSeries",
        "Kaspersky", "ESET", "Avast", "AVG", "Bitdefender", "Norton", "Gen Digital", "McAfee", "Malwarebytes", "Sophos", "Trend Micro",
    ];

    /// <summary>Two looks a few seconds apart: what is running, and how busy each thing is.</summary>
    public static Preflight Scan(TimeSpan? sample = null)
    {
        var window = sample ?? TimeSpan.FromSeconds(3);
        var self = Environment.ProcessId;
        var session = Process.GetCurrentProcess().SessionId;
        var windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

        var first = Snapshot();
        Thread.Sleep(window);
        var second = Snapshot();
        var services = RunningServices();

        var critical = new List<string>();
        var items = new List<RunningItem>();
        foreach (var (pid, now) in second)
        {
            if (pid == self || pid is 0 or 4) continue;
            var before = first.GetValueOrDefault(pid);
            var cpu = before.Cpu > 0 ? Math.Max(0, (now.Cpu - before.Cpu) / window.TotalSeconds / Environment.ProcessorCount * 100) : 0;
            var mbps = before.Io > 0 ? Math.Max(0, (now.Io - before.Io) / window.TotalSeconds / 1_048_576.0) : 0;
            var busy = cpu >= BusyCpuPercent || mbps >= BusyMegabytesPerSecond;

            if (IsCritical(now.Name, now.Path, cpu, mbps) is { } why)
            {
                critical.Add(why);
                continue;
            }

            if (now.Path is null || now.Path.StartsWith(windows, StringComparison.OrdinalIgnoreCase)) continue;
            if (now.Name.Contains("Nextcalibur", StringComparison.OrdinalIgnoreCase)) continue;
            if (Company(now.Path) is { } company && NeverCloseCompanies.Any(c => company.Contains(c, StringComparison.OrdinalIgnoreCase))) continue;
            if (now.Path.Contains("Windows Defender", StringComparison.OrdinalIgnoreCase)) continue;

            if (services.TryGetValue(pid, out var service))
            {
                if (busy) items.Add(new RunningItem(RunningKind.Service, service.Display, pid, now.Path, cpu, mbps, service.Name));
                continue;
            }
            if (now.Session != session) continue;

            var kind = mbps >= TransferMegabytesPerSecond ? RunningKind.Transfer
                : now.HasWindow ? RunningKind.App
                : busy ? RunningKind.Background
                : (RunningKind?)null;
            if (kind is { } k) items.Add(new RunningItem(k, now.Name, pid, now.Path, cpu, mbps, null));
        }

        return new Preflight(critical.Distinct().ToList(), items.OrderBy(i => i.Kind).ThenByDescending(i => i.CpuPercent).ToList());
    }

    /// <summary>Why a process means the test must not start now, or null.</summary>
    public static string? IsCritical(string name, string? path, double cpu, double mbps)
    {
        var active = cpu >= BusyCpuPercent || mbps >= BusyMegabytesPerSecond;
        if (CriticalNames.Any(n => string.Equals(n, name, StringComparison.OrdinalIgnoreCase)))
            return name is "TiWorker" or "TrustedInstaller" or "MoUsoCoreWorker" or "usocoreworker" or "WaaSMedicAgent" or "SIHClient" or "MpCmdRun" or "srtasks"
                ? active ? Describe(name) : null
                : Describe(name);
        if (string.Equals(name, "msiexec", StringComparison.OrdinalIgnoreCase) && active)
            return Words.Get("S.Core.Critical.Install", "a program being installed ({0})", name);
        if (string.Equals(name, "MsMpEng", StringComparison.OrdinalIgnoreCase) && cpu >= 10)
            return Words.Get("S.Core.Critical.Defender", "a Microsoft Defender scan ({0})", name);
        if (string.Equals(name, "SearchIndexer", StringComparison.OrdinalIgnoreCase) && cpu >= 10)
            return Words.Get("S.Core.Critical.Indexing", "Windows Search indexing ({0})", name);
        if (string.Equals(name, "vssvc", StringComparison.OrdinalIgnoreCase) && active)
            return Words.Get("S.Core.Critical.RestorePoint", "a restore point being made ({0})", name);

        // A driver, firmware or BIOS installer - by a maker of the machine's parts - at work.
        if (active && path is not null && LooksLikeInstaller(name)
            && Company(path) is { } company
            && new[] { "Microsoft", "NVIDIA", "Intel", "Realtek", "Advanced Micro Devices", "American Megatrends", "Casper", "Tongfang" }
                .Any(c => company.Contains(c, StringComparison.OrdinalIgnoreCase)))
            return Words.Get("S.Core.Critical.Driver", "a driver or firmware install ({0})", $"{name}, {company}");
        return null;
    }

    /// <summary>The words for one of <see cref="CriticalNames"/>, its process name kept in brackets for the log.</summary>
    private static string Describe(string name) => name.ToLowerInvariant() switch
    {
        "tiworker" or "trustedinstaller" or "mousocoreworker" or "usocoreworker" or "wuauclt" or "waasmedicagent" or "sihclient"
            => Words.Get("S.Core.Critical.Update", "Windows Update ({0})", name),
        "drvinst" or "pnputil" => Words.Get("S.Core.Critical.Driver", "a driver or firmware install ({0})", name),
        "defrag" or "dfrgui" => Words.Get("S.Core.Critical.Optimise", "disk optimisation ({0})", name),
        "chkdsk" or "autochk" => Words.Get("S.Core.Critical.DiskCheck", "a disk check ({0})", name),
        "mrt" or "mpcmdrun" => Words.Get("S.Core.Critical.Defender", "a Microsoft Defender scan ({0})", name),
        "srtasks" => Words.Get("S.Core.Critical.RestorePoint", "a restore point being made ({0})", name),
        _ => Words.Get("S.Core.Critical.Bios", "a BIOS or firmware update ({0})", name),
    };

    private static bool LooksLikeInstaller(string name) =>
        name.Contains("setup", StringComparison.OrdinalIgnoreCase) || name.Contains("install", StringComparison.OrdinalIgnoreCase)
        || name.Contains("update", StringComparison.OrdinalIgnoreCase) || name.Contains("flash", StringComparison.OrdinalIgnoreCase);

    private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, string?> Companies = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>The file's company, remembered: the same programs come up scan after scan.</summary>
    private static string? Company(string path)
    {
        if (Companies.TryGetValue(path, out var known)) return known;
        string? company;
        try { company = FileVersionInfo.GetVersionInfo(path).CompanyName; }
        catch (Exception ex) when (ex is FileNotFoundException or IOException or UnauthorizedAccessException) { company = null; }
        if (Companies.Count < 2000) Companies[path] = company;
        return company;
    }

    /// <summary>A process's image path, or null when it cannot be asked (protected, gone).</summary>
    private static string? ImagePath(int pid)
    {
        var handle = OpenProcess(ProcessQueryLimitedInformation, false, pid);
        if (handle == IntPtr.Zero) return null;
        try
        {
            var buffer = new char[1024];
            var size = buffer.Length;
            return QueryFullProcessImageName(handle, 0, buffer, ref size) ? new string(buffer, 0, size) : null;
        }
        finally
        {
            CloseHandle(handle);
        }
    }

    private const uint ProcessQueryLimitedInformation = 0x1000;

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint access, bool inherit, int pid);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "QueryFullProcessImageNameW")]
    private static extern bool QueryFullProcessImageName(IntPtr process, int flags, [Out] char[] name, ref int size);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr handle);

    private readonly record struct ProcessLook(string Name, string? Path, int Session, bool HasWindow, double Cpu, double Io);

    private static Dictionary<int, ProcessLook> Snapshot()
    {
        var looks = new Dictionary<int, ProcessLook>();
        foreach (var p in Process.GetProcesses())
        {
            using (p)
            {
                try
                {
                    // The image path from one cheap query, not MainModule - which lists
                    // every module of every process, and this runs by itself now and then.
                    var path = ImagePath(p.Id);
                    double cpu = 0;
                    try { cpu = p.TotalProcessorTime.TotalSeconds; } catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException) { }
                    double io = 0;
                    try { if (GetProcessIoCounters(p.Handle, out var c)) io = c.ReadTransferCount + c.WriteTransferCount + c.OtherTransferCount; }
                    catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException) { }
                    looks[p.Id] = new ProcessLook(p.ProcessName, path, p.SessionId, p.MainWindowHandle != IntPtr.Zero, cpu, io);
                }
                catch (Exception ex) when (ex is InvalidOperationException or System.ComponentModel.Win32Exception)
                {
                    // Gone between the listing and the look.
                }
            }
        }
        return looks;
    }

    private static Dictionary<int, (string Name, string Display)> RunningServices()
    {
        var services = new Dictionary<int, (string, string)>();
        try
        {
            using var searcher = new ManagementObjectSearcher("SELECT Name, DisplayName, ProcessId FROM Win32_Service WHERE State = 'Running'");
            using var results = searcher.Get();
            foreach (ManagementBaseObject o in results)
            {
                using (o)
                {
                    if (o["ProcessId"] is uint pid && pid > 0 && o["Name"] is string name)
                        services.TryAdd((int)pid, (name, o["DisplayName"] as string ?? name));
                }
            }
        }
        catch (Exception ex) when (ex is ManagementException or COMException or UnauthorizedAccessException)
        {
        }
        return services;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct IoCounters
    {
        public ulong ReadOperationCount, WriteOperationCount, OtherOperationCount;
        public ulong ReadTransferCount, WriteTransferCount, OtherTransferCount;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GetProcessIoCounters(IntPtr process, out IoCounters counters);
}
