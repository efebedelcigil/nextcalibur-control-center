using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Power;

namespace Nextcalibur.App;

/// <summary>
/// What the person set, kept across what Windows does while Nextcalibur is not
/// running (the owner's ask of 15 October 2026, after an overnight Windows
/// update put the sleep and screen times of three modes back to Windows'
/// own):
///
/// - <b>Processor ceilings</b> are Nextcalibur's own settings and written into
///   the modes' plans at every start; one found changed is put back, and now
///   also said.
/// - <b>Screen-off and sleep times</b> belong to Windows as much as to
///   Nextcalibur (a change in the Control Panel is the person's and shows
///   here). So they are not put back by themselves: a change seen while
///   Nextcalibur runs is taken as the person's; one found at a start - made
///   while it was not running - is said, with a button that puts the kept
///   times back. Told once per difference.
/// </summary>
public partial class MainWindow
{
    private static readonly Guid[] CeilingSettings = [PlanTuning.MaxState, PlanTuning.MaxState1, ModeProfile.FrequencyMax, ModeProfile.FrequencyMax1];

    /// <summary>Modes whose ceilings the start had to write again because something else had changed them.</summary>
    private readonly List<SystemMode> _ceilingsPutBackAtStart = [];

    /// <summary>From TunePlansAtStart: a mode whose ceiling settings had to be written again.</summary>
    private void NoteCeilingsPutBack(SystemMode mode, IReadOnlyList<Guid> changed)
    {
        if (!_settings.CeilingsKeptBefore) return;   // the very first writes are not a change to tell
        if (changed.Any(CeilingSettings.Contains)) _ceilingsPutBackAtStart.Add(mode);
    }

    /// <summary>The person's times for a plan, kept.</summary>
    private void KeepIdle(Guid plan, Timeouts t)
    {
        _settings.IdleKept[plan.ToString("D")] = [t.Display.Ac, t.Display.Dc, t.Sleep.Ac, t.Sleep.Dc];
        _settings.Save();
    }

    private static Timeouts FromKept(int[] v) => new(new PlanTuning.AcDc(v[0], v[1]), new PlanTuning.AcDc(v[2], v[3]));

    /// <summary>Once, at start, after the plans are tuned and the times read: what changed while Nextcalibur was not running.</summary>
    private void CheckKeptAtStart()
    {
        var firstTime = !_settings.CeilingsKeptBefore;
        _settings.CeilingsKeptBefore = true;

        var changedIdle = new List<(SystemMode Mode, Guid Plan, Timeouts Kept, Timeouts Now)>();
        foreach (var (mode, plan) in _idlePlans)
        {
            if (mode is not { } m || !_idleKnown.TryGetValue(plan, out var now)) continue;
            if (_settings.IdleKept.TryGetValue(plan.ToString("D"), out var kept))
            {
                if (FromKept(kept) != now) changedIdle.Add((m, plan, FromKept(kept), now));
            }
            else
            {
                // Nothing kept for it yet (the first start of this version): what is there now is the starting point.
                _settings.IdleKept[plan.ToString("D")] = [now.Display.Ac, now.Display.Dc, now.Sleep.Ac, now.Sleep.Dc];
            }
        }
        _settings.Save();
        if (firstTime && changedIdle.Count == 0 && _ceilingsPutBackAtStart.Count == 0) return;

        foreach (var c in changedIdle)
            Log.Info("kept", "Screen and sleep times changed while Nextcalibur was not running", ("mode", c.Mode),
                ("kept", $"{c.Kept.Display.Ac}/{c.Kept.Display.Dc} {c.Kept.Sleep.Ac}/{c.Kept.Sleep.Dc}"),
                ("now", $"{c.Now.Display.Ac}/{c.Now.Display.Dc} {c.Now.Sleep.Ac}/{c.Now.Sleep.Dc}"));
        if (_ceilingsPutBackAtStart.Count > 0)
            Log.Info("kept", "Processor ceilings changed while Nextcalibur was not running; written again", ("modes", string.Join(",", _ceilingsPutBackAtStart)));
        if (changedIdle.Count == 0 && _ceilingsPutBackAtStart.Count == 0) return;

        // Told once per difference: the same change found again at the next start is not news.
        var signature = string.Join(";", changedIdle.Select(c => $"{c.Plan:N}:{c.Now}")) + "|" + string.Join(",", _ceilingsPutBackAtStart);
        if (signature == _settings.IdleChangeTold) return;
        _settings.IdleChangeTold = signature;
        _settings.Save();
        Audit("kept-settings", "noticed", ("idleChanged", string.Join(",", changedIdle.Select(c => c.Mode))), ("ceilingsPutBack", string.Join(",", _ceilingsPutBackAtStart)));

        var parts = new List<string>();
        if (_ceilingsPutBackAtStart.Count > 0)
            parts.Add(Strings.Get("S.Kept.Ceilings", string.Join(", ", _ceilingsPutBackAtStart.Distinct().Select(ModeName))));
        if (changedIdle.Count > 0)
            parts.Add(Strings.Get("S.Kept.Idle", string.Join(", ", changedIdle.Select(c => ModeName(c.Mode)))));
        var title = Strings.Get("S.Kept.Title");
        var body = string.Join(" ", parts);

        if (changedIdle.Count == 0)
        {
            _tray?.ShowMessage(title, body);
            return;
        }
        var toPutBack = changedIdle.Select(c => (c.Plan, c.Kept)).ToList();
        if (!Toasts.TryShow(title, body, Strings.Get("S.Kept.PutBack"), () => Dispatcher.BeginInvoke(() => PutIdleBack(toPutBack))))
            Dispatcher.BeginInvoke(() =>
            {
                if (Dialogs.Ask(title, body + "\n\n" + Strings.Get("S.Kept.PutBackQuestion"), defaultNo: false)) PutIdleBack(toPutBack);
            });
    }

    /// <summary>The button: the kept times written back into the plans that changed.</summary>
    private void PutIdleBack(IReadOnlyList<(Guid Plan, Timeouts Kept)> plans)
    {
        var written = 0;
        foreach (var (plan, kept) in plans)
        {
            if (IdleTimeouts.Write(plan, IdleSetting.DisplayOff, kept.Display)) written++;
            if (IdleTimeouts.Write(plan, IdleSetting.Sleep, kept.Sleep)) written++;
        }
        Log.Info("kept", "Kept screen and sleep times put back", ("plans", plans.Count), ("settings", written));
        Audit("kept-settings", "idle-put-back", ("plans", plans.Count), ("settings", written));
        RememberIdleTimeouts();
    }
}
