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
