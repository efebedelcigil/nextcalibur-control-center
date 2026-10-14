using System.Runtime.InteropServices;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;

namespace Nextcalibur.App;

/// <summary>
/// Thermal management, 11 October 2026: the plans' pinned floors repaired,
/// our Office plan given its ceiling, cooling started on the way up to the
/// warning line rather than at it, and programs that heat the machine for
/// nothing named. See <see cref="PlanTuning"/>, <see cref="CoolingPolicy"/>,
/// <see cref="FanTurbo"/> and <see cref="RunawayWatch"/>.
/// </summary>
public partial class MainWindow
{
    private readonly CoolingPolicy _cooling = new();
    private readonly RunawayWatch _runaways = new();
    private DateTime _runawaysLookedAt = DateTime.MinValue;

    /// <summary>What cooling has done this time: the ceiling lowered, the turbo suggested.</summary>
    private bool _coolingCapped, _coolingSuggested;

    /// <summary>The turbo as the firmware last said; false until it has said anything.</summary>
    private bool _fanTurboOn;

    /// <summary>
    /// Once, at start, after the plans exist. A run that ended mid-cooling -
    /// a crash, a power cut - left a lowered ceiling behind; it goes first.
    /// </summary>
    private void TunePlansAtStart()
    {
        try
        {
            if (PlanTuning.StopCooling()) Log.Info("cooling", "A ceiling lowered by an earlier run was put back");

            foreach (var name in PlanTuning.RepairPinnedFloors())
            {
                Log.Info("power", $"Repaired the pinned minimum processor state of {name}");
                Audit("plan-floor-repair", "done-automatically", ("plan", name), ("to", PlanTuning.SafeFloor));
            }

            // What Windows chose for this machine, read afresh each start.
            var baseline = ModeProfile.ReadBaseline();
            Log.Info("power", $"Mode profile from Windows' own plans: balanced EPP {baseline.Balanced[ModeProfile.EnergyPreference]}, " +
                              $"power saver EPP {baseline.PowerSaver[ModeProfile.EnergyPreference]}, hybrid {baseline.Hybrid}");
            foreach (var mode in SystemModes.InOrder)
            {
                if (SystemModeService.PlanFor(mode) is not { } plan) continue;
                if (PlanTuning.RepairNeverOffDisplay(plan.Plan))
                {
                    Log.Info("power", $"{mode} plan's display timeout was never; set to {PlanTuning.DisplayOffSeconds / 60} minutes");
                    Audit("plan-display-timeout", "done-automatically", ("mode", mode.ToString()), ("to", PlanTuning.DisplayOffSeconds));
                }
                var changed = PlanTuning.ApplyProfile(mode, plan.Plan, baseline, _settings.LimitsFor(mode));
                if (changed.Count == 0) continue;
                Log.Info("power", $"{mode} plan given the mode profile: {changed.Count} setting(s)");
                Audit("plan-profile", "done-automatically", ("mode", mode.ToString()),
                    ("settings", string.Join(",", changed.Select(g => g.ToString("D")))));
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Warn("power", "Plan tuning at start failed: " + ex.Message);
        }
    }

    /// <summary>Whether a reading is wanted for cooling even where the warning alone would not need one.</summary>
    private bool CoolingWantsReadings => _support.AllowsWrites && (_settings.AutoCooling || _settings.AutoFanTurbo) && _settings.CpuWarningTemperatureC > 0;

    /// <summary>One reading's worth of the cooling decision.</summary>
    private void CoolingTick(ThermalSample s)
    {
        if (!CoolingWantsReadings)
        {
            if (_cooling.Engaged) StopCooling("switched off in Settings");
            return;
        }

        var step = _cooling.Next(s.CpuTemperatureC, _settings.CpuWarningTemperatureC,
            s.GpuTemperatureC, _settings.GpuWarningTemperatureC, DateTime.UtcNow);

        if (step == CoolingStep.Start) StartCooling(s);
        else if (step == CoolingStep.Stop) StopCooling("cooled down");
    }

    private void StartCooling(ThermalSample s)
    {
        // On a plan that is none of the modes', its ceiling is not lowered.
        if (OnForeignPlan) return;
        try
        {
            _coolingCapped = _settings.AutoCooling && PlanTuning.StartCooling();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Warn("cooling", "Could not start cooling: " + ex.Message);
        }

        // The fans' turbo is the controller's own, switched only by Fn+1 on
        // the built-in keyboard: nothing in the BIOS sets it, and reaching
        // the controller directly races Windows' own driver (PROTOCOL.md,
        // measured 11-12 October 2026). So it is suggested, not set.
        _coolingSuggested = _settings.AutoFanTurbo && !_fanTurboOn;

        _cooling.Engaged = _coolingCapped || _coolingSuggested;
        if (!_cooling.Engaged) return;
        if (_coolingCapped)
            Audit("emergency-office", "started-automatically", ("ceiling", PlanTuning.CoolCeiling),
                ("cpu", s.CpuTemperatureC), ("gpu", s.GpuTemperatureC));

        var what = _coolingCapped && _coolingSuggested ? Strings.Get("S.Cooling.Both", PlanTuning.CoolCeiling)
                 : _coolingSuggested ? Strings.Get("S.Cooling.Fans")
                 : Strings.Get("S.Cooling.Cap", PlanTuning.CoolCeiling);
        var rate = _cooling.DegreesPerMinute;
        Log.Info("cooling", $"Started at CPU {s.CpuTemperatureC} °C / GPU {s.GpuTemperatureC} °C, {rate:+0.0;-0.0} °C/min; capped={_coolingCapped} turbo suggested={_coolingSuggested}");
        NotifyOrKeepForAfterGame(Strings.Get("S.Cooling.Title"),
            Strings.Get("S.Cooling.Body", s.CpuTemperatureC, s.GpuTemperatureC, rate.ToString("+0.0;-0.0"), what));
    }

    private void StopCooling(string why)
    {
        try
        {
            if (_coolingCapped) PlanTuning.StopCooling();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Warn("cooling", "Could not stop cooling: " + ex.Message);
        }
        Log.Info("cooling", $"Stopped ({why}); capped={_coolingCapped} turbo suggested={_coolingSuggested}");
        if (_coolingCapped) Audit("emergency-office", "stopped-automatically", ("why", why));

        // Cooled, with the turbo still on: it can go back the same way.
        if (why == "cooled down" && _coolingSuggested && _fanTurboOn)
            NotifyOrKeepForAfterGame(Strings.Get("S.Cooling.CooledTitle"), Strings.Get("S.Cooling.TurboStillOn"));
        _coolingCapped = _coolingSuggested = false;
        _cooling.Engaged = false;
    }

    /// <summary>
    /// A mode change while cooling: the lowered ceiling belongs to the plan
    /// being left, so it goes back there and is lowered again on the new one.
    /// </summary>
    private void CarryCoolingAcrossModeChange(Action applyMode)
    {
        var recap = _coolingCapped;
        if (recap) PlanTuning.StopCooling();
        applyMode();
        if (recap) _coolingCapped = PlanTuning.StartCooling();
    }

    /// <summary>The firmware said the turbo changed: Fn+1 on the built-in keyboard. Nothing listens with the setting off.</summary>
    private void OnFanTurboEvent(bool on)
    {
        if (!_settings.AutoFanTurbo) return;
        _fanTurboOn = on;
        Log.Info("cooling", $"Fan turbo is now {(on ? "on" : "off")} (Fn+1)");
    }

    /// <summary>
    /// The heat warning, with a way out when there is one: from Gaming or
    /// Performance, a button that switches to Office - the same as clicking
    /// it on the System page. In Office already, just the warning.
    /// </summary>
    private void ShowHeatWarning(string title, string body)
    {
        if (_currentMode is SystemMode.Office or null || !_support.AllowsReads
            || !Toasts.TryShow(title, body, Strings.Get("S.Heat.SwitchOffice"),
                () => Dispatcher.BeginInvoke(() => { Log.Info("mode", "Office chosen from the heat warning"); ModeOffice.IsChecked = true; })))
            _tray?.ShowMessage(title, body);
    }

    // ---------------------------------------------------------------- runaways

    /// <summary>
    /// Once a minute: programs keeping a core or the graphics card busy, flat,
    /// for a quarter of an hour, in the background. Named, never touched.
    /// </summary>
    private async void RunawayTick()
    {
        if (!_settings.WarnsAboutHeat || DateTime.UtcNow - _runawaysLookedAt < TimeSpan.FromMinutes(1)) return;
        _runawaysLookedAt = DateTime.UtcNow;

        try
        {
            var foreground = ForegroundProcess();
            var skip = new HashSet<string>(WindowsFaults.Known.Select(w => w.Process), StringComparer.OrdinalIgnoreCase);
            var found = new List<Runaway>();

            var snapshot = await Task.Run(() => AtBackgroundPriority(RunawayWatch.ReadProcesses));
            found.AddRange(_runaways.SampleCpu(snapshot, DateTime.UtcNow, foreground.Pid, skip));

            // The card only while it is awake anyway: asking NVML wakes it.
            if (_currentGpuMode != GpuMode.Uma && CardIsAwakeNow() && await Task.Run(() => _gpuClock.ReadLoad()) is { } load)
            {
                var fullScreen = UserPresence.IsForegroundFullScreenOrBorderless();
                found.AddRange(_runaways.SampleGpu(load.UtilisationPercent, fullScreen, foreground.Name, DateTime.UtcNow,
                    () => _gpuClock.ReadGraphicsRunningProcessNames()));
            }

            foreach (var r in found)
            {
                var minutes = (int)Math.Round(r.For.TotalMinutes);
                var body = r.OnGpu
                    ? Strings.Get("S.Runaway.Gpu", r.Name, minutes)
                    : Strings.Get("S.Runaway.Cpu", r.Name, r.Cores.ToString("0.0"), minutes);
                Log.Info("runaway", body);
                if (UserPresence.WouldRatherNotBeDisturbed()) continue;
                if (!Toasts.TryShow(Strings.Get("S.Runaway.Title"), body, Strings.Get("S.Runaway.TaskManager"), OpenTaskManager))
                    _tray?.ShowMessage(Strings.Get("S.Runaway.Title"), body);
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Warn("runaway", ex.Message);
        }
    }

    private static void OpenTaskManager()
    {
        try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(System.IO.Path.Combine(Environment.SystemDirectory, "taskmgr.exe")) { UseShellExecute = true }); }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException) { }
    }

    private static (int Pid, string? Name) ForegroundProcess()
    {
        var hwnd = GetForegroundWindow();
        if (hwnd == IntPtr.Zero || GetWindowThreadProcessId(hwnd, out var pid) == 0) return (0, null);
        try
        {
            using var p = System.Diagnostics.Process.GetProcessById((int)pid);
            return ((int)pid, p.ProcessName);
        }
        catch (ArgumentException) { return ((int)pid, null); }
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint processId);
}
