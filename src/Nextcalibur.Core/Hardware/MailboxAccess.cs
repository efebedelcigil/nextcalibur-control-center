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

    /// <summary>
    /// The firmware's event class, <c>GMC_WMIEvent</c>. Administrators only by
    /// default like the block, and the only way to hear Fn+Space: the key
    /// changes the backlight level in firmware, nothing reads that level back,
    /// so the event is what keeps this application from stamping on it.
    /// </summary>
    public const string EventGuid = "74286d6e-429c-427a-b34b-b5d15d032b05";

    private const string SecurityKey = @"SYSTEM\CurrentControlSet\Control\WMI\Security";

    /// <summary>
    /// WMIGUID_QUERY | WMIGUID_SET | WMIGUID_NOTIFICATION |
    /// WMIGUID_READ_DESCRIPTION | WMIGUID_EXECUTE, with READ_CONTROL and
    /// SYNCHRONIZE. SET is not optional: writing is how the keyboard is lit.
    /// </summary>
    private const string AccessMask = "0x12001f";

    /// <summary>The same mask as a number, for comparing entries already present.</summary>
    private const uint AccessMaskValue = 0x12001f;

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
    /// Adds the current account to the block's security descriptor, leaving
    /// everything already in it alone.
    ///
    /// **Adds an entry; does not write a descriptor.** An earlier version
    /// replaced whatever was there with one of its own, which on a machine
    /// running the vendor's Control Center would have taken away the access
    /// that software grants and broken it - the exact discourtesy this project
    /// complains about elsewhere.
    ///
    /// The entry it adds is deliberately narrower than the vendor's, which
    /// grants every authenticated user. The block accepts writes as well as
    /// reads, so widening it that far hands every local process a route to the
    /// embedded controller. One account is enough for a laptop.
    /// </summary>
    /// <returns>False when the account already had access and nothing was written.</returns>
    /// <exception cref="UnauthorizedAccessException">Not running elevated.</exception>
    public static bool Grant()
    {
        // Cheapest check first, and the only one an ordinary account can make:
        // if the block is already readable from here, the permission is there
        // and nothing needs writing - which also means no elevation prompt.
        //
        // Only meaningful unelevated. An administrator can reach the block
        // whatever the descriptor says, so "it works for me" from an elevated
        // process proves nothing about the account's own permission; that case
        // falls through and compares the entries properly below.
        if (!IsElevated() && Check() == MailboxAvailability.Available && CanHearEvents()) return false;

        // Both, not either: the event grant is worthless without the block and
        // the block alone leaves Fn+Space unheard.
        var block = Grant(BlockGuid);
        var events = Grant(EventGuid);
        return block || events;
    }

    /// <summary>
    /// Whether this account may subscribe to the firmware's event class.
    /// Answered by trying, since there is no cheaper honest way.
    /// </summary>
    public static bool CanHearEvents()
    {
        try
        {
            using var watcher = new System.Management.ManagementEventWatcher(
                new System.Management.ManagementScope(@"root\WMI"),
                new System.Management.EventQuery("SELECT * FROM GMC_WMIEvent"));
            watcher.Start();
            watcher.Stop();
            return true;
        }
        catch (System.Management.ManagementException)
        {
            return false;
        }
        catch (UnauthorizedAccessException)
        {
            return false;
        }
    }

    private static bool Grant(string guid)
    {
        var sid = WindowsIdentity.GetCurrent().User
            ?? throw new InvalidOperationException("Could not determine the current account.");

        using var key = Registry.LocalMachine.OpenSubKey(SecurityKey, writable: true)
            ?? throw new InvalidOperationException($@"Missing HKLM\{SecurityKey}.");

        var existing = key.GetValue(guid) as byte[];

        // A descriptor with no entries of its own means Windows' default, which
        // is administrators only. Start from that rather than from nothing, so
        // an elevated process keeps working either way.
        var descriptor = existing is not null
            ? new RawSecurityDescriptor(existing, 0)
            : new RawSecurityDescriptor($"O:BAG:BAD:(A;;{AccessMask};;;BA)(A;;{AccessMask};;;SY)");

        if (HasAccess(descriptor, sid)) return false;

        // Keep the original so Revoke has something to compare against rather
        // than guess at. Only the first time: a second grant must not overwrite
        // the record of what was there before the first.
        if (existing is not null && key.GetValue(BackupValue(guid)) is null)
            key.SetValue(BackupValue(guid), existing, RegistryValueKind.Binary);

        descriptor.DiscretionaryAcl ??= new RawAcl(GenericAcl.AclRevision, 1);
        descriptor.DiscretionaryAcl.InsertAce(
            descriptor.DiscretionaryAcl.Count,
            new CommonAce(AceFlags.None, AceQualifier.AccessAllowed,
                unchecked((int)AccessMaskValue), sid, isCallback: false, opaque: null));

        Write(key, guid, descriptor);
        return true;
    }

    /// <summary>
    /// Removes this account's entry and leaves everyone else's alone.
    ///
    /// Not "restore the backup": on a machine where the vendor's software has
    /// been installed since, restoring would delete its access as well. The
    /// only thing this ever takes away is what it put there.
    ///
    /// When that leaves a descriptor holding nothing this machine had before,
    /// the value goes entirely, so an uninstall leaves no trace - which is the
    /// half the vendor's uninstaller does not do.
    /// </summary>
    /// <exception cref="UnauthorizedAccessException">Not running elevated.</exception>
    /// <summary>
    /// Whether an earlier version widened the interface's permission to this
    /// account by name. The elevated application does not need it, and a
    /// permission that lets any process running as the account send
    /// firmware commands is one the machine is better without; the caller
    /// takes it back with <see cref="Revoke()"/>.
    /// </summary>
    public static bool WidenedForCurrentAccount()
    {
        var sid = WindowsIdentity.GetCurrent().User;
        if (sid is null) return false;
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(SecurityKey);
            if (key is null) return false;
            foreach (var guid in new[] { BlockGuid, EventGuid })
            {
                if (key.GetValue(guid) is not byte[] bytes) continue;
                var acl = new RawSecurityDescriptor(bytes, 0).DiscretionaryAcl;
                if (acl is null) continue;
                foreach (var entry in acl)
                    if (entry is CommonAce { AceType: AceType.AccessAllowed } ace && ace.SecurityIdentifier == sid)
                        return true;
            }
        }
        catch (Exception ex) when (ex is System.Security.SecurityException or UnauthorizedAccessException or IOException)
        {
        }
        return false;
    }

    public static void Revoke()
    {
        Revoke(BlockGuid);
        Revoke(EventGuid);
    }

    private static void Revoke(string guid)
    {
        var sid = WindowsIdentity.GetCurrent().User;

        using var key = Registry.LocalMachine.OpenSubKey(SecurityKey, writable: true)
            ?? throw new InvalidOperationException($@"Missing HKLM\{SecurityKey}.");

        if (key.GetValue(guid) is not byte[] current || sid is null)
        {
            key.DeleteValue(BackupValue(guid), throwOnMissingValue: false);
            return;
        }

        var descriptor = new RawSecurityDescriptor(current, 0);
        var acl = descriptor.DiscretionaryAcl;
        if (acl is not null)
        {
            for (var i = acl.Count - 1; i >= 0; i--)
                if (acl[i] is CommonAce ace && ace.SecurityIdentifier == sid)
                    acl.RemoveAce(i);
        }

        var backup = key.GetValue(BackupValue(guid)) as byte[];
        if (backup is null && (acl is null || acl.Count == 0))
        {
            // Nothing was here before us and nothing is left: take the value
            // away rather than leave an empty one behind.
            key.DeleteValue(guid, throwOnMissingValue: false);
        }
        else
        {
            Write(key, guid, descriptor);
        }

        key.DeleteValue(BackupValue(guid), throwOnMissingValue: false);
    }

    /// <summary>
    /// True when this account already has what a grant would give it - as an
    /// ordinary account, not as an administrator.
    ///
    /// The distinction is the whole point. This runs elevated, where the token
    /// carries the administrators group, so counting group entries naively made
    /// the descriptor's built-in `BA` entry look like the account was already
    /// allowed. It reported "already allowed", wrote nothing, and left every
    /// reading broken the moment the window ran without elevation - found by
    /// revoking access and watching the re-grant refuse to do anything.
    /// </summary>
    internal static bool HasAccess(RawSecurityDescriptor descriptor, SecurityIdentifier sid)
    {
        var acl = descriptor.DiscretionaryAcl;
        if (acl is null) return false;

        var groups = WindowsIdentity.GetCurrent().Groups;

        foreach (var entry in acl)
        {
            if (entry is not CommonAce ace || ace.AceType != AceType.AccessAllowed) continue;

            // Entries that only apply while elevated say nothing about what the
            // account can do the rest of the time.
            if (OnlyHelpsWhenElevated(ace.SecurityIdentifier)) continue;

            // Other group entries do count: the vendor grants Authenticated
            // Users, and an account covered by that needs nothing of its own.
            var covers = ace.SecurityIdentifier == sid
                || (groups?.Contains(ace.SecurityIdentifier) ?? false);

            if (covers && (uint)ace.AccessMask >= AccessMaskValue) return true;
        }

        return false;
    }

    private static bool OnlyHelpsWhenElevated(SecurityIdentifier sid) =>
        sid.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid)
        || sid.IsWellKnown(WellKnownSidType.LocalSystemSid);

    private static void Write(RegistryKey key, string guid, RawSecurityDescriptor descriptor)
    {
        var bytes = new byte[descriptor.BinaryLength];
        descriptor.GetBinaryForm(bytes, 0);
        key.SetValue(guid, bytes, RegistryValueKind.Binary);
    }

    /// <summary>Where the pre-existing descriptor is parked during a grant.</summary>
    private static string BackupValue(string guid) => guid + "-nextcalibur-previous";

    /// <summary>True when this process could grant access without another prompt.</summary>
    public static bool IsElevated()
    {
        using var identity = WindowsIdentity.GetCurrent();
        return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
    }
}
