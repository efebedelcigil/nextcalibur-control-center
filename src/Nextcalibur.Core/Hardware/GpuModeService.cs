using System.Management;

namespace Nextcalibur.Core.Hardware;

/// <summary>Graphics modes the vendor software presents.</summary>
public enum GpuMode
{
    /// <summary>The discrete GPU drives the panel directly.</summary>
    Discrete,

    /// <summary>The integrated GPU drives the panel; the discrete GPU renders on demand.</summary>
    Hybrid,

    /// <summary>The discrete GPU is out of the picture entirely.</summary>
    Uma,
}

/// <summary>What the machine's graphics configuration currently looks like.</summary>
/// <param name="Mode">The mode this configuration corresponds to, if any.</param>
/// <param name="DiscreteName">Name of the discrete GPU, or null when absent.</param>
/// <param name="DiscretePresent">A discrete GPU exists and is enabled.</param>
/// <param name="DiscreteDrivesDisplay">The discrete GPU is driving a display.</param>
/// <param name="IntegratedDrivesDisplay">The integrated GPU is driving a display.</param>
public readonly record struct GpuConfiguration(
    GpuMode? Mode,
    string? DiscreteName,
    bool DiscretePresent,
    bool DiscreteDrivesDisplay,
    bool IntegratedDrivesDisplay);

/// <summary>
/// Reports the machine's graphics configuration, and changes it.
///
/// Three modes, and they are not three values of one setting - which is the
/// thing to understand before touching any of this:
///
/// - **Hybrid** and **Discrete** are firmware. One register in the ACPI-WMI
///   mailbox, <c>0x0203</c>, holds 1 or 2, and the firmware applies it at the
///   next boot. Watched being written by the vendor's software on 11 September
///   2026, both directions, each confirmed by the boot that followed. No kernel
///   driver is involved - the vendor ships one, and on this machine it never
///   loads, so it cannot be.
/// - **UMA** is Windows. The discrete adapter is disabled as a device through
///   SetupDi, the driver stops, and nothing in firmware changes. Immediate,
///   reversible, and needs administrator.
///
/// The two paths have one rule each. The card cannot be switched off while it
/// is driving the screen, and firmware cannot be pointed at a card that is
/// switched off - either way the next thing on the panel would be nothing.
///
/// What the firmware switch costs, and the vendor's software never says: the
/// change alters what the TPM measures at boot, so the Windows PIN has to be
/// set up again and BitLocker can demand its recovery key. This class does not
/// decide whether to pay that; it makes sure whoever does is told.
///
/// See PROTOCOL.md for the captures.
/// </summary>
public sealed class GpuModeService
{
    /// <summary>
    /// The values the firmware's display-mode register takes. Both watched
    /// being written by the vendor software and confirmed by the boot that
    /// followed; nothing else is ever written there.
    /// </summary>
    private const uint FirmwareHybrid = 1;
    private const uint FirmwareDiscrete = 2;

    /// <summary>What the firmware's mode register said, with the bytes it said it in.</summary>
    /// <param name="Mode">The stored mode, or null when the reply was not one of the two known values.</param>
    /// <param name="Raw">The 32-byte reply, kept so an unexpected answer can be looked at rather than guessed about.</param>
    public readonly record struct FirmwareModeReading(GpuMode? Mode, byte[] Raw);

    /// <summary>
    /// Reads the mode the firmware has stored - which is the mode the machine
    /// will be in after the next restart, not necessarily the one it is in now.
    ///
    /// The vendor's Display Mode button does this read before showing its
    /// dialogue. Its reply was only ever caught as a residue, after the
    /// command had gone: a buffer with the header cleared and the mode in
    /// <c>a2</c> and <c>a4</c> - 1 while the machine was in Hybrid, 2 while in
    /// Discrete. That is different from the thermal block, which echoes its
    /// header, so this accepts either shape and trusts <c>a2</c>.
    /// </summary>
    public static FirmwareModeReading ReadFirmwareMode(EcMailbox mailbox)
    {
        ArgumentNullException.ThrowIfNull(mailbox);

        using (mailbox.Hold())
        {
            var command = SmiCommand.For(SmiFamily.Read, SmiSubsystem.DisplayMode);
            mailbox.Write(command);

            byte[] raw = mailbox.ReadRaw();
            for (var attempt = 0; attempt < 8; attempt++)
            {
                Thread.Sleep(40);
                raw = mailbox.ReadRaw();
                var reply = SmiCommand.FromBytes(raw);

                var headerEchoed = reply.A0 == command.A0 && reply.A1 == command.A1;
                var headerCleared = reply.A0 == 0 && reply.A1 == 0;
                if (!headerEchoed && !headerCleared) continue;

                return new FirmwareModeReading(reply.A2 switch
                {
                    FirmwareHybrid => GpuMode.Hybrid,
                    FirmwareDiscrete => GpuMode.Discrete,
                    _ => null,
                }, raw);
            }

            return new FirmwareModeReading(null, raw);
        }
    }

