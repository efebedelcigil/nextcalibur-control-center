using Path = System.IO.Path;
using Directory = System.IO.Directory;
using IOException = System.IO.IOException;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Nextcalibur.App;

/// <summary>
/// One identity for the taskbar.
///
/// Windows groups taskbar buttons by Application User Model ID. A pinned
/// shortcut carries one; a running window carries its process's. When the
/// two differ - the shortcut's derived from one path, the process launched
/// through Velopack's stub from another - the pinned icon and the running
/// application become two buttons, which is what the owner saw on 12
/// September 2026. So the process claims one fixed identity before any
/// window exists, and every shortcut of ours it can find is stamped with the
/// same one.
/// </summary>
public static class AppIdentity
{
    /// <summary>The identity. Fixed for the life of the product; changing it splits the pins.</summary>
    public const string Id = "Nextcalibur.ControlCenter";

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    private static extern int SetCurrentProcessExplicitAppUserModelID(string appId);

    /// <summary>
    /// The same identity on the window itself, with what pinning it should
    /// start. The process's identity alone did not hold: started elevated
    /// through the scheduled task, or shown again from the tray, the window
    /// came up beside the pinned icon instead of on it (owner, 12 October
    /// 2026). A window's own property outranks the process's, and the
    /// relaunch values make a pin taken from the running window the same as
    /// the installed shortcut.
    /// </summary>
    public static void ClaimWindow(IntPtr hwnd)
    {
        if (hwnd == IntPtr.Zero || Environment.ProcessPath is not { } exe) return;
        var iid = typeof(IPropertyStore).GUID;
        if (SHGetPropertyStoreForWindow(hwnd, ref iid, out var obj) != 0 || obj is not IPropertyStore store) return;
        try
        {
            Set(store, 5, Id);                              // System.AppUserModel.ID
            Set(store, 2, "\"" + exe + "\"");               // System.AppUserModel.RelaunchCommand
            Set(store, 3, exe + ",0");                      // System.AppUserModel.RelaunchIconResource
            Set(store, 4, "Nextcalibur Control Center");    // System.AppUserModel.RelaunchDisplayNameResource
            store.Commit();
        }
        catch (Exception ex) when (ex is COMException or UnauthorizedAccessException) { }
        finally
        {
            Marshal.FinalReleaseComObject(store);
        }
    }

