# Grant-MailboxAccess.ps1 - let an ordinary account read the firmware mailbox.
#
#   (elevated)  .\Grant-MailboxAccess.ps1            grant to the current user
#   (elevated)  .\Grant-MailboxAccess.ps1 -AllUsers  grant to everyone who logs in
#   (elevated)  .\Grant-MailboxAccess.ps1 -Revoke    put it back the way it was
#               .\Grant-MailboxAccess.ps1 -Show      report, no changes, no admin
#
# Why this is needed at all, and why it was invisible for weeks:
#
# Every reading this project takes comes from one ACPI data block, declared by
# the machine's own firmware and surfaced by Windows' in-box wmiacpi.sys. No
# vendor driver is involved. But a kernel-WMI data block with no security
# descriptor of its own falls back to a default that admits administrators only,
# and this block has none.
#
# The vendor's Control Center installs one. So on any machine where that
# software has ever been installed, an ordinary account can read the block, and
# an application like this one appears to need no privileges. Uninstall it and
# the readings stop - which is exactly what happened on 10 September 2026, and
# is how this was found.
#
# What is granted, and to whom, is a real decision rather than a formality. The
# block accepts writes as well as reads: it is how the keyboard lighting is set,
# and how fan control would work if it existed. Granting it to every local
# account - which is what the vendor does - hands every local process a route to
# the embedded controller. The default here is one account, which is narrower
# and enough for a laptop with one person on it.
#
# Reversible: -Revoke deletes the value and the default comes back.

param(
    [switch]$AllUsers,
    [switch]$Revoke,
    [switch]$Show
)

$ErrorActionPreference = 'Stop'

# The mailbox data block, from the machine's DSDT.
#
# Written without braces and in lower case, because that is how every one of the
# 579 entries already under that key is named, and a value written any other way
# is simply never consulted - it sits in the registry looking correct while the
# block stays administrators-only.
$Guid = '644c5791-b7b0-4123-a90b-e93876e0daad'
$SecurityKey = 'HKLM:\SYSTEM\CurrentControlSet\Control\WMI\Security'

# WMIGUID_QUERY | WMIGUID_SET | WMIGUID_NOTIFICATION | WMIGUID_READ_DESCRIPTION
# | WMIGUID_EXECUTE, plus READ_CONTROL and SYNCHRONIZE. The same mask Windows
# uses for the blocks it ships with security of their own; SET is not optional,
# because writing is how the keyboard is lit.
$Access = '0x12001f'

function Test-Admin {
    ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()
    ).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
}

function Get-CurrentDescriptor {
    $existing = Get-ItemProperty $SecurityKey -Name $Guid -ErrorAction SilentlyContinue
    if (-not $existing) { return $null }
    return $existing.$Guid
}

# Reading that key needs administrator too, so an ordinary account cannot tell
# "there is no descriptor" from "I am not allowed to look". Saying the first
# when it means the second is how a diagnostic sends somebody the wrong way.
function Test-CanReadSecurityKey {
    try { $null = Get-ItemProperty $SecurityKey -ErrorAction Stop; return $true }
    catch { return $false }
}

function Show-State {
    if (-not (Test-CanReadSecurityKey)) {
        Write-Host "Cannot read the security key from here - that needs administrator." -ForegroundColor Yellow
        Write-Host "What this account can actually do is below, which is the useful half anyway."
    }
    $sd = Get-CurrentDescriptor
    if ($null -eq $sd) {
        if (Test-CanReadSecurityKey) {
            Write-Host "No descriptor: the block is administrators-only (Windows' default)." -ForegroundColor Yellow
        }
    } else {
        $raw = New-Object Security.AccessControl.RawSecurityDescriptor ($sd, 0)
        Write-Host "Descriptor present:" -ForegroundColor Green
        Write-Host "  $($raw.GetSddlForm('All'))"
    }

    # The question people actually care about, answered directly.
    $instance = Get-CimInstance -Namespace root\wmi -ClassName RW_GMWMI -ErrorAction SilentlyContinue
    if ($instance) {
        Write-Host "This account can see the mailbox: $($instance.InstanceName)" -ForegroundColor Green
    } else {
        Write-Host "This account cannot see the mailbox." -ForegroundColor Yellow
    }
}

if ($Show) { Show-State; exit 0 }

if (-not (Test-Admin)) {
    Write-Host "Run this as administrator. Granting access is a one-time change;" -ForegroundColor Red
    Write-Host "afterwards Nextcalibur runs as an ordinary user." -ForegroundColor Red
    exit 1
}

$BackupFile = Join-Path $PSScriptRoot 'trace\wmi-sd-before.txt'

if ($Revoke) {
    # Put back exactly what was there, not "nothing". Deleting the value leaves
    # no descriptor at all, which is not the same state: this machine had one,
    # granting administrators only. Undo should mean undo.
    if (Test-Path $BackupFile) {
        $hex = (Get-Content $BackupFile -Raw).Trim()
        $bytes = [byte[]]($hex -split '-' | ForEach-Object { [Convert]::ToByte($_, 16) })
        New-ItemProperty $SecurityKey -Name $Guid -PropertyType Binary -Value $bytes -Force | Out-Null
        $raw = New-Object Security.AccessControl.RawSecurityDescriptor ($bytes, 0)
        Write-Host "Restored the descriptor that was there before:" -ForegroundColor Green
        Write-Host "  $($raw.GetSddlForm('All'))"
    }
    elseif ($null -ne (Get-CurrentDescriptor)) {
        Remove-ItemProperty $SecurityKey -Name $Guid
        Write-Host "Removed. No descriptor now, so Windows' default applies." -ForegroundColor Green
    }
    else {
        Write-Host "Nothing to revoke." -ForegroundColor Yellow
        exit 0
    }
    Write-Host "Readings will stop for ordinary accounts."
    exit 0
}

# Keep whatever was there before, so a mistake here is undoable from the file
# rather than from memory.
$previous = Get-CurrentDescriptor
if ($null -ne $previous) {
    $backup = "$PSScriptRoot\trace\wmi-sd-before.txt"
    if (-not (Test-Path (Split-Path $backup))) { New-Item -ItemType Directory -Path (Split-Path $backup) | Out-Null }
    [BitConverter]::ToString($previous) | Out-File $backup -Encoding utf8
    Write-Host "Existing descriptor saved to $backup"
}

# SYSTEM and the administrators group are always included: a descriptor that
# leaves them out is how a data block becomes unreachable by anyone.
$who = if ($AllUsers) { 'BU' } else { [Security.Principal.WindowsIdentity]::GetCurrent().User.Value }
$sddl = "O:BAG:BAD:(A;;$Access;;;BA)(A;;$Access;;;SY)(A;;$Access;;;$who)"

$descriptor = New-Object Security.AccessControl.RawSecurityDescriptor ($sddl)
$bytes = New-Object byte[] $descriptor.BinaryLength
$descriptor.GetBinaryForm($bytes, 0)

New-ItemProperty $SecurityKey -Name $Guid -PropertyType Binary -Value $bytes -Force | Out-Null

Write-Host "Granted." -ForegroundColor Green
Write-Host "  $sddl"
Write-Host ""
Write-Host "Sign out and back in is not needed; the change applies to new queries."
