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
    # One file per run. Two listeners writing to one log made it impossible to
    # say which run produced a line - and the whole question was whether the
    # elevated run behaved differently from the ordinary one.
    [string]$LogFile = "$PSScriptRoot\hotkey-events-$(Get-Date -Format yyyyMMdd-HHmmss).csv"
)

$ErrorActionPreference = 'Stop'

$EventGuid = '74286d6e-429c-427a-b34b-b5d15d032b05'

$elevated = ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()
    ).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)

Write-Host "Listening for GMC_WMIEvent for $Minutes minute(s)." -ForegroundColor Cyan
Write-Host ("Running as administrator: {0}" -f $(if ($elevated) { 'YES' } else { 'no' })) -ForegroundColor $(if ($elevated) { 'Green' } else { 'Yellow' })
if (-not $elevated) {
    Write-Host "The firmware's event class is administrators-only on this machine, so an" -ForegroundColor Yellow
    Write-Host "ordinary run cannot receive its events whether or not any are sent." -ForegroundColor Yellow
}
Write-Host "Press each Fn combination in turn, slowly, and say aloud which one you pressed."
Write-Host "Log: $LogFile"
Write-Host ""

if (-not (Test-Path $LogFile)) {
    'time,detailLength,detailHex,instance' | Out-File $LogFile -Encoding utf8
}

try {
    Register-CimIndicationEvent -Namespace 'root/wmi' `
        -Query 'SELECT * FROM GMC_WMIEvent' -SourceIdentifier 'NextcaliburHotkeys' -ErrorAction Stop

    # A control, and the test is worthless without one. Seeing no firmware events
    # proves nothing on its own: "the firmware sends none" and "it sends them and
    # we are not receiving them" look identical from here. Brightness changes
    # raise an event Windows itself defines, so pressing the brightness key
    # should produce a line. If it does and the firmware class stays silent, the
    # silence is real.
    Register-CimIndicationEvent -Namespace 'root/wmi' `
        -Query 'SELECT * FROM WmiMonitorBrightnessEvent' -SourceIdentifier 'BrightnessControl' -ErrorAction Stop

    Write-Host "Subscribed to the firmware's key events, and to brightness as a control." -ForegroundColor Green
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
$control = 0

try {
    while ((Get-Date) -lt $deadline) {
        $event = Wait-Event -Timeout 2
        if (-not $event) { continue }

        $source = $event.SourceIdentifier
        if ($source -notin @('NextcaliburHotkeys', 'BrightnessControl')) {
            Remove-Event -EventIdentifier $event.EventIdentifier
            continue
        }

        $instance = $event.SourceEventArgs.NewEvent
        $stamp = (Get-Date).ToString('HH:mm:ss.fff')

        if ($source -eq 'BrightnessControl') {
            $control++
            Write-Host ("{0}  [control] brightness now {1}%" -f $stamp, $instance.Brightness) -ForegroundColor DarkGray
            '{0},control,brightness={1},' -f $stamp, $instance.Brightness | Add-Content $LogFile -Encoding utf8
        }
        else {
            $detail = $instance.EventDetail
            $hex = if ($detail) { [BitConverter]::ToString([byte[]]$detail) } else { '' }
            $length = if ($detail) { $detail.Count } else { 0 }

            $seen++
            Write-Host ("{0}  {1,3} bytes  {2}" -f $stamp, $length, $hex) -ForegroundColor Green
            '{0},{1},{2},{3}' -f $stamp, $length, $hex, $instance.InstanceName | Add-Content $LogFile -Encoding utf8
        }

        Remove-Event -EventIdentifier $event.EventIdentifier
    }
}
finally {
    Unregister-Event -SourceIdentifier 'NextcaliburHotkeys' -ErrorAction SilentlyContinue
    Unregister-Event -SourceIdentifier 'BrightnessControl' -ErrorAction SilentlyContinue
}

Write-Host ""
Write-Host "Firmware key events : $seen" -ForegroundColor Cyan
Write-Host "Control events      : $control" -ForegroundColor Cyan
Write-Host ""

if ($seen -eq 0 -and $control -eq 0) {
    Write-Host "Nothing arrived at all, including the control." -ForegroundColor Yellow
    Write-Host "That says the subscription is not delivering, not that the firmware is quiet."
    Write-Host "Nothing can be concluded about the keys from this run."
}
elseif ($seen -eq 0) {
    Write-Host "The control worked and the firmware said nothing." -ForegroundColor Green
    Write-Host "These keys are handled without software. Replacing the vendor's Control"
    Write-Host "Center takes nothing away from them."
}
else {
    Write-Host "$seen firmware event(s). Written to $LogFile" -ForegroundColor Cyan
}
