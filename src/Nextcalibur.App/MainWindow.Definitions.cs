using System.Windows;
using System.Windows.Controls;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;

namespace Nextcalibur.App;

/// <summary>
/// The Power page's last card: each component's power values as the
/// machine has them - what is in force, what the component's maker defines,
/// and who set it (the owner's ask of 14 October 2026: read, never assumed,
/// and shown without crowding the page). Read when the page is opened, off
/// the interface thread; the graphics card only while it is awake.
/// </summary>
public partial class MainWindow
{
    private bool _definitionsLoading;

    private async Task LoadDefinitionsAsync()
    {
        if (_definitionsLoading || FindName("DefinitionRows") is not StackPanel rows) return;
        _definitionsLoading = true;
        try
        {
            var gpuAwake = CardIsAwakeNow();
            var found = await Task.Run(() =>
            {
                var all = new List<PowerDefinition>();
                try
                {
                    using var msr = new CpuPowerReader();
                    if (msr.CanOpen()) all.AddRange(PowerDefinitions.Processor(msr));
                }
                catch (Exception ex) when (ex is not OutOfMemoryException) { Log.Warn("definitions", ex.Message); }
                if (gpuAwake)
                {
                    using var gpu = new GpuClockReader();
                    if (gpu.ReadPowerLimits() is { } limits) all.AddRange(PowerDefinitions.GraphicsCard(limits));
                }
                all.AddRange(PowerDefinitions.Battery());
                return all;
            });

            rows.Children.Clear();
            foreach (var d in found) rows.Children.Add(DefinitionRow(d));
            if (MachineScan.Firmware is { ProcessorMaxMhz: { } top })
                rows.Children.Add(DefinitionRow(new PowerDefinition("cpu.top", top / 1000.0, null, SetBy.Intel, null, "GHz")));
            if (rows.Children.Count == 0)
                rows.Children.Add(Localized.Text(new TextBlock { Style = (Style)FindResource("StatusTextStyle") }, () => Strings.Get("S.Def.None")));
            if (!gpuAwake)
                rows.Children.Add(Localized.Text(new TextBlock { Style = (Style)FindResource("StatusTextStyle"), Margin = new Thickness(0, 4, 0, 0) }, () => Strings.Get("S.Def.GpuAsleep")));
        }
        finally
        {
            _definitionsLoading = false;
        }
    }

    private Grid DefinitionRow(PowerDefinition d)
    {
        var row = new Grid { Margin = new Thickness(0, 2, 0, 2) };
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(210) });
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        row.Children.Add(Localized.Text(new TextBlock { FontSize = 12, Foreground = (System.Windows.Media.Brush)FindResource("Ink"), TextWrapping = TextWrapping.Wrap }, () => Strings.Get($"S.Def.{d.Key}")));

        var value = new TextBlock { Text = Format(d.Value, d.Unit), FontSize = 12, FontWeight = FontWeights.SemiBold, Foreground = (System.Windows.Media.Brush)FindResource("Ink") };
        Grid.SetColumn(value, 1);
        row.Children.Add(value);

        var source = Localized.Text(new TextBlock { Style = (Style)FindResource("StatusTextStyle"), TextWrapping = TextWrapping.Wrap }, () =>
        {
            var note = Strings.Get("S.Def.SetBy", Who(d.SetBy));
            if (d.Defined is { } defined && d.DefinedBy is { } by && Math.Abs(defined - d.Value) >= 0.5)
                note += " · " + Strings.Get("S.Def.Defined", Who(by), Format(defined, d.Unit));
            return note;
        });
        Grid.SetColumn(source, 2);
        row.Children.Add(source);
        return row;
    }

    private static string Format(double value, string unit) => unit switch
    {
        "GHz" => $"{value:0.0} GHz",
        "%" => $"{value:0}%",
        "Wh" => $"{value:0.0} Wh",
        _ => $"{value:0.#} {unit}",
    };

    private string Who(SetBy by) => by switch
    {
        SetBy.LaptopMaker => Strings.Get(_support.AllowsReads ? "S.Def.By.Casper" : "S.Def.By.Maker"),
        _ => Strings.Get($"S.Def.By.{by}"),
    };
}
