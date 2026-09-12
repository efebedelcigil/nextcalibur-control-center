using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Hardware;

public enum NvidiaDriverState
{
    /// <summary>The card is there and its driver with it.</summary>
    Present,
    /// <summary>The card is there; the driver is not - removed, or never installed.</summary>
    Missing,
    /// <summary>No NVIDIA device on the PCI bus at all.</summary>
    NoCard,
}

/// <summary>
/// The drivers the application cannot do without, checked at start and
/// once a minute while running - because a driver can be removed while
/// the application is up, or between two starts, and the pages that use
/// it must say so rather than show a blank or a guess. The NVIDIA driver
/// is the one: <c>nvml.dll</c> ships with it and goes with it, and the
/// card itself is found on the PCI bus whether or not it has a driver.
/// PawnIO is watched by the dependency system, which offers to put it
/// back.
/// </summary>
public static class EssentialDrivers
{
    public static NvidiaDriverState Nvidia()
    {
        var card = NvidiaCardOnTheBus();
        var driver = File.Exists(Path.Combine(Environment.SystemDirectory, "nvml.dll"));
        if (!card) return NvidiaDriverState.NoCard;
        return driver ? NvidiaDriverState.Present : NvidiaDriverState.Missing;
    }

    /// <summary>
    /// Whether any PCI device is NVIDIA's (vendor 10DE), driver or no driver.
    /// WMI's video-controller class only lists devices with a display driver
    /// bound, which is exactly what a card without one is not.
    /// </summary>
    private static bool NvidiaCardOnTheBus()
    {
        try
        {
            if (CM_Get_Device_ID_List_SizeW(out var length, "PCI", CmGetIdListFilterEnumerator) != 0 || length == 0) return false;
            var buffer = new char[length];
            if (CM_Get_Device_ID_ListW("PCI", buffer, length, CmGetIdListFilterEnumerator) != 0) return false;
            foreach (var id in new string(buffer).Split('\0', StringSplitOptions.RemoveEmptyEntries))
                if (id.Contains("VEN_10DE", StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            return false;
        }
    }

    private const uint CmGetIdListFilterEnumerator = 0x00000001;

    [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
    private static extern int CM_Get_Device_ID_List_SizeW(out uint length, string filter, uint flags);

    [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
    private static extern int CM_Get_Device_ID_ListW(string filter, char[] buffer, uint length, uint flags);
}
