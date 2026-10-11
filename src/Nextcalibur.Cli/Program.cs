using System.Diagnostics;
using System.Security.Principal;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;

namespace Nextcalibur.Cli;

internal static class Program
{
    private static int Main(string[] args)
    {
        // Degree signs and the like: the console's default code page mangles them.
        try { Console.OutputEncoding = System.Text.Encoding.UTF8; } catch (IOException) { }

        var command = args.Length > 0 ? args[0].ToLowerInvariant() : "help";

        try
        {
            // The same gate the window applies: nothing that writes to firmware
            // is offered to a machine that has not shown it speaks this protocol.
            var writes = (command == "led" && args.Length > 1) || (command == "gpu" && args.Length > 1) || command == "fanmax"
                         || command == "ecturbo";
            if (writes)
            {
                var verdict = HardwareSupport.Check();
                if (!verdict.AllowsWrites)
                {
                    Error("Not on this laptop. " + string.Join(" ", verdict.Reasons));
                    return 7;
                }
            }

            return command switch
            {
                "sensors" => Sensors(args),
                "watch" => Watch(args),
                "overlay" => Overlay(args),
                "info" => Info(),
                "led" => Led(args),
                "clocks" => Clocks(),
                "access" => Access(args),
                "gpu" => Gpu(args),
                "regs" => Registers(args),
                "fanmax" => FanMaxTest(),
                "events" => Events(args),
                "plans" => Plans(),
                "ecturbo" => EcTurbo(args),
                _ => Help(),
            };
        }
        catch (EcMailboxUnavailableException ex)
        {
            Error(ex.Message);
            return 2;
        }
        catch (OperationCanceledException ex)
        {
            Console.WriteLine(ex.Message);
            return 6;
        }
        catch (Exception ex)
        {
            Error(ex.Message);
            return 1;
        }
    }

    /// <summary>
    /// Watches the registers PROTOCOL.md's sweep covers - 0x0200-0x0207 and
    /// 0x0300-0x0302 - once a second, through the read command only, and
    /// prints every one that changes. Made to find which register a key
    /// combination moves (Fn+1, 11 October 2026), the way Fn+Space was found
    /// to move 0x0200 a6. Writes nothing to the controller; needs administrator.
    /// </summary>
    private static int Registers(string[] args)
    {
        var seconds = args.Length > 1 && int.TryParse(args[1], out var n) && n > 0 ? n : 60;
        ushort[] registers = [0x0200, 0x0201, 0x0202, 0x0203, 0x0204, 0x0205, 0x0206, 0x0207, 0x0300, 0x0301, 0x0302];

        using var mailbox = new EcMailbox();
        var last = new Dictionary<ushort, string>();
        var clock = Stopwatch.StartNew();
        Console.WriteLine($"Watching {registers.Length} registers for {seconds} s, reads only. Press the keys now.");

        while (clock.Elapsed.TotalSeconds < seconds)
        {
            foreach (var register in registers)
            {
                if (ReadRegister(mailbox, register) is not { } reply) continue;

                // The thermal block's temperatures and fan speeds move every
                // second; shown on their own line, the rest compared whole.
                var text = register == 0x0200
                    ? $"a6={reply.A6}  (cpu {reply.A2} gpu {reply.A3} fan {reply.A4}/{reply.A5})"
                    : $"a2={reply.A2} a3={reply.A3} a4={reply.A4} a5={reply.A5} a6={reply.A6}";
                var key = register == 0x0200 ? $"a6={reply.A6}" : text;

                if (register == 0x0200)
                    Console.WriteLine($"{DateTime.Now:HH:mm:ss}  fans {reply.A4}/{reply.A5} rpm  cpu {reply.A2} gpu {reply.A3}");
                if (!last.TryGetValue(register, out var before))
                    Console.WriteLine($"{DateTime.Now:HH:mm:ss}  {register:X4}  {text}");
                else if (before != key)
                    Console.WriteLine($"{DateTime.Now:HH:mm:ss}  {register:X4}  CHANGED  {text}");
                last[register] = key;
            }
            Thread.Sleep(1000);
        }
        return 0;
    }

