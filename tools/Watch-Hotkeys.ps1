# Watch-Hotkeys.ps1 - listen to the firmware's key events and print what arrives.
#
#   .\Watch-Hotkeys.ps1 [-Minutes 3]
#
# The vendor's Control Center subscribes to GMC_WMIEvent, the firmware's own
# event class, and does something in response to the Fn row. Nextcalibur does
# not listen at all, so whatever those keys do through that software stops when
# it is replaced - and nobody has established what that covers.
#
# This answers the first half: which keys produce an event, and what the
# firmware says when they do. Press each Fn combination in turn and watch.
#
# Two things worth knowing before drawing conclusions from silence:
#
#   * Some Fn keys never reach Windows at all. Brightness and volume are often
#     handled by the firmware itself, and a key that changes something without
#     producing an event here is working exactly as intended.
#   * Subscribing needs permission on the event's own GUID, which is a different
#     GUID from the data block. If nothing arrives for any key, that is the
#     first thing to rule out - the script says how.

param(
    [int]$Minutes = 3,
    [string]$LogFile = "$PSScriptRoot\hotkey-events.csv"
)

$ErrorActionPreference = 'Stop'

$EventGuid = '74286d6e-429c-427a-b34b-b5d15d032b05'

Write-Host "Listening for GMC_WMIEvent for $Minutes minute(s)." -ForegroundColor Cyan
Write-Host "Press each Fn combination in turn, slowly, and say aloud which one you pressed."
Write-Host "Log: $LogFile"
Write-Host ""

if (-not (Test-Path $LogFile)) {
    'time,detailLength,detailHex,instance' | Out-File $LogFile -Encoding utf8
}

$subscription = $null
try {
    $subscription = Register-CimIndicationEvent -Namespace 'root/wmi' `
        -Query 'SELECT * FROM GMC_WMIEvent' -SourceIdentifier 'NextcaliburHotkeys' -ErrorAction Stop
    Write-Host "Subscribed." -ForegroundColor Green
}
catch {
    Write-Host "Could not subscribe: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
    Write-Host "If this is an access problem, the event GUID needs granting the same way the"
    Write-Host "data block did. It is a separate GUID:"
    Write-Host "  $EventGuid" -ForegroundColor Yellow
    exit 1
}

$deadline = (Get-Date).AddMinutes($Minutes)
$seen = 0

try {
    while ((Get-Date) -lt $deadline) {
        $event = Wait-Event -SourceIdentifier 'NextcaliburHotkeys' -Timeout 2
        if (-not $event) { continue }

        $instance = $event.SourceEventArgs.NewEvent
        $detail = $instance.EventDetail
        $hex = if ($detail) { [BitConverter]::ToString([byte[]]$detail) } else { '' }
        $stamp = (Get-Date).ToString('HH:mm:ss.fff')

        $seen++
        Write-Host ("{0}  {1,3} bytes  {2}" -f $stamp, $(if ($detail) { $detail.Count } else { 0 }), $hex) -ForegroundColor Green
        '{0},{1},{2},{3}' -f $stamp, $(if ($detail) { $detail.Count } else { 0 }), $hex, $instance.InstanceName |
            Add-Content $LogFile -Encoding utf8

        Remove-Event -EventIdentifier $event.EventIdentifier
    }
}
finally {
    Unregister-Event -SourceIdentifier 'NextcaliburHotkeys' -ErrorAction SilentlyContinue
}

Write-Host ""
if ($seen -eq 0) {
    Write-Host "No events at all." -ForegroundColor Yellow
    Write-Host "Either these keys never reach Windows on this machine, or subscribing is"
    Write-Host "permitted but delivering is not. Try again elevated to tell those apart."
} else {
    Write-Host "$seen event(s). Written to $LogFile" -ForegroundColor Cyan
}
