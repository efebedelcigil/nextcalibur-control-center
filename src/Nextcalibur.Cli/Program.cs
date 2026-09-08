using System.Diagnostics;
using System.Security.Principal;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;

namespace Nextcalibur.Cli;

internal static class Program
{
    private static int Main(string[] args)
    {
        var command = args.Length > 0 ? args[0].ToLowerInvariant() : "help";

        try
        {
            return command switch
            {
                "sensors" => Sensors(args),
                "watch" => Watch(args),
                "overlay" => Overlay(args),
                "info" => Info(),
                _ => Help(),
            };
        }
        catch (EcMailboxUnavailableException ex)
        {
            Error(ex.Message);
            return 2;
        }
        catch (Exception ex)
        {
            Error(ex.Message);
            return 1;
        }
    }

    private static int Help()
    {
        Console.WriteLine("""
            Nextcalibur Control Center - command line

              nextcalibur info              Show hardware support and environment
              nextcalibur sensors           Read temperatures and fan speeds once
              nextcalibur watch [seconds]   Stream sensor readings (default 30)
              nextcalibur overlay           Diagnose the Windows power-mode overlay
              nextcalibur overlay --fix     Repair the stuck-overlay fault

            Not affiliated with Casper, Tongfang, or Uniwill.
            """);
        return 0;
    }

    private static int Info()
    {
        var supported = EcMailbox.IsSupported();
        Console.WriteLine($"Firmware mailbox (RW_GMWMI) : {(supported ? "present" : "NOT FOUND")}");
        Console.WriteLine($"Elevated                    : {(IsElevated() ? "yes" : "no")}");

        var stock = FindStockSoftware();
        Console.WriteLine($"Vendor Control Center       : {(stock is null ? "not running" : $"RUNNING (pid {stock})")}");

        if (stock is not null)
        {
            Warn("The vendor Control Center writes to the same firmware mailbox.");
            Warn("Close it before using fan or LED features, or readings will race.");
        }

        if (!supported)
        {
            Warn("This machine does not expose the interface Nextcalibur needs.");
            return 3;
        }

        return 0;
    }

    private static int Sensors(string[] args)
    {
        using var mailbox = new EcMailbox();
        var sample = new ThermalReader(mailbox).Read();
        PrintSample(sample);
        return 0;
    }

    private static int Watch(string[] args)
    {
        var seconds = args.Length > 1 && int.TryParse(args[1], out var s) ? s : 30;

        using var mailbox = new EcMailbox();
        var reader = new ThermalReader(mailbox);

        Console.WriteLine($"{"time",-10} {"CPU °C",7} {"GPU °C",7} {"CPU RPM",8} {"GPU RPM",8}");
        Console.WriteLine(new string('-', 44));

        var deadline = DateTime.UtcNow.AddSeconds(seconds);
        var failures = 0;

        while (DateTime.UtcNow < deadline)
        {
            if (reader.TryRead(out var sample))
            {
                failures = 0;
                Console.WriteLine(
                    $"{sample.Timestamp:HH:mm:ss}   {sample.CpuTemperatureC,7} {sample.GpuTemperatureC,7} " +
                    $"{sample.CpuFanRpm,8} {sample.GpuFanRpm,8}");
            }
            else if (++failures >= 3)
            {
                Error("Repeated read failures - is the vendor Control Center running?");
                return 2;
            }

            Thread.Sleep(1000);
        }

        return 0;
    }

    private static int Overlay(string[] args)
    {
        var fix = args.Contains("--fix", StringComparer.OrdinalIgnoreCase);
        var service = new PowerOverlayService();
        var diagnosis = service.Diagnose();

        Console.WriteLine($"Active power mode : {PowerOverlays.Describe(diagnosis.ActiveOverlay)}");
        Console.WriteLine($"Overlay guard     : " + (diagnosis.GuardMissing
            ? "MISSING"
            : $"active (minimum processor state {diagnosis.MinProcessorStateOverride}%)"));

        if (diagnosis.ProvisionedMinProcessorState is { } provisioned)
            Console.WriteLine($"OEM provisioned   : minimum processor state {provisioned}%");

        if (!diagnosis.NeedsRepair)
        {
            Console.WriteLine();
            Console.WriteLine("No problem found.");
            return 0;
        }

        Console.WriteLine();
        if (diagnosis.OverlayIsStuck)
        {
            Warn("The Best-performance overlay is active. It overrides your power plan and");
            Warn("pins the CPU at maximum frequency, even at idle.");
        }
        if (diagnosis.GuardMissing)
        {
            Warn("No guard is in place: if any application re-activates that overlay,");
            Warn("the CPU will be pinned again.");
        }

        if (!fix)
        {
            Console.WriteLine();
            Console.WriteLine("Run 'nextcalibur overlay --fix' to repair.");
            return 4;
        }

        Console.WriteLine();
        foreach (var action in service.Repair())
            Console.WriteLine($"  {action}");

        Console.WriteLine();
        Console.WriteLine("Repaired.");
        return 0;
    }

    private static void PrintSample(ThermalSample s)
    {
        Console.WriteLine($"CPU  {s.CpuTemperatureC,3} °C   fan {s.CpuFanRpm,5} rpm");
        Console.WriteLine($"GPU  {s.GpuTemperatureC,3} °C   fan {s.GpuFanRpm,5} rpm");
    }

    private static int? FindStockSoftware()
    {
        foreach (var name in new[] { "ControlCenter", "ControlCenterDaemon" })
        {
            var found = Process.GetProcessesByName(name);
            try
            {
                if (found.Length > 0) return found[0].Id;
            }
            finally
            {
                foreach (var p in found) p.Dispose();
            }
        }
        return null;
    }

    private static bool IsElevated()
    {
        using var identity = WindowsIdentity.GetCurrent();
        return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
    }

    private static void Warn(string message) => Write(message, ConsoleColor.Yellow);
    private static void Error(string message) => Write($"error: {message}", ConsoleColor.Red);

    private static void Write(string message, ConsoleColor colour)
    {
        var previous = Console.ForegroundColor;
        Console.ForegroundColor = colour;
        Console.WriteLine(message);
        Console.ForegroundColor = previous;
    }
}
