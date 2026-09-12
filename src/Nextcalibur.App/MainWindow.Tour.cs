using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Nextcalibur.Core.Configuration;
using Button = System.Windows.Controls.Button;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Nextcalibur.App;

/// <summary>
/// The guided tour: every page, every control, one at a time, ending at the
/// tray. The order and the words live here; the look lives in the markup
/// (`TourOverlay` and its parts, BRIEF §20). The parts are looked up by
/// name when the tour starts, so this compiles and does nothing until the
/// markup is there, and a renamed part is a skipped step rather than a
/// crash.
/// </summary>
public partial class MainWindow
{
    private enum TourPage { Any, System, Power, Display, Lighting }

    /// <param name="Target">x:Name of the control to spotlight; null for a step with no spotlight.</param>
    private sealed record TourStep(string? Target, TourPage Page, string Title, string Body);

    private const double TourGap = 12;
    private const double TourCardWidth = 300;

    private int _tourIndex = -1;
    private int _tourDirection = 1;
    private bool _tourActive;

    private Grid? _tourOverlay;
    private Canvas? _tourCanvas;
    private System.Windows.Shapes.Path? _tourShade;
    private FrameworkElement? _tourCard;
    private TextBlock? _tourTitle, _tourBody, _tourCounter;
    private Button? _tourBack, _tourNext, _tourSkip;

