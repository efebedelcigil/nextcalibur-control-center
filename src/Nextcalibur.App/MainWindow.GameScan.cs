using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Power;

namespace Nextcalibur.App;

/// <summary>
/// Settings > Program rules > Scan for games (the owner's ask of 14 October
/// 2026, after NVIDIA's app): the games installed through the launchers
/// become rules - Gaming while the game runs - beside the ones added by hand.
/// A game whose rule is removed is never added again. Switched on (the
/// default), the scan runs by itself once a day, never during a game, so a
/// game installed later is found too; it only reads the launchers' records.
/// </summary>
public partial class MainWindow
{
    private static readonly TimeSpan GameScanEvery = TimeSpan.FromDays(1);
    private bool _gameScanRunning;
    private readonly DateTime _gameScanNotBefore = DateTime.UtcNow.AddMinutes(5);

    /// <summary>The last scan's outcome, said again in the language chosen later.</summary>
    private (int Found, int Added, bool SwitchedOn)? _lastGameScan;

    private void ShowGameScanResult()
    {
        if (_lastGameScan is not { } last || FindName("GameScanResult") is not TextBlock result) return;
        result.Text = last.Found == 0
            ? Strings.Get("S.Games.NoneFound")
            : Strings.Get("S.Games.Result", last.Found, last.Added) + (last.SwitchedOn ? " " + Strings.Get("S.Games.RulesOn") : "");
        result.Visibility = Visibility.Visible;
    }

    private void WireGameScan()
    {
        if (FindName("GameScanButton") is System.Windows.Controls.Button scan)
            scan.Click += async (_, _) =>
            {
                scan.IsEnabled = false;
                try { await ScanForGamesAsync(byHand: true); }
                finally { scan.IsEnabled = true; }
            };
        if (FindName("SettingGameScanAuto") is ToggleButton auto)
        {
            auto.IsChecked = _settings.GameScanAuto;
            auto.Click += (_, _) =>
            {
                _settings.GameScanAuto = auto.IsChecked == true;
                _settings.Save();
                Log.Info("games", "Daily game scan " + (_settings.GameScanAuto ? "on" : "off"));
            };
        }
    }

    /// <summary>From the slow timer: the daily scan, when it is due and nothing is full screen.</summary>
    private void GameScanTick()
    {
        if (!_settings.GameScanAuto || _gameScanRunning || !_support.AllowsWrites || NoticeGate.ShouldWait) return;
        if (_settings.GamesScannedUtc is { } last && DateTime.UtcNow - last < GameScanEvery) return;
        // Not in the first minutes after Nextcalibur starts: the machine has enough to do then.
        if (DateTime.UtcNow < _gameScanNotBefore) return;
        _ = ScanForGamesAsync(byHand: false);
    }

    private async Task ScanForGamesAsync(bool byHand)
    {
        if (_gameScanRunning) return;
        _gameScanRunning = true;
        try
        {
            var found = await Task.Run(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
                try { return GameLibrary.Scan(); }
                finally { Thread.CurrentThread.Priority = ThreadPriority.Normal; }
            });
            _settings.GamesScannedUtc = DateTime.UtcNow;

            var comparer = StringComparer.OrdinalIgnoreCase;
            var added = new List<FoundGame>();
            foreach (var game in found)
            {
                var fresh = game.Processes
                    .Where(p => !_settings.ProgramRules.Any(r => comparer.Equals(r.Process, p)) && !_settings.GamesExcluded.Contains(p, comparer))
                    .ToList();
                if (fresh.Count == 0) continue;
                foreach (var process in fresh)
                    _settings.ProgramRules.Add(new ProgramRule { Process = process, Mode = SystemMode.Gaming, Label = game.Name, FromScan = true });
                added.Add(game);
            }

            // Pressed by hand, the rules are wanted: they are switched on.
            var switchedOn = false;
            if (byHand && added.Count > 0 && !_settings.ProgramRulesEnabled && _support.AllowsWrites)
            {
                _settings.ProgramRulesEnabled = true;
                switchedOn = true;
            }
            _settings.Save();

            Log.Info("games", $"Game scan ({(byHand ? "by hand" : "daily")}): {found.Count} found, {added.Count} added" +
                (added.Count > 0 ? ": " + string.Join("; ", added.Select(g => $"{g.Name} [{g.Source}] {string.Join("/", g.Processes)}")) : ""));
            Audit("game-scan", byHand ? "by-hand" : "daily", ("found", found.Count), ("added", added.Count), ("rulesSwitchedOn", switchedOn));

            if (byHand || added.Count > 0)
            {
                _lastGameScan = (found.Count, added.Count, switchedOn);
                ShowGameScanResult();
            }
            if (added.Count > 0)
            {
                StartProgramRules();
                RefreshProgramRuleRows();
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Warn("games", "Game scan: " + ex.Message);
        }
        finally
        {
            _gameScanRunning = false;
        }
    }
}
