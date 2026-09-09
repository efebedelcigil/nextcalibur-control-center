# Watch-Graphics.ps1 - log graphics device state continuously while somebody
# presses the vendor software's Display Mode buttons. Reads only.
#
#   .\Watch-Graphics.ps1 -Minutes 15
#
# Samples every second, because the vendor software disables one device, sleeps
# a second, then disables another — a slower sample would show the end state and
# hide the sequence, and the sequence is the thing worth knowing.
#
# Writes each sample to a CSV as it goes rather than at the end, so the trace
# survives the screen going dark, the session being logged out, or the machine
# being restarted mid-test.

param(
    [int]$Minutes = 15,
    [string]$OutFile = "$PSScriptRoot\graphics-trace.csv"
)

$ErrorActionPreference = 'Continue'

'time,device,status,cmError,present,resolution,vendorRunning' | Out-File $OutFile -Encoding utf8

$deadline = (Get-Date).AddMinutes($Minutes)
$previous = ''

while ((Get-Date) -lt $deadline) {
    $stamp = (Get-Date).ToString('HH:mm:ss.fff')

    $vendor = @(Get-Process -Name ControlCenter, ControlCenterDaemon -ErrorAction SilentlyContinue).Count

    $rows = @()

    foreach ($d in Get-PnpDevice -Class Display -ErrorAction SilentlyContinue) {
        $short = if ($d.InstanceId -match 'VEN_10DE') { 'NVIDIA' }
                 elseif ($d.InstanceId -match 'VEN_8086') { 'Intel' }
                 else { $d.FriendlyName }
        $rows += [pscustomobject]@{
            Device  = $short
            Status  = $d.Status
            CmError = $d.ConfigManagerErrorCode
            Present = $d.Present
            Res     = ''
        }
    }

    foreach ($v in Get-CimInstance Win32_VideoController -ErrorAction SilentlyContinue) {
        $short = if ($v.PNPDeviceID -match 'VEN_10DE') { 'NVIDIA' }
                 elseif ($v.PNPDeviceID -match 'VEN_8086') { 'Intel' }
                 else { $v.Name }
        $row = $rows | Where-Object { $_.Device -eq $short } | Select-Object -First 1
        if ($row) {
            $row.Res = if ($v.CurrentHorizontalResolution) {
                "$($v.CurrentHorizontalResolution)x$($v.CurrentVerticalResolution)"
            } else { 'none' }
        }
    }

    # Only write when something moved. A per-second log of an unchanging machine
    # buries the two lines that matter.
    $fingerprint = ($rows | ForEach-Object { "$($_.Device)|$($_.Status)|$($_.CmError)|$($_.Present)|$($_.Res)" }) -join ';'
    $fingerprint += "|vendor=$vendor"

    if ($fingerprint -ne $previous) {
        foreach ($r in $rows) {
            '{0},{1},{2},{3},{4},{5},{6}' -f `
                $stamp, $r.Device, $r.Status, $r.CmError, $r.Present, $r.Res, $vendor |
                Add-Content $OutFile -Encoding utf8
        }
        '' | Add-Content $OutFile -Encoding utf8
        $previous = $fingerprint
    }

    Start-Sleep -Milliseconds 700
}

"--- watch ended {0} ---" -f (Get-Date).ToString('HH:mm:ss') | Add-Content $OutFile -Encoding utf8
