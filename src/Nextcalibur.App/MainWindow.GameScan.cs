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
        Localized.Text(result, () => last.Found == 0
            ? Strings.Get("S.Games.NoneFound")
            : Strings.Get("S.Games.Result", last.Found, last.Added) + (last.SwitchedOn ? " " + Strings.Get("S.Games.RulesOn") : ""));
        result.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// A square button showing one Segoe Fluent Icons glyph. The theme's button
    /// pads its text 12 px each side, which left a 28 px button 2 px for the
    /// glyph - a "|" and a ":" were all that showed - so this one pads nothing.
    /// </summary>
    internal static System.Windows.Controls.Button IconButton(string glyph) => new()
    {
        Content = glyph,
        FontFamily = new System.Windows.Media.FontFamily("Segoe Fluent Icons, Segoe MDL2 Assets"),
        FontSize = 13, Width = 28, Height = 28, Padding = new Thickness(0),
        VerticalAlignment = VerticalAlignment.Center,
    };

    /// <summary>The small "open file location" button beside a game or a program; null when there is no place to show.</summary>
    private System.Windows.Controls.Button? FolderButton(string? path)
    {
        if (string.IsNullOrEmpty(path) || !(System.IO.File.Exists(path) || System.IO.Directory.Exists(path))) return null;
        var button = IconButton("\uE838");   // open folder
        button.Margin = new Thickness(0, 0, 6, 0);
        Localized.Tip(button, () => Strings.Get("S.Common.OpenFolder") + "\n" + path);
        Localized.Bind(button, System.Windows.Automation.AutomationProperties.NameProperty, () => Strings.Get("S.Common.OpenFolder"));
        var target = path;
        button.Click += (_, _) =>
        {
            Unelevated.ShowInFolder(target);
            Log.Info("shell", "File location shown", ("path", target));
        };
        return button;
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
        // Due daily - or at once while found games still lack the folder their button opens.
        var foldersMissing = _settings.ProgramRules.Any(r => r.FromScan && (r.Folder is null || r.Source is null));
        if (!foldersMissing && _settings.GamesScannedUtc is { } last && DateTime.UtcNow - last < GameScanEvery) return;
        // Not in the first minutes after Nextcalibur starts: the machine has enough to do then.
        if (DateTime.UtcNow < _gameScanNotBefore) return;
        _ = ScanForGamesAsync(byHand: false);
    }

    /// <summary>
    /// The program rules on, for the games on the list: not when the person
    /// switched them off by hand, and only where modes may be changed. Said
    /// when it happens. True when they were switched on now.
    /// </summary>
    private bool SwitchRulesOnForGames(string why)
    {
        if (_settings.ProgramRulesEnabled || _settings.ProgramRulesOffByHand || !_support.AllowsWrites) return false;
        _settings.ProgramRulesEnabled = true;
        _settings.Save();
        Log.Info("games", "Program rules switched on for the games found", ("why", why));
        Audit("program-rules", "switched-on-for-games", ("why", why));
        _tray?.ShowMessage(Strings.Get("S.Games.RulesOnTitle"), Strings.Get("S.Games.RulesOnBody"));
        StartProgramRules();
        RefreshProgramRuleRows();
        return true;
    }

    /// <summary>
    /// Once at start: games already on the list - added by an earlier daily
    /// scan, which then left the rules off - get the rules switched on too.
    /// </summary>
    private void SwitchRulesOnForListedGamesAtStart()
    {
        if (_settings.ProgramRules.Any(r => r.FromScan)) SwitchRulesOnForGames("games already on the list");
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
            // Rules found before get what is known of them now: the folder, where
            // they came from, whether Windows' Installed apps lists them.
            foreach (var rule in _settings.ProgramRules.Where(r => r.FromScan))
                if (found.FirstOrDefault(g => g.Processes.Contains(rule.Process, comparer)
                                              || string.Equals(g.Name, rule.Label, StringComparison.CurrentCultureIgnoreCase)) is { } owner)
                {
                    rule.Folder ??= owner.Folder;
                    rule.Source = owner.Source;
                    rule.InInstalledApps = owner.InInstalledApps;
                }
            var added = new List<FoundGame>();
            foreach (var game in found)
            {
                var fresh = game.Processes
                    .Where(p => !_settings.ProgramRules.Any(r => comparer.Equals(r.Process, p)) && !_settings.GamesExcluded.Contains(p, comparer))
                    .ToList();
                if (fresh.Count == 0) continue;
                foreach (var process in fresh)
                    _settings.ProgramRules.Add(new ProgramRule
                    {
                        Process = process, Mode = SystemMode.Gaming, Label = game.Name, FromScan = true,
                        Folder = game.Folder, Source = game.Source, InInstalledApps = game.InInstalledApps,
                    });
                added.Add(game);
            }

            // Games found are games to switch for: the rules are switched on -
            // by the button or by the daily scan - unless the person turned them
            // off by hand (the owner's choice, 15 October 2026).
            var switchedOn = added.Count > 0 && SwitchRulesOnForGames(byHand ? "game scan by hand" : "daily game scan");
            _settings.Save();

            Log.Info("games", $"Game scan ({(byHand ? "by hand" : "daily")}): {found.Count} found, {added.Count} added" +
                (added.Count > 0 ? ": " + string.Join("; ", added.Select(g => $"{g.Name} [{g.Source}] {string.Join("/", g.Processes)}")) : ""));
            Audit("game-scan", byHand ? "by-hand" : "daily", ("found", found.Count), ("added", added.Count), ("rulesSwitchedOn", switchedOn));

            if (byHand || added.Count > 0)
            {
                _lastGameScan = (found.Count, added.Count, switchedOn);
                ShowGameScanResult();
            }
            if (added.Count > 0) StartProgramRules();
            RefreshProgramRuleRows();
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
