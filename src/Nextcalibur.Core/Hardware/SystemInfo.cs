using System.Management;
using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Hardware;

/// <summary>Memory or storage use at a moment in time.</summary>
/// <param name="UsedBytes">Bytes in use.</param>
/// <param name="TotalBytes">Bytes available in total.</param>
public readonly record struct StorageUse(ulong UsedBytes, ulong TotalBytes)
{
    public double Percent => TotalBytes == 0 ? 0 : UsedBytes * 100.0 / TotalBytes;

    /// <summary>
    /// The vendor software writes these as "14,9/31,7GB". The separator follows
    /// the user's locale, so the machine's own formatting is used rather than a
    /// hardcoded comma.
    /// </summary>
    public string Describe()
    {
        const double gb = 1024d * 1024 * 1024;
        return $"{UsedBytes / gb:N1}/{TotalBytes / gb:N1}GB";
    }
}

/// <summary>
/// Reads the parts of the machine that are not behind the firmware mailbox:
/// memory, storage, and what the processor and graphics card call themselves.
///
/// Device names are read at runtime and never written into the source. This
/// repository is public: a hardcoded model would be wrong on every other
/// machine, and would publish a detail of the author's own.
/// </summary>
public static class SystemInfo
{
    [StructLayout(LayoutKind.Sequential)]
    private struct MemoryStatusEx
    {
        public uint Length;
        public uint MemoryLoad;
        public ulong TotalPhys;
        public ulong AvailPhys;
        public ulong TotalPageFile;
        public ulong AvailPageFile;
        public ulong TotalVirtual;
        public ulong AvailVirtual;
        public ulong AvailExtendedVirtual;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GlobalMemoryStatusEx(ref MemoryStatusEx buffer);

    /// <summary>
    /// Physical memory in use. Uses the kernel call rather than WMI — this is
    /// polled, and a WMI query for it costs orders of magnitude more.
    /// </summary>
    public static StorageUse Memory()
    {
        var status = new MemoryStatusEx { Length = (uint)Marshal.SizeOf<MemoryStatusEx>() };
        if (!GlobalMemoryStatusEx(ref status)) return default;

        return new StorageUse(status.TotalPhys - status.AvailPhys, status.TotalPhys);
    }

    /// <summary>One fixed drive: its letter, its label if it has one, and its use.</summary>
    /// <param name="Letter">"C:", "D:" - what the person calls it.</param>
    /// <param name="Label">The volume label, or empty.</param>
    /// <param name="Use">Used and total bytes.</param>
    public readonly record struct DriveUse(string Letter, string Label, StorageUse Use)
    {
        /// <summary>"C:" or "D: Games" - the label only when it adds something.</summary>
        public string Name => string.IsNullOrWhiteSpace(Label) ? Letter : $"{Letter} {Label}";
    }

    /// <summary>
    /// Every fixed drive that is ready, the Windows drive first. Removable
    /// media and network shares are not the laptop's storage and are left
    /// out. One <c>DriveInfo</c> read per drive, nothing spun up: the
    /// numbers come from the volume, not the disk.
    /// </summary>
    public static IReadOnlyList<DriveUse> FixedDrives()
    {
        var system = Path.GetPathRoot(Environment.SystemDirectory);
        var drives = new List<DriveUse>();
        try
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                try
                {
                    if (drive.DriveType != DriveType.Fixed || !drive.IsReady) continue;
                    var use = new StorageUse((ulong)(drive.TotalSize - drive.TotalFreeSpace), (ulong)drive.TotalSize);
                    drives.Add(new DriveUse(drive.Name.TrimEnd('\\'), drive.VolumeLabel, use));
                }
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
                {
                    // A drive that will not answer is not a drive to show.
                }
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
        }

        return drives
            .OrderBy(d => string.Equals(d.Letter + "\\", system, StringComparison.OrdinalIgnoreCase) ? 0 : 1)
            .ThenBy(d => d.Letter, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    /// <summary>
    /// Space used on the drive Windows is installed on. Other drives are the
    /// user's business, not this application's.
    /// </summary>
    public static StorageUse SystemDrive()
    {
        try
        {
            var root = Path.GetPathRoot(Environment.SystemDirectory);
            if (root is null) return default;

            var drive = new DriveInfo(root);
            if (!drive.IsReady) return default;

            return new StorageUse(
                (ulong)(drive.TotalSize - drive.TotalFreeSpace),
                (ulong)drive.TotalSize);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException)
        {
            return default;
        }
    }

    /// <summary>The processor's own name, or null when it cannot be read.</summary>
    public static string? ProcessorName() =>
        QueryFirst("SELECT Name FROM Win32_Processor", "Name");

    /// <summary>
    /// The graphics card's name. Prefers a discrete adapter when the machine has
    /// one, since that is the part the user thinks of as "the graphics card".
    /// </summary>
    public static string? GraphicsName()
    {
        try
        {
            using var searcher = new ManagementObjectSearcher(
                "SELECT Name, AdapterCompatibility FROM Win32_VideoController");

            string? fallback = null;

            foreach (ManagementObject gpu in searcher.Get())
            {
                using (gpu)
                {
                    var name = gpu["Name"] as string;
                    if (string.IsNullOrWhiteSpace(name)) continue;

                    var vendor = gpu["AdapterCompatibility"] as string ?? string.Empty;
                    if (vendor.Contains("NVIDIA", StringComparison.OrdinalIgnoreCase) ||
                        vendor.Contains("Advanced Micro Devices", StringComparison.OrdinalIgnoreCase))
                    {
                        return Tidy(name);
                    }

                    fallback ??= name;
                }
            }

            return fallback is null ? null : Tidy(fallback);
        }
        catch (ManagementException)
        {
            return null;
        }
    }

    private static string? QueryFirst(string query, string property)
    {
        try
        {
            using var searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject item in searcher.Get())
            {
                using (item)
                {
                    if (item[property] is string value && !string.IsNullOrWhiteSpace(value))
                        return Tidy(value);
                }
            }
        }
        catch (ManagementException)
        {
            // An unreadable name is shown as nothing rather than as an error.
        }
        return null;
    }

    /// <summary>
    /// Trims the noise vendors put in these strings — trademark symbols, "CPU",
    /// the clock speed — leaving the part a person would recognise.
    ///
    /// Internal rather than private so it can be tested. It is string handling
    /// against strings this project cannot choose, and when it goes wrong it
    /// goes wrong quietly: a name that is merely a bit uglier, or suddenly
    /// empty.
    /// </summary>
    internal static string Tidy(string name)
    {
        var cleaned = name
            .Replace("(R)", string.Empty, StringComparison.OrdinalIgnoreCase)
            .Replace("(TM)", string.Empty, StringComparison.OrdinalIgnoreCase)
            .Replace("(C)", string.Empty, StringComparison.OrdinalIgnoreCase);

        var cpuMarker = cleaned.IndexOf(" CPU ", StringComparison.OrdinalIgnoreCase);
        if (cpuMarker > 0) cleaned = cleaned[..cpuMarker];

        return string.Join(' ', cleaned.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    }
}
