using Nextcalibur.Core.Configuration;

namespace Nextcalibur.Core.Hardware;

/// <summary>Addressable lighting devices. See docs/PROTOCOL.md §4.</summary>
public enum LedZone : uint
{
    /// <summary>Broadcast to every lighting device.</summary>
    Everything = 0,

    /// <summary>Left of the keyboard. The vendor UI calls this Zone A.</summary>
    Left = 5,

    /// <summary>Middle of the keyboard. The vendor UI calls this Zone B.</summary>
    Middle = 4,

    /// <summary>Right of the keyboard. The vendor UI calls this Zone C.</summary>
    Right = 3,

    /// <summary>Broadcast to all three keyboard zones at once.</summary>
    AllKeyboard = 6,
}

/// <summary>
/// Lighting effects. Values 2, 3, 4, 6 and 7 animate in firmware and need no
/// further writes.
/// </summary>
public enum LedEffect : byte
{
    Off = 0,
    Static = 1,

    /// <summary>Square-wave on/off at roughly 1 Hz.</summary>
    Blink = 2,

    Breathing = 3,

    /// <summary>Breathing with a double pulse at the peak.</summary>
    Heartbeat = 4,

    ColourCycle = 6,

    /// <summary>Colour travels across the keyboard. The vendor UI calls this Ambilight.</summary>
    Wave = 7,
}

/// <summary>Global brightness. The hardware offers three levels, not a range.</summary>
public enum LedBrightness : byte
{
    Off = 0,
    Half = 1,
    Full = 2,
}

/// <summary>
/// Keyboard lighting through the firmware mailbox (<c>a1 = 0x0100</c>).
///
/// Two things about this hardware shape the design:
///
/// Firmware cannot report the current lighting — LED reads echo the header and
/// return zero for every payload field — so this class tracks what it last
/// wrote and persists it.
///
/// A write carries colour, effect and brightness together, but their scopes
/// differ: the colour applies only to the addressed zone while effect and
/// brightness apply to the whole keyboard. Recolouring one zone therefore has to
/// re-send the current global effect and brightness, or it would silently reset
/// them.
/// </summary>
public sealed class LedController(EcMailbox mailbox, LedState? state = null)
{
    private readonly EcMailbox _mailbox = mailbox ?? throw new ArgumentNullException(nameof(mailbox));

    /// <summary>The lighting state as last written by this application.</summary>
    public LedState State { get; } = state ?? LedState.Load();

    /// <summary>Sets one zone's colour, preserving the current effect and brightness.</summary>
    public void SetColour(LedZone zone, byte r, byte g, byte b)
    {
        State.SetColour(zone, r, g, b);
        Send(zone, r, g, b);
        State.Save();
    }

    /// <summary>Sets the effect for the whole keyboard.</summary>
    public void SetEffect(LedEffect effect)
    {
        State.Effect = effect;
        ReapplyAllZones();
        State.Save();
    }

    /// <summary>Brightness moves in ten-point steps, giving eleven levels.</summary>
    public const int BrightnessStep = 10;

    /// <summary>
    /// Sets the perceived brightness for the whole keyboard.
    ///
    /// The value is rounded to the nearest ten. Testing on the hardware showed
    /// every one of the eleven resulting levels to be tell-apart-able, while
    /// finer steps are not — so this is the honest resolution of the control
    /// rather than an arbitrary limit imposed on the UI alone.
    /// </summary>
    public void SetBrightness(int percent)
    {
        var clamped = Math.Clamp(percent, 0, 100);
        State.BrightnessPercent = (int)Math.Round(clamped / (double)BrightnessStep) * BrightnessStep;
        ReapplyAllZones();
        State.Save();
    }

    /// <summary>Turns the lighting off without forgetting the colours.</summary>
    public void TurnOff() => Send(LedZone.AllKeyboard, 0, 0, 0, LedEffect.Off);

    /// <summary>Switches the lighting on or off, remembering the choice.</summary>
    public void SetEnabled(bool enabled)
    {
        State.Enabled = enabled;
        if (enabled) ReapplyAllZones(); else TurnOff();
        State.Save();
    }

    /// <summary>
    /// Switches to a saved profile and applies it. Unknown names create an empty
    /// profile rather than failing, so a hand-edited settings file cannot brick
    /// the lighting page.
    /// </summary>
    public void SetProfile(string name)
    {
        State.ActiveProfile = name;
        ReapplyAllZones();
        State.Save();
    }

    /// <summary>Re-sends the stored state, for example after resume.</summary>
    public void Apply() => ReapplyAllZones();

    private void ReapplyAllZones()
    {
        if (!State.Enabled)
        {
            TurnOff();
            return;
        }

        if (State.BrightnessPercent == 0)
        {
            // Scaling to zero would send black, which on some effects still
            // drives the LEDs. Effect 0 is the real off switch.
            Send(LedZone.AllKeyboard, 0, 0, 0, LedEffect.Off);
            return;
        }

        // Each zone keeps its own colour, so a global change has to be written
        // once per zone rather than broadcast.
        foreach (var zone in new[] { LedZone.Left, LedZone.Middle, LedZone.Right })
        {
            var (r, g, b) = State.GetColour(zone);
            Send(zone, r, g, b);

            // The vendor software spaces these writes out; firmware drops some
            // of them when they arrive back to back.
            Thread.Sleep(30);
        }
    }

    private void Send(LedZone zone, byte r, byte g, byte b, LedEffect? effectOverride = null)
    {
        var effect = effectOverride ?? State.Effect;

        // The hardware brightness field stays at its top step; the useful range
        // comes from scaling the colour. Mixing both would make one control
        // silently limit the other.
        var mode = (byte)(((byte)effect << 4) | (byte)LedBrightness.Full);

        var command = SmiCommand.For(SmiFamily.Write, SmiSubsystem.Led);
        command.A2 = (uint)zone;
        command.A3 = ((uint)mode << 24)
                   | ((uint)Scale(r) << 16)
                   | ((uint)Scale(g) << 8)
                   | Scale(b);
        _mailbox.Write(command);
    }

    /// <summary>Applies the brightness percentage to one colour channel.</summary>
    private byte Scale(byte channel) =>
        (byte)Math.Clamp(channel * State.BrightnessPercent / 100, 0, 255);
}
