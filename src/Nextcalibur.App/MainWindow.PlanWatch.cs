using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Power;

namespace Nextcalibur.App;

/// <summary>
/// Watching the plans Nextcalibur's work rests on (the owner's rules of 14
/// October 2026):
///
/// - <b>Changed from outside.</b> What Nextcalibur wrote into a mode's plan -
///   the mode profile, the ceilings - is remembered; when Windows' own
///   settings, the Control Panel or powercfg change it, the person is told,
///   once per change, with a button that puts Nextcalibur's values back. Not
///   pressed, the change stands. A ceiling raised above its upper bound is
///   said as such. The display and sleep timeouts are not watched here: those
///   are the person's (see MainWindow.Idle).
/// - <b>Another plan.</b> When the active plan is none of the modes', the
///   things that act on the modes' plans by themselves stand down -
///   Emergency Power Saver's ceiling, the mode on battery, program rules -
///   since on a plan that is not theirs they would leave the machine in a
///   state nobody chose. The person is told once; choosing a mode brings
///   everything back.
/// </summary>
public partial class MainWindow
{
    private readonly Dictionary<(Guid Plan, Guid Setting), PlanTuning.AcDc> _profileExpected = [];
    private string? _profileChangeTold;
    private bool _foreignPlan;
    private Guid _foreignPlanTold;
    private DateTime _plansWatchedAt = DateTime.MinValue;

    /// <summary>True while the active plan is none of the modes': the automatic parts stand down.</summary>
    private bool OnForeignPlan => _foreignPlan;

    /// <summary>What the modes' plans should hold now, after Nextcalibur wrote them.</summary>
    partial void RememberProfileWrites()
    {
        _profileExpected.Clear();
        if (!_support.AllowsWrites || _settings.FirstChangesRestorePoint is null) return;
        var baseline = ModeProfile.ReadBaseline();
        foreach (var mode in SystemModes.InOrder)
        {
            if (SystemModeService.PlanFor(mode) is not { } plan) continue;
            foreach (var (setting, value) in ModeProfile.For(mode, baseline, _settings.LimitsFor(mode)))
                _profileExpected[(plan.Plan, setting)] = value;
        }
        _profileChangeTold = null;
    }

    /// <summary>From the slow timer, once a minute: the plans compared, and the active one looked at.</summary>
    private void PlanWatchTick()
    {
        if (!_support.AllowsWrites || DateTime.UtcNow - _plansWatchedAt < TimeSpan.FromMinutes(1)) return;
        // Not during a game: the comparison reads every mode's plan, and what
        // it would say waits anyway. Looked at again once the screen is free.
        if (NoticeGate.ShouldWait) return;
        _plansWatchedAt = DateTime.UtcNow;
        try
        {
            WatchActivePlan();
            WatchProfileValues();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Warn("plans", ex.Message);
        }
    }

    private void WatchActivePlan()
    {
        var active = PlanTuning.ActivePlan();
        var modePlans = SystemModes.InOrder.Select(SystemModeService.PlanFor).OfType<(Guid Plan, bool Ours)>().Select(p => p.Plan).ToHashSet();
        var foreign = active != Guid.Empty && modePlans.Count > 0 && !modePlans.Contains(active);
        if (foreign == _foreignPlan) return;
        _foreignPlan = foreign;

        if (!foreign)
        {
            Log.Info("plans", "Back on a mode's plan; the automatic parts are on again");
            return;
        }

        Log.Info("plans", $"Another plan is active ({active:D}); the automatic parts stand down");
        Audit("plan-foreign", "noticed", ("plan", active.ToString("D")), ("described", SystemModeService.DescribeCurrent()));
        if (_cooling.Engaged) StopCooling("another plan was chosen");
        if (_foreignPlanTold != active)
        {
            _foreignPlanTold = active;
            _tray?.ShowMessage(Strings.Get("S.Plans.ForeignTitle"), Strings.Get("S.Plans.ForeignBody", SystemModeService.DescribeCurrent()));
        }
        LoadSystemMode();
    }

    /// <summary>True while a ceiling test writes its steps into a plan: those are ours, not a change from outside.</summary>
    internal bool CeilingTestRunning { get; set; }

    private void WatchProfileValues()
    {
        if (_profileExpected.Count == 0 || CeilingTestRunning) return;
        var cooling = PlanTuning.Cooling;
        var changed = new List<(SystemMode Mode, Guid Setting, PlanTuning.AcDc Was, PlanTuning.AcDc Now)>();
        foreach (var mode in SystemModes.InOrder)
        {
            if (SystemModeService.PlanFor(mode) is not { } plan) continue;
            foreach (var ((p, setting), expected) in _profileExpected)
            {
                if (p != plan.Plan) continue;
                // Emergency Power Saver lowers the ceiling itself, for a while.
                if (cooling && (setting == PlanTuning.MaxState || setting == PlanTuning.MaxState1)) continue;
                if (PlanTuning.Read(p, setting) is { } now && now != expected) changed.Add((mode, setting, expected, now));
            }
        }

        if (changed.Count == 0)
        {
            _profileChangeTold = null;
            return;
        }

        var signature = string.Join(";", changed.Select(c => $"{c.Mode}:{c.Setting}:{c.Now.Ac}/{c.Now.Dc}"));
        if (signature == _profileChangeTold) return;
        _profileChangeTold = signature;

        var aboveBound = changed.Any(c => c.Mode == SystemMode.Office
            && (c.Setting == PlanTuning.MaxState || c.Setting == PlanTuning.MaxState1)
            && Math.Max(c.Now.Ac, c.Now.Dc) > ModeProfile.UpperPercent(SystemMode.Office));
        var modes = string.Join(", ", changed.Select(c => ModeName(c.Mode)).Distinct());
        Log.Info("plans", "Changed outside Nextcalibur: " + signature);
        Audit("plan-changed-outside", "noticed", ("changes", signature), ("aboveUpperBound", aboveBound));

        var body = Strings.Get(aboveBound ? "S.Plans.ChangedAboveBody" : "S.Plans.ChangedBody", modes);
        if (!Toasts.TryShow(Strings.Get("S.Plans.ChangedTitle"), body, Strings.Get("S.Plans.PutBack"),
                () => Dispatcher.BeginInvoke(PutProfileBack)))
            _tray?.ShowMessage(Strings.Get("S.Plans.ChangedTitle"), body);
    }

    /// <summary>The button on the notice: Nextcalibur's values back into the modes' plans.</summary>
    private void PutProfileBack()
    {
        var baseline = ModeProfile.ReadBaseline();
        var written = 0;
        foreach (var mode in SystemModes.InOrder)
            if (SystemModeService.PlanFor(mode) is { } plan)
                written += PlanTuning.ApplyProfile(mode, plan.Plan, baseline, _settings.LimitsFor(mode)).Count;
        Log.Info("plans", $"Nextcalibur's values put back: {written} setting(s)");
        Audit("plan-changed-outside", "put-back", ("settings", written));
        RememberProfileWrites();
    }
}
