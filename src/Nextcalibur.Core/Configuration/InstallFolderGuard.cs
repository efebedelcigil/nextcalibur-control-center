using System.Security.AccessControl;
using System.Security.Principal;

namespace Nextcalibur.Core.Configuration;

/// <summary>
/// Keeps the installed files out of reach of anything running as the
/// account.
///
/// The application is started elevated, without a prompt, by a scheduled
/// task - from wherever it is installed. If that place can be written by
/// the account, anything running as the account can replace the executable
/// and be run as administrator at the next start: the textbook shape of a
/// UAC bypass. New installs go under Program Files, which only
/// administrators can write. A copy that lives in the profile from an
/// earlier version is hardened here instead: the folder stops inheriting
/// the profile's permissions and gets SYSTEM and Administrators full
/// control, Users read and execute. The elevated application still
/// updates itself there (its updater inherits the elevation); the account
/// cannot touch the files. The uninstall hands the folder back so the
/// uninstaller, which runs as the account, can delete it.
/// </summary>
public static class InstallFolderGuard
{
    /// <summary>The install root for a running copy: the folder above <c>current\</c>, or the folder itself.</summary>
    public static string? RootOf(string executablePath)
    {
        var folder = Path.GetDirectoryName(executablePath);
        if (folder is null) return null;
        return folder.EndsWith("current", StringComparison.OrdinalIgnoreCase)
            ? Path.GetDirectoryName(folder)
            : folder;
    }

