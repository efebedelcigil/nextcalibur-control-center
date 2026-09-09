# Snapshot-Graphics.ps1 - record the machine's graphics state. Reads only.
#
# Used to find out what the vendor software's Display Mode buttons actually do,
# by taking a snapshot before and after each one is pressed and comparing them.
#
#   .\Snapshot-Graphics.ps1 -Label before-discrete
#
# Writes a .json beside itself and prints a summary. Nothing here writes to the
# machine: no device is enabled, disabled or reconfigured. Switching graphics
# modes on a guess can leave a laptop with no working display, so this half of
# the work is deliberately incapable of it.

param(
    [string]$Label = (Get-Date -Format 'HHmmss'),
    [string]$OutDir = $PSScriptRoot
)

$ErrorActionPreference = 'Stop'

function Get-Adapters {
    Get-CimInstance Win32_VideoController | ForEach-Object {
        [pscustomobject]@{
            Name                  = $_.Name
            Vendor                = $_.AdapterCompatibility
            PnpDeviceId           = $_.PNPDeviceID
            Status                = $_.Status
            Availability          = $_.Availability
            ConfigManagerErrorCode = $_.ConfigManagerErrorCode
            DriverVersion         = $_.DriverVersion
            HorizontalResolution  = $_.CurrentHorizontalResolution
            VerticalResolution    = $_.CurrentVerticalResolution
            RefreshHz             = $_.CurrentRefreshRate
            DrivesDisplay         = [bool]($_.CurrentHorizontalResolution -gt 0)
        }
    }
}

function Get-DisplayDevices {
    # The PnP view, which is what SetupDi acts on. Error code 22 is "disabled".
    Get-CimInstance Win32_PnPEntity -Filter "PNPClass='Display'" | ForEach-Object {
        [pscustomobject]@{
            Name                   = $_.Name
            DeviceId               = $_.DeviceID
            Status                 = $_.Status
            ConfigManagerErrorCode = $_.ConfigManagerErrorCode
            Present                = $_.Present
            HardwareIds            = ($_.HardwareID -join ' | ')
        }
    }
}

function Get-Monitors {
    Get-CimInstance Win32_DesktopMonitor -ErrorAction SilentlyContinue | ForEach-Object {
        [pscustomobject]@{
            Name        = $_.Name
            DeviceId    = $_.DeviceID
            Availability = $_.Availability
        }
    }
}

function Get-NvmlReachable {
    # Whether NVML still answers. A disabled device usually means it does not,
    # which matters because the clock reading depends on it.
    try {
        $out = & "$PSScriptRoot\..\src\Nextcalibur.Cli\bin\Release\net8.0-windows\nextcalibur.exe" clocks 2>&1 |
               Select-Object -First 1
        return "$out"
    } catch {
        return "cli unavailable: $($_.Exception.Message)"
    }
}

$snapshot = [ordered]@{
    Label          = $Label
    TakenAt        = (Get-Date).ToString('o')
    Adapters       = @(Get-Adapters)
    DisplayDevices = @(Get-DisplayDevices)
    Monitors       = @(Get-Monitors)
    ClocksSays     = Get-NvmlReachable
}

$path = Join-Path $OutDir "graphics-$Label.json"
$snapshot | ConvertTo-Json -Depth 6 | Out-File $path -Encoding utf8

Write-Host "=== $Label ===" -ForegroundColor Cyan
foreach ($a in $snapshot.Adapters) {
    $drives = if ($a.DrivesDisplay) { "$($a.HorizontalResolution)x$($a.VerticalResolution)@$($a.RefreshHz)" } else { 'no display' }
    "  {0,-42} {1,-10} cm={2,-4} {3}" -f $a.Name, $a.Status, $a.ConfigManagerErrorCode, $drives
}
"  display PnP devices:"
foreach ($d in $snapshot.DisplayDevices) {
    "    {0,-42} {1,-10} cm={2}" -f $d.Name, $d.Status, $d.ConfigManagerErrorCode
}
"  clocks: $($snapshot.ClocksSays)"
"  written to $path"
