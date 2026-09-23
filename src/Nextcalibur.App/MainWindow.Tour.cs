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
/// tray. The order lives here, the words in the dictionaries; the look lives in the markup
/// (`TourOverlay` and its parts, BRIEF §20). The parts are looked up by
/// name when the tour starts, so this compiles and does nothing until the
/// markup is there, and a renamed part is a skipped step rather than a
/// crash.
/// </summary>
public partial class MainWindow
{
    private enum TourPage { Any, System, Power, Display, Lighting, Settings }

    /// <param name="Target">x:Name of the control to spotlight; null for a step with no spotlight.</param>
    /// <param name="Key">The step's name in the dictionaries: <c>S.Tour.Step.{Key}.Title</c> and <c>.Body</c>.</param>
    /// <param name="Section">The part of the interface the step belongs to (<c>S.Tour.Section.{Section}</c>); "Skip section" jumps to the next one.</param>
    private sealed record TourStep(string? Target, TourPage Page, string Key, string Section)
    {
        public string Title => Strings.Get($"S.Tour.Step.{Key}.Title");
        public string Body => Strings.Get($"S.Tour.Step.{Key}.Body");
        public string SectionName => Strings.Get($"S.Tour.Section.{Section}");
    }

    private const double TourGap = 12;
    private const double TourCardWidth = 300;

    private int _tourIndex = -1;
    private int _tourDirection = 1;
    private bool _tourActive;
    // Set while a step is on its way to the screen: presses that land before
    // the card has appeared are ignored, so a page switch that takes a
    // moment cannot be pressed through.
    private bool _tourMoving;
    // Presses that arrived while moving. Two or more mean somebody was
    // hammering the key; the step that finally lands then stays deaf for a
    // moment so the hammering does not carry straight through it.
    private int _tourPressesWhileMoving;
    // Input carries the tick it was generated at. Anything generated before
    // the current step appeared was aimed at the previous one and is
    // dropped - even when a slow page switch queued it up and Windows
    // delivers it only after the card is on screen. Two or more such
    // presses and the step stays deaf for 1.5 s on top; the deafness is
    // decided once, at landing, so it cannot be extended into a lock-up.
    private int _tourAcceptFromTick;

    private Grid? _tourOverlay;
    private Canvas? _tourCanvas;
    private System.Windows.Shapes.Path? _tourShade;
    private FrameworkElement? _tourCard;
    private TextBlock? _tourTitle, _tourBody, _tourCounter;
    private Button? _tourBack, _tourNext, _tourSkip, _tourSkipSection;