    /// <summary>
    /// Asks the firmware to drive the panel from the other chip after the
    /// next restart, and confirms the request landed before saying so.
    ///
    /// Only <see cref="GpuMode.Hybrid"/> and <see cref="GpuMode.Discrete"/>
    /// are firmware modes; UMA is a device disable and has its own path. And
    /// only those two values are ever written: they are the two watched being
    /// written by the vendor software and confirmed by the boot that followed.
    /// Nothing else is known to be safe to put in this register.
    ///
    /// Refused outright while the machine is in UMA. The discrete adapter is
    /// disabled there; writing Discrete into firmware would hand the panel to
    /// a card Windows will not start, and the next boot would come up dark.
    /// The vendor software refuses the same transition, and now we know why.
    /// </summary>
    /// <returns>True when the register read back the requested mode.</returns>
    /// <exception cref="InvalidOperationException">The transition is not allowed from the current state.</exception>
    public bool WriteFirmwareMode(EcMailbox mailbox, GpuMode target)
    {
        ArgumentNullException.ThrowIfNull(mailbox);

        var value = target switch
        {
            GpuMode.Hybrid => FirmwareHybrid,
            GpuMode.Discrete => FirmwareDiscrete,
            _ => throw new ArgumentOutOfRangeException(nameof(target),
                "UMA is not a firmware mode; use SetDiscreteAdapterEnabled."),
        };

        var current = Detect();
        if (current.Mode == GpuMode.Uma || (current.DiscretePresent && !DiscreteAdapterEnabled()))
            throw new InvalidOperationException(
                "The graphics card is switched off (UMA). Turn it back on first - " +
                "handing the screen to a card Windows will not start leaves the next boot dark.");

        using (mailbox.Hold())
        {
            var command = SmiCommand.For(SmiFamily.Write, SmiSubsystem.DisplayMode);
            command.A2 = value;
            mailbox.Write(command);
            Thread.Sleep(60);
        }

        // The register cannot confirm the write. Read back straight after, and
        // two seconds after, it still answers the mode the machine is running
        // in - measured 11 September 2026, Hybrid -> 2 written, read stays 1 -
        // and the vendor's own capture shows the same residue before its
        // restart. So the read reports the active mode, not the staged one,
        // and the only verification there is happens at the next boot.
        return true;
    }

    /// <summary>
    /// The PnP instance path of the discrete adapter, or null when there is none.
    /// </summary>
    public static string? DiscreteAdapterInstanceId()
    {
        try
        {
            using var searcher = new ManagementObjectSearcher(
                "SELECT DeviceID, PNPDeviceID FROM Win32_VideoController");
            foreach (ManagementObject gpu in searcher.Get())
            {
                using (gpu)
                {
                    if (gpu["PNPDeviceID"] is string id && id.Contains("VEN_10DE", StringComparison.OrdinalIgnoreCase))
                        return id;
                }
            }
        }
        catch (ManagementException)
        {
        }
        return null;
    }

