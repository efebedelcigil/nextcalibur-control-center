using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Nextcalibur.App.Controls;

/// <summary>
/// An HSV colour wheel: hue around the circumference, saturation from centre to
/// edge.
///
/// Value is deliberately fixed at maximum. Brightness is a separate, global
/// control on this hardware — folding it into the wheel would make the same
/// setting reachable two ways with different scopes.
/// </summary>
public sealed class ColourWheel : Control
{
    private const int Resolution = 256;

    private Image? _wheel;
    private Ellipse? _thumb;
    private Canvas? _canvas;
    private bool _dragging;

    public static readonly DependencyProperty SelectedColourProperty =
        DependencyProperty.Register(
            nameof(SelectedColour), typeof(Color), typeof(ColourWheel),
            new FrameworkPropertyMetadata(Colors.White,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedColourChanged));

    public Color SelectedColour
    {
        get => (Color)GetValue(SelectedColourProperty);
        set => SetValue(SelectedColourProperty, value);
    }

    /// <summary>Raised when the user picks a colour, not when it is set in code.</summary>
    public event EventHandler<Color>? ColourPicked;

    public ColourWheel()
    {
        Focusable = false;
        Loaded += (_, _) => Build();
        SizeChanged += (_, _) => Layout();
        IsEnabledChanged += (_, _) => ApplyEnabledLook();
    }

    private void Build()
    {
        if (_canvas is not null) return;

        _canvas = new Canvas();
        _wheel = new Image
        {
            Source = RenderWheel(),
            Stretch = Stretch.Fill,
            IsHitTestVisible = true,
        };
        _thumb = new Ellipse
        {
            Width = 16,
            Height = 16,
            Stroke = Brushes.White,
            StrokeThickness = 2.5,
            Fill = Brushes.Transparent,
            IsHitTestVisible = false,
            Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                ShadowDepth = 0, BlurRadius = 4, Opacity = 0.7, Color = Colors.Black,
            },
        };

        _canvas.Children.Add(_wheel);
        _canvas.Children.Add(_thumb);
        AddVisualChild(_canvas);
        AddLogicalChild(_canvas);

        MouseLeftButtonDown += OnDown;
        MouseMove += OnMove;
        MouseLeftButtonUp += OnUp;
        MouseLeave += OnUp;

