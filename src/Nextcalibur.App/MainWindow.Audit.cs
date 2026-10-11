using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Security;

namespace Nextcalibur.App;

/// <summary>
/// Changes that could need undoing: asked about with a restore point offered
/// (ticked), and every step written to the audit log - the question, the
/// answer, the box, the restore point and its number, what was done. The
/// owner's rule of 11 October 2026; see <see cref="AuditLog"/> and
/// <see cref="RestorePoint"/>.
/// </summary>
public partial class MainWindow
{
    /// <summary>
    /// The question for a change that could need undoing. True to go ahead -
    /// after the restore point, when one was asked for and made, or when the
    /// person chose to go on without the one Windows would not make.
    /// </summary>
    /// <param name="action">A short, stable name for the audit log: "gpu-mode", "ndu", ...</param>
    private async Task<bool> ConfirmRiskyAsync(string action, string title, string body, bool defaultNo = true,
        IReadOnlyDictionary<string, object?>? details = null)
    {
        var yes = Dialogs.AskWithCheck(title, body, Strings.Get("S.Restore.Checkbox"), checkDefault: true, out var restore, defaultNo);
        AuditLog.Record("asked", With(details, ("change", action), ("question", title), ("answer", yes ? "yes" : "no"),
            ("restorePointTicked", restore)));
        if (!yes) return false;

        // Unticked: the same warning as when Windows makes none - the change
        // is not one this application recommends making without a way back.
        if (!restore) return ConfirmWithoutRestorePoint(action, Strings.Get("S.Restore.Unticked"));

        ShowProgress(Strings.Get("S.Restore.Title"), Strings.Get("S.Restore.Creating"));
        DialogProgress.IsIndeterminate = true;
        RestorePointOutcome outcome;
        try
        {
            outcome = await Task.Run(() => RestorePoint.Create("Nextcalibur: " + action));
        }
        finally
        {
            DialogProgress.IsIndeterminate = false;
            HideProgress();
        }

        AuditLog.Record("restore-point", new Dictionary<string, object?>
        {
            ["change"] = action,
            ["created"] = outcome.Created,
            ["sequence"] = outcome.Sequence,
            ["pointCreatedAt"] = outcome.CreatedAt?.ToString("yyyy-MM-ddTHH:mm:ss"),
            ["status"] = outcome.Status,
            ["reason"] = outcome.Reason,
        });

        if (outcome.Created)
        {
            Dialogs.Tell(Strings.Get("S.Restore.Title"),
                Strings.Get("S.Restore.Made", outcome.Sequence ?? 0, outcome.CreatedAt?.ToString("yyyy-MM-dd HH:mm") ?? "?"));
            return true;
        }

        return ConfirmWithoutRestorePoint(action, Strings.Get("S.Restore.NotMadeWhy", outcome.Reason ?? ""));
    }

    /// <summary>
    /// The second question, with "no" as its default: there is no restore
    /// point to go back to, the change is a risky one, and this application
    /// does not recommend it without one.
    /// </summary>
    private static bool ConfirmWithoutRestorePoint(string action, string why)
    {
        var goOn = Dialogs.Ask(Strings.Get("S.Restore.NoPointTitle"), why + Environment.NewLine + Environment.NewLine + Strings.Get("S.Restore.NotRecommended"), defaultNo: true);
        AuditLog.Record("asked", new Dictionary<string, object?>
        {
            ["change"] = action,
            ["question"] = "go on without a restore point (warned: not recommended)",
            ["why"] = why,
            ["answer"] = goOn ? "yes" : "no",
        });
        return goOn;
    }

    /// <summary>What was done, or that it failed - after the question, or for a change made without one.</summary>
    private static void Audit(string action, string outcome, params (string Key, object? Value)[] details) =>
        AuditLog.Record(outcome, With(null, [("change", action), .. details]));

    private static Dictionary<string, object?> With(IReadOnlyDictionary<string, object?>? extra, params (string Key, object? Value)[] fields)
    {
        var all = new Dictionary<string, object?>();
        foreach (var (k, v) in fields) all[k] = v;
        if (extra is not null) foreach (var (k, v) in extra) all[k] = v;
        return all;
    }
}
