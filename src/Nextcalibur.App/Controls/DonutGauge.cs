using System.Windows;
using System.Windows.Media;

namespace Nextcalibur.App.Controls;

public sealed class DonutGauge : FrameworkElement
{
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value), typeof(double), typeof(DonutGauge),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(
            nameof(Maximum), typeof(double), typeof(DonutGauge),
            new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty ArcBrushProperty =
        DependencyProperty.Register(
            nameof(ArcBrush), typeof(Brush), typeof(DonutGauge),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty TrackBrushProperty =
        DependencyProperty.Register(
            nameof(TrackBrush), typeof(Brush), typeof(DonutGauge),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty BracketBrushProperty =
        DependencyProperty.Register(
            nameof(BracketBrush), typeof(Brush), typeof(DonutGauge),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty StrokeThicknessProperty =
        DependencyProperty.Register(
            nameof(StrokeThickness), typeof(double), typeof(DonutGauge),
            new FrameworkPropertyMetadata(7.0, FrameworkPropertyMetadataOptions.AffectsRender));

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

    public Brush? BracketBrush
    {
        get => (Brush?)GetValue(BracketBrushProperty);
        set => SetValue(BracketBrushProperty, value);
    }

    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    static DonutGauge()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DonutGauge), new FrameworkPropertyMetadata(typeof(DonutGauge)));
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        var d = 64.0;
        if (!double.IsInfinity(availableSize.Width) && !double.IsInfinity(availableSize.Height))
            d = Math.Min(availableSize.Width, availableSize.Height);
        else if (!double.IsInfinity(availableSize.Width))
            d = availableSize.Width;
        else if (!double.IsInfinity(availableSize.Height))
            d = availableSize.Height;
        return new Size(d, d);
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
        var strokeThick = StrokeThickness;

        // Radius of center line of the arc
        var radius = Math.Max(1.0, (diameter - strokeThick - 12.0) / 2.0);

        var trackBrush = TrackBrush ?? new SolidColorBrush(Color.FromRgb(37, 37, 37));
        var arcBrush = ArcBrush ?? new SolidColorBrush(Color.FromRgb(76, 141, 255));
        var bracketBrush = BracketBrush ?? new SolidColorBrush(Color.FromRgb(140, 140, 140));

        // 1. Draw outer brackets (decorative framing arcs)
        var bRadius = radius + strokeThick / 2.0 + 3.0;
        var bPen = new Pen(bracketBrush, 1.2);
        bPen.Freeze();

        // Left bracket: from 135 deg to 225 deg
        DrawArc(dc, bPen, cx, cy, bRadius, 135, 90);
        // Right bracket: from -45 deg to +45 deg
        DrawArc(dc, bPen, cx, cy, bRadius, -45, 90);

        // 2. Draw background track circle
        var trackPen = new Pen(trackBrush, strokeThick);
        trackPen.Freeze();
        dc.DrawEllipse(null, trackPen, new Point(cx, cy), radius, radius);

        // 3. Draw inner hub solid disc
        var hubRadius = Math.Max(0.5, radius - strokeThick / 2.0 - 2.0);
        dc.DrawEllipse(new SolidColorBrush(Color.FromRgb(32, 34, 40)), null, new Point(cx, cy), hubRadius, hubRadius);

        // 4. Draw progress arc
        var max = Math.Max(0.001, Maximum);
        var fraction = Math.Clamp(Value / max, 0.0, 1.0);
        if (fraction > 0.001)
        {
            var sweepAngle = fraction * 360.0;
            var arcPen = new Pen(arcBrush, strokeThick) { StartLineCap = PenLineCap.Flat, EndLineCap = PenLineCap.Flat };
            arcPen.Freeze();
            DrawArc(dc, arcPen, cx, cy, radius, -90, sweepAngle);
        }
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
