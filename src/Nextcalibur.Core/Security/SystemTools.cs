namespace Nextcalibur.Core.Security;

/// <summary>
/// Full paths for the Windows tools this application runs.
///
/// A process that starts <c>schtasks.exe</c> by name lets Windows look for
/// it, and the search goes through the current directory before the
/// system folder. This process is elevated, and its current directory is
/// whatever the unelevated launcher had - the folder the person opened the
/// portable copy from, say - which anything running as the account can
/// write to. A <c>schtasks.exe</c> planted there would run as
/// administrator. So every tool is named by its full path, under the
/// system folder, and nothing is searched for.
/// </summary>
public static class SystemTools
{
    private static string System32(string name) => Path.Combine(Environment.SystemDirectory, name);
    private static string Windows(string name) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), name);

    public static string Schtasks => System32("schtasks.exe");
    public static string Pnputil => System32("pnputil.exe");
    public static string Powercfg => System32("powercfg.exe");
    public static string Cmd => System32("cmd.exe");
    public static string PowerShell => System32(@"WindowsPowerShell\v1.0\powershell.exe");
    public static string Explorer => Windows("explorer.exe");

    /// <summary>
    /// A file only administrators can reach, with a name nobody can guess,
    /// created new: for anything this elevated process writes and then
    /// runs or hands to another program. The account's own temporary
    /// folder is the wrong place for that - anything running as the
    /// account could rewrite the file between the write and the run.
    /// <c>%WINDIR%\Temp</c> lets ordinary accounts add files but not list,
    /// read or replace what others put there; a random name and
    /// <see cref="FileMode.CreateNew"/> close the rest.
    /// </summary>
    public static FileStream CreateProtectedTemporaryFile(string suffix, out string path)
    {
        var folder = Windows("Temp");
        for (var attempt = 0; attempt < 8; attempt++)
        {
            path = Path.Combine(folder, "nextcalibur-" + Guid.NewGuid().ToString("N") + suffix);
            try
            {
                return new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read);
            }
            catch (IOException) when (File.Exists(path))
            {
                // Taken - by chance, or by somebody guessing. Another name.
            }
        }
        throw new IOException("Could not create a protected temporary file.");
    }
}
