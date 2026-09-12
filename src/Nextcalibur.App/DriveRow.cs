using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nextcalibur.App;

/// <summary>
/// One drive as the Memory & Disk panel shows it. Updated in place every
/// refresh rather than rebuilt, so the gauges keep their identity - no
/// re-created controls, no animation restarting from zero every five seconds.
/// </summary>
public sealed class DriveRow : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private double _percent;
    private string _percentText = "--";
    private string _detail = "--";

    /// <summary>"C:" or "D: Games".</summary>
    public string Name { get => _name; set => Set(ref _name, value); }

    /// <summary>Used, 0-100, for the gauge.</summary>
    public double Percent { get => _percent; set => Set(ref _percent, value); }

    /// <summary>"63,1%".</summary>
    public string PercentText { get => _percentText; set => Set(ref _percentText, value); }

    /// <summary>"586,9/930,3GB".</summary>
    public string Detail { get => _detail; set => Set(ref _detail, value); }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