    private static void Set(IPropertyStore store, uint pid, string text)
    {
        var key = new PropertyKey(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), pid);
        var value = new PropVariant { vt = 31 /* VT_LPWSTR */, p = Marshal.StringToCoTaskMemUni(text) };
        try { store.SetValue(ref key, ref value); }
        finally { PropVariantClear(ref value); }
    }

    [DllImport("shell32.dll")]
    private static extern int SHGetPropertyStoreForWindow(IntPtr hwnd, ref Guid iid, [MarshalAs(UnmanagedType.Interface)] out object propertyStore);

    /// <summary>Called first thing in <c>Main</c>, before Velopack and before any window.</summary>
    public static void Claim()
    {
        try { SetCurrentProcessExplicitAppUserModelID(Id); }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException) { }
    }

    /// <summary>
    /// Stamps the identity onto our shortcuts: Start menu, desktop, and the
    /// taskbar pins. Only shortcuts whose target lives in our install folder
    /// are touched. Cheap - a handful of files - and idempotent.
    /// </summary>
    public static void StampShortcuts()
    {
        var install = Path.GetDirectoryName(Environment.ProcessPath);
        if (install is null) return;
        // The install root, whether we run from it or from current\ under it.
        var root = install.EndsWith("current", StringComparison.OrdinalIgnoreCase)
            ? Path.GetDirectoryName(install)! : install;

        var folders = new[]
        {
            Environment.GetFolderPath(Environment.SpecialFolder.Programs),
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar"),
        };

        foreach (var folder in folders)
        {
            if (!Directory.Exists(folder)) continue;
            foreach (var link in Directory.EnumerateFiles(folder, "*.lnk"))
            {
                try { StampIfOurs(link, root); }
                catch (Exception ex) when (ex is COMException or IOException or UnauthorizedAccessException) { }
            }
        }
    }

    /// <summary>The folders our shortcuts can be in: Start menu, desktop, the taskbar's pins.</summary>
    private static IEnumerable<string> ShortcutFolders()
    {
        yield return Environment.GetFolderPath(Environment.SpecialFolder.Programs);
        yield return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        yield return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            @"Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar");
    }

    /// <summary>
    /// Deletes every shortcut that points into the install folder - the
    /// Start-menu and desktop ones the installer made and any pin the person
    /// added. For the uninstall, which must leave nothing.
    /// </summary>
    public static int RemoveShortcutsPointingAt(string installRoot)
    {
        var removed = 0;
        foreach (var folder in ShortcutFolders())
        {
            if (!Directory.Exists(folder)) continue;
            foreach (var link in Directory.EnumerateFiles(folder, "*.lnk"))
            {
                try
                {
                    if (!PointsInto(link, installRoot)) continue;
                    System.IO.File.Delete(link);
                    removed++;
                }
                catch (Exception ex) when (ex is COMException or IOException or UnauthorizedAccessException) { }
            }
        }
        return removed;
    }

    private static bool PointsInto(string linkPath, string installRoot)
    {
        var shellLink = (IShellLinkW)new ShellLink();
        try
        {
            ((IPersistFile)shellLink).Load(linkPath, 0 /* STGM_READ */);
            var target = new System.Text.StringBuilder(1024);
            shellLink.GetPath(target, target.Capacity, IntPtr.Zero, 0);
            return target.ToString().StartsWith(installRoot, StringComparison.OrdinalIgnoreCase);
        }
        finally
        {
            Marshal.FinalReleaseComObject(shellLink);
        }
    }

    private static void StampIfOurs(string linkPath, string installRoot)
    {
        var shellLink = (IShellLinkW)new ShellLink();
        try
        {
            var persist = (IPersistFile)shellLink;
            persist.Load(linkPath, 2 /* STGM_READWRITE */);

            var target = new System.Text.StringBuilder(1024);
            shellLink.GetPath(target, target.Capacity, IntPtr.Zero, 0);
            if (!target.ToString().StartsWith(installRoot, StringComparison.OrdinalIgnoreCase)) return;

            var store = (IPropertyStore)shellLink;
            var key = new PropertyKey(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 5); // System.AppUserModel.ID

            store.GetValue(ref key, out var current);
            var existing = current.vt == 31 /* VT_LPWSTR */ ? Marshal.PtrToStringUni(current.p) : null;
            PropVariantClear(ref current);

            // The comment a shortcut shows in its Details tab and on hover:
            // what it opens, and which version - renewed after every update.
            var version = typeof(AppIdentity).Assembly.GetName().Version?.ToString(3) ?? "";
            var description = $"Nextcalibur Control Center {version}";
            var comment = new System.Text.StringBuilder(260);
            shellLink.GetDescription(comment, comment.Capacity);
            if (existing == Id && comment.ToString() == description) return;

            shellLink.SetDescription(description);
            if (existing != Id)
            {
                var value = new PropVariant { vt = 31, p = Marshal.StringToCoTaskMemUni(Id) };
                try
                {
                    store.SetValue(ref key, ref value);
                    store.Commit();
                }
                finally
                {
                    PropVariantClear(ref value);
                }
            }
            persist.Save(linkPath, true);
        }
        finally
        {
            Marshal.FinalReleaseComObject(shellLink);
        }
    }

    [DllImport("ole32.dll")]
    private static extern int PropVariantClear(ref PropVariant pvar);

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct PropertyKey
    {
        public Guid fmtid;
        public uint pid;
        public PropertyKey(Guid fmtid, uint pid) { this.fmtid = fmtid; this.pid = pid; }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct PropVariant
    {
        public ushort vt;
        public ushort r1, r2, r3;
        public IntPtr p;
        public IntPtr p2;
    }

    [ComImport, Guid("00021401-0000-0000-C000-000000000046")]
    private class ShellLink { }

    [ComImport, Guid("000214F9-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IShellLinkW
    {
        void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder file, int cch, IntPtr findData, uint flags);
        void GetIDList(out IntPtr pidl);
        void SetIDList(IntPtr pidl);
        void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder name, int cch);
        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string name);
        void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder dir, int cch);
        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string dir);
        void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder args, int cch);
        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string args);
        void GetHotkey(out short hotkey);
        void SetHotkey(short hotkey);
        void GetShowCmd(out int showCmd);
        void SetShowCmd(int showCmd);
        void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder path, int cch, out int icon);
        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string path, int icon);
        void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string path, uint reserved);
        void Resolve(IntPtr hwnd, uint flags);
        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string file);
    }

    [ComImport, Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IPropertyStore
    {
        void GetCount(out uint count);
        void GetAt(uint index, out PropertyKey key);
        void GetValue(ref PropertyKey key, out PropVariant value);
        void SetValue(ref PropertyKey key, ref PropVariant value);
        void Commit();
    }
}
