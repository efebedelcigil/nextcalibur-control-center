# Test-Led.ps1
# LED probe for the Nextcalibur project.
#
# Sends ONE LED command through the firmware mailbox and reports what came back,
# so the physical result can be observed and recorded.
#
# SAFETY
#   - This script refuses to touch any subsystem other than LED (a1 = 0x0100).
#     The thermal subsystem is never addressed, so fan behaviour cannot be
#     affected.
#   - LED writes are recoverable: the vendor software or a reboot restores the
#     previous lighting.
#   - Values suggested by the test plan come from the vendor's own saved
#     profiles, so they are values this firmware already accepts.
#
# USAGE
#   .\Test-Led.ps1 -Device 6 -Value 0x11FF0000      # write
#   .\Test-Led.ps1 -Device 6 -Read                  # read back
#   .\Test-Led.ps1 -Device 6 -Value 0x11FF0000 -WhatIf
#
#   Device : 0 = all devices, 6 = all keyboard, otherwise a device index
#   Value  : packed 32-bit LED value, 0xMMRRGGBB (MM = mode/brightness byte)

[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [Parameter(Mandatory)][uint32]$Device,
    [uint32]$Value = 0,
    [switch]$Read
)

$ErrorActionPreference = 'Stop'

$CLASS       = 'RW_GMWMI'
$FAMILY_READ = [uint16]0xFA00
$FAMILY_WRITE= [uint16]0xFB00
$SUB_LED     = [uint16]0x0100

function Get-Mailbox { Get-CimInstance -Namespace root\wmi -ClassName $CLASS }

function Format-Response {
    param([byte[]]$b)
    [pscustomobject]@{
        a0 = '0x{0:X4}' -f [BitConverter]::ToUInt16($b, 0)
        a1 = '0x{0:X4}' -f [BitConverter]::ToUInt16($b, 2)
        a2 = [BitConverter]::ToUInt32($b, 4)
        a3 = '0x{0:X8}' -f [BitConverter]::ToUInt32($b, 8)
        a4 = '0x{0:X8}' -f [BitConverter]::ToUInt32($b, 12)
        a5 = '0x{0:X8}' -f [BitConverter]::ToUInt32($b, 16)
    }
}

# --- guard: the vendor software races us for the same mailbox ---
if (Get-Process ControlCenter -ErrorAction SilentlyContinue) {
    Write-Warning 'The vendor Control Center is running. It writes to the same mailbox.'
    Write-Warning 'Close it before testing, or results will be unreliable.'
}

$family = if ($Read) { $FAMILY_READ } else { $FAMILY_WRITE }

$buffer = New-Object byte[] 32
[BitConverter]::GetBytes($family).CopyTo($buffer, 0)
[BitConverter]::GetBytes($SUB_LED).CopyTo($buffer, 2)
[BitConverter]::GetBytes($Device).CopyTo($buffer, 4)
if (-not $Read) { [BitConverter]::GetBytes($Value).CopyTo($buffer, 8) }

$what = if ($Read) { 'READ' } else { 'WRITE' }
$desc = "$what  a0=0x{0:X4} a1=0x{1:X4} device={2} value=0x{3:X8}" -f $family, $SUB_LED, $Device, $Value
Write-Host $desc -ForegroundColor Cyan
if (-not $Read) {
    $r = ($Value -shr 16) -band 0xFF; $g = ($Value -shr 8) -band 0xFF; $b = $Value -band 0xFF
    $mode = ($Value -shr 24) -band 0xFF
    Write-Host ("      mode byte 0x{0:X2}   RGB #{1:X2}{2:X2}{3:X2}" -f $mode, $r, $g, $b) -ForegroundColor DarkGray
}

if (-not $PSCmdlet.ShouldProcess("LED device $Device", $what)) { return }

$mo = Get-Mailbox
Set-CimInstance -InputObject $mo -Property @{ BufferBytes = $buffer } | Out-Null
Start-Sleep -Milliseconds 80

Write-Host 'Response:' -ForegroundColor DarkGray
Format-Response ((Get-Mailbox).BufferBytes) | Format-List

Write-Host 'Now look at the keyboard and record what changed.' -ForegroundColor Yellow
