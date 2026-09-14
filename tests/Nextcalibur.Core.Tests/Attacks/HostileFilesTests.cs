using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Security;
using Xunit;

namespace Nextcalibur.Core.Tests.Attacks;

/// <summary>
/// The application runs as administrator and writes into folders the account
/// owns, and reads files the account may edit. Both are attacks waiting to
/// be tried: a link where a folder should be, and a number where a number
/// should not be.
/// </summary>
public class HostileFilesTests
{
    private static string Scratch()
    {
        var folder = Path.Combine(Path.GetTempPath(), "nc-attack-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(folder);
        return folder;
    }

    /// <summary>
    /// Taking the scratch folder away again. A tree with a junction in it
    /// does not delete like an ordinary one - the recursive delete throws
    /// rather than follow it - so the links go first, one at a time.
    /// </summary>
    private static void Clear(string folder)
    {
        try
        {
            foreach (var entry in Directory.EnumerateDirectories(folder, "*", SearchOption.AllDirectories).Reverse())
                if ((File.GetAttributes(entry) & FileAttributes.ReparsePoint) != 0) Directory.Delete(entry);
            Directory.Delete(folder, recursive: true);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            // A test that cannot tidy up is not a test that failed.
        }
    }

    /// <summary>
    /// A link where a folder should be. A symbolic link needs a privilege
    /// or Developer Mode; a junction needs neither, which is exactly why
    /// this is worth guarding against - so the junction is tried first,
    /// because that is what an unprivileged attacker would use.
    /// </summary>
    private static bool TryLink(string link, string target)
    {
        using var cmd = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = Path.Combine(Environment.SystemDirectory, "cmd.exe"),
            Arguments = $"/c mklink /J \"{link}\" \"{target}\"",
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        });
        cmd?.WaitForExit(15000);
        if (Directory.Exists(link) && (File.GetAttributes(link) & FileAttributes.ReparsePoint) != 0) return true;

