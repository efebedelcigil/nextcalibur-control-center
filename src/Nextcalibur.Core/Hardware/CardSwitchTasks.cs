using System.Diagnostics;
using System.Security.Principal;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// Two scheduled tasks that switch the discrete graphics card off and on
/// without an elevation prompt each time.
///
/// Disabling a device needs administrator, and Windows asks every time. The
/// vendor's software never asks because its daemon is started at logon by a
/// scheduled task with the highest privileges and stays elevated all day.
/// Nextcalibur will not run that way - but the same mechanism, pointed at one
/// narrow job, gives the same result: a task that runs this executable with
/// <c>--card off</c> (and one with <c>--card on</c>), highest privileges, no
/// trigger, started on demand. Registering them needs administrator once, which
/// is folded into the one elevation the application already asks for. Running
/// them does not.
///
/// This works for an administrator account, which is what a personal laptop
/// has. A standard account cannot elevate without a password whatever the
/// mechanism, and falls back to the prompt.
///
/// Registered from an XML definition rather than from <c>schtasks</c>'s
/// command-line switches, because those take dates in the current locale's
/// format and this machine's locale is Turkish. XML has no such opinion.
/// </summary>
public static class CardSwitchTasks
{
    private const string Folder = @"\Nextcalibur\";
    public const string OffTask = Folder + "Card off";
    public const string OnTask = Folder + "Card on";

    /// <summary>The argument the elevated task passes back to this executable.</summary>
    public const string Argument = "--card";

    /// <summary>True when both tasks are registered.</summary>
    public static bool Registered() => Query(OffTask) && Query(OnTask);

    /// <summary>
    /// Registers both tasks to run the given executable. Needs administrator.
    /// Idempotent: an existing task is replaced, so a moved executable is
    /// picked up by registering again.
    /// </summary>
    public static bool Register(string executablePath)
    {
        var user = WindowsIdentity.GetCurrent().User?.Value
            ?? throw new InvalidOperationException("Could not determine the current account.");

        return Create(OffTask, executablePath, "off", user) && Create(OnTask, executablePath, "on", user);
    }

    /// <summary>Removes both tasks. Needs administrator.</summary>
    public static void Unregister()
    {
        Schtasks($"/delete /tn \"{OffTask}\" /f");
        Schtasks($"/delete /tn \"{OnTask}\" /f");
    }

    /// <summary>
    /// Starts the task that switches the card off or on. Needs no privileges.
    /// </summary>
    /// <returns>False when the task is not registered or would not start.</returns>
    public static bool Run(bool enable) => Schtasks($"/run /tn \"{(enable ? OnTask : OffTask)}\"") == 0;

    private static bool Query(string task) => Schtasks($"/query /tn \"{task}\"") == 0;

    private static bool Create(string task, string executable, string direction, string userSid)
    {
        var xml = $"""
            <?xml version="1.0" encoding="UTF-16"?>
            <Task version="1.4" xmlns="http://schemas.microsoft.com/windows/2004/02/mit/task">
              <RegistrationInfo>
                <Description>Switches the discrete graphics card {direction} for Nextcalibur, without an elevation prompt. Started on demand only.</Description>
              </RegistrationInfo>
              <Principals>
                <Principal id="Author">
                  <UserId>{userSid}</UserId>
                  <LogonType>InteractiveToken</LogonType>
                  <RunLevel>HighestAvailable</RunLevel>
                </Principal>
              </Principals>
              <Settings>
                <AllowStartOnDemand>true</AllowStartOnDemand>
                <DisallowStartIfOnBatteries>false</DisallowStartIfOnBatteries>
                <StopIfGoingOnBatteries>false</StopIfGoingOnBatteries>
                <Hidden>true</Hidden>
                <ExecutionTimeLimit>PT1M</ExecutionTimeLimit>
                <MultipleInstancesPolicy>IgnoreNew</MultipleInstancesPolicy>
                <StartWhenAvailable>false</StartWhenAvailable>
              </Settings>
              <Actions Context="Author">
                <Exec>
                  <Command>{executable}</Command>
                  <Arguments>{Argument} {direction}</Arguments>
                </Exec>
              </Actions>
            </Task>
            """;

        var file = Path.Combine(Path.GetTempPath(), $"nextcalibur-card-{direction}.xml");
        File.WriteAllText(file, xml, System.Text.Encoding.Unicode);
        try
        {
            return Schtasks($"/create /tn \"{task}\" /xml \"{file}\" /f") == 0;
        }
        finally
        {
            try { File.Delete(file); } catch (IOException) { }
        }
    }

    /// <summary>Runs schtasks and returns what it printed, or null when it failed.</summary>
    internal static string? SchtasksOutput(string arguments)
    {
        try
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = "schtasks.exe",
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                // The XML declares UTF-16 but schtasks writes the console
                // code page when redirected; read it as the console does.
                StandardOutputEncoding = ConsoleEncoding(),
            });
            if (process is null) return null;
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit(15000);
            return process.HasExited && process.ExitCode == 0 ? output : null;
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            return null;
        }
    }

    private static System.Text.Encoding ConsoleEncoding()
    {
        try
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            return System.Text.Encoding.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
        }
        catch (Exception ex) when (ex is ArgumentException or NotSupportedException)
        {
            return System.Text.Encoding.Default;
        }
    }

    internal static int Schtasks(string arguments)
    {
        try
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = "schtasks.exe",
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            });
            if (process is null) return -1;
            process.WaitForExit(15000);
            return process.HasExited ? process.ExitCode : -1;
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            return -1;
        }
    }
}