    private static readonly IReadOnlyList<TourStep> TourSteps = new[]
    {
        // ---- The window
        new TourStep("TourButton", TourPage.Any, "TourButton", "Window"),
        new TourStep("TitleBar", TourPage.Any, "TitleBar", "Window"),
        new TourStep("ThemeSwitch", TourPage.Any, "ThemeSwitch", "Window"),
        new TourStep("MinimiseButton", TourPage.Any, "MinimiseButton", "Window"),
        new TourStep("HideToTrayButton", TourPage.Any, "HideToTrayButton", "Window"),
        new TourStep("CloseButton", TourPage.Any, "CloseButton", "Window"),
        // ---- The rail
        new TourStep("NavSystem", TourPage.System, "NavSystem", "Rail"),
        new TourStep("NavPower", TourPage.Any, "NavPower", "Rail"),
        new TourStep("NavDisplay", TourPage.Any, "NavDisplay", "Rail"),
        new TourStep("NavLighting", TourPage.Any, "NavLighting", "Rail"),
        new TourStep("NavSettings", TourPage.Any, "NavSettings", "Rail"),
        new TourStep("UpdateNowButton", TourPage.Any, "UpdateNowButton", "Rail"),
        new TourStep("OpenLogButton", TourPage.Any, "OpenLogButton", "Rail"),
        new TourStep("VersionText", TourPage.Any, "VersionText", "Rail"),
        // ---- System
        new TourStep("ModeOffice", TourPage.System, "ModeOffice", "System"),
        new TourStep("ModeGaming", TourPage.System, "ModeGaming", "System"),
        new TourStep("ModePerformance", TourPage.System, "ModePerformance", "System"),
        new TourStep("SystemModeNote", TourPage.System, "SystemModeNote", "System"),
        new TourStep("SubtitleText", TourPage.System, "SubtitleText", "System"),
        new TourStep("Banner", TourPage.System, "Banner", "System"),
        new TourStep("CpuFanGauge", TourPage.System, "CpuFanGauge", "System"),
        new TourStep("CpuName", TourPage.System, "CpuName", "System"),
        new TourStep("GpuFanGauge", TourPage.System, "GpuFanGauge", "System"),
        new TourStep("GpuName", TourPage.System, "GpuName", "System"),
        // ---- Power Mode
        new TourStep("PowerModeEfficiency", TourPage.Power, "PowerModeEfficiency", "Power"),
        new TourStep("PowerModeBalanced", TourPage.Power, "PowerModeBalanced", "Power"),
        new TourStep("PowerModeBetter", TourPage.Power, "PowerModeBetter", "Power"),
        new TourStep("PowerModeBest", TourPage.Power, "PowerModeBest", "Power"),
        new TourStep("OverlayState", TourPage.Power, "OverlayState", "Power"),
        new TourStep("OverlayDetail", TourPage.Power, "OverlayDetail", "Power"),
        new TourStep("FixButton", TourPage.Power, "FixButton", "Power"),
        // ---- Display Mode
        new TourStep("ModeDiscrete", TourPage.Display, "ModeDiscrete", "Display"),
        new TourStep("ModeHybrid", TourPage.Display, "ModeHybrid", "Display"),
        new TourStep("ModeUma", TourPage.Display, "ModeUma", "Display"),
        new TourStep("GpuRestartPending", TourPage.Display, "GpuRestartPending", "Display"),
        new TourStep("GpuModeDetail", TourPage.Display, "GpuModeDetail", "Display"),
        // ---- Lighting
        new TourStep("LedPower", TourPage.Lighting, "LedPower", "Lighting"),
        new TourStep("TabZoneA", TourPage.Lighting, "TabZoneA", "Lighting"),
        new TourStep("SelectAll", TourPage.Lighting, "SelectAll", "Lighting"),
        new TourStep("ProfileRow", TourPage.Lighting, "ProfileRow", "Lighting"),
        new TourStep("EffectPanel", TourPage.Lighting, "EffectPanel", "Lighting"),
        new TourStep("Wheel", TourPage.Lighting, "Wheel", "Lighting"),
        new TourStep("BrightnessSlider", TourPage.Lighting, "BrightnessSlider", "Lighting"),
        new TourStep("ReloadButton", TourPage.Lighting, "ReloadButton", "Lighting"),
        // ---- Settings
        new TourStep("SettingWinUtilButton", TourPage.Settings, "SettingWinUtilButton", "Settings"),
        new TourStep("SettingDriversButton", TourPage.Settings, "SettingDriversButton", "Settings"),
        new TourStep("SettingReportButton", TourPage.Settings, "SettingReportButton", "Settings"),
        new TourStep("SettingPrivacyButton", TourPage.Settings, "SettingPrivacyButton", "Settings"),
        new TourStep("SettingStartWithWindows", TourPage.Settings, "SettingStartWithWindows", "Settings"),
        new TourStep("SettingStartHow", TourPage.Settings, "SettingStartHow", "Settings"),
        new TourStep("SettingOfficeOnBattery", TourPage.Settings, "SettingOfficeOnBattery", "Settings"),
        new TourStep("SettingOverheatWarning", TourPage.Settings, "SettingOverheatWarning", "Settings"),
        new TourStep("SettingInterval2", TourPage.Settings, "SettingInterval2", "Settings"),
        new TourStep("SettingAutoCheckUpdates", TourPage.Settings, "SettingAutoCheckUpdates", "Settings"),
        new TourStep("SettingAutoInstallUpdates", TourPage.Settings, "SettingAutoInstallUpdates", "Settings"),
        new TourStep("SettingLanguageEnglish", TourPage.Settings, "SettingLanguageEnglish", "Settings"),
        new TourStep("SettingCheckNowButton", TourPage.Settings, "SettingCheckNowButton", "Settings"),
        // ---- Readings
        new TourStep("CpuClock", TourPage.System, "CpuClock", "Readings"),
        new TourStep("CpuTemp", TourPage.System, "CpuTemp", "Readings"),
        new TourStep("GpuClock", TourPage.System, "GpuClock", "Readings"),
        new TourStep("GpuTemp", TourPage.System, "GpuTemp", "Readings"),
        new TourStep("OverheatWarningToggle", TourPage.System, "OverheatWarningToggle", "Readings"),
        new TourStep("CpuWarnSlider", TourPage.System, "CpuWarnSlider", "Readings"),
        new TourStep("GpuWarnSlider", TourPage.System, "GpuWarnSlider", "Readings"),
        new TourStep("OverheatResetButton", TourPage.System, "OverheatResetButton", "Readings"),
        new TourStep("RamGauge", TourPage.System, "RamGauge", "Readings"),
        new TourStep("DriveList", TourPage.System, "DriveList", "Readings"),
        // ---- The tray
        new TourStep(null, TourPage.Any, "TrayIcon", "Tray"),
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
        _tourMoving = false;
        _tourPressesWhileMoving = 0;
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
        _tourSkipSection = FindName("TourSkipSection") as Button;   // optional

        if (_tourOverlay is null || _tourCanvas is null || _tourShade is null || _tourCard is null ||
            _tourTitle is null || _tourBody is null || _tourCounter is null ||
            _tourBack is null || _tourNext is null || _tourSkip is null)
        {
            Log.Warn("tour", "the overlay is missing from the markup; the tour cannot run");
            _tourOverlay = null;
            return false;
        }

        _tourBack.Click += (_, _) => { if (TourIsBusy()) return; _tourDirection = -1; GoToTourStep(_tourIndex - 1); };
        _tourNext.Click += (_, _) =>
        {
            if (TourIsBusy()) return;
            if (_tourIndex >= TourSteps.Count - 1) EndTour();
            else { _tourDirection = 1; GoToTourStep(_tourIndex + 1); }
        };
        _tourSkip.Click += (_, _) => EndTour();
        _tourOverlay.PreviewMouseDown += (_, e) => { if (TourInputIsStale(e.Timestamp)) e.Handled = true; };
        if (_tourSkipSection is not null)
            _tourSkipSection.Click += (_, _) =>
            {
                if (TourIsBusy()) return;
                var next = NextSectionStart(_tourIndex);
                _tourDirection = 1;
                if (next < 0) EndTour(); else GoToTourStep(next);
            };
        return true;
    }

