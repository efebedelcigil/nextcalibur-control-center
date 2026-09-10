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
/// Reports the machine's graphics configuration.
///
/// **Detection only â€” this class does not switch modes, and that is a decision
/// rather than a gap.**
///
/// The vendor software does switch the display path: watched on hardware, its
/// MS Hybrid button plus a restart moved the panel from the discrete card to the
/// integrated one. It reaches firmware through the kernel driver it installs â€”
/// `DeviceIoControl` in its native library, not in its managed code, which is
/// why an earlier search of the managed assembly found nothing and concluded,
/// wrongly, that no software could do this.
///
/// Nextcalibur does not follow, because doing so needs an undocumented IOCTL
/// into a driver this project neither ships nor installs, and administrator
/// rights it has committed never to request. It also carries a cost the vendor
/// never mentions: the change invalidates TPM-sealed credentials, so the Windows
/// PIN has to be set up again and BitLocker can demand its recovery key.
///
/// See PROTOCOL.md for the evidence.
/// </summary>
public sealed class GpuModeService
{
    /// <summary>Reads the current configuration.</summary>
    public GpuConfiguration Detect()
    {
        string? discreteName = null;
        var discretePresent = false;
        var discreteDrives = false;
        var integratedDrives = false;

        try
        {
            using var searcher = new ManagementObjectSearcher(
                "SELECT Name, AdapterCompatibility, CurrentHorizontalResolution, Status " +
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

        var mode = (discretePresent, discreteDrives, integratedDrives) switch
        {
            (false, _, true) => GpuMode.Uma,
            (true, true, _) => GpuMode.Discrete,
            (true, false, true) => GpuMode.Hybrid,
            _ => (GpuMode?)null,
        };

        return new GpuConfiguration(
            mode, discreteName, discretePresent, discreteDrives, integratedDrives);
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
            "\n\nCasper's own Control Center can change this, through the kernel driver it " +
            "installs. Nextcalibur will not: it would need that driver and administrator " +
            "rights, and this application asks for neither. Your BIOS setup may also offer " +
            "it, as a display or graphics mode setting." +
            "\n\nIf you change it anywhere: find your BitLocker recovery key first. " +
            "Switching which chip drives the screen changes what the TPM measures at " +
            "startup, and the next boot can ask for that key â€” without it the drive does " +
            "not open. Expect it to reset your Windows PIN as well.",

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