    /// <summary>
    /// Finds which way round the firmware's fan-max request goes. The BIOS's
    /// own mailbox handler (DSDT, WSAA) answers FB00/0x0200 with a2=1 by
    /// setting one of two request bits in the controller: FAND when a6 bit 0
    /// is set, FANE when it is clear - the turbo Fn+1 toggles, whose state
    /// is the controller's FANM bit. Which of the two is "on" is not in the
    /// names, so this sends each in turn and prints the fans for twenty
    /// seconds after each, then sends the other once more so the machine is
    /// left as the second half found it... and finally FANE/FAND to settle
    /// on whichever read lower. Both requests are the firmware's own; nothing
    /// else is written.
    /// </summary>
    private static int FanMaxTest()
    {
        using var mailbox = new EcMailbox();
        void Fans(string label, int seconds)
        {
            for (var i = 0; i < seconds; i += 2)
            {
                if (ReadRegister(mailbox, 0x0200) is { } r)
                    Console.WriteLine($"{DateTime.Now:HH:mm:ss}  {label,-10} fans {r.A4}/{r.A5} rpm  cpu {r.A2}");
                Thread.Sleep(2000);
            }
        }
        void Request(uint a6)
        {
            using (mailbox.Hold())
            {
                mailbox.Write(new SmiCommand { A0 = (ushort)SmiFamily.Write, A1 = 0x0200, A2 = 1, A6 = a6 });
                Thread.Sleep(60);
            }
            Console.WriteLine($"{DateTime.Now:HH:mm:ss}  sent FB00/0200 a2=1 a6={a6} ({(a6 == 1 ? "FAND" : "FANE")})");
        }

        Fans("before", 10);
        Request(0); Fans("a6=0", 20);
        Request(1); Fans("a6=1", 20);
        Request(0); Fans("a6=0", 20);
        Request(1); Fans("a6=1", 20);
        Console.WriteLine("Done. If the fans are still at full speed, press Fn+1 once on the laptop's own keyboard.");
        return 0;
    }

    /// <summary>
    /// Prints the firmware's WMI events for a while - Fn keys, the fan turbo -
    /// every property of each. Reads nothing from the controller.
    /// </summary>
    private static int Events(string[] args)
    {
        var seconds = args.Length > 1 && int.TryParse(args[1], out var n) && n > 0 ? n : 60;
        using var watcher = new System.Management.ManagementEventWatcher(
            new System.Management.ManagementScope(@"root\wmi"),
            new System.Management.WqlEventQuery("SELECT * FROM GMC_WMIEvent"));
        watcher.EventArrived += (_, e) =>
        {
            var parts = new List<string>();
            foreach (var property in e.NewEvent.Properties)
            {
                var value = property.Value is byte[] bytes ? BitConverter.ToString(bytes) : property.Value?.ToString();
                parts.Add($"{property.Name}={value}");
            }
            Console.WriteLine($"{DateTime.Now:HH:mm:ss}  {string.Join("  ", parts)}");
        };
        watcher.Start();
        Console.WriteLine($"Listening for GMC_WMIEvent for {seconds} s. Press Fn keys now.");
        Thread.Sleep(TimeSpan.FromSeconds(seconds));
        watcher.Stop();
        return 0;
    }

    /// <summary>
    /// The diagnostic before any write: every two seconds, the controller
    /// byte the BIOS names as the fan turbo's (0xC3, bit 0) and the fans,
    /// while the person presses Fn+1. Reads only - see FanTurboBit for what
    /// it refuses to do.
    /// </summary>
    private static int EcTurbo(string[] args)
    {
        var seconds = args.Length > 1 && int.TryParse(args[1], out var n) && n is > 0 and <= 300 ? n : 60;
        using var bit = new FanTurboBit();
        using var mailbox = new EcMailbox();
        Console.WriteLine($"Reading EC 0xC3 bit 0 and the fans every 2 s for {seconds} s. Press Fn+1 on the laptop's keyboard now.");
        var clock = Stopwatch.StartNew();
        while (clock.Elapsed.TotalSeconds < seconds)
        {
            var reading = bit.Read();
            var fans = ReadRegister(mailbox, 0x0200) is { } r ? $"{r.A4}/{r.A5} rpm" : "?";
            Console.WriteLine(reading.Value is { } v
                ? $"{DateTime.Now:HH:mm:ss}  0xC3 = 0x{v:X2}  turbo bit = {(v & 1)}  fans {fans}"
                : $"{DateTime.Now:HH:mm:ss}  no reading ({reading.Refusal})  fans {fans}");
            if (reading.Refusal is EcReadRefusal.PawnIoMissingOrUntrusted or EcReadRefusal.ModuleNotAsShipped or EcReadRefusal.ModuleRefused)
                return 3;
            Thread.Sleep(2000);
        }
        return 0;
    }

