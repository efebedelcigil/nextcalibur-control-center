# Watch-PowerSource.ps1 - record what changes when the charger comes out.
#
#   .\Watch-PowerSource.ps1 [-Minutes 5]
#
# The vendor's registry keeps a value called ModeBeforeDC, which reads like
# software that changes the machine's mode when it goes onto battery and puts it
# back afterwards. Reads like is not evidence, so this watches the things that
# would move if it did: the active power plan, the power-mode overlay, and
# whether the machine is on mains.
#
# Deliberately independent of how the software finds out. Firmware events, a
# Windows power notification, polling - it does not matter which; if the mode
# changes, it changes here.
#
# Run it with the vendor software installed and again without, and the
# difference is the answer.

param(
    [int]$Minutes = 5,
    [string]$LogFile = "$PSScriptRoot\power-source-$(Get-Date -Format yyyyMMdd-HHmmss).csv"
)

$ErrorActionPreference = 'Continue'

$SchemesKey = 'HKLM:\SYSTEM\CurrentControlSet\Control\Power\User\PowerSchemes'

function Get-State {
    $onMains = $null
    $battery = Get-CimInstance Win32_Battery -ErrorAction SilentlyContinue
    if ($battery) { $onMains = ($battery.BatteryStatus -eq 2) }

    $plan = 'unknown'
    foreach ($line in (powercfg /getactivescheme 2>$null)) {
        if ($line -match 'GUID:\s+(\S+)\s+\((.+?)\)') { $plan = $Matches[2] }
    }

    $overlay = 'none'
    try {
        $p = Get-ItemProperty $SchemesKey -ErrorAction Stop
        $ac = $p.ActiveOverlayAcPowerScheme
        $dc = $p.ActiveOverlayDcPowerScheme
        $overlay = "ac=$ac dc=$dc"
    } catch { }

    $vendor = @(Get-Process -Name ControlCenter, ControlCenterDaemon -ErrorAction SilentlyContinue).Count

    return [pscustomobject]@{
        OnMains = $onMains
        Plan    = $plan
        Overlay = $overlay
        Vendor  = $vendor
    }
}

'time,onMains,plan,overlay,vendorProcs,note' | Out-File $LogFile -Encoding utf8

Write-Host "Watching the power source for $Minutes minute(s)." -ForegroundColor Cyan
Write-Host "Unplug the charger, wait ten seconds, plug it back in."
Write-Host "Log: $LogFile"
Write-Host ""

$previous = $null
$deadline = (Get-Date).AddMinutes($Minutes)

while ((Get-Date) -lt $deadline) {
    $now = Get-State
    $line = '{0},{1},{2},{3},{4}' -f $now.OnMains, $now.Plan, $now.Overlay, $now.Vendor, ''

    if ($line -ne $previous) {
        $stamp = (Get-Date).ToString('HH:mm:ss.fff')
        $note = if ($null -eq $previous) { 'start' } else { 'CHANGED' }
        $colour = if ($null -eq $previous) { 'Gray' } else { 'Green' }

        Write-Host ("{0}  mains={1,-5} plan={2,-28} overlay={3} vendor={4}  {5}" -f `
            $stamp, $now.OnMains, $now.Plan, $now.Overlay, $now.Vendor, $note) -ForegroundColor $colour

        '{0},{1},{2},{3},{4},{5}' -f $stamp, $now.OnMains, $now.Plan, $now.Overlay, $now.Vendor, $note |
            Add-Content $LogFile -Encoding utf8

        $previous = $line
    }

    Start-Sleep -Milliseconds 700
}

Write-Host ""
Write-Host "Done. Any line marked CHANGED is something that moved by itself." -ForegroundColor Cyan
