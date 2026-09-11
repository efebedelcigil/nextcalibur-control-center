using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Power;

/// <summary>
/// Decides what to do with the system mode when the charger comes out and
/// goes back in. The vendor's behaviour, measured 11 September 2026: drop to
/// Office on battery, and put back whatever was selected when the charger
/// returns.
///
/// Two rules, both learned from watching it: remember the mode the <i>person</i>
/// chose, never the one we switched to; and if they changed mode by hand while
/// on battery, that was their decision - leave it alone when the charger
/// returns. Pure, so the rules can be tested without a battery.
/// </summary>
public sealed class BatteryModePolicy
{
    /// <summary>The mode this laptop runs on battery.</summary>
    public const SystemMode QuietMode = SystemMode.Office;

    /// <summary>The mode to put back when the charger returns, if any.</summary>
    public SystemMode? Remembered { get; private set; }

    /// <summary>
    /// The charger came out. Returns the mode to apply, or null when there is
    /// nothing to do - already quiet, or no mode of ours is active at all.
    /// </summary>
    public SystemMode? OnUnplugged(SystemMode? current)
    {
        if (current is null)
        {
            Remembered = null;
            return null;
        }

        // Windows raises the same notification for a battery-level change as
        // for the charger coming out, so this can arrive again while already
        // quiet. Keep what was remembered the first time.
        if (current == QuietMode) return null;

        Remembered = current;
        return QuietMode;
    }

    /// <summary>
    /// The charger came back. Returns the mode to restore, or null when there
    /// is nothing to restore - or when the mode is no longer the one we set,
    /// which means the person changed it and it is theirs now.
    /// </summary>
    public SystemMode? OnPluggedIn(SystemMode? current)
    {
        var remembered = Remembered;
        Remembered = null;

        if (remembered is null) return null;
        return current == QuietMode ? remembered : null;
    }

    /// <summary>
    /// The person picked a mode themselves. On battery that cancels any
    /// restore; on the charger there was nothing pending anyway.
    /// </summary>
    public void UserChose(SystemMode mode, bool onBattery)
    {
        if (onBattery) Remembered = null;
    }
}

/// <summary>Whether the machine is running on its battery right now.</summary>
public static class PowerSource
{
    [StructLayout(LayoutKind.Sequential)]
    private struct SystemPowerStatus
    {
        public byte ACLineStatus;
        public byte BatteryFlag;
        public byte BatteryLifePercent;
        public byte SystemStatusFlag;
        public int BatteryLifeTime;
        public int BatteryFullLifeTime;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GetSystemPowerStatus(out SystemPowerStatus status);

    /// <summary>True on battery; false on the charger or when Windows cannot say.</summary>
    public static bool OnBattery() =>
        GetSystemPowerStatus(out var status) && status.ACLineStatus == 0;
}