    /// <summary>Every plan's processor floor and ceiling, and what the thermal repairs would change. Reads only.</summary>
    private static int Plans()
    {
        Console.WriteLine($"Active: {PlanTuning.ActivePlan():D}");
        foreach (var (name, plan) in SystemModeService.EnumeratePlans(includeBuiltIn: true))
        {
            var min = PlanTuning.Read(plan, PlanTuning.MinState);
            var max = PlanTuning.Read(plan, PlanTuning.MaxState);
            Console.WriteLine($"{name,-34} min {min?.Ac}/{min?.Dc}  max {max?.Ac}/{max?.Dc}");
        }
        foreach (var (name, _, floor) in PlanTuning.PinnedFloors())
            Console.WriteLine($"Pinned floor: {name} at {floor.Ac}/{floor.Dc} - the app sets it to {PlanTuning.SafeFloor}");
        Console.WriteLine($"Cooling marker present: {PlanTuning.Cooling}; floor repaired: {PlanTuning.FloorRepaired()}");
        return 0;
    }

    private static SmiCommand? ReadRegister(EcMailbox mailbox, ushort register)
    {
        using (mailbox.Hold())
        {
            var command = new SmiCommand { A0 = (ushort)SmiFamily.Read, A1 = register };
            mailbox.Write(command);
            for (var attempt = 0; attempt < 8; attempt++)
            {
                Thread.Sleep(40);
                var reply = SmiCommand.FromBytes(mailbox.ReadRaw());
                if ((reply.A0 == command.A0 && reply.A1 == command.A1) || (reply.A0 == 0 && reply.A1 == 0))
                    return reply;
            }
            return null;
        }
    }

    /// <summary>
    /// The graphics mode: what the machine is in now, and what the firmware
    /// has stored for the next restart. The two differ between a switch and the
    /// reboot that applies it.
    /// </summary>
    private static int Gpu(string[] args)
    {
        var now = new GpuModeService().Detect();
        Console.WriteLine($"Now                : {(now.Mode?.ToString() ?? "unknown")}" +
                          (now.DiscreteName is { } n ? $"  ({n} {(now.DiscreteDrivesDisplay ? "drives the panel" : "idle")})" : ""));

        using var mailbox = new EcMailbox();
        var stored = GpuModeService.ReadFirmwareMode(mailbox);
        Console.WriteLine($"Stored in firmware : {(stored.Mode?.ToString() ?? "unrecognised")}");
        Console.WriteLine($"  reply bytes      : {BitConverter.ToString(stored.Raw, 0, 16)}");

        if (stored.Mode is { } s && now.Mode is { } m && s != m && m != GpuMode.Uma)
            Warn("The stored mode differs from the current one: a restart will apply it.");

        if (args.Length < 2) return 0;

        if (!Enum.TryParse<GpuMode>(args[1], ignoreCase: true, out var target))
        {
            Error("Give hybrid, discrete or uma.");
            return 1;
        }

        Console.WriteLine();
        if (target != GpuMode.Uma && !(now.Mode == GpuMode.Uma && target == GpuMode.Hybrid))
        {
            Warn("Switching which chip drives the screen changes what the TPM measures at boot.");
            Warn("Your Windows PIN will need setting up again. If BitLocker is on, have the");
            Warn("recovery key to hand before restarting.");
            Console.WriteLine();
        }

        var outcome = new GpuModeService().Switch(mailbox, target);
        Console.WriteLine(outcome.Summary);
        if (outcome.RestartNeeded) Console.WriteLine("Restart to apply it.");
        return outcome.Changed || outcome.Summary.StartsWith("Already") ? 0 : 5;
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
                "This machine has the interface, but this account may not use it unelevated." +
                Environment.NewLine + "Run the command from an elevated prompt. ('nextcalibur access --grant' opens the" +
                Environment.NewLine + "interface to this account permanently; the application takes that back at its next start," +
                Environment.NewLine + "because it lets anything running as the account send firmware commands.)",
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
              nextcalibur gpu                    Which chip drives the panel, now and after restart
              nextcalibur gpu <hybrid|discrete|uma>  Switch (uma and back need admin; the others a restart)
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

        var verdict = HardwareSupport.Check();
        Console.WriteLine($"Support verdict             : {verdict.Level}");
        foreach (var reason in verdict.Reasons) Console.WriteLine($"  - {reason}");
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
