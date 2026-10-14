namespace Nextcalibur.Core.Power;

/// <summary>
/// The processor's temperature from two sources, together (the owner's
/// rule of 14 October 2026). The embedded controller's reading is the one
/// trusted - it is read through the firmware, with an interrupt - but it is
/// late: under full load the package was at 97 °C while the controller still
/// said 56, for 15-20 seconds (measured that day). The processor's own
/// register answers at once, with no interrupt, but alone it is not trusted.
///
/// So the register is calibrated against the controller: the offset between
/// them (register minus controller) is learnt while the temperature is
/// steady - when the controller's lag does not matter - and the register's
/// reading less that offset is used, never below the controller's own. A
/// climb shows at once and stays shown until the controller catches up; at
/// rest the number is the controller's.
///
/// (A first version added the register's change to the controller's level;
/// run on the recorded load, the change had faded by the time the controller
/// caught up, and it warned no earlier than the controller alone.)
///
/// When the two disagree for long - more than <see cref="DisagreeC"/> apart
/// for <see cref="DisagreeFor"/> after the offset - the register is set aside
/// and the controller's reading is used alone, until they agree again.
/// </summary>
public sealed class TemperatureFusion
{
    /// <summary>The register's slow average, for telling steady from changing.</summary>
    public static readonly TimeSpan SlowTimeConstant = TimeSpan.FromSeconds(15);

    /// <summary>Within this of its own slow average, the register counts as steady.</summary>
    public const double SteadyC = 3;

    /// <summary>The offset learnt is kept within this: more than that is not an offset but a fault.</summary>
    public const double MaxOffsetC = 10;

    public const double DisagreeC = 15;
    public static readonly TimeSpan DisagreeFor = TimeSpan.FromMinutes(2);

    private double? _ec, _msr, _msrSlow;
    private double _offset;
    private bool _offsetLearnt;
    private DateTime? _lastMsrAt, _disagreeSince;

    /// <summary>Whether the register is set aside, the two having disagreed for too long.</summary>
    public bool RegisterSetAside { get; private set; }

    /// <summary>The offset learnt between the two, register minus controller.</summary>
    public double Offset => _offset;

    /// <summary>The controller's reading, when one comes.</summary>
    public void Controller(double celsius, DateTime now)
    {
        _ec = celsius;
        Learn();
        Check(now);
    }

    /// <summary>The register's reading, as often as it is read; null when it could not be.</summary>
    public void Register(double? celsius, DateTime now)
    {
        if (celsius is not { } c)
        {
            _msr = null;
            _msrSlow = null;
            _lastMsrAt = null;
            return;
        }

        if (_msrSlow is not { } slow || _lastMsrAt is not { } last || now <= last) _msrSlow = c;
        else _msrSlow = slow + Math.Clamp((now - last).TotalSeconds / SlowTimeConstant.TotalSeconds, 0, 1) * (c - slow);
        _msr = c;
        _lastMsrAt = now;
        Check(now);
    }

    /// <summary>The temperature to act on, or null with no reading at all.</summary>
    public double? Value
    {
        get
        {
            if (_ec is not { } ec) return RegisterSetAside ? null : _msr;
            if (RegisterSetAside || _msr is not { } msr) return ec;
            return Math.Max(ec, msr - _offset);
        }
    }

    /// <summary>Learns the offset, but only while the register is steady: then the controller's lag does not matter.</summary>
    private void Learn()
    {
        if (_ec is not { } ec || _msr is not { } msr || _msrSlow is not { } slow || Math.Abs(msr - slow) > SteadyC) return;
        var seen = Math.Clamp(msr - ec, -MaxOffsetC, MaxOffsetC);
        _offset = _offsetLearnt ? _offset + 0.3 * (seen - _offset) : seen;
        _offsetLearnt = true;
    }

    private void Check(DateTime now)
    {
        if (_ec is not { } ec || _msrSlow is not { } slow) return;
        if (Math.Abs(ec - (slow - _offset)) > DisagreeC)
        {
            _disagreeSince ??= now;
            if (now - _disagreeSince >= DisagreeFor) RegisterSetAside = true;
        }
        else
        {
            _disagreeSince = null;
            RegisterSetAside = false;
        }
    }
}
