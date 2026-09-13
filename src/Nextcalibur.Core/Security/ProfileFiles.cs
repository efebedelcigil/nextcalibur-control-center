namespace Nextcalibur.Core.Security;

/// <summary>
/// The files this application keeps in the profile - settings, the lighting
/// state, the log - are written by an elevated process into folders the
/// account owns. A directory junction or a symbolic link put there by
/// anything running as the account would make the elevated process write,
/// or delete, wherever the link points. So a folder or file that is a
/// reparse point is not written to, and neither is anything under one.
///
/// A junction needs no privilege to create, which is what makes this worth
/// checking rather than assuming.
/// </summary>
public static class ProfileFiles
{
    /// <summary>
    /// Creates the folder if it is not there and says whether it is safe to
    /// write into: neither it nor anything between it and the boundary below
    /// is a link.
    ///
    /// The parents are checked before the folder is created, because
    /// creating a folder under a link is itself a write through the link.
    /// </summary>
    public static bool EnsureOrdinaryFolder(string folder)
    {
        try
        {
            folder = Path.GetFullPath(folder);
            foreach (var step in UpToBoundary(folder))
                if (Exists(step) && IsReparsePoint(step)) return false;

            Directory.CreateDirectory(folder);
            return !IsReparsePoint(folder);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException or NotSupportedException)
        {
            return false;
        }
    }

    /// <summary>True when the file may be opened for writing: it does not exist, or exists and is not a link.</summary>
    public static bool IsOrdinaryFileOrAbsent(string file)
    {
        try
        {
            file = Path.GetFullPath(file);
            foreach (var step in UpToBoundary(file))
                if (Exists(step) && IsReparsePoint(step)) return false;
            return true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException or NotSupportedException)
        {
            return false;
        }
    }

    /// <summary>
    /// The path itself and every folder above it, up to but not including
    /// the boundary: the account's profile, since that is the part of the
    /// tree the account can rearrange. A path outside the profile is walked
    /// to its drive's root - there is no reason to trust an unknown folder
    /// more than a known one.
    /// </summary>
    private static IEnumerable<string> UpToBoundary(string path)
    {
        var boundary = Boundary(path);
        for (var current = path;
             current is not null && !current.Equals(boundary, StringComparison.OrdinalIgnoreCase);
             current = Path.GetDirectoryName(current))
            yield return current;
    }

    private static string Boundary(string path)
    {
        var profile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        if (profile.Length > 0 && path.StartsWith(profile.TrimEnd('\\') + "\\", StringComparison.OrdinalIgnoreCase))
            return profile.TrimEnd('\\');
        return (Path.GetPathRoot(path) ?? string.Empty).TrimEnd('\\');
    }

    private static bool Exists(string path) => Directory.Exists(path) || File.Exists(path);

    private static bool IsReparsePoint(string path)
    {
        try
        {
            return (File.GetAttributes(path) & FileAttributes.ReparsePoint) != 0;
        }
        catch (Exception ex) when (ex is FileNotFoundException or DirectoryNotFoundException)
        {
            return false;
        }
    }
}
