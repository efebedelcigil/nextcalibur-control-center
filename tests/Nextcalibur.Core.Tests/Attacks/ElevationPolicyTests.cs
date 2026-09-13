using Nextcalibur.Core.Configuration;
using Xunit;

namespace Nextcalibur.Core.Tests.Attacks;

/// <summary>
/// Which copies of this application may be started as administrator without
/// anybody being asked. This is the whole of the local-escalation question:
/// a task that runs a file the account can replace is a way for anything
/// running as the account to become administrator at the next start.
/// </summary>
public class ElevationPolicyTests
{
    private static string ProgramFiles => Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
    private static string Profile => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    /// <summary>A folder that looks like an install: Update.exe, current\, the executable.</summary>
    private static string MakeInstall(string root)
    {
        Directory.CreateDirectory(Path.Combine(root, "current"));
        File.WriteAllText(Path.Combine(root, "Update.exe"), "not really");
        var exe = Path.Combine(root, "current", "Nextcalibur.exe");
        File.WriteAllText(exe, "not really");
        return exe;
    }

    [Fact]
    public void An_install_in_the_profile_is_never_started_without_a_prompt()
    {
        var root = Path.Combine(Profile, "nc-attack-" + Guid.NewGuid().ToString("N"));
        try
        {
            var exe = MakeInstall(root);
            Assert.True(Elevation.IsInstalledCopy(exe), "it is an installed copy - Update.exe is beside current\\");
            Assert.False(Elevation.MayStartWithoutPrompt(exe),
                "but the folder above it belongs to the account, which can rename it and put its own there");
        }
        finally
        {
            try { Directory.Delete(root, recursive: true); } catch (IOException) { }
        }
    }

    [Fact]
    public void A_copy_that_is_not_an_install_is_never_started_without_a_prompt()
    {
        var root = Path.Combine(Path.GetTempPath(), "nc-attack-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(root);
        try
        {
            var exe = Path.Combine(root, "Nextcalibur.exe");
            File.WriteAllText(exe, "not really");
            Assert.False(Elevation.IsInstalledCopy(exe));
            Assert.False(Elevation.MayStartWithoutPrompt(exe));
        }
        finally
        {
            try { Directory.Delete(root, recursive: true); } catch (IOException) { }
        }
    }

    /// <summary>
    /// The one shape that may: under Program Files, where only
    /// administrators write, with the updater beside it.
    /// </summary>
    [Fact]
    public void An_install_under_program_files_may_be_started_without_a_prompt()
    {
        // Written as a question about paths, not as an install: this test
        // must not need administrator rights to run.
        var exe = Path.Combine(ProgramFiles, "Nextcalibur", "current", "Nextcalibur.exe");
        Assert.True(InstallFolderGuard.IsUnderProgramFiles(InstallFolderGuard.RootOf(exe)!));
        Assert.Equal(Path.Combine(ProgramFiles, "Nextcalibur"), InstallFolderGuard.RootOf(exe));
    }

    /// <summary>Paths that are not under Program Files however they are spelt.</summary>
    [Theory]
    [InlineData(@"C:\Users\someone\AppData\Local\Nextcalibur")]
    [InlineData(@"C:\Program Files Extra\Nextcalibur")]
    [InlineData(@"C:\ProgramFiles\Nextcalibur")]
    [InlineData(@"C:\Temp\Program Files\Nextcalibur")]
    [InlineData(@"D:\Games\Nextcalibur")]
    public void These_folders_are_not_program_files(string root)
    {
        Assert.False(InstallFolderGuard.IsUnderProgramFiles(root));
    }

    [Theory]
    [InlineData(@"C:\Program Files\Nextcalibur")]
    [InlineData(@"c:\program files\nextcalibur")]
    [InlineData(@"C:\Program Files (x86)\Nextcalibur")]
    [InlineData(@"C:\Program Files\Vendor\Nextcalibur\current")]
    public void These_folders_are_program_files(string root)
    {
        Assert.True(InstallFolderGuard.IsUnderProgramFiles(root));
    }

    /// <summary>The install root is the folder above <c>current\</c>, wherever the executable sits.</summary>
    [Theory]
    [InlineData(@"C:\Program Files\Nextcalibur\current\Nextcalibur.exe", @"C:\Program Files\Nextcalibur")]
    [InlineData(@"C:\Program Files\Nextcalibur\Nextcalibur.exe", @"C:\Program Files\Nextcalibur")]
    [InlineData(@"C:\x\CURRENT\Nextcalibur.exe", @"C:\x")]
    public void The_install_root_is_read_from_the_path(string exe, string root)
    {
        Assert.Equal(root, InstallFolderGuard.RootOf(exe));
    }
}
