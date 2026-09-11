using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// Whether a PnP device is awake, asked in a way that does not wake it.
///
/// The Display page wants the card's draw in every mode, and NVML - the only
/// source of that number - wakes the card to answer, which in Hybrid and UMA
/// is the application causing the very draw it then reports. The way out is
/// to ask Windows first. The PnP manager keeps each device's most recent
/// power state as a property (<c>DEVPKEY_Device_PowerData</c>), read from
/// its own records without touching the device. A card in D3 is asleep and
/// drawing nothing worth measuring; a card in D0 is awake already, and
/// asking NVML costs nothing extra.
/// </summary>
public static class DevicePowerState
{
    /// <summary>The device is fully on.</summary>
    public const int D0 = 1;

    /// <summary>The device is off (D3); the deepest state the PnP manager reports.</summary>
    public const int D3 = 4;

    [StructLayout(LayoutKind.Sequential)]
    private struct DevPropKey
    {
        public Guid Fmtid;
        public uint Pid;
    }

    // DEVPKEY_Device_PowerData = {A45C254E-DF1C-4EFD-8020-67D146A850E0}, 32
    private static readonly DevPropKey PowerData = new()
    {
        Fmtid = new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),
        Pid = 32,
    };

    [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
    private static extern int CM_Locate_DevNodeW(out uint devInst, string deviceId, uint flags);

    [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
    private static extern int CM_Get_DevNode_PropertyW(uint devInst, in DevPropKey key, out uint propertyType,
        byte[]? buffer, ref uint bufferSize, uint flags);

    /// <summary>
    /// The device's most recent power state as Windows recorded it: 1 for D0
    /// (awake) through 4 for D3 (off). Null when the device or the property
    /// cannot be read - in which case the caller should assume awake, since
    /// the cost of a wrong "asleep" is a missing number and of a wrong
    /// "awake" a woken card.
    /// </summary>
    public static int? MostRecent(string deviceInstanceId)
    {
        if (CM_Locate_DevNodeW(out var node, deviceInstanceId, 0) != 0) return null;

        uint size = 0;
        CM_Get_DevNode_PropertyW(node, in PowerData, out _, null, ref size, 0);
        if (size < 8) return null;

        var buffer = new byte[size];
        if (CM_Get_DevNode_PropertyW(node, in PowerData, out _, buffer, ref size, 0) != 0) return null;

        // CM_POWER_DATA: ULONG PD_Size; DEVICE_POWER_STATE PD_MostRecentPowerState; ...
        var state = BitConverter.ToInt32(buffer, 4);
        return state is >= D0 and <= D3 ? state : null;
    }

    /// <summary>True when the device is recorded as anything but fully on.</summary>
    public static bool IsAsleep(string deviceInstanceId) => MostRecent(deviceInstanceId) is { } s && s > D0;
}
