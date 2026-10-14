using System.Management;
using Microsoft.Win32;
using Nextcalibur.Core.Power;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// What this machine has been through before Nextcalibur makes its first
/// changes - read once, recorded beside the restore point made then (the
/// owner's rule, 14 October 2026): a fresh Windows, a machine that had
/// Nextcalibur and lost it, one used for a while without it, one with the
/// vendor's Control Center installed now or before. The changes are the same
/// in every case and each original is kept; this says which case it was, so
/// what was done can be read against what was there.
/// </summary>
/// <param name="WindowsInstalled">When Windows was installed, when Windows says.</param>
/// <param name="VendorInstalled">The vendor's Control Center is installed now.</param>
/// <param name="VendorPlans">The vendor's power plans are there - its software is installed now, or was.</param>
/// <param name="OwnPlans">Plans named for Nextcalibur are there - it was installed before.</param>
/// <param name="OwnMarkers">Originals Nextcalibur kept for its plan changes, left from an earlier install.</param>
/// <param name="OverlayFault">The power-mode fault the vendor's software leaves is present.</param>
public sealed record MachineHistory(
    DateTime? WindowsInstalled,
    bool VendorInstalled,
    bool VendorPlans,
    bool OwnPlans,
    int OwnMarkers,
    bool OverlayFault)
{
    /// <summary>Windows installed in the last two days: a fresh install, or a new machine.</summary>
    public bool FreshWindows(DateTime now) => WindowsInstalled is { } at && now - at < TimeSpan.FromDays(2);

    /// <summary>The cases, in words for the log and the audit.</summary>
    public IReadOnlyList<string> Describe(DateTime now)
    {
        var cases = new List<string>();
        if (FreshWindows(now)) cases.Add("fresh Windows");
        if (VendorInstalled) cases.Add("vendor Control Center installed");
        else if (VendorPlans) cases.Add("vendor Control Center was installed before");
        if (OwnPlans || OwnMarkers > 0) cases.Add("Nextcalibur was installed before");
        if (!FreshWindows(now) && !OwnPlans && OwnMarkers == 0) cases.Add("used without Nextcalibur until now");
        if (OverlayFault) cases.Add("power-mode fault present");
        return cases;
    }

    public static MachineHistory Read()
    {
        var plans = SystemModeService.EnumeratePlans();
        bool overlayFault;
        try { overlayFault = new PowerOverlayService().Diagnose().NeedsRepair; }
        catch (Exception ex) when (ex is not OutOfMemoryException) { overlayFault = false; }

        return new MachineHistory(
            ReadInstallDate(),
            VendorSoftware.IsInstalled(),
            plans.ContainsKey("Office") && plans.ContainsKey("Gaming"),
            plans.Keys.Any(n => n.StartsWith("Nextcalibur ", StringComparison.Ordinal)),
            CountMarkers(),
            overlayFault);
    }

    private static int CountMarkers()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(PlanTuning.MarkersKey);
            return key?.ValueCount ?? 0;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException)
        {
            return 0;
        }
    }

    private static DateTime? ReadInstallDate()
    {
        try
        {
            using var searcher = new ManagementObjectSearcher("SELECT InstallDate FROM Win32_OperatingSystem");
            using var results = searcher.Get();
            foreach (ManagementBaseObject o in results)
            {
                using (o)
                {
                    if (o["InstallDate"] is string s) return ManagementDateTimeConverter.ToDateTime(s);
                }
            }
        }
        catch (Exception ex) when (ex is ManagementException or UnauthorizedAccessException or System.Runtime.InteropServices.COMException or ArgumentOutOfRangeException)
        {
        }
        return null;
    }
}
