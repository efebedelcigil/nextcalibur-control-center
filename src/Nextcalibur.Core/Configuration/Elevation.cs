using System.Diagnostics;
using System.Security.Principal;
using Nextcalibur.Core.Hardware;

namespace Nextcalibur.Core.Configuration;

/// <summary>
/// The application runs elevated, and asks for it once.
///
/// Decided by the owner on 12 September 2026, the way the vendor's software
/// does it: the process holds administrator rights for its whole life. That
/// is what reading the CPU's power through PawnIO needs, and it retires a
/// row of workarounds - the permission grant on the firmware mailbox, the
/// card-switch tasks, the elevated helper copies.
///
/// Elevation without a prompt every time is a scheduled task: one that
/// runs this executable at the highest level available, registered once
/// (that registration is the one prompt) and run on demand from then on.
/// An unelevated start - the Start menu, a pin, Velopack's restart after an
/// update - runs the task and exits; the task starts the real one. A second
/// task with a logon trigger is "start with Windows", since a <c>Run</c>
/// entry cannot start an elevated program.
/// </summary>
public static class Elevation
{
    /// <summary>The on-demand task: the executable at the highest level, no arguments.</summary>
    public const string OpenTask = @"\Nextcalibur\Open";

    /// <summary>The logon task: the same, with <c>--tray</c>, at sign-in.</summary>
    public const string StartupTask = @"\Nextcalibur\Start with Windows";

    /// <summary>Passed by the tasks so the elevated copy knows how it was started.</summary>
    public const string ViaTaskArgument = "--via-task";

    public static bool IsElevated()
    {
        using var identity = WindowsIdentity.GetCurrent();
        return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
    }

