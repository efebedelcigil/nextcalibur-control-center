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
/// - <b>The mode profile.</b> Each mode's plan - the vendor's or ours - is
///   given <see cref="ModeProfile"/>'s processor settings: the floor, both
///   processor classes' ceilings, boost, energy preference, scheduling and
///   cooling policy. Each original is kept so uninstall can put it back.
/// - <b>The display that never sleeps.</b> The vendor's Gaming plan turns
///   the display off "never" when plugged in (found 14 October 2026): an
///   external monitor switched off at its button stays lit as far as Windows
///   knows, and the graphics card keeps sending it frames. Set once to
///   <see cref="DisplayOffSeconds"/>; a "never" chosen later in Windows'
///   Settings is the owner's and is left alone.
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

    /// <summary>The display group, and its "turn off the display after" (VIDEOIDLE, seconds).</summary>
    public static readonly Guid SubVideo = new("7516b95f-f776-4464-8c53-06167f40cc99");
    public static readonly Guid VideoIdle = new("3c0bc021-c8a8-4e07-a973-6b14cbcb2b7e");

    /// <summary>What a "never" display timeout is set to: ten minutes.</summary>
    public const int DisplayOffSeconds = 600;

    /// <summary>The group a setting lives in: the processor's, but for the display and sleep timeouts.</summary>
    private static Guid SubgroupOf(Guid setting) =>
        setting == VideoIdle ? SubVideo : setting == IdleTimeouts.StandbyIdle ? IdleTimeouts.SubSleep : SubProcessor;
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
    private const string ProfileMarkerPrefix = "NextcaliburProfile:";
    private const string DisplayMarker = "NextcaliburOriginalVideoIdle";

    private static string ProfileMarker(Guid setting) => ProfileMarkerPrefix + setting.ToString("D");

    /// <summary>The changes uninstall may put back or keep: each setting with the marker of its original.</summary>
    private static IEnumerable<(Guid Setting, string Marker)> Repairs =>
        ModeProfile.Settings.Select(s => (s, ProfileMarker(s))).Prepend((MinState, FloorMarker)).Append((VideoIdle, DisplayMarker));

    /// <summary>A plan's value for one setting, plugged in and on battery.</summary>
    public readonly record struct AcDc(int Ac, int Dc);

    public static AcDc? Read(Guid plan, Guid setting)
    {
        var sub = SubgroupOf(setting);
        if (PowerReadACValueIndex(IntPtr.Zero, ref plan, ref sub, ref setting, out var ac) != 0) return null;
        if (PowerReadDCValueIndex(IntPtr.Zero, ref plan, ref sub, ref setting, out var dc) != 0) return null;
        return new AcDc((int)ac, (int)dc);
    }

    public static bool Write(Guid plan, Guid setting, AcDc value)
    {
        var sub = SubgroupOf(setting);
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
            // A plan of ours is removed before this runs: nothing to put back.
            if (Read(plan, setting) is null || Write(plan, setting, original)) Unmark(plan, marker);
        }
    }

    /// <summary>Forgets the originals and keeps the repair. For uninstall, when the repairs are kept.</summary>
    public static void ForgetFloors()
    {
        foreach (var (_, marker) in Repairs)
            foreach (var (plan, _) in MarkedPlans(marker)) Unmark(plan, marker);
    }

    // ------------------------------------------------------- the mode profile

    /// <summary>
    /// Gives a mode's plan the <see cref="ModeProfile"/> settings that differ,
    /// keeping each original the first time it is changed so uninstall can
    /// put it back. Returns the settings changed; the profile is asserted at
    /// every start, so a plan reset by Windows or the vendor gets it again.
    /// </summary>
    public static IReadOnlyList<Guid> ApplyProfile(SystemMode mode, Guid plan)
    {
        var changed = new List<Guid>();
        var kept = MarkedPlans(ProfileMarkerPrefix, prefix: true).Select(m => (m.Marker, m.Plan)).ToHashSet();
        foreach (var (setting, target) in ModeProfile.For(mode))
        {
            if (Read(plan, setting) is not { } current || current == target) continue;
            var marker = ProfileMarker(setting);

            // The original first: a change that cannot be undone is not made.
            if (!kept.Contains((marker, plan)) && !Mark(plan, marker, current)) continue;
            if (Write(plan, setting, target)) changed.Add(setting);
        }
        return changed;
    }

    /// <summary>
    /// Gives a plan whose display never turns off when plugged in a timeout
    /// of <see cref="DisplayOffSeconds"/>, once: with the original kept,
    /// a later "never" is somebody's choice. True when it was changed.
    /// </summary>
    public static bool RepairNeverOffDisplay(Guid plan)
    {
        if (MarkedPlans(DisplayMarker).Any(m => m.Plan == plan)) return false;
        if (Read(plan, VideoIdle) is not { Ac: 0 } current) return false;
        if (!Mark(plan, DisplayMarker, current)) return false;
        if (Write(plan, VideoIdle, current with { Ac = DisplayOffSeconds })) return true;
        Unmark(plan, DisplayMarker);
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

    private static List<(Guid Plan, AcDc Original)> MarkedPlans(string name) =>
        MarkedPlans(name, prefix: false).Select(m => (m.Plan, m.Original)).ToList();

    private static List<(string Marker, Guid Plan, AcDc Original)> MarkedPlans(string name, bool prefix)
    {
        var found = new List<(string, Guid, AcDc)>();
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(MarkersKey);
            if (key is null) return found;
            foreach (var value in key.GetValueNames())
            {
                var parts = value.Split('|');
                if (parts.Length != 2 || !Guid.TryParse(parts[1], out var plan)) continue;
                if (prefix ? !parts[0].StartsWith(name, StringComparison.Ordinal) : parts[0] != name) continue;
                if (key.GetValue(value) is string text && ParseMarker(text) is { } original) found.Add((parts[0], plan, original));
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

    /// <summary>"99,99" back into a value; null for anything that is not two values from 0 to a day's seconds.</summary>
    private const int MaxMarkerValue = 86_400;

    public static AcDc? ParseMarker(string text)
    {
        var parts = text.Split(',');
        return parts.Length == 2
               && int.TryParse(parts[0], out var ac) && ac is >= 0 and <= MaxMarkerValue
               && int.TryParse(parts[1], out var dc) && dc is >= 0 and <= MaxMarkerValue
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
