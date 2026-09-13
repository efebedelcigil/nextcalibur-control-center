namespace Nextcalibur.Core.Security;

/// <summary>
/// The files this application keeps in the profile - settings, the lighting
/// state, the log - are written by an elevated process into folders the
/// account owns. A directory junction or a symbolic link put there by
/// anything running as the account would make the elevated process write
/// wherever the link points. So a folder or file that is a reparse point
/// is not written to, and the write is dropped rather than followed.
/// </summary>
public static class ProfileFiles
{
    /// <summary>Creates the folder if needed and says whether it is an ordinary folder - not a link, and under no link below the profile's own root.</summary>
    public static bool EnsureOrdinaryFolder(string folder)
    {
        try
        {
            Directory.CreateDirectory(folder);
            var root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var current = folder;
            while (current is not null && current.StartsWith(root, StringComparison.OrdinalIgnoreCase)
                   && !current.Equals(root, StringComparison.OrdinalIgnoreCase))
            {
                if (IsReparsePoint(current)) return false;
                current = Path.GetDirectoryName(current);
            }
            return true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return false;
        }
    }

    /// <summary>True when the file may be opened for writing: it does not exist, or exists and is not a link.</summary>
    public static bool IsOrdinaryFileOrAbsent(string file)
    {
        try
        {
            return !File.Exists(file) || !IsReparsePoint(file);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return false;
        }
    }

    private static bool IsReparsePoint(string path) =>
        (File.GetAttributes(path) & FileAttributes.ReparsePoint) != 0;
}
