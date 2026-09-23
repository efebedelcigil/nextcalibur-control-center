using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Security;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// The program's own check: the release's list of hashes, the install
/// folder's permissions, and the two dependencies Windows installs.
/// Built on a fake install folder, so a test run never depends on - or
/// touches - a real installation.
/// </summary>
public class IntegrityTests : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "nc-integrity-" + Guid.NewGuid().ToString("N"));
    private string Current => Path.Combine(_root, "current");
    private string Exe => Path.Combine(Current, "Nextcalibur.exe");
    private string Manifest => Path.Combine(Current, Integrity.ManifestName);

    public IntegrityTests()
    {
        Directory.CreateDirectory(Current);
        File.WriteAllText(Path.Combine(_root, "Update.exe"), "updater");
        File.WriteAllText(Exe, "the program as released");
    }

    public void Dispose()
    {
        try { Directory.Delete(_root, recursive: true); } catch (IOException) { }
    }

    private void WriteManifest(string? hash = null) =>
        File.WriteAllText(Manifest, $$"""{"Nextcalibur.exe":"{{hash ?? Integrity.Sha256(Exe)}}"}""");

    [Fact]
    public void The_program_as_released_passes()
    {
        WriteManifest();
        Assert.Empty(Integrity.CheckProgram(Exe));
    }

    [Fact]
    public void A_changed_executable_is_serious()
    {
        WriteManifest();
        File.AppendAllText(Exe, "!");
        var finding = Assert.Single(Integrity.CheckProgram(Exe));
        Assert.Equal(IntegrityKind.ProgramChanged, finding.Kind);
        Assert.True(finding.Serious);
    }

    [Fact]
    public void A_removed_list_is_serious()
    {
        var finding = Assert.Single(Integrity.CheckProgram(Exe));
        Assert.Equal(IntegrityKind.ProgramUnverified, finding.Kind);
        Assert.True(finding.Serious);
    }

    [Fact]
    public void A_list_that_leaves_the_executable_out_proves_nothing()
    {
        File.WriteAllText(Manifest, """{"other.dll":"00"}""");
        Assert.Contains(Integrity.CheckProgram(Exe), f => f.Kind == IntegrityKind.ProgramChanged && f.Subject == Exe);
    }

    [Fact]
    public void A_list_may_not_point_outside_its_folder()
    {
        File.WriteAllText(Manifest, $$"""{"Nextcalibur.exe":"{{Integrity.Sha256(Exe)}}","..\\Update.exe":"00"}""");
        Assert.Contains(Integrity.CheckProgram(Exe), f => f.Kind == IntegrityKind.ProgramChanged && f.Subject == Manifest);
    }

    [Fact]
    public void An_unreadable_list_fails_rather_than_passes()
    {
        File.WriteAllText(Manifest, "not json");
        Assert.Contains(Integrity.CheckProgram(Exe), f => f.Serious);
    }

    [Fact]
    public void A_development_build_is_not_checked()
    {
        File.Delete(Path.Combine(_root, "Update.exe"));
        Assert.Empty(Integrity.CheckProgram(Exe));
    }

    [Fact]
    public void A_folder_the_account_may_write_is_seen()
    {
        // The temporary folder is the account's: exactly what an install folder must not be.
        Assert.True(InstallFolderGuard.WritableByOthers(_root));
    }

    [Fact]
    public void Windows_own_folder_is_not_writable_by_others()
    {
        Assert.False(InstallFolderGuard.WritableByOthers(Environment.SystemDirectory));
    }

    [Theory]
    [InlineData(@"\SystemRoot\System32\drivers\x.sys", @"System32\drivers\x.sys")]
    [InlineData(@"System32\drivers\x.sys", @"System32\drivers\x.sys")]
    [InlineData(@"\??\C:\Drivers\x.sys", @"C:\Drivers\x.sys")]
    public void Driver_paths_are_read_in_every_form_Windows_writes_them(string image, string expected)
    {
        var windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        var want = expected.StartsWith("System32", StringComparison.OrdinalIgnoreCase) ? Path.Combine(windows, expected) : expected;
        Assert.Equal(want, Integrity.ResolveDriverPath(image));
    }

    [Fact]
    public void Nvml_if_present_is_trusted()
    {
        if (!File.Exists(Integrity.NvmlPath)) return;   // a machine without NVIDIA's driver
        Assert.True(Integrity.NvmlIsTrusted());
    }

    [Fact]
    public void PawnIO_if_present_is_trusted()
    {
        if (Integrity.PawnIoDriverPath() is null) return;
        Assert.True(Integrity.PawnIoIsTrusted());
    }

    [Fact]
    public void The_runtime_this_test_runs_on_is_Microsofts()
    {
        Assert.DoesNotContain(Integrity.CheckLoadedModules(null), f => f.Kind == IntegrityKind.RuntimeUntrusted);
    }
}
