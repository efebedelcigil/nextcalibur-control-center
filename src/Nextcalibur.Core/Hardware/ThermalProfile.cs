using Nextcalibur.Core.Power;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// The firmware's side of a system mode: register <c>0x0300</c>.
///
/// The vendor's mode is two things, and only one of them is the Windows
/// power plan. The other is this register, written with the mode and read
/// back with it - found 11 September 2026 by sweeping the registers while
/// each mode was chosen in the vendor's software: Performance 0, Gaming 1,
/// Office 2, and the fans stepped up on 0. So it is the embedded controller's
/// fan and thermal profile, and a mode that sets only the plan is half a mode.
///
/// This is the one fan-related write this project makes, and it is made
/// because the vendor makes it - "if it is in the vendor's, copy it exactly;
/// if it is not, do not touch it". No curve, no manual speed.
/// </summary>
public static class ThermalProfile
{
    /// <summary>The register's value for a mode, as measured.</summary>
    public static uint ValueFor(SystemMode mode) => mode switch
    {
        SystemMode.Performance => 0,
        SystemMode.Gaming => 1,
        SystemMode.Office => 2,
        _ => throw new ArgumentOutOfRangeException(nameof(mode)),
    };

    /// <summary>The mode a register value means, or null for one never seen.</summary>
    public static SystemMode? ModeFor(uint value) => value switch
    {
        0 => SystemMode.Performance,
        1 => SystemMode.Gaming,
        2 => SystemMode.Office,
        _ => null,
    };

    /// <summary>Tells the firmware the mode. Read back to confirm.</summary>
    /// <returns>False when the register did not take the value.</returns>
    public static bool Write(EcMailbox mailbox, SystemMode mode)
    {
        ArgumentNullException.ThrowIfNull(mailbox);

        using (mailbox.Hold())
        {
            var command = SmiCommand.For(SmiFamily.Write, SmiSubsystem.Profile);
            command.A2 = ValueFor(mode);
            mailbox.Write(command);
            Thread.Sleep(60);
        }

        return Read(mailbox) == mode;
    }

    /// <summary>What the firmware says the mode is.</summary>
    public static SystemMode? Read(EcMailbox mailbox)
    {
        ArgumentNullException.ThrowIfNull(mailbox);

        using (mailbox.Hold())
        {
            var command = SmiCommand.For(SmiFamily.Read, SmiSubsystem.Profile);
            mailbox.Write(command);
            Thread.Sleep(50);
            var reply = SmiCommand.FromBytes(mailbox.ReadRaw());
            if (reply.A0 != command.A0 || reply.A1 != command.A1) return null;
            return ModeFor(reply.A2);
        }
    }
}