    private static readonly IReadOnlyList<TourStep> TourSteps = new[]
    {
        // ---- the window itself
        new TourStep("TourButton", TourPage.Any, "Welcome to Nextcalibur",
            "This is the tour. It walks through every page and every control, one at a time, and finishes at the tray icon. " +
            "Nothing is changed while it runs - it only points. Use Next and Back, press Escape or Skip to leave at any time; " +
            "this button starts it again whenever you want."),
        new TourStep("TitleBar", TourPage.Any, "The title bar",
            "Drag anywhere on it to move the window. The window has one fixed size, so there is nothing to resize."),
        new TourStep("ThemeSwitch", TourPage.Any, "Dark, light, or Windows' choice",
            "Three looks: dark, light, or follow whatever Windows is set to. The choice is remembered."),
        new TourStep("MinimiseButton", TourPage.Any, "Minimise",
            "Sends the window to the taskbar. Nextcalibur keeps running exactly as before; click the taskbar button to bring it back."),
        new TourStep("HideToTrayButton", TourPage.Any, "Hide to the tray",
            "Takes the window off the taskbar altogether. The application stays in the notification area by the clock, " +
            "still reading the sensors and keeping your mode; double-click the tray icon to open it again."),
        new TourStep("CloseButton", TourPage.Any, "Close",
            "Asks whether you mean to exit. Exiting stops Nextcalibur completely: no readings, no overheat warning, " +
            "and no mode change when the charger comes and goes - until it is started again. If you only want it out of the way, hide it to the tray instead."),

        // ---- the rail
        new TourStep("NavSystem", TourPage.System, "System",
            "The first page: the system mode, the fans, and the two chips. It is the page the application opens on."),
        new TourStep("NavPower", TourPage.Any, "Power Mode",
            "Windows' own power modes, shown as cards, and a check that Windows is honouring the mode you chose."),
        new TourStep("NavDisplay", TourPage.Any, "Display Mode",
            "Which graphics card drives the screen: the NVIDIA card alone, both together, or the processor's own."),
        new TourStep("NavLighting", TourPage.Any, "Lighting",
            "The keyboard backlight: colour per zone, effects, brightness, and saved profiles."),
        new TourStep("UpdateNowButton", TourPage.Any, "Updates",
            "Reads \"Up to date\" until a newer release is found - the check happens quietly in the background if you let it. " +
            "When there is one, the button names it; press it and the update is downloaded, verified and applied, and the application reopens as the new version."),
        new TourStep("OpenLogButton", TourPage.Any, "Open log",
            "Opens the folder with Nextcalibur's own log: one file a day, seven days kept, events only - starts, mode changes, " +
            "graphics switches, updates, anything that went wrong. Attach one when reporting a problem."),
        new TourStep("VersionText", TourPage.Any, "Version",
            "The version you are running. Mention it in any report."),

        // ---- System page
        new TourStep("ModeOffice", TourPage.System, "Office mode",
            "Quiet and cool: the power plan, Windows' power mode and the firmware's own profile are all set for light work. " +
            "Nextcalibur switches to Office on its own when the charger is unplugged, if you let it (a tray setting), and returns to your mode when it is plugged back in."),
        new TourStep("ModeGaming", TourPage.System, "Gaming mode",
            "The everyday fast mode. The firmware runs its Gaming profile, Windows gets its Balanced or Better-performance mode, and the processor is allowed to idle down between bursts."),
        new TourStep("ModePerformance", TourPage.System, "Performance mode",
            "Everything on: the firmware's Performance profile and Windows' fastest modes. Louder and hotter; for when the fans are worth it."),
        new TourStep("SystemModeNote", TourPage.System, "What the mode means",
            "A line under the cards explains what the chosen mode is doing right now. If Windows has been moved to a mode none of the cards match, it says so here instead of guessing."),
        new TourStep("SubtitleText", TourPage.System, "The status line",
            "The active mode, whether the laptop is on the charger or the battery, and the time of the last sensor reading. If a reading fails, the number shows as \"--\" rather than a made-up value."),
        new TourStep("Banner", TourPage.System, "The banner",
            "Appears only when something needs saying: the laptop is not supported, the firmware answered oddly, or the vendor's software is installed alongside. Otherwise it stays out of the way."),
        new TourStep("CpuFanGauge", TourPage.System, "The processor's fan",
            "Fan speed in revolutions per minute, read from the firmware. Nextcalibur does not set fan curves - the firmware profile chosen by the mode decides them, exactly as the original software did."),
        new TourStep("CpuName", TourPage.System, "The processor",
            "Read from the machine, never assumed."),
        new TourStep("GpuFanGauge", TourPage.System, "The graphics card's fan",
            "The second fan, same source. When the card is asleep or switched off, the fan is simply stopped."),
        new TourStep("GpuName", TourPage.System, "The graphics card",
            "Likewise read from the machine. In UMA mode the card is disabled and this still names it."),

        // ---- Power page
        new TourStep("PowerModeEfficiency", TourPage.Power, "Best power efficiency",
            "Windows' battery-saving mode. Only offered while the system mode is Office - the cards outside your mode's range are greyed so a fast mode cannot be paired with a slow profile, or the reverse."),
        new TourStep("PowerModeBalanced", TourPage.Power, "Balanced",
            "Windows' default. Available in Office and Gaming."),
        new TourStep("PowerModeBetter", TourPage.Power, "Better performance",
            "Available in Gaming and Performance. Choosing a system mode always picks that mode's own default here; these cards let you lean one step either way."),
        new TourStep("PowerModeBest", TourPage.Power, "Best performance",
            "Windows' fastest mode, for Performance only. Nextcalibur keeps a guard on it so that, when a laptop is idle, this mode can no longer hold the processor at full speed - the fault the original software left behind."),
        new TourStep("OverlayState", TourPage.Power, "Is Windows honouring the mode?",
            "A check of Windows' power overlay against your chosen mode. Green means all is well."),
        new TourStep("OverlayDetail", TourPage.Power, "What it found",
            "The explanation in plain words - what Windows is doing and why it matters."),
        new TourStep("FixButton", TourPage.Power, "Repair",
            "Shown only when the check finds a problem. Pressing it puts the power settings right; nothing else is touched."),

        // ---- Display page
        new TourStep("ModeDiscrete", TourPage.Display, "Discrete",
            "The NVIDIA card drives the screen alone: the most performance, the most power. Changing graphics mode needs a restart, and the switch is written to the firmware only as Windows is actually restarting - cancel the restart and nothing has changed."),
        new TourStep("ModeHybrid", TourPage.Display, "Hybrid",
            "Both: the processor's graphics drive the screen and the NVIDIA card wakes for games and heavy work, sleeping in between. The usual choice."),
        new TourStep("ModeUma", TourPage.Display, "UMA",
            "The processor's graphics only; the NVIDIA card is switched off entirely for the longest battery life. Unlike the other two this takes effect at once, no restart."),
        new TourStep("GpuRestartPending", TourPage.Display, "A change is waiting",
            "After choosing Discrete or Hybrid this line stays until the restart. Exiting Nextcalibur before restarting drops the pending change."),
        new TourStep("GpuModeDetail", TourPage.Display, "What the card is doing",
            "The card's clock and power draw when it is awake, and \"asleep\" when Windows has powered it down. Nextcalibur checks the sleep state from Windows' own records so that looking never wakes the card."),

        // ---- Lighting page
        new TourStep("LedPower", TourPage.Lighting, "Lighting on or off",
            "Turns the keyboard backlight off completely, or back on with everything as it was. Fn+Space on the keyboard still steps the brightness, and the page follows it."),
        new TourStep("TabZoneA", TourPage.Lighting, "Zones",
            "The keyboard lights in three zones, left to right. Pick a tab to colour that zone; the drawing above it shows what is set."),
        new TourStep("SelectAll", TourPage.Lighting, "All zones at once",
            "Tick it and the colour and effect you choose go to all three zones together."),
        new TourStep("ProfileRow", TourPage.Lighting, "Profiles",
            "Four saved sets of lighting - Office, Gaming, Performance and one of your own. Choosing a profile applies it; whatever you change afterwards is saved into it."),
        new TourStep("EffectPanel", TourPage.Lighting, "Effects",
            "Static, breathing, blink, heartbeat, colour cycle and wave. The last two make their own colours, so the wheel is greyed while they are chosen."),
        new TourStep("Wheel", TourPage.Lighting, "The colour wheel",
            "Click or drag to pick a colour for the selected zone. The change is sent to the keyboard as you release."),
        new TourStep("BrightnessSlider", TourPage.Lighting, "Brightness",
            "Dims the chosen colours. Fn+Space steps the hardware's own three levels; both are shown here as one percentage."),
        new TourStep("ReloadButton", TourPage.Lighting, "Reload",
            "Sends the saved lighting to the keyboard again - useful if something else reset it."),

        // ---- the readings panel (every page but Lighting)
        new TourStep("CpuClock", TourPage.System, "The processor, live",
            "Its clock in gigahertz and, on Intel machines with the PawnIO driver installed, its package power in watts. Read every few seconds; the interval is a tray setting."),
        new TourStep("CpuTemp", TourPage.System, "Processor temperature",
            "From the firmware's own sensor - the same one the fans answer to."),
        new TourStep("GpuClock", TourPage.System, "The graphics card, live",
            "Clock and watts while the card is awake; \"asleep\" while it is not. Nextcalibur never wakes the card to ask."),
        new TourStep("GpuTemp", TourPage.System, "Graphics card temperature",
            "From the firmware, so it is available even when the card's own driver is not."),
        new TourStep("OverheatWarningToggle", TourPage.System, "Overheat alert",
            "When on, a warning appears from the tray if either chip crosses its threshold. It is a warning only; Nextcalibur never throttles anything."),
        new TourStep("CpuWarnSlider", TourPage.System, "Processor threshold",
            "Drag the slider or type a whole number in the box. Out-of-range values are refused, not clamped."),
        new TourStep("GpuWarnSlider", TourPage.System, "Graphics card threshold",
            "The same for the card. The defaults are safe values for laptops of this class."),
        new TourStep("OverheatResetButton", TourPage.System, "Reset",
            "Puts both thresholds back to their defaults."),
        new TourStep("RamGauge", TourPage.System, "Memory",
            "How much of the installed memory is in use, read from Windows once a second."),
        new TourStep("DriveList", TourPage.System, "Drives",
            "Every fixed drive in the machine, each with its own gauge. Read once a minute - a drive's use does not change faster than that."),

        // ---- the tray
        new TourStep(null, TourPage.Any, "The tray icon",
            "Nextcalibur lives in the notification area by the clock; its menu has just been opened there. " +
            "Open Nextcalibur brings the window back. Start with Windows makes it start at logon, elevated, without a prompt. " +
            "Office mode on battery is the automatic switch when the charger comes out. Warn when the CPU or GPU runs hot is the overheat alert. " +
            "Read the sensors every sets how often the numbers refresh. Check for updates automatically and Check for updates now are what they say. " +
            "Open the log folder is the same as the Open log button. Exit stops the application - which is the only way it stops. " +
            "That is the whole of it. Enjoy the machine."),
    };