        try
        {
            Directory.CreateSymbolicLink(link, target);
            return true;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or IOException or PlatformNotSupportedException)
        {
            return false;
        }
    }

    [Fact]
    public void A_folder_that_is_a_link_is_not_written_to()
    {
        var scratch = Scratch();
        try
        {
            var elsewhere = Path.Combine(scratch, "elsewhere");
            Directory.CreateDirectory(elsewhere);
            var link = Path.Combine(scratch, "logs");
            Assert.True(TryLink(link, elsewhere), "the attack could not be set up: no junction and no symbolic link");

            Assert.False(ProfileFiles.EnsureOrdinaryFolder(link),
                "an elevated process must not write into a folder the account replaced with a link");

            // Nor into anything under one: creating a folder there is already
            // a write through the link.
            Assert.False(ProfileFiles.EnsureOrdinaryFolder(Path.Combine(link, "deeper")),
                "nor into a folder under a link");
            Assert.False(Directory.Exists(Path.Combine(elsewhere, "deeper")),
                "and nothing was created on the other side of it");

            // A file inside a linked folder is refused for the same reason.
            Assert.False(ProfileFiles.IsOrdinaryFileOrAbsent(Path.Combine(link, "settings.json")));
        }
        finally
        {
            Clear(scratch);
        }
    }

    [Fact]
    public void An_ordinary_folder_is_written_to()
    {
        var scratch = Scratch();
        try
        {
            Assert.True(ProfileFiles.EnsureOrdinaryFolder(Path.Combine(scratch, "a", "b")));
            Assert.True(Directory.Exists(Path.Combine(scratch, "a", "b")));
        }
        finally
        {
            Clear(scratch);
        }
    }

    [Fact]
    public void A_file_that_is_a_link_is_not_written_to()
    {
        var scratch = Scratch();
        try
        {
            var target = Path.Combine(scratch, "target.json");
            File.WriteAllText(target, "{}");
            var link = Path.Combine(scratch, "settings.json");
            try
            {
                File.CreateSymbolicLink(link, target);
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException or IOException or PlatformNotSupportedException)
            {
                Assert.True(ProfileFiles.IsOrdinaryFileOrAbsent(Path.Combine(scratch, "absent.json")));
                return;
            }

            Assert.False(ProfileFiles.IsOrdinaryFileOrAbsent(link));
            Assert.True(ProfileFiles.IsOrdinaryFileOrAbsent(target));
            Assert.True(ProfileFiles.IsOrdinaryFileOrAbsent(Path.Combine(scratch, "absent.json")));
        }
        finally
        {
            Clear(scratch);
        }
    }

    /// <summary>
    /// Where this process writes a file it is about to run: not the account's
    /// temporary folder, and not a name anything can guess or create first.
    /// </summary>
    [Fact]
    public void A_file_that_will_be_run_is_written_somewhere_the_account_cannot_reach()
    {
        string path;
        using (var stream = SystemTools.CreateProtectedTemporaryFile(".exe", out path))
        {
            var windowsTemp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp");
            Assert.Equal(windowsTemp, Path.GetDirectoryName(path), ignoreCase: true);
            Assert.False(string.Equals(Path.GetTempPath().TrimEnd('\\'), Path.GetDirectoryName(path), StringComparison.OrdinalIgnoreCase),
                "not the account's temporary folder, which anything running as the account can write to");

            // A name nobody can sit on: 32 hexadecimal characters of chance.
            var name = Path.GetFileNameWithoutExtension(path);
            Assert.StartsWith("nextcalibur-", name);
            Assert.Equal(32, name["nextcalibur-".Length..].Length);

            // Open for writing, shared for reading only: nothing can replace
            // its contents between the signature check and the run.
            Assert.True(stream.CanWrite);
            Assert.Throws<IOException>(() => new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.ReadWrite));

            // And it is new, every time, never an existing file taken over.
            Assert.Throws<IOException>(() => new FileStream(path, FileMode.CreateNew, FileAccess.Write));
        }
        File.Delete(path);
    }

    /// <summary>Numbers from a file the account may edit, put back inside the range they belong to.</summary>
    [Theory]
    [InlineData(0, 2000)]
    [InlineData(-1, 2000)]
    [InlineData(int.MinValue, 2000)]
    [InlineData(1, 2000)]
    [InlineData(499, 2000)]
    [InlineData(int.MaxValue, 2000)]
    [InlineData(500, 500)]
    [InlineData(60000, 60000)]
    [InlineData(2000, 2000)]
    public void A_polling_interval_from_the_file_is_a_sane_one(int written, int expected)
    {
        var settings = AppSettings.Migrate(new AppSettings { PollIntervalMs = written });
        Assert.Equal(expected, settings.PollIntervalMs);
    }

    [Theory]
    [InlineData(-40)]
    [InlineData(0)]
    [InlineData(1000)]
    [InlineData(int.MaxValue)]
    public void A_warning_threshold_from_the_file_is_a_sane_one(int written)
    {
        var settings = AppSettings.Migrate(new AppSettings { CpuWarningTemperatureC = written, GpuWarningTemperatureC = written });
        Assert.InRange(settings.CpuWarningTemperatureC, 40, 110);
        Assert.InRange(settings.GpuWarningTemperatureC, 40, 110);
    }

    /// <summary>
    /// The effect number goes into the high nibble of a byte written to the
    /// embedded controller. A value from outside the six is not a firmware
    /// command this project has ever seen answered, so it is not sent.
    /// </summary>
    [Theory]
    [InlineData(0, true)]     // Off
    [InlineData(1, true)]     // Static
    [InlineData(4, true)]     // Heartbeat
    [InlineData(6, true)]     // ColourCycle
    [InlineData(7, true)]     // Wave
    [InlineData(5, false)]    // the gap in the middle: the firmware has no 5
    [InlineData(8, false)]
    [InlineData(15, false)]   // the whole nibble, which is what is written
    [InlineData(200, false)]
    [InlineData(255, false)]
    public void An_effect_number_from_the_file_is_one_of_the_six(byte written, bool kept)
    {
        var effect = (LedEffect)written;
        var used = LedController.Sanitise(effect);
        Assert.Equal(kept ? effect : LedEffect.Static, used);
        Assert.True(Enum.IsDefined(used));
    }
}
