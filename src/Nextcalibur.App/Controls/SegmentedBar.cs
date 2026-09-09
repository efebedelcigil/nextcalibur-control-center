using System.Windows;
using System.Windows.Media;

namespace Nextcalibur.App.Controls;

public sealed class SegmentedBar : FrameworkElement
{
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value), typeof(double), typeof(SegmentedBar),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(
            nameof(Minimum), typeof(double), typeof(SegmentedBar),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(
            nameof(Maximum), typeof(double), typeof(SegmentedBar),
            new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty SegmentCountProperty =
        DependencyProperty.Register(
            nameof(SegmentCount), typeof(int), typeof(SegmentedBar),
            new FrameworkPropertyMetadata(16, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty LitBrushProperty =
        DependencyProperty.Register(
            nameof(LitBrush), typeof(Brush), typeof(SegmentedBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty UnlitBrushProperty =
        DependencyProperty.Register(
            nameof(UnlitBrush), typeof(Brush), typeof(SegmentedBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public int SegmentCount
    {
        get => (int)GetValue(SegmentCountProperty);
        set => SetValue(SegmentCountProperty, value);
    }

    public Brush? LitBrush
    {
        get => (Brush?)GetValue(LitBrushProperty);
        set => SetValue(LitBrushProperty, value);
    }

    public Brush? UnlitBrush
    {
        get => (Brush?)GetValue(UnlitBrushProperty);
        set => SetValue(UnlitBrushProperty, value);
    }

    static SegmentedBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SegmentedBar), new FrameworkPropertyMetadata(typeof(SegmentedBar)));
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        var w = double.IsInfinity(availableSize.Width) ? 160.0 : availableSize.Width;
        var h = double.IsInfinity(availableSize.Height) ? 8.0 : availableSize.Height;
        return new Size(w, Math.Max(h, 6.0));
    }

    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);

        var count = Math.Max(1, SegmentCount);
        var min = Minimum;
        var max = Math.Max(min + 0.001, Maximum);
        var val = Math.Clamp(Value, min, max);

        var fraction = (val - min) / (max - min);
        var litCount = (int)Math.Round(fraction * count);

        var w = ActualWidth;
        var h = ActualHeight;
        if (w <= 0 || h <= 0) return;

        var defaultLit = LitBrush ?? new SolidColorBrush(Color.FromRgb(76, 141, 255));
        var defaultUnlit = UnlitBrush ?? new SolidColorBrush(Color.FromRgb(26, 27, 31));

        // Angled chevron slope (angle ~70 degrees)
        var slant = h * 0.36;
        var gap = 3.5;
        var totalGaps = (count - 1) * gap;
        var segWidth = Math.Max(1.0, (w - slant - totalGaps) / count);

        for (var i = 0; i < count; i++)
        {
            var x0 = i * (segWidth + gap);
            var isLit = i < litCount;
            var brush = isLit ? defaultLit : defaultUnlit;

            var p0 = new Point(x0 + slant, 0);
            var p1 = new Point(x0 + slant + segWidth, 0);
            var p2 = new Point(x0 + segWidth, h);
            var p3 = new Point(x0, h);

            var geo = new StreamGeometry();
            using (var ctx = geo.Open())
            {
                ctx.BeginFigure(p0, isFilled: true, isClosed: true);
                ctx.LineTo(p1, isStroked: false, isSmoothJoin: false);
                ctx.LineTo(p2, isStroked: false, isSmoothJoin: false);
                ctx.LineTo(p3, isStroked: false, isSmoothJoin: false);
            }
            geo.Freeze();

            dc.DrawGeometry(brush, null, geo);
        }
    }
}