    // ------------------------------------------------------------- entry

    private void OnTourClick(object sender, RoutedEventArgs e) => StartTour();

    private void StartTour()
    {
        if (_tourActive) return;
        if (!ResolveTourParts()) return;

        _tourActive = true;
        _tourDirection = 1;
        _tourOverlay!.Visibility = Visibility.Visible;
        PreviewKeyDown += OnTourKey;
        SizeChanged += OnTourLayoutChanged;
        Log.Info("tour", "started");
        GoToTourStep(0);
    }

    private void EndTour()
    {
        if (!_tourActive) return;
        _tourActive = false;
        PreviewKeyDown -= OnTourKey;
        SizeChanged -= OnTourLayoutChanged;
        _tourOverlay!.Visibility = Visibility.Collapsed;
        Log.Info("tour", _tourIndex >= TourSteps.Count - 1 ? "finished" : $"left at step {_tourIndex + 1}");
        _tourIndex = -1;
        NavSystem.IsChecked = true;
    }

    /// <summary>
    /// Finds the overlay and its parts by name. False when the markup is
    /// not there (yet), in which case the button does nothing.
    /// </summary>
    private bool ResolveTourParts()
    {
        if (_tourOverlay is not null) return true;

        _tourOverlay = FindName("TourOverlay") as Grid;
        _tourCanvas = FindName("TourCanvas") as Canvas;
        _tourShade = FindName("TourShade") as System.Windows.Shapes.Path;
        _tourCard = FindName("TourCard") as FrameworkElement;
        _tourTitle = FindName("TourTitle") as TextBlock;
        _tourBody = FindName("TourBody") as TextBlock;
        _tourCounter = FindName("TourCounter") as TextBlock;
        _tourBack = FindName("TourBack") as Button;
        _tourNext = FindName("TourNext") as Button;
        _tourSkip = FindName("TourSkip") as Button;

        if (_tourOverlay is null || _tourCanvas is null || _tourShade is null || _tourCard is null ||
            _tourTitle is null || _tourBody is null || _tourCounter is null ||
            _tourBack is null || _tourNext is null || _tourSkip is null)
        {
            Log.Warn("tour", "the overlay is missing from the markup; the tour cannot run");
            _tourOverlay = null;
            return false;
        }

        _tourBack.Click += (_, _) => { _tourDirection = -1; GoToTourStep(_tourIndex - 1); };
        _tourNext.Click += (_, _) =>
        {
            if (_tourIndex >= TourSteps.Count - 1) EndTour();
            else { _tourDirection = 1; GoToTourStep(_tourIndex + 1); }
        };
        _tourSkip.Click += (_, _) => EndTour();
        return true;
    }