    /// <summary>
    /// Ensures the process that continues is elevated. Returns true to carry
    /// on - this process is elevated. Returns false when another process has
    /// been started to take over, or the person declined, and this one must
    /// exit without doing anything.
    /// </summary>
    public static bool EnsureElevated(string[] args, string executablePath)
    {
        if (IsElevated()) return true;

        var tray = args.Contains("--tray", StringComparer.OrdinalIgnoreCase);

        // Started by the task and still not elevated: the task cannot give
        // more than the account has (a demoted account, a policy). Running
        // the task again from here would start another copy that finds the
        // same thing and runs it again - a chain of processes with no end,
        // at every logon. So from here it is the prompt or nothing.
        var viaTask = args.Contains(ViaTaskArgument, StringComparer.OrdinalIgnoreCase);

        // The prompt-free way, when the tasks exist and point at this copy.
        if (!viaTask
            && TaskPointsAt(tray ? StartupTask : OpenTask, executablePath)
            && CardSwitchTasks.Schtasks($"/run /tn \"{(tray ? StartupTask : OpenTask)}\"") == 0)
            return false;

        // The one prompt: relaunch elevated. The elevated copy registers the
        // tasks, so this is the last time.
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = executablePath,
                Arguments = string.Join(' ', args.Where(a => !a.Equals(ViaTaskArgument, StringComparison.OrdinalIgnoreCase))
                    .Select(a => a.Contains(' ') ? $"\"{a}\"" : a)),
                UseShellExecute = true,
                Verb = "runas",
            });
        }
        catch (System.ComponentModel.Win32Exception)
        {
            // Declined. Nothing runs; nothing to clean up.
        }
        return false;
    }

    /// <summary>
    /// Registers (or repoints) the on-demand task. Elevated only. Idempotent:
    /// a task that already runs this executable is left alone.
    /// </summary>
    public static bool RegisterOpenTask(string executablePath)
    {
        if (!IsInstalledCopy(executablePath)) return false;
        if (TaskPointsAt(OpenTask, executablePath)) return false;
        return Create(OpenTask, executablePath, ViaTaskArgument, logon: false);
    }

    /// <summary>
    /// Whether this executable is an installed copy - one with the updater
    /// beside its <c>current\</c> folder - as opposed to the portable zip, a
    /// build output, or a copy somebody dropped in a folder. Only an
    /// installed copy gets the no-prompt tasks: its folder is Program
    /// Files' or made Administrators' (see InstallFolderGuard). A task that
    /// elevated a file in an ordinary folder without a prompt would be the
    /// UAC bypass the install location exists to prevent; a portable copy
    /// is prompted at every start instead, which is the honest price.
    /// </summary>
    public static bool IsInstalledCopy(string executablePath) =>
        InstallFolderGuard.RootOf(executablePath) is { } root
        && File.Exists(Path.Combine(root, "Update.exe"));

    /// <summary>Whether the logon task exists and points at this executable.</summary>
    public static bool StartsWithWindows(string executablePath) => TaskPointsAt(StartupTask, executablePath);

    /// <summary>Enables or disables start at sign-in. Elevated only.</summary>
    /// <returns>False when nothing needed changing.</returns>
    public static bool SetStartWithWindows(bool enabled, string executablePath)
    {
        if (enabled == StartsWithWindows(executablePath)) return false;
        if (!enabled) return CardSwitchTasks.Schtasks($"/delete /tn \"{StartupTask}\" /f") == 0;
        if (!IsInstalledCopy(executablePath))
            throw new InvalidOperationException(
                "Start with Windows is for an installed copy. This one runs from a folder any program could " +
                "write to, and starting it elevated at sign-in without a prompt would let such a program run as " +
                "administrator. Install Nextcalibur with its installer to start it with Windows.");
        return Create(StartupTask, executablePath, $"--tray {ViaTaskArgument}", logon: true);
    }

    /// <summary>Removes both tasks. Elevated only; part of the uninstall.</summary>
    public static void RemoveTasks()
    {
        CardSwitchTasks.Schtasks($"/delete /tn \"{OpenTask}\" /f");
        CardSwitchTasks.Schtasks($"/delete /tn \"{StartupTask}\" /f");
    }

    /// <summary>Whether a task exists whose command is this executable - so a moved or updated copy re-registers.</summary>
    private static bool TaskPointsAt(string task, string executablePath)
    {
        var xml = CardSwitchTasks.SchtasksOutput($"/query /tn \"{task}\" /xml");
        // schtasks writes the path XML-escaped; compare like with like, or a
        // folder with an ampersand in its name never matches and every
        // start asks for elevation again.
        return xml is not null && xml.Contains($"<Command>{Xml(executablePath)}</Command>", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// The three characters a text node cannot carry, escaped - a path can
    /// have an ampersand. Only those three: schtasks writes quotes and
    /// apostrophes back as they are, and the comparison must match its output.
    /// </summary>
    private static string Xml(string text) => text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

    private static bool Create(string task, string executablePath, string arguments, bool logon)
    {
        var sid = WindowsIdentity.GetCurrent().User?.Value;
        if (sid is null) return false;

        var trigger = logon
            ? $"""
                  <Triggers>
                    <LogonTrigger>
                      <Enabled>true</Enabled>
                      <UserId>{sid}</UserId>
                    </LogonTrigger>
                  </Triggers>
              """
            : "  <Triggers />";

        var xml = $"""
            <?xml version="1.0" encoding="UTF-16"?>
            <Task version="1.4" xmlns="http://schemas.microsoft.com/windows/2004/02/mit/task">
              <RegistrationInfo>
                <Description>Starts Nextcalibur with the rights it runs with, without a prompt. Registered by Nextcalibur; removed by its uninstaller.</Description>
              </RegistrationInfo>
            {trigger}
              <Principals>
                <Principal id="Author">
                  <UserId>{sid}</UserId>
                  <LogonType>InteractiveToken</LogonType>
                  <RunLevel>HighestAvailable</RunLevel>
                </Principal>
              </Principals>
              <Settings>
                <AllowStartOnDemand>true</AllowStartOnDemand>
                <DisallowStartIfOnBatteries>false</DisallowStartIfOnBatteries>
                <StopIfGoingOnBatteries>false</StopIfGoingOnBatteries>
                <ExecutionTimeLimit>PT0S</ExecutionTimeLimit>
                <MultipleInstancesPolicy>IgnoreNew</MultipleInstancesPolicy>
                <StartWhenAvailable>false</StartWhenAvailable>
                <Hidden>false</Hidden>
              </Settings>
              <Actions Context="Author">
                <Exec>
                  <Command>{Xml(executablePath)}</Command>
                  <Arguments>{Xml(arguments)}</Arguments>
                  <WorkingDirectory>{Xml(Path.GetDirectoryName(executablePath) ?? string.Empty)}</WorkingDirectory>
                </Exec>
              </Actions>
            </Task>
            """;

        var file = Path.Combine(Path.GetTempPath(), $"nextcalibur-task-{(logon ? "startup" : "open")}.xml");
        File.WriteAllText(file, xml, System.Text.Encoding.Unicode);
        try
        {
            return CardSwitchTasks.Schtasks($"/create /tn \"{task}\" /xml \"{file}\" /f") == 0;
        }
        finally
        {
            try { File.Delete(file); } catch (IOException) { }
        }
    }
}
