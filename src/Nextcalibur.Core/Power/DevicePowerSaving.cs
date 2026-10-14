namespace Nextcalibur.Core.Power;

/// <summary>USB selective suspend, and the wireless adapter's power saving.</summary>
public enum DeviceSaving
{
    Usb,
    Wifi,
}

/// <summary>
/// The two power savings that cost the person something they notice - a
/// mouse that wakes late, a connection that stalls - kept out of the mode
/// profile and put in their hands (the owner's rule, 14 October 2026). Never
/// changed by Nextcalibur on its own; Windows' value stands until a switch
/// on the Settings page is pressed, and a change made in Windows shows on
/// that switch. One switch is the same value in every mode's plan.
/// </summary>
public static class DevicePowerSaving
{
    public static readonly Guid UsbSubgroup = new("2a737441-1930-4402-8d77-b2bebba308a3");
    public static readonly Guid UsbSuspend = new("48e6b7a6-50f5-4782-a5d4-53bb8f07e226");
    public static readonly Guid WifiSubgroup = new("19cbb8fa-5279-450e-9fac-8a3d5fedd0c1");
    public static readonly Guid WifiSaving = new("12bbebe6-58d6-4636-95bb-3217ef867c1a");

    public static Guid SettingOf(DeviceSaving kind) => kind == DeviceSaving.Usb ? UsbSuspend : WifiSaving;

    /// <summary>Saving off: USB never suspended, the wireless adapter at maximum performance.</summary>
    public static PlanTuning.AcDc Off => new(0, 0);

    /// <summary>
    /// Saving on: what Windows' own Balanced plan has on this machine, when
    /// that saves something; otherwise Windows' documented Balanced values
    /// (USB suspended, wireless at medium saving on battery).
    /// </summary>
    public static PlanTuning.AcDc On(DeviceSaving kind, PlanTuning.AcDc? balanced) =>
        balanced is { } b && Saving(b) ? b : kind == DeviceSaving.Usb ? new(1, 1) : new(0, 2);

    /// <summary>Whether a plan's value saves anything, plugged in or on battery.</summary>
    public static bool Saving(PlanTuning.AcDc value) => value.Ac != 0 || value.Dc != 0;

    /// <summary>On in every plan, off in every plan, or null when the modes differ.</summary>
    public static bool? State(IEnumerable<PlanTuning.AcDc> values)
    {
        var all = values.ToList();
        if (all.Count == 0) return null;
        if (all.All(Saving)) return true;
        if (all.All(v => !Saving(v))) return false;
        return null;
    }

    public static PlanTuning.AcDc? Read(Guid plan, DeviceSaving kind) => PlanTuning.Read(plan, SettingOf(kind));

    public static bool Write(Guid plan, DeviceSaving kind, PlanTuning.AcDc value) => PlanTuning.Write(plan, SettingOf(kind), value);
}
