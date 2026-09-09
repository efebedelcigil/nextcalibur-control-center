using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Nextcalibur.App.Controls;

public sealed class TemperatureToDoubleConverter : IValueConverter
{
    private static readonly Regex NumberRegex = new(@"[-+]?[0-9]*\.?[0-9]+", RegexOptions.Compiled);

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return 0.0;
        if (value is double d) return d;
        if (value is int i) return (double)i;

        var str = value.ToString();
        if (string.IsNullOrWhiteSpace(str)) return 0.0;

        var match = NumberRegex.Match(str);
        if (match.Success && double.TryParse(match.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var num))
        {
            return num;
        }

        return 0.0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

public sealed class RpmToDoubleConverter : IValueConverter
{
    private static readonly Regex NumberRegex = new(@"[0-9]+", RegexOptions.Compiled);

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return 0.0;
        if (value is double d) return d;
        if (value is int i) return (double)i;

        var str = value.ToString();
        if (string.IsNullOrWhiteSpace(str)) return 0.0;

        var match = NumberRegex.Match(str);
        if (match.Success && double.TryParse(match.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var num))
        {
            return num;
        }

        return 0.0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
