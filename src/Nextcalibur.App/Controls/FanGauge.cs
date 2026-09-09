using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nextcalibur.App.Controls;

public class FanGauge : ContentControl
{
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value), typeof(double), typeof(FanGauge),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(
            nameof(Maximum), typeof(double), typeof(FanGauge),
            new FrameworkPropertyMetadata(6000.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty BladeBrushProperty =
        DependencyProperty.Register(
            nameof(BladeBrush), typeof(Brush), typeof(FanGauge),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty ArcBrushProperty =
        DependencyProperty.Register(
            nameof(ArcBrush), typeof(Brush), typeof(FanGauge),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty TrackBrushProperty =
        DependencyProperty.Register(
            nameof(TrackBrush), typeof(Brush), typeof(FanGauge),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public Brush? BladeBrush
    {
        get => (Brush?)GetValue(BladeBrushProperty);
        set => SetValue(BladeBrushProperty, value);
    }

    public Brush? ArcBrush
    {
        get => (Brush?)GetValue(ArcBrushProperty);
        set => SetValue(ArcBrushProperty, value);
    }

    public Brush? TrackBrush
    {
        get => (Brush?)GetValue(TrackBrushProperty);
        set => SetValue(TrackBrushProperty, value);
    }

    static FanGauge()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(FanGauge), new FrameworkPropertyMetadata(typeof(FanGauge)));
    }

    public FanGauge()
    {
        HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
        VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
    }

    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);

        var w = ActualWidth;
        var h = ActualHeight;
        if (w <= 0 || h <= 0) return;

        var cx = w / 2.0;
        var cy = h / 2.0;
        var diameter = Math.Min(w, h);
        var radius = diameter / 2.0;

        var defaultBlade = BladeBrush ?? new SolidColorBrush(Color.FromRgb(255, 119, 0));
        var defaultArc = ArcBrush ?? new SolidColorBrush(Color.FromRgb(76, 141, 255));
        var defaultTrack = TrackBrush ?? new SolidColorBrush(Color.FromRgb(32, 34, 40));

        // 1. Draw outer RPM meter ring track (thickness 4px)
        var arcRadius = radius - 5.0;
        var trackPen = new Pen(defaultTrack, 4.0);
        trackPen.Freeze();
        dc.DrawEllipse(null, trackPen, new Point(cx, cy), arcRadius, arcRadius);

        // 2. Draw active RPM arc (scaled against 6000 max)
        var max = Math.Max(1.0, Maximum);
        var fraction = Math.Clamp(Value / max, 0.0, 1.0);
        if (fraction > 0.005)
        {
            var sweepAngle = fraction * 360.0;
            var arcPen = new Pen(defaultArc, 4.0) { StartLineCap = PenLineCap.Round, EndLineCap = PenLineCap.Round };
            arcPen.Freeze();
            DrawArc(dc, arcPen, cx, cy, arcRadius, -90, sweepAngle);
        }

        // 3. Draw static 12-blade impeller turbine blades
        var rOuter = radius - 12.0;
        var rInner = radius * 0.46;
        var bladeCount = 12;
        var angleStep = 360.0 / bladeCount;

        for (var i = 0; i < bladeCount; i++)
        {
            var baseAngle = i * angleStep;
            var a0 = (baseAngle) * Math.PI / 180.0;
            var a1 = (baseAngle + 12.0) * Math.PI / 180.0;
            var a2 = (baseAngle + 26.0) * Math.PI / 180.0;

            // Curved impeller blade geometry
            var p0 = new Point(cx + rInner * Math.Cos(a0), cy + rInner * Math.Sin(a0));
            var p1 = new Point(cx + rOuter * Math.Cos(a1), cy + rOuter * Math.Sin(a1));
            var p2 = new Point(cx + rOuter * Math.Cos(a2), cy + rOuter * Math.Sin(a2));
            var p3 = new Point(cx + (rInner + 4) * Math.Cos(a1), cy + (rInner + 4) * Math.Sin(a1));

            var geo = new StreamGeometry();
            using (var ctx = geo.Open())
            {
                ctx.BeginFigure(p0, isFilled: true, isClosed: true);
                ctx.LineTo(p1, isStroked: false, isSmoothJoin: false);
                ctx.LineTo(p2, isStroked: false, isSmoothJoin: false);
                ctx.LineTo(p3, isStroked: false, isSmoothJoin: false);
            }
            geo.Freeze();

            dc.DrawGeometry(defaultBlade, null, geo);
        }

        // 4. Draw central dark hub disc
        var hubRadius = rInner + 2.0;
        var hubBrush = new SolidColorBrush(Color.FromRgb(22, 24, 28));
        var hubBorder = new Pen(new SolidColorBrush(Color.FromRgb(45, 48, 56)), 1.5);
        hubBrush.Freeze();
        hubBorder.Freeze();
        dc.DrawEllipse(hubBrush, hubBorder, new Point(cx, cy), hubRadius, hubRadius);
    }

    private static void DrawArc(DrawingContext dc, Pen pen, double cx, double cy, double radius, double startAngleDeg, double sweepAngleDeg)
    {
        if (sweepAngleDeg >= 360.0)
        {
            dc.DrawEllipse(null, pen, new Point(cx, cy), radius, radius);
            return;
        }

        var startRad = startAngleDeg * Math.PI / 180.0;
        var endRad = (startAngleDeg + sweepAngleDeg) * Math.PI / 180.0;

        var startPoint = new Point(cx + radius * Math.Cos(startRad), cy + radius * Math.Sin(startRad));
        var endPoint = new Point(cx + radius * Math.Cos(endRad), cy + radius * Math.Sin(endRad));
        var isLargeArc = sweepAngleDeg > 180.0;

        var geo = new StreamGeometry();
        using (var ctx = geo.Open())
        {
            ctx.BeginFigure(startPoint, isFilled: false, isClosed: false);
            ctx.ArcTo(endPoint, new Size(radius, radius), 0, isLargeArc, SweepDirection.Clockwise, isStroked: true, isSmoothJoin: false);
        }
        geo.Freeze();

        dc.DrawGeometry(null, pen, geo);
    }
}