    private bool TourInputIsStale(int timestamp)
    {
        if (_tourAcceptFromTick != int.MaxValue && unchecked(timestamp - _tourAcceptFromTick) >= 0) return false;
        _tourPressesWhileMoving++;
        return true;
    }

    private bool TourIsBusy()
    {
        if (!_tourMoving) return false;
        _tourPressesWhileMoving++;
        return true;
    }

    /// <summary>First step of the section after the one <paramref name="from"/> is in; -1 when it is the last.</summary>
    private static int NextSectionStart(int from)
    {
        var section = TourSteps[from].Section;
        for (var i = from + 1; i < TourSteps.Count; i++)
            if (TourSteps[i].Section != section) return i;
        return -1;
    }

    private void OnTourKey(object sender, KeyEventArgs e)
    {
        // The dialogue overlay sits above the tour and handles its own keys.
        if (ModalDialogOverlay.Visibility == Visibility.Visible) return;
        if (TourInputIsStale(e.Timestamp)) { e.Handled = true; return; }

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
        _tourMoving = true;
        _tourAcceptFromTick = int.MaxValue;   // nothing gets through until the step is on screen
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
            // Laid out is not seen. The frame is drawn at Render priority
            // after this; ContextIdle runs once that and any input already
            // queued behind it are done - so the tick taken there is when
            // the person could first have seen the card, and presses that
            // were waiting in the queue have gone by with the old marker.
            Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, () =>
            {
                if (!_tourActive || _tourIndex != index) return;
                _tourAcceptFromTick = Environment.TickCount + (_tourPressesWhileMoving >= 2 ? 1500 : 0);
                _tourPressesWhileMoving = 0;
                _tourMoving = false;
                _tourNext!.Focus();   // so the keys always have somewhere to land
            });
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
            TourPage.Settings => FindName("NavSettings") as RadioButton,
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
        _tourCounter!.Text = $"{step.SectionName} · {_tourIndex + 1} / {TourSteps.Count}";
        if (_tourSkipSection is not null)
        {
            var next = NextSectionStart(_tourIndex);
            _tourSkipSection.Content = next < 0 ? Strings.Get("S.Tour.Finish") : Strings.Get("S.Tour.SkipTo", TourSteps[next].SectionName);
        }
        _tourBack!.IsEnabled = _tourIndex > 0;
        _tourNext!.Content = _tourIndex == TourSteps.Count - 1 ? Strings.Get("S.Tour.Finish") : Strings.Get("S.Tour.Next");

        // Measure alone would hand back the previous step's size: new text
        // dirties only the TextBlock, and a parent whose own measure is
        // still valid answers from its cache. A layout pass first.
        var card = _tourCard!;
        card.Width = TourCardWidth;
        card.UpdateLayout();
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