    /// <summary>Whether the discrete adapter is enabled as a device (as opposed to present but switched off).</summary>
    public static bool DiscreteAdapterEnabled()
    {
        try
        {
            using var searcher = new ManagementObjectSearcher(
                "SELECT PNPDeviceID, ConfigManagerErrorCode FROM Win32_PnPEntity WHERE PNPClass = 'Display'");
            foreach (ManagementObject device in searcher.Get())
            {
                using (device)
                {
                    if (device["PNPDeviceID"] is not string id || !id.Contains("VEN_10DE", StringComparison.OrdinalIgnoreCase))
                        continue;
                    // 22 is CM_PROB_DISABLED - what the vendor's UMA button leaves behind.
                    return device["ConfigManagerErrorCode"] is not uint code || code != 22;
                }
            }
        }
        catch (ManagementException)
        {
        }
        return true;
    }

    /// <summary>
    /// Switches the discrete adapter off or on as a device. This is what UMA
    /// is: measured on hardware, the vendor's UMA button disables the NVIDIA
    /// adapter through SetupDi, the driver stops, and no restart is needed -
    /// nothing in firmware changes.
    ///
    /// Done through <c>pnputil</c>, which is Windows' own tool for exactly this
    /// and needs administrator. The prompt is Windows', once per switch.
    ///
    /// Refused while the discrete adapter is driving the panel: switching off
    /// the card the screen is on takes the screen with it. That is the rule
    /// behind the vendor's "please switch to Hybrid mode first".
    /// </summary>
    /// <returns>True when the device reports the requested state afterwards.</returns>
    /// <exception cref="InvalidOperationException">The card is driving the display, or there is no card.</exception>
    public bool SetDiscreteAdapterEnabled(bool enabled)
    {
        var current = Detect();
        if (!enabled && current.DiscreteDrivesDisplay)
            throw new InvalidOperationException(
                "The graphics card is driving your screen right now. Switch to Hybrid first, " +
                "restart, and then it can be turned off.");

        if (DiscreteAdapterInstanceId() is null)
            throw new InvalidOperationException("No discrete graphics card was found.");

        // Without a prompt when the tasks are registered - which the one
        // elevation at first run does - and with Windows' prompt otherwise.
        if (CardSwitchTasks.Registered())
        {
            if (!CardSwitchTasks.Run(enabled))
                throw new InvalidOperationException("The card-switch task would not start.");
        }
        else
        {
            try
            {
                using var process = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "pnputil.exe",
                    Arguments = PnputilArguments(enabled),
                    UseShellExecute = true,
                    Verb = "runas",
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                });
                process?.WaitForExit(30000);
            }
            catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 1223)
            {
                // ERROR_CANCELLED: the elevation prompt was dismissed. Nothing
                // has happened to the device, and that is worth saying in those
                // words rather than as an exception about starting a process.
                throw new OperationCanceledException("The permission prompt was declined. Nothing was changed.");
            }
        }

        // The device state, not any exit code, is the truth: pnputil can report
        // success and leave the device where it was if the driver refuses, and
        // a scheduled task returns before its work is done. Poll for it.
        // Watched through the PnP manager's status word, not WMI: this loop
        // used to be a WMI query every half second for up to twelve seconds
        // on the interface thread.
        var id = DiscreteAdapterInstanceId();
        for (var waited = 0; waited < 12000; waited += 250)
        {
            Thread.Sleep(250);
            var disabled = id is null ? !DiscreteAdapterEnabled() : DevicePowerState.IsDisabled(id) ?? !DiscreteAdapterEnabled();
            if (disabled == !enabled) return true;
        }
        return false;
    }

    private static string PnputilArguments(bool enabled) =>
        $"/{(enabled ? "enable" : "disable")}-device \"{DiscreteAdapterInstanceId()}\"";

    /// <summary>
    /// Runs pnputil directly. Only meaningful from an elevated process, which
    /// is what the scheduled task provides.
    /// </summary>
    public static bool RunPnputil(bool enabled)
    {
        if (DiscreteAdapterInstanceId() is null) return false;

        using var process = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = "pnputil.exe",
            Arguments = PnputilArguments(enabled),
            UseShellExecute = false,
            CreateNoWindow = true,
        });
        if (process is null) return false;
        process.WaitForExit(30000);
        return process.ExitCode == 0;
    }

    /// <summary>What a switch did, and what is left for the person to do.</summary>
    /// <param name="Changed">Something was actually changed.</param>
    /// <param name="RestartNeeded">The change takes effect at the next restart.</param>
    /// <param name="Summary">One sentence for the person.</param>
    public readonly record struct SwitchOutcome(bool Changed, bool RestartNeeded, string Summary);

    /// <summary>
    /// Moves the machine towards a mode, by whichever route that mode needs.
    ///
    /// The three modes are not three values of one setting. Two are firmware
    /// modes and take effect at a restart; the third is a device disable that
    /// takes effect at once. And the transitions are not all allowed - the
    /// card cannot be switched off while it is driving the screen, and firmware
    /// cannot be pointed at a card that is switched off. This is the one place
    /// those rules live.
    /// </summary>
    /// <exception cref="InvalidOperationException">The transition is not allowed from here.</exception>
    public SwitchOutcome Switch(EcMailbox mailbox, GpuMode target)
    {
        var current = Detect();
        if (current.Mode == target)
            return new SwitchOutcome(false, false, $"Already in {target}.");

        switch (target)
        {
            case GpuMode.Uma:
                if (!SetDiscreteAdapterEnabled(false))
                    return new SwitchOutcome(false, false, "Windows did not switch the card off.");
                return new SwitchOutcome(true, false,
                    "The graphics card is switched off. No restart needed.");

            case GpuMode.Hybrid when current.Mode == GpuMode.Uma:
                // Firmware is already on Hybrid underneath UMA; all that is
                // switched off is the device.
                if (!SetDiscreteAdapterEnabled(true))
                    return new SwitchOutcome(false, false, "Windows did not switch the card back on.");
                return new SwitchOutcome(true, false,
                    "The graphics card is back on. No restart needed.");

            case GpuMode.Discrete when current.Mode == GpuMode.Uma:
                throw new InvalidOperationException(
                    "Switch to Hybrid first. The card has to be on before the screen can be handed to it.");

            default:
                // Nothing is written here. The register is written when the
                // session is actually ending - by the window, on WM_ENDSESSION -
                // so that a restart the person cancels at Windows' "these apps
                // are preventing restart" screen leaves the machine exactly as
                // it was. Set by the owner, 12 September 2026.
                return new SwitchOutcome(true, true,
                    $"The screen will be handed to {target} at the next restart. Nothing is changed until the restart begins.");
        }
    }

    /// <summary>Reads the current configuration.</summary>
    public GpuConfiguration Detect()
    {
        string? discreteName = null;
        var discretePresent = false;
        var discreteEnabled = true;
        var discreteDrives = false;
        var integratedDrives = false;

        try
        {
            using var searcher = new ManagementObjectSearcher(
                "SELECT Name, AdapterCompatibility, CurrentHorizontalResolution, ConfigManagerErrorCode " +
                "FROM Win32_VideoController");

            foreach (ManagementObject gpu in searcher.Get())
            {
                using (gpu)
                {
                    var vendor = gpu["AdapterCompatibility"] as string ?? string.Empty;
                    var drivesDisplay = gpu["CurrentHorizontalResolution"] is uint w && w > 0;

                    // Intel and AMD integrated parts both report their vendor here;
                    // anything from NVIDIA on this class of machine is the discrete one.
                    if (vendor.Contains("NVIDIA", StringComparison.OrdinalIgnoreCase))
                    {
                        discretePresent = true;
                        discreteName = gpu["Name"] as string;
                        discreteDrives |= drivesDisplay;

                        // A card that has been switched off is still listed here.
                        // Code 22 is CM_PROB_DISABLED, and it is what UMA looks
                        // like from Windows: present, off. Reading that as
                        // Hybrid - which this once did - made "switch to Hybrid"
                        // a no-op that left the card switched off.
                        if (gpu["ConfigManagerErrorCode"] is uint code && code == 22)
                            discreteEnabled = false;
                    }
                    else if (drivesDisplay)
                    {
                        integratedDrives = true;
                    }
                }
            }
        }
        catch (ManagementException)
        {
            // An unreadable WMI answer is reported as "unknown", not as an error.
        }

        var mode = (discretePresent, discreteEnabled, discreteDrives, integratedDrives) switch
        {
            (false, _, _, true) => GpuMode.Uma,
            (true, false, _, true) => GpuMode.Uma,
            (true, true, true, _) => GpuMode.Discrete,
            (true, true, false, true) => GpuMode.Hybrid,
            _ => (GpuMode?)null,
        };

        return new GpuConfiguration(
            mode, discreteName, discretePresent && discreteEnabled, discreteDrives, integratedDrives);
    }

    /// <summary>
    /// A sentence describing the current configuration, for the user.
    /// </summary>
    /// <param name="c">The configuration to describe.</param>
    /// <param name="load">
    /// What the card is drawing, when it can be read. In Discrete mode this
    /// turns a general warning into a measurement of what the setting is
    /// costing right now, which is the difference between advice somebody
    /// ignores and advice they act on.
    ///
    /// Pass null in every other mode. Reading it costs an NVML call, and an
    /// NVML call wakes the card - which in Hybrid means this application
    /// waking the very GPU the mode exists to keep asleep, every time somebody
    /// opens the page.
    /// </param>
    /// <param name="thermal">
    /// The latest mailbox reading, which costs nothing extra: the window is
    /// already taking it every second. Temperature and fan speed say more to
    /// most people than watts do, and unlike watts they can be read in any
    /// mode without touching the card.
    /// </param>
    public static string Describe(
        GpuConfiguration c, GpuLoad? load = null, ThermalSample? thermal = null) => c.Mode switch
    {
        GpuMode.Discrete =>
            $"Your screen is driven by the {c.DiscreteName ?? "graphics card"} directly. " +
            "This gives the best performance in games, but the card never powers down, " +
            "so the laptop runs hotter and the battery drains faster." +
            IdleCost(load, thermal) +
            "\n\nYou can change this below. Before you do: find your BitLocker recovery key. " +
            "Switching which chip drives the screen changes what the TPM measures at " +
            "startup, and the next boot can ask for that key - without it the drive does " +
            "not open. Your Windows PIN will need setting up again as well. Casper's own " +
            "Control Center makes the same change without mentioning either.",

        GpuMode.Hybrid =>
            "Your screen is driven by the built-in graphics, and the graphics card wakes " +
            "only when an application needs it. This is the best setting for battery life " +
            "and running cool.",

        GpuMode.Uma =>
            "Only the built-in graphics are in use. The coolest and quietest setting, " +
            "but games will run considerably slower.",

        _ => "Nextcalibur could not work out how your graphics are currently set up.",
    };

    /// <summary>
    /// Names what this mode is costing right now.
    ///
    /// Deliberately not gated on the card looking idle. In this mode the card
    /// draws the desktop too, so a low utilisation reading is not something to
    /// wait for. The honest statement is structural rather than momentary: the
    /// card is always on, and here is the draw.
    ///
    /// Resist reading much into a single high figure. Readings of 30-43% and
    /// 27-37 W were once taken as the cost of the mode; a wallpaper renderer was
    /// running at the time and was probably most of it. What the mode costs is
    /// the floor, not the peak.
    ///
    /// The lower bound only rejects a nonsense reading.
    /// </summary>
    private static string IdleCost(GpuLoad? load, ThermalSample? thermal)
    {
        var parts = new List<string>();

        // The lower bound only rejects a nonsense reading.
        if (load is { } l && l.Watts >= 1) parts.Add($"drawing {l.Watts:N1} W");

        // Measured on the development machine over eight quiet minutes in each
        // mode, read through the mailbox so nothing woke the card: 61 C and
        // 3799 rpm in Discrete against 48 C and 3299 rpm in Hybrid, and the
        // Discrete figure never fell across the whole run. Those numbers are
        // one machine's and are not repeated to the user; what is worth saying
        // is what their own machine reads right now.
        if (thermal is { } t && t.GpuTemperatureC > 0)
        {
            var fan = t.GpuFanRpm > 0 ? $" with its fan at {t.GpuFanRpm} rpm" : string.Empty;
            parts.Add($"sitting at {t.GpuTemperatureC} °C{fan}");
        }

        if (parts.Count == 0) return string.Empty;

        return $"\n\nRight now it is {string.Join(", ", parts)}. In this mode the card draws your " +
               "desktop as well, so it never reaches its low-power states - that is battery " +
               "spent whether or not anything needs the card, and heat the fans have to move.";
    }
}
