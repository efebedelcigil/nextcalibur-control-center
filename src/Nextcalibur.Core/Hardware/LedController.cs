namespace Nextcalibur.Core.Hardware;

/// <summary>Known LED device selectors (field <c>a2</c>).</summary>
public static class LedDevice
{
    /// <summary>Every LED device on the machine.</summary>
    public const uint All = 0;

    /// <summary>Every keyboard LED device.</summary>
    public const uint AllKeyboard = 6;
}

/// <summary>
/// A packed 32-bit LED value.
///
/// The low 24 bits are RGB. The high byte carries mode and brightness, but its
/// exact encoding has not been confirmed, so this type never fabricates one — it
/// only ever carries a high byte that came back from firmware.
/// </summary>
public readonly record struct LedValue(uint Raw)
{
    public byte Red => (byte)((Raw >> 16) & 0xFF);
    public byte Green => (byte)((Raw >> 8) & 0xFF);
    public byte Blue => (byte)(Raw & 0xFF);

    /// <summary>The mode/brightness byte. Encoding not yet confirmed.</summary>
    public byte Mode => (byte)((Raw >> 24) & 0xFF);

    /// <summary>
    /// Returns this value with a new colour, preserving the high byte exactly as
    /// firmware reported it.
    /// </summary>
    public LedValue WithColour(byte r, byte g, byte b) =>
        new((Raw & 0xFF000000u) | ((uint)r << 16) | ((uint)g << 8) | b);

    public override string ToString() => $"0x{Raw:X8} (mode 0x{Mode:X2}, #{Red:X2}{Green:X2}{Blue:X2})";
}

/// <summary>
/// Keyboard and zone lighting through the firmware mailbox
/// (<c>a1 = 0x0100</c>). See docs/PROTOCOL.md §4.
///
/// Writes are deliberately read-modify-write: the current value is read back
/// from firmware and only its colour bits are replaced. The mode/brightness byte
/// is passed through untouched, so this class cannot invent a value the hardware
/// has not already accepted.
/// </summary>
public sealed class LedController(EcMailbox mailbox)
{
    private readonly EcMailbox _mailbox = mailbox ?? throw new ArgumentNullException(nameof(mailbox));

    /// <summary>
    /// Reads the current LED value. Returns null when firmware answers but
    /// reports nothing usable, which is how machines without addressable
    /// lighting behave.
    /// </summary>
    public LedValue? TryReadState()
    {
        try
        {
            var response = _mailbox.Execute(
                SmiCommand.For(SmiFamily.Read, SmiSubsystem.Led),
                // The header echo is the only guarantee firmware gives here; any
                // payload including zero is a legitimate LED state.
                _ => true);

            return new LedValue(response.A3);
        }
        catch (EcMailboxUnavailableException)
        {
            return null;
        }
    }

    /// <summary>
    /// Sets the colour of one LED device, preserving its current mode byte.
    /// </summary>
    /// <param name="device">
    /// A selector from <see cref="LedDevice"/>, or a zone index. Zone index
    /// mapping is not yet confirmed — see docs/PROTOCOL.md §4.
    /// </param>
    /// <param name="r">Red.</param>
    /// <param name="g">Green.</param>
    /// <param name="b">Blue.</param>
    public void SetColour(uint device, byte r, byte g, byte b)
    {
        var current = TryReadState()
            ?? throw new EcMailboxUnavailableException(
                "Could not read the current LED state, so there is no mode byte to preserve. " +
                "Refusing to write a fabricated value.");

        Write(device, current.WithColour(r, g, b));
    }

    /// <summary>
    /// Writes a packed value verbatim. Only pass values that originated from
    /// <see cref="TryReadState"/>, optionally recoloured.
    /// </summary>
    public void Write(uint device, LedValue value)
    {
        var command = SmiCommand.For(SmiFamily.Write, SmiSubsystem.Led);
        command.A2 = device;
        command.A3 = value.Raw;
        _mailbox.Write(command);
    }
}
