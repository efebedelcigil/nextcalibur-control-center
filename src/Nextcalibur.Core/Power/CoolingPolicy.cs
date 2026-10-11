namespace Nextcalibur.Core.Power;

/// <summary>What the cooling policy wants done after a reading.</summary>
public enum CoolingStep
{
    Nothing,
    Start,
    Stop,
}

/// <summary>
/// When to turn the cooling on, and when to let it go - the decision,
/// without the doing, so it can be tested against made-up readings.
///
/// It works on the margin: how far the hotter chip, measured against its own
/// warning line, is from that line (negative below it). The owner's rule, 11
/// October 2026: start <em>before</em> the line, when the temperature is on
/// its way there, so the line is never reached. So the last three minutes of
/// readings are fitted with a straight line, and the cooling starts when
/// - the margin is within <see cref="WatchFrom"/> degrees of the line and,
///   at its present rate, would cross it within <see cref="Lookahead"/>; or
/// - it is over the line already.
/// Either has to hold on two readings in a row, and for at least
/// <see cref="HeadingFor"/>: one reading is a spike, and so is the burst of
/// work at a start - the window reads every five seconds, so "two readings"
/// alone was five seconds of a start-up climb (0.6.0, its first minute).
///
/// It stops when the margin has been <see cref="ReleaseMargin"/> degrees
/// under the line and not rising for <see cref="CoolFor"/> - far enough
/// below that the load which made it hot, still running, does not walk it
/// straight back up and set the two flapping. A gap of more than
/// <see cref="Gap"/> between readings is sleep and starts everything again.
/// </summary>
public sealed class CoolingPolicy
{
    // The same ten degrees as the window's cheap-sensor gate
    // (MainWindow.CheapGateMarginC): further out, hidden, nothing is read.
    public const int WatchFrom = 10;
    public static readonly TimeSpan HeadingFor = TimeSpan.FromSeconds(30);
    public static readonly TimeSpan Lookahead = TimeSpan.FromMinutes(2);
    public static readonly TimeSpan History = TimeSpan.FromMinutes(3);
    public static readonly TimeSpan CoolFor = TimeSpan.FromMinutes(5);
    public const int ReleaseMargin = 15;
    public static readonly TimeSpan Gap = TimeSpan.FromMinutes(3.5);

    private readonly List<(DateTime At, double Margin)> _readings = new();
    private int _heading;
    private DateTime _headingSince = DateTime.MinValue;
    private DateTime _coolSince = DateTime.MinValue;

    /// <summary>Whether the cooling is on now, as far as this policy knows.</summary>
    public bool Engaged { get; set; }

    /// <summary>The rate of the last fit, in degrees a minute, for the notification.</summary>
    public double DegreesPerMinute { get; private set; }

    /// <summary>One reading of both chips against their lines.</summary>
    public CoolingStep Next(int cpuC, int cpuLimitC, int gpuC, int gpuLimitC, DateTime now) =>
        Next(Math.Max(cpuC - cpuLimitC, gpuC > 0 ? gpuC - gpuLimitC : int.MinValue), now);

    /// <summary>One reading of the margin: degrees over the line (negative under it).</summary>
    public CoolingStep Next(double margin, DateTime now)
    {
        if (_readings.Count > 0 && (now - _readings[^1].At > Gap || now < _readings[^1].At))
        {
            _readings.Clear();
            _heading = 0;
            _headingSince = DateTime.MinValue;
            _coolSince = DateTime.MinValue;
        }
        _readings.Add((now, margin));
        _readings.RemoveAll(r => now - r.At > History);
        DegreesPerMinute = Slope();

        if (!Engaged)
        {
            var projected = margin + DegreesPerMinute * Lookahead.TotalMinutes;
            var heading = margin >= 0 || (margin >= -WatchFrom && projected >= 0 && DegreesPerMinute > 0);
            if (!heading) { _heading = 0; _headingSince = DateTime.MinValue; return CoolingStep.Nothing; }
            if (_heading++ == 0) _headingSince = now;
            if (_heading < 2 || now - _headingSince < HeadingFor) return CoolingStep.Nothing;
            _heading = 0;
            _headingSince = DateTime.MinValue;
            return CoolingStep.Start;
        }

        if (margin > -ReleaseMargin || DegreesPerMinute > 0.5) { _coolSince = DateTime.MinValue; return CoolingStep.Nothing; }
        if (_coolSince == DateTime.MinValue) _coolSince = now;
        if (now - _coolSince < CoolFor) return CoolingStep.Nothing;
        _coolSince = DateTime.MinValue;
        return CoolingStep.Stop;
    }

    /// <summary>Least-squares slope of the kept readings, degrees a minute; 0 with too little to go on.</summary>
    private double Slope()
    {
        if (_readings.Count < 2 || (_readings[^1].At - _readings[0].At).TotalSeconds < 50) return 0;
        var t0 = _readings[0].At;
        double n = _readings.Count, sx = 0, sy = 0, sxx = 0, sxy = 0;
        foreach (var (at, m) in _readings)
        {
            var x = (at - t0).TotalMinutes;
            sx += x; sy += m; sxx += x * x; sxy += x * m;
        }
        var d = n * sxx - sx * sx;
        return d <= 0 ? 0 : (n * sxy - sx * sy) / d;
    }
}
