using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Nextcalibur.Core.Power;

/// <summary>
/// The processor settings inside power plans that decide how hot the machine
/// runs, read and written through the same API powercfg uses.
///
/// Three jobs, each undone by name:
/// - <b>The pinned floor.</b> A plan whose minimum processor state is 90% or
///   more holds the processor near full speed with nothing to do - the fault
///   the overlay guard exists for, in a plan instead of an overlay. The
///   vendor's Gaming plan ships at 99% (found 11 October 2026; measured then
///   not to bite, the processor's own management settling it at 1-1.8 GHz,
///   but one change of that management away from 4 GHz at idle). Set to 5%,
///   the original kept beside it so uninstall can put it back.
/// - <b>Our Office plan's ceiling.</b> Our plans are copies of Balanced; the
///   vendor's Office plan caps the processor at 70%, ours did not, so Office
///   on a machine without the vendor's plans was Balanced by another name.
/// - <b>The vendor's Office plan's performance cores.</b> Its 70% ceiling is
///   set for the first processor class only; on a hybrid processor the
///   performance cores kept 100% and boosted to full clock in Office (seen
///   at 3.8 GHz and 94 °C on 14 October 2026). Brought down to the same
///   ceiling, the original kept so uninstall can put it back.
/// - <b>Cooling.</b> While the processor stays over its warning line the
///   active plan's ceiling is lowered to the Office plan's 70%, and put back
///   when it has cooled. The value it replaced is kept under
///   <see cref="MarkersKey"/>, so a crash or a power cut in the middle is
///   undone at the next start.
/// </summary>
public static class PlanTuning
{
    public static readonly Guid SubProcessor = new("54533251-82be-4824-96c1-47b60b740d00");
    public static readonly Guid MinState = new("893dee8e-2bef-41e0-89c6-b55d0929964c");
    public static readonly Guid MaxState = new("bc5038f7-23e0-4960-96da-33abaf5935ec");

    /// <summary>
    /// The ceiling of the second processor class: on a hybrid processor the
    /// performance cores. <see cref="MaxState"/> alone holds only the
    /// efficient cores, and the performance cores went on boosting to full
    /// clock in Office (seen at 3.8 GHz and 94 °C on 14 October 2026).
    /// </summary>
    public static readonly Guid MaxState1 = new("bc5038f7-23e0-4960-96da-33abaf5935ed");

    /// <summary>Every ceiling a cap has to lower, each with the marker its original is kept under.</summary>
    private static readonly (Guid Setting, string Marker)[] Ceilings =
    [
        (MaxState, CoolingMarker),
        (MaxState1, CoolingMarker + "1"),
    ];

    /// <summary>A floor at or above this pins the processor.</summary>
    public const int PinnedFloor = 90;

    /// <summary>What a pinned floor is set to: Windows' own Balanced value.</summary>
    public const int SafeFloor = 5;

    /// <summary>The Office ceiling - the vendor's Office plan's own value - and the one cooling uses.</summary>
    public const int CoolCeiling = 70;

    private const string SchemesKey = @"SYSTEM\CurrentControlSet\Control\Power\User\PowerSchemes";
    private const string FloorMarker = "NextcaliburOriginalMinState";
    private const string CoolingMarker = "NextcaliburCoolingFrom";
    private const string OfficeCoresMarker = "NextcaliburOriginalMaxState1";

    /// <summary>The repairs uninstall may put back or keep: each setting with the marker of its original.</summary>
    private static readonly (Guid Setting, string Marker)[] Repairs =
    [
        (MinState, FloorMarker),
        (MaxState1, OfficeCoresMarker),
    ];

    /// <summary>A plan's value for one setting, plugged in and on battery.</summary>
    public readonly record struct AcDc(int Ac, int Dc);

    public static AcDc? Read(Guid plan, Guid setting)
    {
        var sub = SubProcessor;
        if (PowerReadACValueIndex(IntPtr.Zero, ref plan, ref sub, ref setting, out var ac) != 0) return null;
        if (PowerReadDCValueIndex(IntPtr.Zero, ref plan, ref sub, ref setting, out var dc) != 0) return null;
        return new AcDc((int)ac, (int)dc);
    }

    public static bool Write(Guid plan, Guid setting, AcDc value)
    {
        var sub = SubProcessor;
        if (PowerWriteACValueIndex(IntPtr.Zero, ref plan, ref sub, ref setting, (uint)value.Ac) != 0) return false;
        if (PowerWriteDCValueIndex(IntPtr.Zero, ref plan, ref sub, ref setting, (uint)value.Dc) != 0) return false;

        // A written value reaches the running system only when the plan is
        // made active again; the active plan is the only one that needs it.
        // Activating a plan can take Windows' power mode with it, and the
        // mode is half of a system mode here - so whatever was on is put back.
        if (ActivePlan() == plan)
        {
            var overlay = PowerOverlayService.GetActiveOverlay();
            PowerSetActiveScheme(IntPtr.Zero, ref plan);
            if (PowerOverlayService.GetActiveOverlay() != overlay) PowerOverlayService.SetActiveOverlay(overlay);
        }
        return true;
    }

