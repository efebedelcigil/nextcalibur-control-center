namespace Nextcalibur.Core.Hardware;

/// <summary>A single thermal/fan sample from firmware.</summary>
/// <param name="CpuTemperatureC">CPU package temperature, °C.</param>
/// <param name="GpuTemperatureC">GPU temperature, °C.</param>
/// <param name="CpuFanRpm">CPU fan speed, RPM.</param>
/// <param name="GpuFanRpm">GPU fan speed, RPM.</param>
/// <param name="Timestamp">When the sample was taken.</param>
/// <param name="BacklightLevel">
/// The keyboard backlight step the firmware is at: 0 off, 1 dim, 2 full.
/// Rides along in <c>a6</c> of the thermal block - found 11 September 2026
/// by sweeping the registers while Fn+Space was pressed. The one place the
/// key's effect can be read, so lighting writes can carry it from the first
/// sample rather than assume full until the key is next heard.
/// </param>
public readonly record struct ThermalSample(
    int CpuTemperatureC,
    int GpuTemperatureC,
    int CpuFanRpm,
    int GpuFanRpm,
    DateTimeOffset Timestamp,
    int BacklightLevel = 2);

/// <summary>
/// Reads temperatures and fan speeds through the firmware mailbox
/// (<c>a0=0xFA00, a1=0x0200</c>). See docs/PROTOCOL.md §3.
/// </summary>
public sealed class ThermalReader(EcMailbox mailbox)
{
    private readonly EcMailbox _mailbox = mailbox ?? throw new ArgumentNullException(nameof(mailbox));

    // Bounds used to reject torn reads. Firmware clears the buffer before
    // filling it, so a response caught mid-write shows zeroes in fields the
    // hardware never actually reports as zero while running.
    private const int MinPlausibleTempC = 1;
    private const int MaxPlausibleTempC = 125;
    private const int MaxPlausibleRpm = 12000;

    /// <summary>
    /// Takes one sample. Fan speeds of zero are accepted (fans genuinely stop),
    /// but a zero temperature is always a torn read.
    /// </summary>
    public ThermalSample Read()
    {
        // Retry less hard while the vendor software is running; hammering a
        // shared mailbox turns one collision into a stall for both of us.
        var response = _mailbox.Execute(
            SmiCommand.For(SmiFamily.Read, SmiSubsystem.Thermal),
            IsComplete,
            attempts: VendorSoftware.Attempts());

        return new ThermalSample(
            CpuTemperatureC: (int)response.A2,
            GpuTemperatureC: (int)response.A3,
            CpuFanRpm: (int)response.A4,
            GpuFanRpm: (int)response.A5,
            Timestamp: DateTimeOffset.Now,
            BacklightLevel: (int)response.A6);
    }

    /// <summary>Attempts a sample, returning false rather than throwing.</summary>
    public bool TryRead(out ThermalSample sample)
    {
        try
        {
            sample = Read();
            return true;
        }
        catch (EcMailboxUnavailableException)
        {
            sample = default;
            return false;
        }
    }

    private static bool IsComplete(SmiCommand r)
    {
        // Both temperatures must be populated and sane. A running machine never
        // reports 0 °C, so zero here means the payload was not written yet.
        if (r.A2 < MinPlausibleTempC || r.A2 > MaxPlausibleTempC) return false;
        if (r.A3 < MinPlausibleTempC || r.A3 > MaxPlausibleTempC) return false;

        // Fan speeds may legitimately be zero, but not absurd.
        if (r.A4 > MaxPlausibleRpm || r.A5 > MaxPlausibleRpm) return false;

        return true;
    }
}