    /// <summary>Whether the folder is under a Program Files tree, where Windows' own permissions already do the job.</summary>
    public static bool IsUnderProgramFiles(string root)
    {
        foreach (var special in new[] { Environment.SpecialFolder.ProgramFiles, Environment.SpecialFolder.ProgramFilesX86 })
        {
            var pf = Environment.GetFolderPath(special);
            if (pf.Length > 0 && root.StartsWith(pf.TrimEnd('\\') + "\\", StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

    /// <summary>Whether the folder already carries the protected permissions.</summary>
    public static bool IsHardened(string root)
    {
        try
        {
            var security = new DirectoryInfo(root).GetAccessControl();
            if (!security.AreAccessRulesProtected) return false;
            foreach (FileSystemAccessRule rule in security.GetAccessRules(true, false, typeof(SecurityIdentifier)))
                if (rule.IdentityReference == Administrators && rule.AccessControlType == AccessControlType.Allow
                    && (rule.FileSystemRights & FileSystemRights.FullControl) == FileSystemRights.FullControl)
                    return true;
            return false;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or IOException or System.Security.SecurityException)
        {
            return false;
        }
    }

    /// <summary>
    /// Protects the folder. Elevated only. Idempotent: a folder under
    /// Program Files or one already protected is left alone. Returns
    /// whether anything was changed.
    /// </summary>
    public static bool Harden(string root)
    {
        if (!Directory.Exists(root) || IsUnderProgramFiles(root) || IsHardened(root)) return false;

        var directory = new DirectoryInfo(root);
        var security = directory.GetAccessControl();

        // Stop inheriting the profile's rules (which give the account full
        // control) without copying them, then state the three that apply.
        security.SetAccessRuleProtection(isProtected: true, preserveInheritance: false);
        foreach (FileSystemAccessRule rule in security.GetAccessRules(true, false, typeof(SecurityIdentifier)))
            security.RemoveAccessRuleAll(rule);

        security.AddAccessRule(Rule(LocalSystem, FileSystemRights.FullControl));
        security.AddAccessRule(Rule(Administrators, FileSystemRights.FullControl));
        // Users, not this account by name: the account that starts the
        // application unelevated (from the Start menu) may not be the one the
        // elevated token belongs to, and it needs to read the executable.
        security.AddAccessRule(Rule(Users, FileSystemRights.ReadAndExecute | FileSystemRights.ListDirectory));
        // The owner of a file may always rewrite its permissions, and the
        // account owns what it installed; so the owner changes too, or the
        // permissions above are a suggestion.
        security.SetOwner(Administrators);
        directory.SetAccessControl(security);
        foreach (var child in Directory.EnumerateFileSystemEntries(root, "*", SearchOption.AllDirectories))
            TrySetOwner(child, Administrators);
        return true;
    }

    /// <summary>
    /// Hands the folder back to the account so an unelevated uninstaller can
    /// delete it: inheritance restored, and an explicit full-control entry
    /// for the account either way (Program Files does not inherit one).
    /// Elevated only.
    /// </summary>
    public static void Release(string root)
    {
        if (!Directory.Exists(root)) return;
        var user = WindowsIdentity.GetCurrent().User ?? throw new InvalidOperationException("Could not determine the current account.");
        var directory = new DirectoryInfo(root);
        var security = directory.GetAccessControl();
        security.SetAccessRuleProtection(isProtected: false, preserveInheritance: true);
        security.AddAccessRule(Rule(user, FileSystemRights.FullControl));
        directory.SetAccessControl(security);
    }

    /// <summary>
    /// Takes back what <see cref="Release"/> gave, on a copy under Program
    /// Files: the account's explicit full control on the root. Elevated only.
    ///
    /// Release runs partway through an uninstall. An uninstall that stopped
    /// after it - cancelled, killed, a file in use - left the account able to
    /// replace the executable, and the next start registered the no-prompt
    /// task again, because that decision looks at where the copy is, not at
    /// who can write there. So this runs before the task is registered.
    /// </summary>
    public static bool Reclaim(string root)
    {
        if (!Directory.Exists(root) || !IsUnderProgramFiles(root)) return false;
        var user = WindowsIdentity.GetCurrent().User;
        if (user is null) return false;

        var directory = new DirectoryInfo(root);
        var security = directory.GetAccessControl();
        var changed = false;
        foreach (FileSystemAccessRule rule in security.GetAccessRules(true, false, typeof(SecurityIdentifier)))
        {
            if (rule.AccessControlType != AccessControlType.Allow || rule.IdentityReference != user) continue;
            security.RemoveAccessRuleSpecific(rule);
            changed = true;
        }
        if (changed) directory.SetAccessControl(security);
        return changed;
    }

    /// <summary>
    /// Whether anyone but SYSTEM, Administrators and TrustedInstaller may
    /// change this file or folder - its content, its entries, its
    /// permissions or its owner - by an entry that applies to it.
    /// </summary>
    public static bool WritableByOthers(string path)
    {
        try
        {
            FileSystemSecurity security = Directory.Exists(path)
                ? new DirectoryInfo(path).GetAccessControl()
                : new FileInfo(path).GetAccessControl();
            foreach (FileSystemAccessRule rule in security.GetAccessRules(true, true, typeof(SecurityIdentifier)))
                if (GrantsWriteToOthers(rule)) return true;
            if (security.GetOwner(typeof(SecurityIdentifier)) is SecurityIdentifier owner && !IsTrusted(owner))
                return true;   // an owner may always rewrite the permissions
            return false;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or IOException or System.Security.SecurityException)
        {
            return false;
        }
    }

    /// <summary>
    /// Removes the explicit entries that let anyone else write, and gives an
    /// untrusted owner's place to Administrators. Elevated only. Inherited
    /// entries come from the folder above and are that folder's business.
    /// </summary>
    public static void RemoveOthersWrite(string path)
    {
        try
        {
            if (Directory.Exists(path))
            {
                var info = new DirectoryInfo(path);
                var security = info.GetAccessControl();
                if (Strip(security)) info.SetAccessControl(security);
            }
            else if (File.Exists(path))
            {
                var info = new FileInfo(path);
                var security = info.GetAccessControl();
                if (Strip(security)) info.SetAccessControl(security);
            }
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or IOException or InvalidOperationException or System.Security.SecurityException)
        {
            Configuration.Log.Warn("integrity", $"Could not take back write access on {path}: {ex.Message}");
        }

        static bool Strip(FileSystemSecurity security)
        {
            var changed = false;
            foreach (FileSystemAccessRule rule in security.GetAccessRules(true, false, typeof(SecurityIdentifier)))
            {
                if (!GrantsWriteToOthers(rule)) continue;
                security.RemoveAccessRuleSpecific(rule);
                changed = true;
            }
            if (security.GetOwner(typeof(SecurityIdentifier)) is SecurityIdentifier owner && !IsTrusted(owner))
            {
                security.SetOwner(Administrators);
                changed = true;
            }
            return changed;
        }
    }

    private const FileSystemRights WriteRights =
        FileSystemRights.WriteData | FileSystemRights.AppendData | FileSystemRights.WriteExtendedAttributes
        | FileSystemRights.WriteAttributes | FileSystemRights.Delete | FileSystemRights.DeleteSubdirectoriesAndFiles
        | FileSystemRights.ChangePermissions | FileSystemRights.TakeOwnership;

    /// <summary>GENERIC_WRITE and GENERIC_ALL, which the enum does not name.</summary>
    private const int GenericWriteOrAll = 0x40000000 | 0x10000000;

    private static bool GrantsWriteToOthers(FileSystemAccessRule rule) =>
        rule.AccessControlType == AccessControlType.Allow
        && (rule.PropagationFlags & PropagationFlags.InheritOnly) == 0
        && rule.IdentityReference is SecurityIdentifier sid && !IsTrusted(sid)
        && (((int)rule.FileSystemRights & (int)WriteRights) != 0 || ((int)rule.FileSystemRights & GenericWriteOrAll) != 0);

    private static bool IsTrusted(SecurityIdentifier sid) =>
        sid == LocalSystem || sid == Administrators || sid.Value == TrustedInstaller;

    /// <summary>NT SERVICE\TrustedInstaller, which owns Windows' own folders and some of Program Files.</summary>
    private const string TrustedInstaller = "S-1-5-80-956008885-3418522649-1831038044-1853292631-2271478464";

    private static void TrySetOwner(string path, SecurityIdentifier owner)
    {
        try
        {
            if (Directory.Exists(path))
            {
                var info = new DirectoryInfo(path);
                var security = info.GetAccessControl(AccessControlSections.Owner);
                security.SetOwner(owner);
                info.SetAccessControl(security);
            }
            else
            {
                var info = new FileInfo(path);
                var security = info.GetAccessControl(AccessControlSections.Owner);
                security.SetOwner(owner);
                info.SetAccessControl(security);
            }
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or IOException or System.Security.SecurityException or InvalidOperationException)
        {
        }
    }

    private static FileSystemAccessRule Rule(SecurityIdentifier who, FileSystemRights rights) =>
        new(who, rights,
            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
            PropagationFlags.None, AccessControlType.Allow);

    private static readonly SecurityIdentifier LocalSystem = new(WellKnownSidType.LocalSystemSid, null);
    private static readonly SecurityIdentifier Administrators = new(WellKnownSidType.BuiltinAdministratorsSid, null);
    private static readonly SecurityIdentifier Users = new(WellKnownSidType.BuiltinUsersSid, null);
}