    public static Guid ActivePlan()
    {
        using var key = Registry.LocalMachine.OpenSubKey(SchemesKey);
        return key?.GetValue("ActivePowerScheme") is string s && Guid.TryParse(s, out var g) ? g : Guid.Empty;
    }

    // ------------------------------------------------------- the pinned floor

    /// <summary>Plans whose floor pins the processor, and are not already repaired.</summary>
    public static IReadOnlyList<(string Name, Guid Plan, AcDc Floor)> PinnedFloors()
    {
        var found = new List<(string, Guid, AcDc)>();
        foreach (var (name, plan) in SystemModeService.EnumeratePlans(includeBuiltIn: true))
        {
            if (Read(plan, MinState) is { } floor && (floor.Ac >= PinnedFloor || floor.Dc >= PinnedFloor))
                found.Add((name, plan, floor));
        }
        return found;
    }

    /// <summary>Whether some plan's floor was repaired by us and not put back.</summary>
    public static bool FloorRepaired() => Repairs.Any(r => MarkedPlans(r.Marker).Count > 0);

    /// <summary>Sets every pinning floor to <see cref="SafeFloor"/>, keeping the original. Returns the plans changed.</summary>
    public static IReadOnlyList<string> RepairPinnedFloors()
    {
        var done = new List<string>();
        foreach (var (name, plan, floor) in PinnedFloors())
        {
            // The original first: a repair that cannot be undone is not made.
            if (!Mark(plan, FloorMarker, floor)) continue;
            if (Write(plan, MinState, new AcDc(Math.Min(floor.Ac, SafeFloor), Math.Min(floor.Dc, SafeFloor))))
            {
                done.Add(name);
                Configuration.Log.Info("power", $"Minimum processor state of {name} was {floor.Ac}%/{floor.Dc}%; set to {SafeFloor}%");
            }
            else
            {
                Unmark(plan, FloorMarker);
            }
        }
        return done;
    }

    /// <summary>Puts every repaired floor back as it was. For uninstall, when the repairs are not kept.</summary>
    public static void RestoreFloors()
    {
        foreach (var (setting, marker) in Repairs)
        foreach (var (plan, original) in MarkedPlans(marker))
        {
            if (Write(plan, setting, original)) Unmark(plan, marker);
        }
    }

    /// <summary>Forgets the originals and keeps the repair. For uninstall, when the repairs are kept.</summary>
    public static void ForgetFloors()
    {
        foreach (var (_, marker) in Repairs)
            foreach (var (plan, _) in MarkedPlans(marker)) Unmark(plan, marker);
    }

    // ------------------------------------------------------- Office's ceiling

    /// <summary>Gives a plan of ours the Office ceiling, when it still has Balanced's.</summary>
    public static bool CapOfficePlan(Guid plan)
    {
        var any = false;
        foreach (var (setting, _) in Ceilings)
        {
            if (Read(plan, setting) is not { } max || (max.Ac <= CoolCeiling && max.Dc <= CoolCeiling)) continue;
            any |= Write(plan, setting, new AcDc(Math.Min(max.Ac, CoolCeiling), Math.Min(max.Dc, CoolCeiling)));
        }
        return any;
    }

    /// <summary>
    /// Brings the vendor's Office plan's performance cores down to the
    /// ceiling its first class already has, keeping the original. Nothing
    /// when the plan is not capped at all, or both classes already agree.
    /// </summary>
    public static bool AlignOfficeCores(Guid plan)
    {
        if (Read(plan, MaxState) is not { } first || first.Ac > CoolCeiling || first.Dc > CoolCeiling) return false;
        if (Read(plan, MaxState1) is not { } second || (second.Ac <= first.Ac && second.Dc <= first.Dc)) return false;
        if (!Mark(plan, OfficeCoresMarker, second)) return false;
        if (Write(plan, MaxState1, new AcDc(Math.Min(second.Ac, first.Ac), Math.Min(second.Dc, first.Dc)))) return true;
        Unmark(plan, OfficeCoresMarker);
        return false;
    }

    // ---------------------------------------------------------------- cooling

    /// <summary>Whether cooling has lowered a plan's ceiling and not yet put it back.</summary>
    public static bool Cooling => Ceilings.Any(c => MarkedPlans(c.Marker).Count > 0);

