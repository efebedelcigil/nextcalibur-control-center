using System.Net.Http;
using Microsoft.Win32;

namespace Nextcalibur.Core.Dependencies;

/// <summary>
/// PawnIO: the signed kernel driver the CPU power reading goes through.
/// Optional - without it the number reads "--" - but offered, because the
/// owner wants the number and the driver is the only honest way to it.
/// </summary>
public sealed class PawnIoDependency : Dependency
{
    public override string Id => "pawnio";
    public override string Name => "PawnIO driver";
    public override string Purpose => "reads the processor's power draw";
    public override string UninstallKey => "PawnIO";

    public override Version? InstalledVersion()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\PawnIO");
            return key?.GetValue("DisplayVersion") is string s && Version.TryParse(s, out var v) ? v : null;
        }
        catch (Exception ex) when (ex is System.Security.SecurityException or UnauthorizedAccessException)
        {
            return null;
        }
    }

    public override Task<(Version Version, Uri Download)?> LatestAsync(HttpClient http, CancellationToken ct) =>
        LatestGitHubReleaseAsync(http, "namazso", "PawnIO.Setup", "PawnIO_setup.exe", ct);

    public override string ExpectedSigner => "CN=namazso.eu";

    // Found in the installer's own strings; the uninstall entry uses the same family.
    public override string SilentInstallArguments => "-install -silent";
}
