using System.Text.Json;
using Nextcalibur.Core.Dependencies;
using Xunit;

namespace Nextcalibur.Core.Tests.Attacks;

/// <summary>
/// The .NET runtime is now a dependency like any other: downloaded from
/// Microsoft and run as administrator. Which makes its release index an
/// answer from the network, and every field in it a stranger's.
/// </summary>
public class RuntimeDependencyTests
{
    private static JsonElement Parse(string json) => JsonDocument.Parse(json).RootElement.Clone();

    private static string Index(string version, string name, string url, string? hash = "ab12") =>
        JsonSerializer.Serialize(new
        {
            releases = new[]
            {
                new
                {
                    windowsdesktop = new
                    {
                        version,
                        files = new[] { new { name, url, hash } },
                    },
                },
            },
        });

    private const string Real = "https://builds.dotnet.microsoft.com/dotnet/WindowsDesktop/8.0.31/windowsdesktop-runtime-8.0.31-win-x64.exe";

    [Fact]
    public void The_newest_release_is_read_with_its_hash()
    {
        var file = DotNetRuntimeDependency.Newest(Parse(Index("8.0.31", "windowsdesktop-runtime-win-x64.exe", Real)));

        Assert.NotNull(file);
        Assert.Equal(new Version(8, 0, 31), file!.Version);
        Assert.Equal(new Uri(Real), file.Download);
        Assert.Equal("ab12", file.Sha512);
    }

    /// <summary>What is downloaded is run as administrator, so the link must be Microsoft's own.</summary>
    [Theory]
    [InlineData("https://builds.dotnet.microsoft.com/dotnet/x.exe", true)]
    [InlineData("https://download.visualstudio.microsoft.com/download/pr/x.exe", true)]
    [InlineData("http://builds.dotnet.microsoft.com/dotnet/x.exe", false)]
    [InlineData("https://builds.dotnet.microsoft.com.evil.example/x.exe", false)]
    [InlineData("https://evil.example/dotnet/x.exe", false)]
    [InlineData("https://raw.githubusercontent.com/dotnet/core/main/x.exe", false)]
    [InlineData("file:///C:/Windows/System32/calc.exe", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void Only_microsofts_own_download_hosts(string? link, bool accepted)
    {
        Assert.Equal(accepted, DotNetRuntimeDependency.IsMicrosoftDownload(link, out _));
    }

    [Fact]
    public void A_release_whose_link_is_not_microsofts_is_no_release_at_all()
    {
        Assert.Null(DotNetRuntimeDependency.Newest(Parse(Index("8.0.31", "windowsdesktop-runtime-win-x64.exe", "https://evil.example/x.exe"))));
    }

    [Theory]
    [InlineData("""{"releases":[]}""")]
    [InlineData("""{"releases":{}}""")]
    [InlineData("""{}""")]
    [InlineData("""{"releases":[{"windowsdesktop":{"version":"8.0.31"}}]}""")]
    [InlineData("""{"releases":[{"windowsdesktop":{"version":"nonsense","files":[]}}]}""")]
    [InlineData("""{"releases":[{"windowsdesktop":{"version":"7.0.20","files":[{"name":"windowsdesktop-runtime-win-x64.exe","url":"https://builds.dotnet.microsoft.com/x.exe"}]}}]}""")]
    [InlineData("""[]""")]
    public void An_index_of_the_wrong_shape_is_no_release(string json)
    {
        Assert.Null(DotNetRuntimeDependency.Newest(Parse(json)));
    }

    /// <summary>Only the x64 desktop installer; not the ASP.NET one, not arm64, not the zip.</summary>
    [Theory]
    [InlineData("windowsdesktop-runtime-8.0.31-win-x64.exe", true)]
    [InlineData("windowsdesktop-runtime-8.0.31-win-arm64.exe", false)]
    [InlineData("windowsdesktop-runtime-8.0.31-win-x86.exe", false)]
    [InlineData("windowsdesktop-runtime-8.0.31-win-x64.zip", false)]
    public void Only_the_x64_installer(string name, bool found)
    {
        var file = DotNetRuntimeDependency.Newest(Parse(Index("8.0.31", name, Real)));
        Assert.Equal(found, file is not null);
    }

    /// <summary>
    /// The runtime is never offered for removal by this application's
    /// uninstall, and refuses if something asks anyway: the machine's other
    /// .NET applications are using it.
    /// </summary>
    [Fact]
    public void The_runtime_is_not_ours_to_remove()
    {
        var dotnet = new DotNetRuntimeDependency();
        Assert.False(dotnet.MayBeRemoved);
        Assert.False(dotnet.Uninstall());
        Assert.True(new PawnIoDependency().MayBeRemoved);
    }

    [Fact]
    public void The_runtime_is_the_first_dependency_checked()
    {
        Assert.IsType<DotNetRuntimeDependency>(DependencyManager.All[0]);
    }

    /// <summary>
    /// The channel named in the code is the one the application is actually
    /// built against: a mismatch would have it checking for patches of a
    /// runtime it does not use.
    /// </summary>
    [Fact]
    public void The_channel_matches_the_target_framework()
    {
        var folder = AppContext.BaseDirectory;
        string? csproj = null;
        for (var i = 0; i < 8 && folder is not null; i++)
        {
            var candidate = Path.Combine(folder, "src", "Nextcalibur.App", "Nextcalibur.App.csproj");
            if (File.Exists(candidate)) { csproj = candidate; break; }
            folder = Path.GetDirectoryName(folder);
        }
        Assert.NotNull(csproj);

        var text = File.ReadAllText(csproj!);
        var match = System.Text.RegularExpressions.Regex.Match(text, @"<TargetFramework>net(\d+\.\d+)-");
        Assert.True(match.Success, "the target framework could not be read from the project");
        Assert.Equal(DotNetRuntimeDependency.Channel, match.Groups[1].Value);
    }

    /// <summary>
    /// On this machine, whatever it has: the reading must be a real version
    /// and at least the floor, or the application could not be running.
    /// </summary>
    [Fact]
    public void The_installed_runtime_is_read_from_the_machine()
    {
        var installed = new DotNetRuntimeDependency().InstalledVersion();

        Assert.NotNull(installed);
        Assert.True(installed >= DotNetRuntimeDependency.Minimum,
            $"the application is running, so a runtime of at least {DotNetRuntimeDependency.Minimum} must be installed; read {installed}");
    }
}
