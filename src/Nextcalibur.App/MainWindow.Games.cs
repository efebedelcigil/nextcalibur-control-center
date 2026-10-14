using System.Diagnostics;
using System.Windows;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;

namespace Nextcalibur.App;

/// <summary>
/// A program holding the screen - a game, most likely - followed while it
/// runs and asked about once it closes (see <see cref="GameSessions"/>).
/// While it is in front nothing about it, and nothing about heat, comes as a
/// notification: what would have been said waits, and is said in the
/// window once the program has closed.
/// </summary>
public partial class MainWindow
{
    private sealed class GameSession(int pid, DateTime processStart, string name)
    {
        public int Pid { get; } = pid;
        public DateTime ProcessStart { get; } = processStart;
        public string Name { get; } = name;
        public DateTime FirstSeenUtc { get; } = DateTime.UtcNow;
        public Dictionary<SystemMode, int> Looks { get; } = [];
    }

    /// <summary>What is said once the program has closed and the screen is free.</summary>
    private sealed record GameEnd(string Name, bool Ask, IReadOnlyList<string> Notes, SystemMode? PlayedIn);

    private GameSession? _game;
    private GameEnd? _gameEndPending;
    private readonly List<string> _gameNotes = [];

    /// <summary>True while the program of a game session has the screen in front.</summary>
    private bool GameInFront => _game is { } g && UserPresence.ForegroundFullScreenProcessId() == (uint)g.Pid;

    /// <summary>A notification, or - with a game in front - a line for the summary after it.</summary>
    private void NotifyOrKeepForAfterGame(string title, string body)
    {
        if (GameInFront)
        {
            _gameNotes.Add(title + " - " + body);
            Log.Info("games", $"Kept for after the game: {title}");
            return;
        }
        _tray?.ShowMessage(title, body);
    }

    /// <summary>From the slow timer, every tick: follows the program in front, and ends its session when it has closed.</summary>
    private void GameTick()
    {
        try
        {
            if (UserPresence.ForegroundFullScreenProcessId() is { } pid && Identify((int)pid) is { } seen
                && !GameSessions.IsNotAGame(seen.Name))
            {
                if (_game is { } old && (old.Pid != seen.Pid || old.ProcessStart != seen.Start)) EndGameSession();
                _game ??= new GameSession(seen.Pid, seen.Start, seen.Name);
                if (_currentMode is { } mode) _game.Looks[mode] = _game.Looks.GetValueOrDefault(mode) + 1;
            }

            if (_game is { } g && !StillRunning(g)) EndGameSession();

            if (_game is null && _gameEndPending is { } end && UserPresence.ForegroundFullScreenProcessId() is null
                && !UserPresence.NobodyIsWatching())
            {
                _gameEndPending = null;

                // Outside the timer's tick: the questions wait for an answer,
                // and the heat watch must not wait with them.
                Dispatcher.BeginInvoke(async () =>
                {
                    ShowForAfterGame();
                    await Task.Delay(500);
                    SayAfterGame(end);
                });
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Warn("games", ex.Message);
        }
    }

    private static (int Pid, DateTime Start, string Name)? Identify(int pid)
    {
        try
        {
            using var p = Process.GetProcessById(pid);
            return (pid, p.StartTime, p.ProcessName);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or System.ComponentModel.Win32Exception)
        {
            return null;
        }
    }

    private static bool StillRunning(GameSession g) =>
        Identify(g.Pid) is { } now && now.Start == g.ProcessStart;

    private void EndGameSession()
    {
        if (_game is not { } g) return;
        _game = null;

        var held = DateTime.UtcNow - g.FirstSeenUtc;
        var mostly = GameSessions.MostlyIn(g.Looks);
        Log.Info("games", $"{g.Name} closed after {held.TotalMinutes:0} min in front, mostly in {mostly?.ToString() ?? "-"}");

        var notes = new List<string>(_gameNotes);
        _gameNotes.Clear();
        if (_deferredOverheat is { } heat)
        {
            _deferredOverheat = null;
            var chip = Strings.Get(heat.IsCpu ? "S.Heat.ChipCpu" : "S.Heat.ChipGpu");
            notes.Insert(0, Strings.Get("S.Heat.WhileAway", heat.MaxTemperatureC, chip) + " - " + HeatBody(heat.Culprits));
            _overheatNotified = true;
        }

        var ask = _support.AllowsWrites && _settings.AskModeForGames
                  && GameSessions.ShouldAsk(g.Name, held, mostly, _settings.ProgramRules, _settings.GamesAnswered);
        if (!ask && notes.Count == 0) return;

        // Said once the screen is free: the next tick, or after the next game.
        _gameEndPending = _gameEndPending is { } earlier
            ? new GameEnd(g.Name, ask, [.. earlier.Notes, .. notes], mostly)
            : new GameEnd(g.Name, ask, notes, mostly);
    }

    /// <summary>The window up, so the questions are its own dialogues rather than Windows' boxes.</summary>
    private void ShowForAfterGame()
    {
        if (!IsVisible || WindowState == WindowState.Minimized)
        {
            Show();
            WindowState = WindowState.Normal;
        }
        Activate();
    }

    private void SayAfterGame(GameEnd end)
    {
        if (_exiting || _sessionEnding) return;
        if (end.Notes.Count > 0)
            Dialogs.Tell(Strings.Get("S.Game.SummaryTitle", end.Name),
                string.Join(Environment.NewLine + Environment.NewLine, end.Notes));

        if (end.Ask) AskModeForGame(end.Name, end.PlayedIn ?? SystemMode.Office);
    }

    private void AskModeForGame(string program, SystemMode playedIn)
    {
        if (ProgramRules.Normalise(program) is not { } name) return;
        var yes = Dialogs.AskWithCheck(Strings.Get("S.Game.AskTitle", name, ModeName(playedIn)), Strings.Get("S.Game.AskBody", name, ModeName(playedIn)),
            Strings.Get("S.Game.AskPerformance"), false, out var performance, defaultNo: true);

        if (!yes)
        {
            _settings.GamesAnswered.Add(name);
            _settings.Save();
            Log.Info("games", $"{name}: stays in the mode it is in; not asked again");
            return;
        }

        var mode = performance ? SystemMode.Performance : SystemMode.Gaming;
        _settings.ProgramRules.Add(new ProgramRule { Process = name, Mode = mode });
        _settings.ProgramRulesEnabled = true;
        _settings.Save();
        Log.Info("games", $"{name}: rule added, {mode} while it runs");
        StartProgramRules();
        RefreshProgramRuleRows();
    }
}
