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
                "led" => Led(args),
                "clocks" => Clocks(),
                "access" => Access(args),
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

    /// <summary>
    /// Reports, grants or revokes this account's access to the firmware mailbox.
    ///
    /// Worth a command of its own because the failure it fixes does not look
    /// like a permissions problem from the outside: every read simply returns
    /// nothing, which for a long time was reported as the vendor software
    /// holding the mailbox - software that on such a machine is not installed.
    /// </summary>
    private static int Access(string[] args)
    {
        var grant = args.Contains("--grant");
        var revoke = args.Contains("--revoke");

        if (grant || revoke)
        {
            if (!MailboxAccess.IsElevated())
            {
                Error("This needs administrator. Run the same command from an elevated prompt.");
                return 3;
            }

            if (grant)
            {
                // Says so when there was nothing to do, rather than reporting a
                // change it did not make.
                Console.WriteLine(MailboxAccess.Grant()
                    ? "Granted."
                    : "Already allowed - nothing was changed.");
            }
            else
            {
                MailboxAccess.Revoke();
                Console.WriteLine("Reverted.");
            }
            Console.WriteLine();

            // An elevated process can reach the block whatever the descriptor
            // says, so the report below describes this prompt rather than the
            // account it was run for. Saying "readable" straight after revoking
            // access is true and useless.
            Console.WriteLine("Checking from an elevated prompt, which can read it either way.");
            Console.WriteLine("Run 'nextcalibur access' without administrator to see the real answer.");
            Console.WriteLine();
        }

        var state = MailboxAccess.Check();
        Console.WriteLine(state switch
        {
            MailboxAvailability.Available =>
                "The sensors are readable by this account.",
            MailboxAvailability.AccessNotGranted =>
                "This machine has the interface, but this account may not use it." +
                Environment.NewLine + "Run 'nextcalibur access --grant' from an elevated prompt, once.",
            _ =>
                "This machine does not expose the firmware interface Nextcalibur reads.",
        });

        return state == MailboxAvailability.Available ? 0 : 4;
    }

    private static int Help()
    {
        Console.WriteLine("""
            Nextcalibur Control Center - command line

              nextcalibur info              Show hardware support and environment
              nextcalibur sensors           Read temperatures and fan speeds once
              nextcalibur watch [seconds]   Stream sensor readings (default 30)
              nextcalibur clocks                 Show current CPU and GPU clock speeds
              nextcalibur access                 Report whether the sensors are readable
              nextcalibur access --grant         Allow this account to read them (admin)
              nextcalibur access --revoke        Put that back (admin)
              nextcalibur led                    Show the stored lighting state
              nextcalibur led off                Turn the lighting off
              nextcalibur led colour <zone> <hex>  e.g. led colour left FF0000
              nextcalibur led effect <name>      static blink breathing heartbeat
                                                 cycle wave off
              nextcalibur led brightness <0-100>  in steps of 10
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
        // Installed and running are different problems. Running means the two
        // are competing for the mailbox now; installed means they will be, the
        // next time somebody signs in.
        var installed = VendorSoftware.FindInstallation();
        Console.WriteLine($"Vendor Control Center       : {(stock is null ? "not running" : $"RUNNING (pid {stock})")}");
        Console.WriteLine($"  installed                 : {(installed is { } v ? v.Name : "no")}");

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

    private static int Clocks()
    {
        using var cpu = new CpuClockReader();
        using var gpu = new GpuClockReader();

        for (var i = 0; i < 5; i++)
        {
            var c = cpu.ReadGhz();
            var g = gpu.ReadGhz();
            Console.WriteLine(
                $"CPU {(c is null ? "  --  " : $"{c:N2} GHz")}    " +
                $"GPU {(g is null ? "  --  " : $"{g:N2} GHz")}");
            Thread.Sleep(900);
        }
        return 0;
    }

    private static int Led(string[] args)
    {
        using var mailbox = new EcMailbox();
        var led = new LedController(mailbox);
        var action = args.Length > 1 ? args[1].ToLowerInvariant() : "show";

        switch (action)
        {
            case "show":
                Console.WriteLine($"Effect     : {led.State.Effect}");
                Console.WriteLine($"Brightness : {led.State.BrightnessPercent}%");
                foreach (var zone in new[] { LedZone.Left, LedZone.Middle, LedZone.Right })
                {
                    var (r, g, b) = led.State.GetColour(zone);
                    Console.WriteLine($"{zone,-11}: #{r:X2}{g:X2}{b:X2}");
                }
                Console.WriteLine();
                Console.WriteLine("Firmware cannot report its own lighting, so this is what");
                Console.WriteLine("Nextcalibur last wrote - not a reading from the hardware.");
                return 0;

            case "off":
                led.TurnOff();
                Console.WriteLine("Lighting off.");
                return 0;

            case "colour" or "color":
                if (args.Length < 4 ||
                    !Enum.TryParse<LedZone>(args[2], ignoreCase: true, out var target) ||
                    !TryParseRgb(args[3], out var rgb))
                {
                    Error("usage: nextcalibur led colour <left|middle|right|allkeyboard> <RRGGBB>");
                    return 1;
                }
                led.SetColour(target, rgb.R, rgb.G, rgb.B);
                Console.WriteLine($"{target} set to #{rgb.R:X2}{rgb.G:X2}{rgb.B:X2}.");
                return 0;

            case "effect":
                if (args.Length < 3 || !TryParseEffect(args[2], out var effect))
                {
                    Error("usage: nextcalibur led effect <static|blink|breathing|heartbeat|cycle|wave|off>");
                    return 1;
                }
                led.SetEffect(effect);
                Console.WriteLine($"Effect set to {effect}. This applies to the whole keyboard.");
                return 0;

            case "brightness":
                if (args.Length < 3 || !int.TryParse(args[2], out var level) || level is < 0 or > 100)
                {
                    Error("usage: nextcalibur led brightness <0-100>");
                    return 1;
                }
                led.SetBrightness(level);
                Console.WriteLine($"Brightness set to {led.State.BrightnessPercent}%.");
                return 0;

            default:
                Error($"unknown led action '{action}'");
                return 1;
        }
    }

    private static bool TryParseEffect(string text, out LedEffect effect)
    {
        effect = text.ToLowerInvariant() switch
        {
            "static" => LedEffect.Static,
            "blink" => LedEffect.Blink,
            "breathing" => LedEffect.Breathing,
            "heartbeat" => LedEffect.Heartbeat,
            "cycle" => LedEffect.ColourCycle,
            "wave" => LedEffect.Wave,
            "off" => LedEffect.Off,
            _ => (LedEffect)255,
        };
        return effect != (LedEffect)255;
    }

    private static bool TryParseRgb(string text, out (byte R, byte G, byte B) rgb)
    {
        rgb = default;
        var hex = text.TrimStart('#');
        if (hex.Length != 6 ||
            !uint.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out var packed))
            return false;

        rgb = ((byte)((packed >> 16) & 0xFF), (byte)((packed >> 8) & 0xFF), (byte)(packed & 0xFF));
        return true;
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
        var outcome = service.Repair();
        foreach (var action in outcome.Done)
            Console.WriteLine($"  {action}");

        if (outcome.Blocked is { } blocked)
        {
            Console.WriteLine();
            Warn(blocked);
            // Exit non-zero: a script that runs this to make a machine safe
            // should be able to tell that it did not finish.
            return 5;
        }

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
