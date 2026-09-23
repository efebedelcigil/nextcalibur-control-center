using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.Json;

namespace Nextcalibur.Core.Security;

/// <summary>
/// Where the application keeps what it reads back: the settings, the
/// lighting, the log. Nothing outside the application can change them, and
/// a change that happens anyway is noticed and undone.
///
/// They used to live in %AppData%, which is the account's: anything running
/// as the account could rewrite them, and this process - elevated - turned
/// what they said into firmware commands and a driver's start type. So:
///
/// <list type="bullet">
/// <item>The folder is %ProgramData%\Nextcalibur\&lt;account SID&gt;, owned by
/// Administrators, not inheriting: SYSTEM and Administrators may write, the
/// account may only read (the log folder is opened for the person unelevated).
/// Its permissions are checked at every start and put back if they drifted.</item>
/// <item>Every file written here is recorded - its SHA-256 in a record beside
/// it, and a last-good copy. On load and on a timer the file is compared with
/// the record; one that differs was changed by something other than this
/// application, and the last-good copy takes its place.</item>
/// </list>
///
/// What this cannot do: stop an administrator. Something already running
/// with those rights can rewrite the record along with the file. The
/// folder's permissions are what keep everything else out; the record is
/// what notices the rest.
/// </summary>
public static class ProtectedStore
{
    private const string RecordName = "integrity.json";
    private const string GoodSuffix = ".good";

    private static readonly object Gate = new();

    /// <summary>What was last written or verified, by file name: the content and its hash.</summary>
    private static readonly Dictionary<string, (string Content, string Hash)> Known = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>A file was found changed by something other than this application. The file name.</summary>
    public static event Action<string>? Tampered;

    private static readonly List<string> Reported = new();

    /// <summary>
    /// Every name reported since the process started - including those found
    /// while the settings were first read, before anything could listen.
    /// </summary>
    public static IReadOnlyList<string> ReportedSoFar
    {
        get { lock (Gate) return Reported.ToList(); }
    }

    /// <summary>%ProgramData%\Nextcalibur - every account's folder is under it.</summary>
    public static string Root => _rootOverride ?? Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Nextcalibur");

    /// <summary>This account's folder.</summary>
    public static string Folder => Path.Combine(Root, WindowsIdentity.GetCurrent().User?.Value ?? "unknown");

    private static string? _rootOverride;
    private static bool _enforcePermissions = true;

    /// <summary>
    /// For the tests: a folder of their own. Permissions are enforced only
    /// when asked - an unelevated run cannot make the ownership changes, and
    /// the elevated test that checks them asks.
    /// </summary>
    internal static void UseForTests(string root, bool enforcePermissions = false)
    {
        lock (Gate)
        {
            _rootOverride = root;
            _enforcePermissions = enforcePermissions;
            Known.Clear();
            Reported.Clear();
        }
    }

    /// <summary>
    /// Makes the folders exist with the permissions described above, putting
    /// them back if anything changed them. Elevated; unelevated it only says
    /// whether the folder is there to read. False when it is not safe to use.
    /// </summary>
    public static bool EnsureFolder()
    {
        lock (Gate)
        {
            try
            {
                if (!_enforcePermissions)
                {
                    Directory.CreateDirectory(Folder);
                    return true;
                }

                if (!IsElevated()) return Directory.Exists(Folder) && !IsLink(Root) && !IsLink(Folder);

                var user = WindowsIdentity.GetCurrent().User;
                if (user is null) return false;

                Secure(Root, rootLevel: true, user);
                Secure(Folder, rootLevel: false, user);
                return true;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or InvalidOperationException or System.Security.SecurityException or ArgumentException)
            {
                Configuration.Log.Warn("store", "Could not secure the settings folder: " + ex.Message);
                return false;
            }
        }
    }

