namespace Nextcalibur.Core.Hardware;

/// <summary>
/// The fans' full-speed mode - what Fn+1 toggles on the laptop's own
/// keyboard - asked for through the firmware mailbox.
///
/// Found in the BIOS's own ACPI code on 11 October 2026 (PROTOCOL.md): the
/// mailbox handler answers <c>FB00/0x0200</c> with <c>a2 = 1</c> by setting
/// one of two request bits in the embedded controller, FAND when <c>a6</c>
/// bit 0 is set and FANE when it is clear. The controller keeps the state
/// itself (its FANM bit), which no mailbox read returns; the firmware says
/// it in an event instead, <c>GMC_WMIEvent</c> with byte 1 = 3 and byte 3 =
/// the new state, both when Fn+1 is pressed and when software changes it.
///
/// Which of the two requests means "on" is not in their names. Until it is
/// measured on the machine - <c>nextcalibur fanmax</c> - <see cref="OnA6"/>
/// is null and nothing here writes.
/// </summary>
public static class FanTurbo
{
    /// <summary>The <c>a6</c> that turns the turbo on; the other value turns it off. Null until measured.</summary>
    public static readonly uint? OnA6 = null;

    /// <summary>Whether the direction is known, and so whether anything here may write.</summary>
    public static bool Known => OnA6 is not null;

    /// <summary>Asks the controller for full-speed fans, or for its own curve back.</summary>
    /// <returns>False when the direction is not known yet; nothing was sent.</returns>
    public static bool Set(EcMailbox mailbox, bool on)
    {
        ArgumentNullException.ThrowIfNull(mailbox);
        if (OnA6 is not { } onValue) return false;

        using (mailbox.Hold())
        {
            mailbox.Write(new SmiCommand
            {
                A0 = (ushort)SmiFamily.Write,
                A1 = (ushort)SmiSubsystem.Thermal,
                A2 = 1,
                A6 = on ? onValue : onValue ^ 1,
            });
            Thread.Sleep(60);
        }
        return true;
    }

    /// <summary>
    /// Reads a firmware event: true or false when it says the turbo is now on
    /// or off, null when the event is about something else.
    /// </summary>
    public static bool? FromEvent(byte[]? detail) =>
        detail is { Length: >= 4 } && detail[1] == 3 ? detail[3] != 0 : null;
}
