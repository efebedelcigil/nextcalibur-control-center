using System.Security.AccessControl;
using System.Security.Principal;
using Nextcalibur.Core.Security;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// The permissions themselves, which only an elevated run can set. Skipped
/// (passes without asserting) unelevated - CI and an ordinary test run - and
/// run on the machine from an elevated prompt before a release.
/// </summary>
[Collection("log")]
public class ProtectedStoreElevatedTests : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "nc-store-acl-" + Guid.NewGuid().ToString("N"));

    private static bool Elevated
    {
        get
        {
            using var identity = WindowsIdentity.GetCurrent();
            return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
        }
    }

    public void Dispose()
    {
        try { Directory.Delete(_root, recursive: true); } catch (IOException) { } catch (UnauthorizedAccessException) { }
    }

    private static readonly SecurityIdentifier Administrators = new(WellKnownSidType.BuiltinAdministratorsSid, null);

    [Fact]
    public void The_folder_is_Administrators_and_the_account_may_only_read()
    {
        if (!Elevated) return;
        ProtectedStore.UseForTests(_root, enforcePermissions: true);

        Assert.True(ProtectedStore.Write("a.json", "{}"));

        var security = new DirectoryInfo(ProtectedStore.Folder).GetAccessControl();
        Assert.True(security.AreAccessRulesProtected);
        Assert.Equal(Administrators, security.GetOwner(typeof(SecurityIdentifier)));
        var user = WindowsIdentity.GetCurrent().User!;
        foreach (FileSystemAccessRule rule in security.GetAccessRules(true, true, typeof(SecurityIdentifier)))
        {
            if (!rule.IdentityReference.Equals(user)) continue;
            Assert.Equal(0, (int)rule.FileSystemRights & (int)(FileSystemRights.WriteData | FileSystemRights.AppendData | FileSystemRights.Delete));
        }

        // Secured once, it is left as it is: no rewrite on the next look.
        var before = security.GetSecurityDescriptorSddlForm(AccessControlSections.All);
        Assert.True(ProtectedStore.EnsureFolder());
        Assert.Equal(before, new DirectoryInfo(ProtectedStore.Folder).GetAccessControl().GetSecurityDescriptorSddlForm(AccessControlSections.All));
    }

    [Fact]
    public void A_folder_taken_first_is_secured_and_what_was_planted_goes()
    {
        if (!Elevated) return;
        ProtectedStore.UseForTests(_root, enforcePermissions: true);

        // Somebody else's folder first, with a planted file owned by the account.
        Directory.CreateDirectory(ProtectedStore.Folder);
        var planted = Path.Combine(ProtectedStore.Folder, "settings.json");
        File.WriteAllText(planted, "{\"CpuWarningTemperatureC\":125}");
        var user = WindowsIdentity.GetCurrent().User!;
        var fileSecurity = new FileInfo(planted).GetAccessControl();
        fileSecurity.SetOwner(user);
        new FileInfo(planted).SetAccessControl(fileSecurity);
        var loose = new DirectoryInfo(ProtectedStore.Folder).GetAccessControl();
        loose.SetAccessRuleProtection(false, true);
        new DirectoryInfo(ProtectedStore.Folder).SetAccessControl(loose);

        Assert.True(ProtectedStore.EnsureFolder());

        Assert.False(File.Exists(planted));
        Assert.True(new DirectoryInfo(ProtectedStore.Folder).GetAccessControl().AreAccessRulesProtected);
    }
}