    /// <summary>
    /// The content of a file, as this application last wrote it. A file that
    /// differs from the record - edited, replaced, deleted - is reported, and
    /// the last-good copy is returned and put back. Null when there is nothing.
    /// </summary>
    public static string? Read(string name)
    {
        lock (Gate)
        {
            var path = Path.Combine(Folder, name);
            var recorded = RecordedHash(name);
            var content = TryRead(path);

            if (recorded is null)
            {
                // Nothing of ours was ever recorded under this name. A file
                // here anyway was not written by this application.
                if (content is not null)
                {
                    Report(name, "a file this application never wrote");
                    TryDelete(path);
                }
                return null;
            }

            if (content is not null && Hash(content) == recorded)
            {
                Known[name] = (content, recorded);
                return content;
            }

            Report(name, content is null ? "missing" : "changed");
            var good = TryRead(path + GoodSuffix);
            if (good is not null && Hash(good) == recorded)
            {
                Known[name] = (good, recorded);
                // Put back, where the rights allow it - unelevated they do not,
                // and the elevated copy that follows puts it back instead.
                try { WriteFile(path, good); }
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException) { }
                return good;
            }
            return null;
        }
    }

    /// <summary>
    /// Whether this application has ever written the file here. A file that
    /// is recorded and fails its check is not "absent": callers must not
    /// fall back to another source for it.
    /// </summary>
    public static bool IsRecorded(string name)
    {
        lock (Gate) return RecordedHash(name) is not null;
    }

    /// <summary>Writes a file and records it. False when the folder is not usable - unelevated, say.</summary>
    public static bool Write(string name, string content)
    {
        lock (Gate)
        {
            if (!EnsureFolder()) return false;
            try
            {
                var path = Path.Combine(Folder, name);
                var hash = Hash(content);
                WriteFile(path + GoodSuffix, content);
                WriteFile(path, content);
                var record = LoadRecord();
                record[name] = hash;
                WriteFile(Path.Combine(Folder, RecordName), JsonSerializer.Serialize(record));
                Known[name] = (content, hash);
                return true;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException)
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Compares every file this run has read or written with what it holds,
    /// and puts back any that changed. The permissions are checked on the way.
    /// Returns the names that had changed.
    /// </summary>
    public static IReadOnlyList<string> Verify()
    {
        var changed = new List<string>();
        lock (Gate)
        {
            EnsureFolder();
            foreach (var (name, known) in Known.ToList())
            {
                var path = Path.Combine(Folder, name);
                var content = TryRead(path);
                var recorded = RecordedHash(name);
                if (content is not null && Hash(content) == known.Hash && recorded == known.Hash) continue;

                changed.Add(name);
                Report(name, content is null ? "missing" : recorded != known.Hash ? "record changed" : "changed");
                try
                {
                    WriteFile(path + GoodSuffix, known.Content);
                    WriteFile(path, known.Content);
                    var record = LoadRecord();
                    record[name] = known.Hash;
                    WriteFile(Path.Combine(Folder, RecordName), JsonSerializer.Serialize(record));
                }
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException)
                {
                }
            }
        }
        return changed;
    }

    /// <summary>Deletes every account's folder. Elevated; part of the uninstall.</summary>
    public static void RemoveAll()
    {
        lock (Gate)
        {
            try
            {
                if (Directory.Exists(Root)) Directory.Delete(Root, recursive: true);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
            }
            Known.Clear();
        }
    }

    /// <summary>SHA-256 of the text as written, hex.</summary>
    internal static string Hash(string content) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(content)));

    // ------------------------------------------------------------------ internals

    private static void Report(string name, string how)
    {
        Configuration.Log.Warn("store", $"{name}: {how} outside the application; the last good copy stands");
        if (!Reported.Contains(name, StringComparer.OrdinalIgnoreCase)) Reported.Add(name);
        try { Tampered?.Invoke(name); }
        catch (Exception ex) when (ex is not OutOfMemoryException) { }
    }

    private static string? RecordedHash(string name) =>
        LoadRecord().TryGetValue(name, out var hash) ? hash : null;

    private static Dictionary<string, string> LoadRecord()
    {
        var text = TryRead(Path.Combine(Folder, RecordName));
        if (text is null) return new(StringComparer.OrdinalIgnoreCase);
        try
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(text) is { } record
                ? new Dictionary<string, string>(record, StringComparer.OrdinalIgnoreCase)
                : new(StringComparer.OrdinalIgnoreCase);
        }
        catch (JsonException)
        {
            return new(StringComparer.OrdinalIgnoreCase);
        }
    }

    private static string? TryRead(string path)
    {
        try
        {
            if (!File.Exists(path) || IsLink(path)) return null;
            return File.ReadAllText(path, Encoding.UTF8);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return null;
        }
    }

    /// <summary>Beside and moved into place: a write cut short leaves the previous file whole.</summary>
    private static void WriteFile(string path, string content)
    {
        if (IsLink(path)) TryDelete(path);
        var temporary = path + ".tmp";
        if (IsLink(temporary)) TryDelete(temporary);
        File.WriteAllText(temporary, content, new UTF8Encoding(false));
        File.Move(temporary, path, overwrite: true);
    }

    private static void TryDelete(string path)
    {
        try { File.Delete(path); }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { }
    }

    private static bool IsLink(string path)
    {
        try
        {
            return (Directory.Exists(path) || File.Exists(path))
                && (File.GetAttributes(path) & FileAttributes.ReparsePoint) != 0;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return false;
        }
    }

    private static bool IsElevated()
    {
        using var identity = WindowsIdentity.GetCurrent();
        return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
    }

    /// <summary>
    /// One folder, made safe: not a link (a folder somebody created first and
    /// pointed elsewhere is taken away and made again), owned by
    /// Administrators, not inheriting, and with exactly the entries below.
    /// Children inherit, and any explicit entry on a child is removed.
    /// </summary>
    private static void Secure(string folder, bool rootLevel, SecurityIdentifier user)
    {
        // %ProgramData% lets any account create a folder, so the name may
        // have been taken before this ran - as a link, or as a folder with
        // its creator's permissions. Neither is ours to keep.
        if (IsLink(folder)) Directory.Delete(folder);
        Directory.CreateDirectory(folder);
        if (IsLink(folder)) throw new InvalidOperationException("The settings folder is a link.");

        var directory = new DirectoryInfo(folder);
        var wanted = new DirectorySecurity();
        wanted.SetOwner(Administrators);
        wanted.SetAccessRuleProtection(isProtected: true, preserveInheritance: false);
        wanted.AddAccessRule(Inherited(LocalSystem, FileSystemRights.FullControl));
        wanted.AddAccessRule(Inherited(Administrators, FileSystemRights.FullControl));
        if (rootLevel)
        {
            // Through, not in: every account may pass the root to reach its
            // own folder, and none may see the others.
            wanted.AddAccessRule(new FileSystemAccessRule(AuthenticatedUsers,
                FileSystemRights.Traverse | FileSystemRights.ReadAttributes | FileSystemRights.Synchronize,
                InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow));
        }
        else
        {
            wanted.AddAccessRule(Inherited(user, FileSystemRights.ReadAndExecute));
        }

        var current = directory.GetAccessControl();
        var wasOurs = Same(current, wanted);
        if (!wasOurs)
        {
            Configuration.Log.Warn("store", $"Permissions on {folder} were not as set; put back");
            directory.SetAccessControl(wanted);
        }

        if (rootLevel) return;

        // Everything this application writes here is written elevated, and so
        // owned by Administrators. In a folder that was not yet secured -
        // the name having been taken first - anything owned by anyone else
        // was put there by someone else, and is deleted, not adopted: a
        // planted record with a planted file to match would otherwise pass
        // its own check.
        // A child that is ours but carries permissions of its own is put
        // back under the folder's.
        foreach (var child in Directory.EnumerateFileSystemEntries(folder, "*", SearchOption.AllDirectories).ToList())
        {
            if (!File.Exists(child) && !Directory.Exists(child)) continue;   // went with its folder
            if (IsLink(child)) { if (Directory.Exists(child)) Directory.Delete(child); else TryDelete(child); continue; }
            FileSystemSecurity security = Directory.Exists(child)
                ? new DirectoryInfo(child).GetAccessControl()
                : new FileInfo(child).GetAccessControl();
            var owner = security.GetOwner(typeof(SecurityIdentifier)) as SecurityIdentifier;
            // Only while the folder was not yet ours: once it is, nothing
            // unelevated can write in it, and a machine whose policy makes
            // the creator the owner would otherwise lose its own files.
            if (!wasOurs && owner != Administrators && owner != LocalSystem)
            {
                Configuration.Log.Warn("store", $"{child} was not written by this application; removed");
                if (Directory.Exists(child)) Directory.Delete(child, recursive: true);
                else TryDelete(child);
                continue;
            }
            var explicitRules = security.GetAccessRules(true, false, typeof(SecurityIdentifier));
            if (explicitRules.Count == 0 && !security.AreAccessRulesProtected) continue;

            foreach (FileSystemAccessRule rule in explicitRules) security.RemoveAccessRuleAll(rule);
            security.SetAccessRuleProtection(isProtected: false, preserveInheritance: false);
            security.SetOwner(Administrators);
            if (security is DirectorySecurity d) new DirectoryInfo(child).SetAccessControl(d);
            else new FileInfo(child).SetAccessControl((FileSecurity)security);
            Configuration.Log.Warn("store", $"Permissions on {child} were changed; put back");
        }
    }

    private static bool Same(DirectorySecurity current, DirectorySecurity wanted) =>
        current.AreAccessRulesProtected
        && current.GetOwner(typeof(SecurityIdentifier)) is SecurityIdentifier owner && owner == Administrators
        && Describe(current) == Describe(wanted);

    private static string Describe(DirectorySecurity security) => string.Join(";",
        security.GetAccessRules(true, false, typeof(SecurityIdentifier)).Cast<FileSystemAccessRule>()
            .Select(r => $"{r.IdentityReference}|{(int)r.FileSystemRights}|{r.AccessControlType}|{r.InheritanceFlags}|{r.PropagationFlags}")
            .OrderBy(x => x, StringComparer.Ordinal));

    private static FileSystemAccessRule Inherited(SecurityIdentifier who, FileSystemRights rights) =>
        new(who, rights, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);

    private static readonly SecurityIdentifier LocalSystem = new(WellKnownSidType.LocalSystemSid, null);
    private static readonly SecurityIdentifier Administrators = new(WellKnownSidType.BuiltinAdministratorsSid, null);
    private static readonly SecurityIdentifier AuthenticatedUsers = new(WellKnownSidType.AuthenticatedUserSid, null);
}
