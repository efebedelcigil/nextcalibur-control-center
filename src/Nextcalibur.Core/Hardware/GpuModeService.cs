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
/// **Detection only — this class does not switch modes.**
///
/// The vendor software's Display Mode page does not touch a hardware MUX. It
/// finds the discrete GPU by PCI hardware ID and disables the device through the
/// SetupDi API. Which of its three buttons maps to which device state has not
/// been established yet, and switching on a guess would leave a machine with no
/// working display path. Until that is observed on real hardware, this reports
/// what is true and changes nothing.
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
    /// </param>
    public static string Describe(GpuConfiguration c, GpuLoad? load = null) => c.Mode switch
    {
        GpuMode.Discrete =>
            $"Your screen is driven by the {c.DiscreteName ?? "graphics card"} directly. " +
            "This gives the best performance in games, but the card never powers down, " +
            "so the laptop runs hotter and the battery drains faster." +
            IdleCost(load) +
            "\n\nCasper's own Control Center can change this, through the kernel driver it " +
            "installs. Nextcalibur will not: it would need that driver and administrator " +
            "rights, and this application asks for neither. Your BIOS setup may also offer " +
            "it, as a display or graphics mode setting." +
            "\n\nIf you change it anywhere: find your BitLocker recovery key first. " +
            "Switching which chip drives the screen changes what the TPM measures at " +
            "startup, and the next boot can ask for that key — without it the drive does " +
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
    /// also draws the desktop, so there is no idle moment to catch — measured on
    /// the development machine, simply moving a window put it at 30% use. The
    /// honest statement is structural rather than momentary: the card is always
    /// on, and here is the draw.
    ///
    /// The lower bound only rejects a nonsense reading.
    /// </summary>
    private static string IdleCost(GpuLoad? load)
    {
        if (load is not { } l || l.Watts < 1) return string.Empty;

        return $"\n\nRight now it is drawing {l.Watts:N1} W. In this mode the card draws your " +
               "desktop as well, so it never reaches its low-power states — that is battery " +
               "spent whether or not anything needs the card, and heat the fans have to move.";
    }
}
