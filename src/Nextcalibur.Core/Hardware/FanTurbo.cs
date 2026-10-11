namespace Nextcalibur.Core.Hardware;

/// <summary>
/// The fans' full-speed mode - what Fn+1 toggles on the laptop's own
/// keyboard - as far as software can know it: from the firmware's event.
///
/// Measured 11-12 October 2026 (PROTOCOL.md): the state is the controller's
/// own bit (FANM, EC 0xC3 bit 0). Nothing in the BIOS writes it - its
/// mailbox requests FANE and FAND were each sent and the fans did not move -
/// and the ports to the controller itself (0x62/0x66) are Windows' ACPI
/// driver's: a read there timed out every time, its answer taken by that
/// driver first. Racing it to write would be forcing it, so nothing here
/// writes. The event is reliable: <c>GMC_WMIEvent</c>, byte 1 = 3 and
/// byte 3 = the new state, on every Fn+1.
/// </summary>
public static class FanTurbo
{
    /// <summary>
    /// Reads a firmware event: true or false when it says the turbo is now on
    /// or off, null when the event is about something else.
    /// </summary>
    public static bool? FromEvent(byte[]? detail) =>
        detail is { Length: >= 4 } && detail[1] == 3 ? detail[3] != 0 : null;
}
