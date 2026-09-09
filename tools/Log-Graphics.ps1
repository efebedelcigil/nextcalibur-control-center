# Log-Graphics.ps1 - record graphics state across restarts, to find out what the
# vendor software's Display Mode buttons actually do. Reads only.
#
#   .\Log-Graphics.ps1 -Install     register it to run at every startup (admin)
#   .\Log-Graphics.ps1 -Uninstall   remove it (admin)
#   .\Log-Graphics.ps1 -Mark "clicked UMA"    write a note into the log
#   .\Log-Graphics.ps1              sample once and append
#
# The point of the startup trigger is the boot itself: whether the panel comes
# up on the discrete card or the integrated one is decided before anyone can log
# in and look, so the answer has to be recorded rather than observed.
#
# Everything appends to one file across reboots. Sessions are separated by the
# boot time, so a line can always be placed in the right restart.

param(
    [switch]$Install,
    [switch]$Uninstall,
    [string]$Mark,
    [int]$Minutes = 12,

    # Off by default, and that is the point. Asking NVML for a clock or a power
    # figure wakes the card out of its low-power state, so a logger that polls it
    # every few seconds holds awake the very thing the test is about. Which chip
    # drives the panel — the actual question — comes from WMI and costs nothing.
    # Turn this on only when the card is known to be busy anyway.
    [switch]$WithNvml,

    [string]$LogFile = "$PSScriptRoot\graphics-boot-log.csv"
)

$taskName = 'Nextcalibur-GraphicsLogger'
$self = $MyInvocation.MyCommand.Path

function Test-Admin {
    ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()
    ).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
}

function Write-Header {
    if (Test-Path $LogFile) { return }
    'time,boot,phase,device,status,cmError,resolution,drivesPanel,gpuMHz,gpuWatts,regGpuMode,vendorProcs' |
        Out-File $LogFile -Encoding utf8
}

function Get-BootStamp {
    try { (Get-CimInstance Win32_OperatingSystem).LastBootUpTime.ToString('MMdd-HHmm') }
    catch { 'unknown' }
}

function Get-RegGpuMode {
    foreach ($p in @(
        'HKLM:\SOFTWARE\WOW6432Node\CASPER EXCALIBUR\ControlCenter',
        'HKLM:\SOFTWARE\CASPER EXCALIBUR\ControlCenter')) {
        try {
            $v = Get-ItemProperty $p -ErrorAction Stop
            foreach ($n in @('GPUMMode', 'IsNewVGAMode', 'VGAMode')) {
                if ($null -ne $v.$n) { return "$n=$($v.$n)" }
            }
        } catch { }
    }
    return 'absent'
}

function Get-Nvidia {
    if (-not $WithNvml) { return @('', '') }
    $smi = (Get-Command nvidia-smi -ErrorAction SilentlyContinue).Source
    if (-not $smi) { $smi = "$env:ProgramFiles\NVIDIA Corporation\NVSMI\nvidia-smi.exe" }
    if (-not (Test-Path $smi)) { return @('', '') }
    try {
        $line = (& $smi --query-gpu=clocks.gr,power.draw --format=csv,noheader,nounits 2>$null | Select-Object -First 1)
        if (-not $line) { return @('', '') }
        $parts = $line -split ',\s*'
        return @($parts[0], $parts[1])
    } catch { return @('', '') }
}

function Sample([string]$phase) {
    Write-Header
    $stamp = (Get-Date).ToString('yyyy-MM-dd HH:mm:ss')
    $boot = Get-BootStamp
    $reg = Get-RegGpuMode
    $vendor = @(Get-Process -Name ControlCenter, ControlCenterDaemon -ErrorAction SilentlyContinue).Count
    $nv = Get-Nvidia

    $video = @{}
    foreach ($v in Get-CimInstance Win32_VideoController -ErrorAction SilentlyContinue) {
        $key = if ($v.PNPDeviceID -match 'VEN_10DE') { 'NVIDIA' }
               elseif ($v.PNPDeviceID -match 'VEN_8086') { 'Intel' }
               else { $v.Name }
        $video[$key] = if ($v.CurrentHorizontalResolution) {
            "$($v.CurrentHorizontalResolution)x$($v.CurrentVerticalResolution)"
        } else { 'none' }
    }

    foreach ($d in Get-PnpDevice -Class Display -ErrorAction SilentlyContinue) {
        $name = if ($d.InstanceId -match 'VEN_10DE') { 'NVIDIA' }
                elseif ($d.InstanceId -match 'VEN_8086') { 'Intel' }
                else { $d.FriendlyName }
        $res = if ($video.ContainsKey($name)) { $video[$name] } else { 'unknown' }
        $drives = if ($res -notin @('none', 'unknown')) { 'YES' } else { 'no' }
        $mhz = if ($name -eq 'NVIDIA') { $nv[0] } else { '' }
        $watts = if ($name -eq 'NVIDIA') { $nv[1] } else { '' }

        '{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}' -f `
            $stamp, $boot, $phase, $name, $d.Status, $d.ConfigManagerErrorCode,
            $res, $drives, $mhz, $watts, $reg, $vendor |
            Add-Content $LogFile -Encoding utf8
    }
}

# ---------------------------------------------------------------- entry points

if ($Uninstall) {
    if (-not (Test-Admin)) { Write-Host "Run as administrator." -ForegroundColor Red; exit 1 }
    Unregister-ScheduledTask -TaskName $taskName -Confirm:$false -ErrorAction SilentlyContinue
    Write-Host "Logger removed. The log itself is kept: $LogFile" -ForegroundColor Green
    exit 0
}

if ($Install) {
    if (-not (Test-Admin)) { Write-Host "Run as administrator." -ForegroundColor Red; exit 1 }

    $nvml = if ($WithNvml) { ' -WithNvml' } else { '' }
    $action = New-ScheduledTaskAction -Execute 'powershell.exe' `
        -Argument "-NoProfile -ExecutionPolicy Bypass -WindowStyle Hidden -File `"$self`" -Minutes $Minutes$nvml"

    # No delay: the whole question is what the machine looks like as it comes up.
    $trigger = New-ScheduledTaskTrigger -AtStartup
    $settings = New-ScheduledTaskSettingsSet -AllowStartIfOnBatteries `
        -DontStopIfGoingOnBatteries -ExecutionTimeLimit (New-TimeSpan -Minutes ($Minutes + 5))

    Register-ScheduledTask -TaskName $taskName -Action $action -Trigger $trigger `
        -Settings $settings -User 'SYSTEM' -RunLevel Highest -Force | Out-Null

    Sample 'installed'
    Write-Host "Logger installed. It samples for $Minutes minutes after every startup." -ForegroundColor Green
    Write-Host "Log: $LogFile"
    exit 0
}

if ($Mark) {
    Write-Header
    '{0},{1},MARK,{2},,,,,,,,' -f (Get-Date).ToString('yyyy-MM-dd HH:mm:ss'), (Get-BootStamp), $Mark |
        Add-Content $LogFile -Encoding utf8
    Write-Host "Marked: $Mark"
    exit 0
}

# Default: sample continuously for the given number of minutes. This is what the
# scheduled task runs at startup.
# Always takes at least one sample, so a zero-minute run is a single reading
# rather than nothing at all.
$deadline = (Get-Date).AddMinutes($Minutes)
$first = $true
do {
    Sample $(if ($first) { 'boot' } else { 'running' })
    $first = $false
    if ((Get-Date) -ge $deadline) { break }
    Start-Sleep -Seconds 5
} while ((Get-Date) -lt $deadline)
