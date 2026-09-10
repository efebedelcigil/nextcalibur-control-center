# Dump-UefiVars.ps1 - list the machine's UEFI variables and hash their contents,
# so two dumps taken in different graphics modes can be compared. Reads only.
#
#   (elevated)  .\Dump-UefiVars.ps1 -Label hybrid
#               .\Dump-UefiVars.ps1 -Diff hybrid,uma
#
# Why this exists: switching the display path leaves no trace in the registry,
# in the vendor's own files, or in driver configuration - a full before/after
# capture of all three came back identical. The vendor software still knows the
# mode after a reboot, so it is reading the setting back from somewhere Windows
# does not own. Firmware is what is left, and this is how to look.
#
# Admin is required because reading the firmware environment needs
# SeSystemEnvironmentPrivilege. That is a property of this diagnostic, not of
# Nextcalibur: the application itself never asks for elevation.
#
# Values are hashed rather than printed. Some of these variables hold Secure
# Boot keys and platform blobs measuring tens of kilobytes; a hash says "this
# changed" without dragging the contents into a repository that is public.

param(
    [string]$Label,
    [string[]]$Diff,
    [string]$OutDir = "$PSScriptRoot\trace"
)

$ErrorActionPreference = 'Stop'

if ($Diff) {
    if ($Diff.Count -ne 2) { Write-Host "Give two labels to compare." -ForegroundColor Red; exit 1 }
    $a = @{}; (Import-Csv "$OutDir\uefi-$($Diff[0]).csv") | ForEach-Object { $a["$($_.Guid)\$($_.Name)"] = "$($_.Size)/$($_.Hash)" }
    $b = @{}; (Import-Csv "$OutDir\uefi-$($Diff[1]).csv") | ForEach-Object { $b["$($_.Guid)\$($_.Name)"] = "$($_.Size)/$($_.Hash)" }

    $changed = 0
    foreach ($k in ($a.Keys + $b.Keys | Sort-Object -Unique)) {
        if ($a[$k] -eq $b[$k]) { continue }
        $changed++
        if (-not $a.ContainsKey($k)) { Write-Host "  + $k = $($b[$k])" -ForegroundColor Green }
        elseif (-not $b.ContainsKey($k)) { Write-Host "  - $k (was $($a[$k]))" -ForegroundColor Red }
        else { Write-Host "  ~ $k`n      $($a[$k])`n   -> $($b[$k])" -ForegroundColor Yellow }
    }
    Write-Host "`n$changed variable(s) differ between $($Diff[0]) and $($Diff[1])." -ForegroundColor Cyan
    exit 0
}

if (-not $Label) { Write-Host "Give -Label or -Diff." -ForegroundColor Red; exit 1 }

if (-not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()
    ).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    Write-Host "Run this one as administrator - the firmware environment is privileged." -ForegroundColor Red
    exit 1
}

Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;

public static class Uefi
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Luid { public uint Low; public int High; }

    [StructLayout(LayoutKind.Sequential)]
    public struct TokenPrivileges { public int Count; public Luid Luid; public int Attributes; }

    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool OpenProcessToken(IntPtr process, int access, out IntPtr token);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern bool LookupPrivilegeValueW(string system, string name, out Luid luid);

    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool AdjustTokenPrivileges(IntPtr token, bool disableAll,
        ref TokenPrivileges newState, int bufferLength, IntPtr previous, IntPtr returnLength);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetCurrentProcess();

    // Enables SeSystemEnvironmentPrivilege on this process.
    //
    // RtlAdjustPrivilege takes a bare integer, and the constant everybody
    // quotes for this privilege was rejected on this machine even though an
    // elevated token clearly held it, disabled. Asking the system for the LUID
    // by name removes the guess: whatever number the build uses, this finds it.
    public static string Enable()
    {
        IntPtr token;
        // TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY
        if (!OpenProcessToken(GetCurrentProcess(), 0x20 | 0x8, out token))
            return "OpenProcessToken failed: " + Marshal.GetLastWin32Error();

        Luid luid;
        if (!LookupPrivilegeValueW(null, "SeSystemEnvironmentPrivilege", out luid))
            return "LookupPrivilegeValue failed: " + Marshal.GetLastWin32Error();

        TokenPrivileges tp = new TokenPrivileges();
        tp.Count = 1;
        tp.Luid = luid;
        tp.Attributes = 0x2;   // SE_PRIVILEGE_ENABLED

        if (!AdjustTokenPrivileges(token, false, ref tp, Marshal.SizeOf(tp), IntPtr.Zero, IntPtr.Zero))
            return "AdjustTokenPrivileges failed: " + Marshal.GetLastWin32Error();

        // It reports success even when it enabled nothing, so the last error
        // has to be read on the success path too.
        int err = Marshal.GetLastWin32Error();
        if (err == 1300) return "the token does not hold SeSystemEnvironmentPrivilege";
        return null;
    }

    // Information class 1 asks for the names and vendor GUIDs of every variable
    // the firmware exposes. There is no documented Win32 call that enumerates
    // them; GetFirmwareEnvironmentVariable can only fetch a name you already
    // know, which is no use when the question is "what is there".
    [DllImport("ntdll.dll")]
    public static extern int NtEnumerateSystemEnvironmentValuesEx(
        int informationClass, IntPtr buffer, ref int length);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern int GetFirmwareEnvironmentVariableExW(
        string name, string guid, byte[] buffer, int size, out int attributes);
}
'@