    /// <summary>
    /// Lowers the active plan's ceiling to <see cref="CoolCeiling"/>. False
    /// when there is nothing to lower - Office is already there - or it
    /// could not be done.
    /// </summary>
    public static bool StartCooling()
    {
        if (Cooling) return true;
        var plan = ActivePlan();
        if (plan == Guid.Empty) return false;

        var any = false;
        foreach (var (setting, marker) in Ceilings)
        {
            if (Read(plan, setting) is not { } max || (max.Ac <= CoolCeiling && max.Dc <= CoolCeiling)) continue;
            if (!Mark(plan, marker, max)) continue;
            if (Write(plan, setting, new AcDc(Math.Min(max.Ac, CoolCeiling), Math.Min(max.Dc, CoolCeiling)))) any = true;
            else Unmark(plan, marker);
        }
        return any;
    }

    /// <summary>Puts back every ceiling cooling lowered. Safe to call at any time; at start it undoes a run that ended mid-cooling.</summary>
    public static bool StopCooling()
    {
        var any = false;
        foreach (var (setting, marker) in Ceilings)
        {
            foreach (var (plan, original) in MarkedPlans(marker))
            {
                if (Write(plan, setting, original)) { Unmark(plan, marker); any = true; }
            }
        }
        return any;
    }

    // ---------------------------------------------------------------- markers

    // Beside the plan was the first idea, but Windows gives administrators
    // only read access to the plans' keys (full control is SYSTEM's): every
    // mark failed, and with it every repair and every cooling cap, silently
    // (found 14 October 2026, a Gaming floor still at 99%). The marks live
    // under a key of our own, which the uninstall removes once they are
    // put back.

    /// <summary>Where the originals are kept: one value per plan and marker, "marker|plan".</summary>
    public const string MarkersKey = @"SOFTWARE\Nextcalibur\PlanMarkers";

    private static string MarkName(Guid plan, string name) => $"{name}|{plan:D}";

    private static bool Mark(Guid plan, string name, AcDc value)
    {
        try
        {
            using var key = Registry.LocalMachine.CreateSubKey(MarkersKey, writable: true);
            key.SetValue(MarkName(plan, name), $"{value.Ac},{value.Dc}", RegistryValueKind.String);
            return true;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException)
        {
            Configuration.Log.Warn("power", $"Could not keep the original of {plan:D} ({name}): {ex.Message}");
            return false;
        }
    }

    private static void Unmark(Guid plan, string name)
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(MarkersKey, writable: true);
            key?.DeleteValue(MarkName(plan, name), throwOnMissingValue: false);
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException) { }
    }

    private static List<(Guid Plan, AcDc Original)> MarkedPlans(string name)
    {
        var found = new List<(Guid, AcDc)>();
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(MarkersKey);
            if (key is null) return found;
            foreach (var value in key.GetValueNames())
            {
                var parts = value.Split('|');
                if (parts.Length != 2 || parts[0] != name || !Guid.TryParse(parts[1], out var plan)) continue;
                if (key.GetValue(value) is string text && ParseMarker(text) is { } original) found.Add((plan, original));
            }
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException) { }
        return found;
    }

    /// <summary>Removes the markers' key once nothing is left in it. For uninstall, after the plans are put back.</summary>
    public static void RemoveMarkersKeyIfEmpty()
    {
        try
        {
            using (var key = Registry.LocalMachine.OpenSubKey(MarkersKey))
            {
                if (key is null || key.ValueCount > 0) return;
            }
            Registry.LocalMachine.DeleteSubKeyTree(MarkersKey, throwOnMissingSubKey: false);
            using var parent = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Nextcalibur");
            if (parent is { SubKeyCount: 0, ValueCount: 0 })
            {
                parent.Dispose();
                Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\Nextcalibur", throwOnMissingSubKey: false);
            }
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException) { }
    }

    /// <summary>"99,99" back into a value; null for anything that is not two percentages.</summary>
    public static AcDc? ParseMarker(string text)
    {
        var parts = text.Split(',');
        return parts.Length == 2
               && int.TryParse(parts[0], out var ac) && ac is >= 0 and <= 100
               && int.TryParse(parts[1], out var dc) && dc is >= 0 and <= 100
            ? new AcDc(ac, dc)
            : null;
    }

    [DllImport("powrprof.dll")]
    private static extern uint PowerReadACValueIndex(IntPtr root, ref Guid scheme, ref Guid subgroup, ref Guid setting, out uint value);

    [DllImport("powrprof.dll")]
    private static extern uint PowerReadDCValueIndex(IntPtr root, ref Guid scheme, ref Guid subgroup, ref Guid setting, out uint value);

    [DllImport("powrprof.dll")]
    private static extern uint PowerWriteACValueIndex(IntPtr root, ref Guid scheme, ref Guid subgroup, ref Guid setting, uint value);

    [DllImport("powrprof.dll")]
    private static extern uint PowerWriteDCValueIndex(IntPtr root, ref Guid scheme, ref Guid subgroup, ref Guid setting, uint value);

    [DllImport("powrprof.dll")]
    private static extern uint PowerSetActiveScheme(IntPtr root, ref Guid scheme);
}