        Layout();
        ApplyEnabledLook();
    }

    protected override int VisualChildrenCount => _canvas is null ? 0 : 1;

    protected override Visual GetVisualChild(int index) =>
        _canvas ?? throw new ArgumentOutOfRangeException(nameof(index));

    protected override Size ArrangeOverride(Size size)
    {
        _canvas?.Arrange(new Rect(size));
        Layout();
        return size;
    }

    private double Diameter => Math.Max(0, Math.Min(ActualWidth, ActualHeight));

    private void Layout()
    {
        if (_wheel is null || _canvas is null) return;

        var d = Diameter;
        _wheel.Width = d;
        _wheel.Height = d;
        Canvas.SetLeft(_wheel, (ActualWidth - d) / 2);
        Canvas.SetTop(_wheel, (ActualHeight - d) / 2);
        MoveThumbTo(SelectedColour);
    }

    private void ApplyEnabledLook() => Opacity = IsEnabled ? 1.0 : 0.28;

    private static void OnSelectedColourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ColourWheel wheel) wheel.MoveThumbTo((Color)e.NewValue);
    }

    // --- input ---

    private void OnDown(object sender, MouseButtonEventArgs e)
    {
        if (!IsEnabled) return;
        _dragging = true;
        CaptureMouse();
        Pick(e.GetPosition(this));
    }

    private void OnMove(object sender, MouseEventArgs e)
    {
        if (_dragging && IsEnabled) Pick(e.GetPosition(this));
    }

    private void OnUp(object sender, MouseEventArgs e)
    {
        if (!_dragging) return;
        _dragging = false;
        ReleaseMouseCapture();
    }

    private void Pick(Point point)
    {
        var d = Diameter;
        if (d <= 0) return;

        var radius = d / 2;
        var cx = ActualWidth / 2;
        var cy = ActualHeight / 2;
        var dx = point.X - cx;
        var dy = point.Y - cy;

        var distance = Math.Sqrt(dx * dx + dy * dy);

        // Dragging past the rim keeps the hue and pins saturation, which is what
        // a user sweeping around the edge expects.
        var saturation = Math.Min(1.0, distance / radius);
        var hue = (Math.Atan2(dy, dx) * 180 / Math.PI + 360) % 360;

        var colour = FromHsv(hue, saturation, 1.0);
        SelectedColour = colour;
        ColourPicked?.Invoke(this, colour);
    }

    private void MoveThumbTo(Color colour)
    {
        if (_thumb is null) return;

        var (hue, saturation, _) = ToHsv(colour);
        var radius = Diameter / 2;
        var angle = hue * Math.PI / 180;

        Canvas.SetLeft(_thumb, ActualWidth / 2 + Math.Cos(angle) * saturation * radius - _thumb.Width / 2);
        Canvas.SetTop(_thumb, ActualHeight / 2 + Math.Sin(angle) * saturation * radius - _thumb.Height / 2);
    }

    // --- rendering ---

    private static BitmapSource RenderWheel()
    {
        var bitmap = new WriteableBitmap(Resolution, Resolution, 96, 96, PixelFormats.Bgra32, null);
        var pixels = new byte[Resolution * Resolution * 4];
        var radius = Resolution / 2.0;

        for (var y = 0; y < Resolution; y++)
        {
            for (var x = 0; x < Resolution; x++)
            {
                var dx = x - radius + 0.5;
                var dy = y - radius + 0.5;
                var distance = Math.Sqrt(dx * dx + dy * dy);
                var i = (y * Resolution + x) * 4;

                if (distance > radius)
                {
                    pixels[i + 3] = 0;      // outside the circle
                    continue;
                }

                var hue = (Math.Atan2(dy, dx) * 180 / Math.PI + 360) % 360;
                var colour = FromHsv(hue, distance / radius, 1.0);

                pixels[i + 0] = colour.B;
                pixels[i + 1] = colour.G;
                pixels[i + 2] = colour.R;

                // Feather the last pixel of the rim so the circle is not jagged.
                var edge = radius - distance;
                pixels[i + 3] = edge >= 1 ? (byte)255 : (byte)(edge * 255);
            }
        }

        bitmap.WritePixels(new Int32Rect(0, 0, Resolution, Resolution), pixels, Resolution * 4, 0);
        bitmap.Freeze();
        return bitmap;
    }

    // --- colour maths ---

    public static Color FromHsv(double hue, double saturation, double value)
    {
        var c = value * saturation;
        var x = c * (1 - Math.Abs(hue / 60 % 2 - 1));
        var m = value - c;

        var (r, g, b) = hue switch
        {
            < 60 => (c, x, 0.0),
            < 120 => (x, c, 0.0),
            < 180 => (0.0, c, x),
            < 240 => (0.0, x, c),
            < 300 => (x, 0.0, c),
            _ => (c, 0.0, x),
        };

        return Color.FromRgb(
            (byte)Math.Round((r + m) * 255),
            (byte)Math.Round((g + m) * 255),
            (byte)Math.Round((b + m) * 255));
    }

    public static (double Hue, double Saturation, double Value) ToHsv(Color colour)
    {
        double r = colour.R / 255.0, g = colour.G / 255.0, b = colour.B / 255.0;
        var max = Math.Max(r, Math.Max(g, b));
        var min = Math.Min(r, Math.Min(g, b));
        var delta = max - min;

        double hue = 0;
        if (delta > 0)
        {
            if (max == r) hue = 60 * (((g - b) / delta + 6) % 6);
            else if (max == g) hue = 60 * ((b - r) / delta + 2);
            else hue = 60 * ((r - g) / delta + 4);
        }

        return (hue, max == 0 ? 0 : delta / max, max);
    }
}