$failure = [Uefi]::Enable()
if ($failure) { Write-Host "Could not read the firmware environment: $failure" -ForegroundColor Red; exit 3 }

# Ask for the size first, then the data. The store is a few tens of kilobytes.
$len = 0
$null = [Uefi]::NtEnumerateSystemEnvironmentValuesEx(1, [IntPtr]::Zero, [ref]$len)
if ($len -le 0) { $len = 65536 }
$buf = [Runtime.InteropServices.Marshal]::AllocHGlobal($len)
try {
    $status = [Uefi]::NtEnumerateSystemEnvironmentValuesEx(1, $buf, [ref]$len)
    if ($status -ne 0) { Write-Host ("Enumeration failed: 0x{0:X8}" -f $status) -ForegroundColor Red; exit 2 }

    $sha = [Security.Cryptography.SHA256]::Create()
    $rows = @()
    $offset = 0

    # Each entry is: next-offset (4), vendor GUID (16), then a null-terminated
    # wide name. A next-offset of zero ends the list.
    while ($true) {
        $entry = [IntPtr]::Add($buf, $offset)
        $next = [Runtime.InteropServices.Marshal]::ReadInt32($entry)
        $guidBytes = New-Object byte[] 16
        [Runtime.InteropServices.Marshal]::Copy([IntPtr]::Add($entry, 4), $guidBytes, 0, 16)
        $guid = (New-Object Guid (,$guidBytes)).ToString('B').ToUpper()
        $name = [Runtime.InteropServices.Marshal]::PtrToStringUni([IntPtr]::Add($entry, 20))

        if ($name) {
            $data = New-Object byte[] 65536
            $attrs = 0
            $size = [Uefi]::GetFirmwareEnvironmentVariableExW($name, $guid, $data, $data.Length, [ref]$attrs)
            if ($size -gt 0) {
                $hash = [BitConverter]::ToString($sha.ComputeHash($data[0..($size - 1)])).Replace('-', '').Substring(0, 16)
                # Small values are kept in full: a single byte that flips
                # between modes is the answer, and hiding it behind a hash
                # would mean another reboot to find out what it flipped to.
                # The bound is generous enough to cover EFI device paths, which
                # is how the console's own record of the display chip is read -
                # a hash said only that ConOut had changed length, and the
                # bytes were gone by the time that turned out to matter.
                $preview = if ($size -le 128) { [BitConverter]::ToString($data[0..($size - 1)]) } else { '' }
            } else {
                $hash = 'unreadable'; $preview = ''
            }
            $rows += [pscustomobject]@{ Guid = $guid; Name = $name; Size = $size; Hash = $hash; Preview = $preview }
        }

        if ($next -eq 0) { break }
        $offset += $next
        if ($offset -ge $len) { break }
    }
} finally {
    [Runtime.InteropServices.Marshal]::FreeHGlobal($buf)
}

if (-not (Test-Path $OutDir)) { New-Item -ItemType Directory -Path $OutDir | Out-Null }
$rows | Sort-Object Guid, Name | Export-Csv "$OutDir\uefi-$Label.csv" -NoTypeInformation -Encoding utf8
Write-Host "Captured $($rows.Count) firmware variables as '$Label'." -ForegroundColor Green
