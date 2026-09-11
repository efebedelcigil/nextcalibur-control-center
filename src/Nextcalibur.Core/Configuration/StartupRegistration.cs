using System.Security.Principal;
using Microsoft.Win32;
using Nextcalibur.Core.Hardware;

namespace Nextcalibur.Core.Configuration;

/// <summary>
/// Registers the application to start with Windows, for the current user.
///
/// A logon task, not a <c>Run</c> entry. The Run entry was tried first and
/// Windows 11 build 29661 skipped it at sign-in - the shell's own log shows
/// the key enumerated and the entry never executed, with or without Task
/// Manager's "enabled" record beside it, while a neighbouring entry ran. The
/// vendor's software starts from a logon task, and that is the mechanism
/// this machine demonstrably honours. Registered in the user's own context:
/// no elevation, no privileges, and the person can see and disable it in
/// Task Scheduler under <c>Nextcalibur</c>.
/// </summary>
public static class StartupRegistration
{
    private const string TaskName = @"\Nextcalibur\Start with Windows";

    /// <summary>The Run entry earlier versions wrote; removed whenever this is set, either way.</summary>
    private const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string ApprovedKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run";
    private const string ValueName = "Nextcalibur";

    public static bool IsEnabled => CardSwitchTasks.Schtasks($"/query /tn \"{TaskName}\"") == 0;

    /// <summary>Enables or disables launch at sign-in.</summary>
    /// <returns>False when the task already said what we were about to say.</returns>
    /// <exception cref="InvalidOperationException">Windows refused to register the task.</exception>
    public static bool Set(bool enabled, string executablePath)
    {
        RemoveOldRunEntry();

        if (enabled == IsEnabled) return false;

        if (!enabled)
            return CardSwitchTasks.Schtasks($"/delete /tn \"{TaskName}\" /f") == 0;

        var sid = WindowsIdentity.GetCurrent().User?.Value
            ?? throw new InvalidOperationException("Could not determine the current account.");

        var xml = $"""
            <?xml version="1.0" encoding="UTF-16"?>
            <Task version="1.4" xmlns="http://schemas.microsoft.com/windows/2004/02/mit/task">
              <RegistrationInfo>
                <Description>Starts Nextcalibur in the notification area when you sign in. Turn off from Nextcalibur's tray menu.</Description>
              </RegistrationInfo>
              <Triggers>
                <LogonTrigger>
                  <Enabled>true</Enabled>
                  <UserId>{sid}</UserId>
                </LogonTrigger>
              </Triggers>
              <Principals>
                <Principal id="Author">
                  <UserId>{sid}</UserId>
                  <LogonType>InteractiveToken</LogonType>
                  <RunLevel>LeastPrivilege</RunLevel>
                </Principal>
              </Principals>
              <Settings>
                <AllowStartOnDemand>true</AllowStartOnDemand>
                <DisallowStartIfOnBatteries>false</DisallowStartIfOnBatteries>
                <StopIfGoingOnBatteries>false</StopIfGoingOnBatteries>
                <ExecutionTimeLimit>PT0S</ExecutionTimeLimit>
                <MultipleInstancesPolicy>IgnoreNew</MultipleInstancesPolicy>
                <StartWhenAvailable>false</StartWhenAvailable>
              </Settings>
              <Actions Context="Author">
                <Exec>
                  <Command>{executablePath}</Command>
                  <Arguments>--tray</Arguments>
                </Exec>
              </Actions>
            </Task>
            """;

        var file = Path.Combine(Path.GetTempPath(), "nextcalibur-startup.xml");
        File.WriteAllText(file, xml, System.Text.Encoding.Unicode);
        try
        {
            if (CardSwitchTasks.Schtasks($"/create /tn \"{TaskName}\" /xml \"{file}\" /f") != 0)
                throw new InvalidOperationException("Windows refused to register the start-up task.");
            return true;
        }
        finally
        {
            try { File.Delete(file); } catch (IOException) { }
        }
    }

    private static void RemoveOldRunEntry()
    {
        try
        {
            using var run = Registry.CurrentUser.OpenSubKey(RunKey, writable: true);
            run?.DeleteValue(ValueName, throwOnMissingValue: false);
            using var approved = Registry.CurrentUser.OpenSubKey(ApprovedKey, writable: true);
            approved?.DeleteValue(ValueName, throwOnMissingValue: false);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException)
        {
        }
    }
}
