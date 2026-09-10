using System.Management;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;

namespace Nextcalibur.Core.Hardware;

/// <summary>Why the firmware mailbox is or is not readable.</summary>
public enum MailboxAvailability
{
    /// <summary>Readable now.</summary>
    Available,

    /// <summary>
    /// The machine has the interface, but this account is not allowed to use
    /// it. Fixable, once, with administrator.
    /// </summary>
    AccessNotGranted,

    /// <summary>This machine does not expose the interface at all.</summary>
    NotSupported,
}

/// <summary>
/// Decides whether this account can reach the firmware mailbox, and grants
/// itself access when it cannot.
///
/// Everything Nextcalibur reads from the machine comes through one ACPI data
/// block. The block is declared by the firmware - its GUID sits in the DSDT,
/// inside a `_WDG` block - and Windows' own `wmiacpi.sys` surfaces it. No
/// driver is needed, and none is installed.
///
/// What *is* needed is permission. A kernel-WMI data block carries a security
/// descriptor, and this one grants the administrators group alone. The vendor's
/// Control Center widens it at install time, which is why this project ran for
/// weeks believing it needed no privileges: it was using a door somebody else
/// had propped open. Uninstalling that software closed it and every reading
/// stopped - measured on hardware, 10 September 2026.
///
/// So the shape is: **elevation once, ordinary use for ever after.**
/// </summary>
public static class MailboxAccess
{
    /// <summary>
    /// The data block, named the way the registry expects it: no braces, lower
    /// case. Every one of the hundreds of entries already under that key is
    /// written this way, and a value written any other way is never consulted -
    /// it sits in the registry looking correct while access stays refused.
    /// </summary>
    public const string BlockGuid = "644c5791-b7b0-4123-a90b-e93876e0daad";

    private const string SecurityKey = @"SYSTEM\CurrentControlSet\Control\WMI\Security";

    /// <summary>
    /// WMIGUID_QUERY | WMIGUID_SET | WMIGUID_NOTIFICATION |
    /// WMIGUID_READ_DESCRIPTION | WMIGUID_EXECUTE, with READ_CONTROL and
    /// SYNCHRONIZE. SET is not optional: writing is how the keyboard is lit.
    /// </summary>
    private const string AccessMask = "0x12001f";

    /// <summary>Whether the mailbox can be reached, and if not, why not.</summary>
    public static MailboxAvailability Check()
    {
        if (EcMailbox.IsSupported()) return MailboxAvailability.Available;

        // The class definition and the instances come from different places. The
        // definition is compiled from the binary MOF the firmware itself carries,
        // so it is present on any machine with this interface, whether or not the
        // caller may use it. The instances come through the security descriptor.
        // A class with no instances is therefore the signature of a permission
        // problem rather than of the wrong laptop.
        return ClassIsDefined() ? MailboxAvailability.AccessNotGranted : MailboxAvailability.NotSupported;
    }

    private static bool ClassIsDefined()
    {
        try
        {
            using var searcher = new ManagementObjectSearcher(
                new ManagementScope(@"root\wmi"),
                new ObjectQuery("SELECT * FROM meta_class WHERE __CLASS = 'RW_GMWMI'"));
            using var results = searcher.Get();
            foreach (var definition in results)
            {
                definition.Dispose();
                return true;
            }
        }
        catch
        {
            // Being unable to ask is not evidence either way; treat it as absent
            // and let the caller report the honest, less alarming answer.
        }
        return false;
    }

    /// <summary>
    /// Adds the current account to the block's security descriptor.
    ///
    /// Deliberately narrower than what the vendor software does, which grants
    /// the whole built-in Users group. The block accepts writes as well as
    /// reads, so widening it that far hands every local process a route to the
    /// embedded controller. One account is enough for a laptop.
    /// </summary>
    /// <exception cref="UnauthorizedAccessException">Not running elevated.</exception>
    public static void Grant()
    {
        var sid = WindowsIdentity.GetCurrent().User?.Value
            ?? throw new InvalidOperationException("Could not determine the current account.");

        using var key = Registry.LocalMachine.OpenSubKey(SecurityKey, writable: true)
            ?? throw new InvalidOperationException($@"Missing HKLM\{SecurityKey}.");

        // Keep whatever is there so Revoke can put it back rather than guess.
        if (key.GetValue(BlockGuid) is byte[] existing && key.GetValue(BackupValue) is null)
            key.SetValue(BackupValue, existing, RegistryValueKind.Binary);

        var descriptor = new RawSecurityDescriptor(
            $"O:BAG:BAD:(A;;{AccessMask};;;BA)(A;;{AccessMask};;;SY)(A;;{AccessMask};;;{sid})");

        var bytes = new byte[descriptor.BinaryLength];
        descriptor.GetBinaryForm(bytes, 0);
        key.SetValue(BlockGuid, bytes, RegistryValueKind.Binary);
    }

    /// <summary>
    /// Puts the descriptor back exactly as it was found.
    ///
    /// Not the same as deleting the value: this machine had a descriptor before,
    /// granting administrators only, and deleting would leave none at all. Undo
    /// should mean undo.
    /// </summary>
    /// <exception cref="UnauthorizedAccessException">Not running elevated.</exception>
    public static void Revoke()
    {
        using var key = Registry.LocalMachine.OpenSubKey(SecurityKey, writable: true)
            ?? throw new InvalidOperationException($@"Missing HKLM\{SecurityKey}.");

        if (key.GetValue(BackupValue) is byte[] previous)
        {
            key.SetValue(BlockGuid, previous, RegistryValueKind.Binary);
            key.DeleteValue(BackupValue, throwOnMissingValue: false);
        }
        else
        {
            key.DeleteValue(BlockGuid, throwOnMissingValue: false);
        }
    }

    /// <summary>Where the pre-existing descriptor is parked during a grant.</summary>
    private const string BackupValue = BlockGuid + "-nextcalibur-previous";

    /// <summary>True when this process could grant access without another prompt.</summary>
    public static bool IsElevated()
    {
        using var identity = WindowsIdentity.GetCurrent();
        return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
    }
}