    private void OnTourKey(object sender, KeyEventArgs e)
    {
        // The dialogue overlay sits above the tour and handles its own keys.
        if (ModalDialogOverlay.Visibility == Visibility.Visible) return;

        switch (e.Key)
        {
            case Key.Escape: EndTour(); e.Handled = true; break;
            case Key.Right or Key.Enter or Key.Space: _tourNext!.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent)); e.Handled = true; break;
            case Key.Left: if (_tourIndex > 0) _tourBack!.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent)); e.Handled = true; break;
        }
    }

    private void OnTourLayoutChanged(object sender, SizeChangedEventArgs e)
    {
        if (_tourActive && _tourIndex >= 0) PlaceTourStep();
    }

    // ------------------------------------------------------------ stepping

    private void GoToTourStep(int index)
    {
        if (index < 0) index = 0;
        if (index >= TourSteps.Count) { EndTour(); return; }

        var step = TourSteps[index];
        ShowTourPage(step.Page);
        _tourIndex = index;

        // The page just switched needs a layout pass before the target has
        // a position; a collapsed target (a banner with nothing to say, a
        // repair button when nothing is broken) is skipped in the direction
        // of travel.
        Dispatcher.BeginInvoke(DispatcherPriority.Loaded, () =>
        {
            if (!_tourActive || _tourIndex != index) return;
            var target = TourTarget(step);
            if (step.Target is not null && (target is null || !target.IsVisible || target.ActualWidth <= 0))
            {
                var next = index + _tourDirection;
                if (next < 0 || next >= TourSteps.Count) { EndTour(); return; }
                GoToTourStep(next);
                return;
            }
            PlaceTourStep();
            if (step.Target is null) _tray?.ShowMenuForTour();
        });
    }

    private void ShowTourPage(TourPage page)
    {
        var nav = page switch
        {
            TourPage.System => NavSystem,
            TourPage.Power => NavPower,
            TourPage.Display => NavDisplay,
            TourPage.Lighting => NavLighting,
            _ => null,
        };
        if (nav is not null && nav.IsChecked != true) nav.IsChecked = true;
    }

    private FrameworkElement? TourTarget(TourStep step) =>
        step.Target is null ? null : FindName(step.Target) as FrameworkElement;

    /// <summary>
    /// Cuts the spotlight and puts the card on the side of the target with
    /// the most room, never over it. A step without a target has no
    /// spotlight and the card sits bottom-right, nearest the tray.
    /// </summary>
    private void PlaceTourStep()
    {
        if (!_tourActive || _tourIndex < 0) return;
        var step = TourSteps[_tourIndex];
        var canvas = _tourCanvas!;
        var full = new Rect(0, 0, canvas.ActualWidth, canvas.ActualHeight);
        if (full.Width <= 0 || full.Height <= 0) return;

        _tourTitle!.Text = step.Title;
        _tourBody!.Text = step.Body;
        _tourCounter!.Text = $"{_tourIndex + 1} / {TourSteps.Count}";
        _tourBack!.IsEnabled = _tourIndex > 0;
        _tourNext!.Content = _tourIndex == TourSteps.Count - 1 ? "Finish" : "Next";

        var card = _tourCard!;
        card.Width = TourCardWidth;
        card.Measure(new Size(TourCardWidth, double.PositiveInfinity));
        var size = card.DesiredSize;

        Rect? hole = null;
        var target = TourTarget(step);
        if (target is not null)
        {
            var origin = target.TransformToVisual(canvas).Transform(new Point(0, 0));
            var r = new Rect(origin, new Size(target.ActualWidth, target.ActualHeight));
            r.Inflate(6, 6);
            hole = r;
        }

        var fullGeometry = new RectangleGeometry(full);
        _tourShade!.Data = hole is { } h
            ? new CombinedGeometry(GeometryCombineMode.Exclude, fullGeometry, new RectangleGeometry(h, 6, 6))
            : fullGeometry;

        var spot = hole is { } hh ? hh : new Rect(full.Right, full.Bottom, 0, 0);
        var at = ChooseTourCardPosition(full, spot, size);
        Canvas.SetLeft(card, Math.Round(at.X));
        Canvas.SetTop(card, Math.Round(at.Y));
    }

    private static Point ChooseTourCardPosition(Rect full, Rect spot, Size card)
    {
        double ClampX(double x) => Math.Max(full.Left + TourGap, Math.Min(x, full.Right - card.Width - TourGap));
        double ClampY(double y) => Math.Max(full.Top + TourGap, Math.Min(y, full.Bottom - card.Height - TourGap));

        // Beside the target first (right, then left), then below, then
        // above - the first side with room for the whole card wins.
        var candidates = new[]
        {
            new Point(spot.Right + TourGap, ClampY(spot.Top)),
            new Point(spot.Left - TourGap - card.Width, ClampY(spot.Top)),
            new Point(ClampX(spot.Left), spot.Bottom + TourGap),
            new Point(ClampX(spot.Left), spot.Top - TourGap - card.Height),
        };
        foreach (var c in candidates)
        {
            var rect = new Rect(c, card);
            if (full.Contains(rect) && !rect.IntersectsWith(spot)) return c;
        }

        // Nothing fits cleanly: take the corner farthest from the target.
        var corners = new[]
        {
            new Point(full.Right - card.Width - TourGap, full.Bottom - card.Height - TourGap),
            new Point(full.Left + TourGap, full.Bottom - card.Height - TourGap),
            new Point(full.Right - card.Width - TourGap, full.Top + TourGap),
            new Point(full.Left + TourGap, full.Top + TourGap),
        };
        var centre = new Point(spot.Left + spot.Width / 2, spot.Top + spot.Height / 2);
        return corners
            .OrderByDescending(c => (c.X + card.Width / 2 - centre.X) * (c.X + card.Width / 2 - centre.X) +
                                    (c.Y + card.Height / 2 - centre.Y) * (c.Y + card.Height / 2 - centre.Y))
            .First();
    }
}
