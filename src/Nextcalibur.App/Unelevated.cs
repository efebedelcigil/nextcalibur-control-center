using System.Diagnostics;
using System.Runtime.InteropServices;
using Nextcalibur.Core.Configuration;

namespace Nextcalibur.App;

/// <summary>
/// Opens things as the person, not as administrator.
///
/// This process runs elevated, and everything it starts inherits that:
/// a browser opened for a web page would run as administrator for the rest
/// of its life, with every page it visits. So a link is opened with the
/// desktop shell's own token - the medium one Explorer runs with - taken
/// from the running Explorer, which an administrator may do. If there is
/// no Explorer to borrow from, the link is still opened, elevated, and the
/// log says so.
/// </summary>
internal static class Unelevated
{
    /// <summary>Opens a URL or a file with its default handler, at the desktop's own level.</summary>
    public static void Open(string target)
    {
        if (TryStartAsDesktop($"explorer.exe \"{target}\"")) return;

        Log.Warn("shell", "No desktop token to borrow; opening elevated: " + target);
        Process.Start(new ProcessStartInfo(target) { UseShellExecute = true });
    }

    private static bool TryStartAsDesktop(string commandLine)
    {
        var shell = GetShellWindow();
        if (shell == IntPtr.Zero) return false;
        GetWindowThreadProcessId(shell, out var pid);
        if (pid == 0) return false;

        var process = OpenProcess(ProcessQueryInformation, false, pid);
        if (process == IntPtr.Zero) return false;
        var token = IntPtr.Zero;
        var duplicate = IntPtr.Zero;
        try
        {
            if (!OpenProcessToken(process, TokenDuplicate | TokenQuery | TokenAssignPrimary, out token)) return false;
            if (!DuplicateTokenEx(token, MaximumAllowed, IntPtr.Zero, SecurityImpersonation, TokenPrimary, out duplicate)) return false;

            var startup = new StartupInfo { cb = Marshal.SizeOf<StartupInfo>() };
            if (!CreateProcessWithTokenW(duplicate, 0, null, commandLine, 0, IntPtr.Zero, null, ref startup, out var info))
                return false;
            CloseHandle(info.hThread);
            CloseHandle(info.hProcess);
            return true;
        }
        finally
        {
            if (duplicate != IntPtr.Zero) CloseHandle(duplicate);
            if (token != IntPtr.Zero) CloseHandle(token);
            CloseHandle(process);
        }
    }

    private const uint ProcessQueryInformation = 0x0400;
    private const uint TokenDuplicate = 0x0002, TokenQuery = 0x0008, TokenAssignPrimary = 0x0001;
    private const uint MaximumAllowed = 0x02000000;
    private const int SecurityImpersonation = 2;
    private const int TokenPrimary = 1;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct StartupInfo
    {
        public int cb;
        public string? lpReserved, lpDesktop, lpTitle;
        public int dwX, dwY, dwXSize, dwYSize, dwXCountChars, dwYCountChars, dwFillAttribute, dwFlags;
        public short wShowWindow, cbReserved2;
        public IntPtr lpReserved2, hStdInput, hStdOutput, hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct ProcessInformation
    {
        public IntPtr hProcess, hThread;
        public int dwProcessId, dwThreadId;
    }

    [DllImport("user32.dll")] private static extern IntPtr GetShellWindow();
    [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint pid);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern IntPtr OpenProcess(uint access, bool inherit, uint pid);
    [DllImport("advapi32.dll", SetLastError = true)] private static extern bool OpenProcessToken(IntPtr process, uint access, out IntPtr token);
    [DllImport("advapi32.dll", SetLastError = true)] private static extern bool DuplicateTokenEx(IntPtr token, uint access, IntPtr attributes, int impersonationLevel, int tokenType, out IntPtr duplicate);
    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool CreateProcessWithTokenW(IntPtr token, uint logonFlags, string? application, string commandLine, uint creationFlags, IntPtr environment, string? currentDirectory, ref StartupInfo startup, out ProcessInformation info);
    [DllImport("kernel32.dll")] private static extern bool CloseHandle(IntPtr handle);
}
