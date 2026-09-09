# Arm-GpuRecovery.ps1 - a safety net before testing the vendor software's
# Display Mode buttons on a machine whose screen is driven by the discrete GPU.
#
# MUST BE RUN AS ADMINISTRATOR.
#
# Why this exists: disabling a device through SetupDi is persistent. If the
# adapter driving the panel is disabled, the screen goes dark and a restart does
# not undo it — the machine boots dark as well. This registers a scheduled task
# that re-enables the adapter on its own, so the test is recoverable without a
# screen to work from.
#
#   .\Arm-GpuRecovery.ps1          arm it
#   .\Arm-GpuRecovery.ps1 -Disarm  remove it once the test is over
#
# Two triggers, because there are two ways to end up stuck:
#   * once, a few minutes from now - for a screen that goes dark and stays there
#   * at every startup, after a delay - for a machine restarted while dark
#
# The startup delay is deliberately long enough to see what the firmware does on
# a boot with the card disabled, which is the interesting part of the test,
# before the net pulls it back.

param(
    [int]$Minutes = 5,
    [int]$StartupDelayMinutes = 3,
    [switch]$Disarm
)

$ErrorActionPreference = 'Stop'
$taskName = 'Nextcalibur-GpuRecovery'

$admin = ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()
         ).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
if (-not $admin) {
    Write-Host "This has to run as administrator - re-open PowerShell with 'Run as administrator'." -ForegroundColor Red
    exit 1
}

if ($Disarm) {
    Unregister-ScheduledTask -TaskName $taskName -Confirm:$false -ErrorAction SilentlyContinue
    Write-Host "Recovery task removed." -ForegroundColor Green
    exit 0
}

# Every NVIDIA display adapter on this machine, found rather than hardcoded.
$targets = @(Get-PnpDevice -Class Display | Where-Object { $_.InstanceId -match 'VEN_10DE' })
if ($targets.Count -eq 0) {
    Write-Host "No NVIDIA display adapter found - nothing to protect." -ForegroundColor Red
    exit 1
}

Write-Host "Will re-enable:" -ForegroundColor Cyan
$targets | ForEach-Object { "  $($_.FriendlyName)`n    $($_.InstanceId)" }

$commands = $targets | ForEach-Object { "pnputil /enable-device `"$($_.InstanceId)`"" }
$script = ($commands -join '; ')

$action = New-ScheduledTaskAction -Execute 'powershell.exe' `
    -Argument "-NoProfile -WindowStyle Hidden -Command `"$script`""

$triggers = @(
    (New-ScheduledTaskTrigger -Once -At (Get-Date).AddMinutes($Minutes)),
    (New-ScheduledTaskTrigger -AtStartup)
)
$triggers[1].Delay = "PT${StartupDelayMinutes}M"

$settings = New-ScheduledTaskSettingsSet -StartWhenAvailable -AllowStartIfOnBatteries `
    -DontStopIfGoingOnBatteries -ExecutionTimeLimit (New-TimeSpan -Minutes 10)

Register-ScheduledTask -TaskName $taskName -Action $action -Trigger $triggers `
    -Settings $settings -User 'SYSTEM' -RunLevel Highest -Force | Out-Null

Write-Host ""
Write-Host "Armed." -ForegroundColor Green
Write-Host "  fires once at        : $((Get-Date).AddMinutes($Minutes).ToString('HH:mm:ss'))"
Write-Host "  and $StartupDelayMinutes minutes after every startup, until you disarm it"
Write-Host ""
Write-Host "If the screen goes dark: wait. Do not reinstall anything, do not panic-restart." -ForegroundColor Yellow
Write-Host "When the test is done:  .\Arm-GpuRecovery.ps1 -Disarm" -ForegroundColor Yellow
