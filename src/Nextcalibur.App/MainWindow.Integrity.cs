using System.Linq;
using System.Windows.Media;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Security;

namespace Nextcalibur.App;

/// <summary>
/// Keeping the application what it was released as, and its settings what
/// it saved: see <see cref="Integrity"/> and <see cref="ProtectedStore"/>.
///
/// At start everything is checked once; then the settings every five
/// minutes (three small files) and the rest every hour (a hash of the
/// executable and a look at what is loaded). All of it off the interface
/// thread, at background priority.
///
/// A settings file changed outside is put back and the person told. The
/// application itself not being what was released - its executable, its
/// folder's permissions, the runtime under it - stops every firmware write
/// until it is reinstalled: a program that may have been altered does not
/// get to drive the embedded controller. A dependency that fails is simply
/// not used, and said so.
/// </summary>
public partial class MainWindow
{
    private static readonly TimeSpan StoreCheckEvery = TimeSpan.FromMinutes(5);
    private static readonly TimeSpan IntegrityCheckEvery = TimeSpan.FromHours(1);

    private DateTime _storeCheckedAt = DateTime.UtcNow;
    private DateTime _integrityCheckedAt = DateTime.MinValue;
    private bool _integrityChecking;

    /// <summary>The serious findings, once there are any. Kept for the session: a restart after a reinstall clears them.</summary>
    private List<IntegrityFinding> _integritySerious = new();

    /// <summary>What the person has been told this session, so each thing is said once.</summary>
    private readonly HashSet<string> _integrityTold = new(StringComparer.OrdinalIgnoreCase);

    private void StartIntegrityWatch()
    {
        ProtectedStore.Tampered += name => Dispatcher.BeginInvoke(() => TellStoreChanged(name));
        // Found while the settings were first read, before anything listened.
        foreach (var name in ProtectedStore.ReportedSoFar) TellStoreChanged(name);
        CheckIntegrity();
    }

    /// <summary>From the slow timer, hidden or not.</summary>
    private void WatchIntegrity()
    {
        var now = DateTime.UtcNow;
        if (now - _storeCheckedAt >= StoreCheckEvery)
        {
            _storeCheckedAt = now;
            _ = Task.Run(() =>
            {
                try { AtBackgroundPriority(() => ProtectedStore.Verify()); }
                catch (Exception ex) when (ex is not OutOfMemoryException) { Log.Warn("store", ex.Message); }
            });
        }
        if (now - _integrityCheckedAt >= IntegrityCheckEvery) CheckIntegrity();
    }

    private async void CheckIntegrity()
    {
        if (_integrityChecking) return;
        _integrityChecking = true;
        _integrityCheckedAt = DateTime.UtcNow;
        try
        {
            var exe = Environment.ProcessPath;
            var findings = await Task.Run(() => AtBackgroundPriority(() => Integrity.CheckAll(exe)));

            foreach (var finding in findings)
                Log.Warn("integrity", $"{finding.Kind}{(finding.Serious ? " (serious)" : string.Empty)}: {finding.Subject}");

            var serious = findings.Where(f => f.Serious).ToList();
            if (serious.Count > 0)
            {
                foreach (var finding in serious)
                    if (!_integritySerious.Any(f => f.Kind == finding.Kind && f.Subject == finding.Subject))
                        _integritySerious.Add(finding);
                StopFirmwareWrites();
                RefreshBanner();
            }

            // An unsigned library loaded by security software or an overlay
            // is in the log; it is not worth a notification.
            foreach (var finding in findings.Where(f => f.Kind != IntegrityKind.ModuleUnsigned))
            {
                if (!_integrityTold.Add(finding.Kind + "|" + finding.Subject)) continue;
                _tray?.ShowMessage(
                    Strings.Get(finding.Serious ? "S.Integrity.ProgramTitle" : "S.Integrity.DependencyTitle"),
                    Describe(finding));
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Warn("integrity", "The integrity check did not finish: " + ex.Message);
        }
        finally
        {
            _integrityChecking = false;
        }
    }

    private void TellStoreChanged(string name)
    {
        if (!_integrityTold.Add("store|" + name)) return;
        _tray?.ShowMessage(Strings.Get("S.Integrity.StoreTitle"), Strings.Get("S.Integrity.StoreBody", name));
    }

    /// <summary>
    /// No firmware writes from here on. The same state as a machine that
    /// answers the protocol differently: readings stay, controls that write
    /// are locked.
    /// </summary>
    private void StopFirmwareWrites()
    {
        if (_support.Level != SupportLevel.Supported) return;
        _support = _support with
        {
            Level = SupportLevel.ReadOnly,
            Reasons = _support.Reasons.Append("integrity: " + string.Join(", ", _integritySerious.Select(f => f.Kind))).ToList(),
        };
        Log.Warn("integrity", "Firmware writes stopped until the application is reinstalled");
        ApplySupportVerdict();
    }

    private static string Describe(IntegrityFinding finding) => Strings.Get(finding.Kind switch
    {
        IntegrityKind.ProgramChanged => "S.Integrity.ProgramChanged",
        IntegrityKind.ProgramUnverified => "S.Integrity.ProgramUnverified",
        IntegrityKind.FolderWritable => "S.Integrity.FolderWritable",
        IntegrityKind.RuntimeUntrusted => "S.Integrity.RuntimeUntrusted",
        IntegrityKind.ModuleUnsigned => "S.Integrity.ModuleUnsigned",
        IntegrityKind.NvmlUntrusted => "S.Integrity.NvmlUntrusted",
        _ => "S.Integrity.PawnIoUntrusted",
    }, finding.Subject);

    /// <summary>The banner for the serious findings, or false when there are none.</summary>
    private bool ShowIntegrityBanner()
    {
        if (_integritySerious.Count == 0) return false;
        ShowBanner(
            Strings.Get("S.Banner.IntegrityTitle"),
            string.Join(" ", _integritySerious.Select(Describe)) + " " + Strings.Get("S.Banner.IntegrityBody"),
            (SolidColorBrush)FindResource("Bad"));
        return true;
    }
}
