# Sandbox-Check.ps1 - run the whole clean-machine test inside Windows Sandbox
# and print one readable report. Run it in the sandbox, not on the laptop.
#
#   powershell -ExecutionPolicy Bypass -File C:\Tools\Sandbox-Check.ps1
#
# A sandbox has no firmware mailbox behind it, so "sensors don't work" is the
# expected and correct answer there. What is being tested is everything else a
# stranger's machine puts in the way:
#
#   - does Setup install the .NET desktop runtime when it is missing
#   - does the application start at all on a machine it does not understand
#   - does it say something true about that, or crash, or claim a permission
#     problem that does not exist

$ErrorActionPreference = 'Continue'

function Section([string]$t) { Write-Host ""; Write-Host "== $t" -ForegroundColor Cyan }

function Get-Runtimes {
    $dir = "$env:ProgramFiles\dotnet\shared\Microsoft.WindowsDesktop.App"
    if (-not (Test-Path $dir)) { return @() }
    return @(Get-ChildItem $dir -Directory | Select-Object -ExpandProperty Name)
}

Section "The machine"
"  $((Get-CimInstance Win32_OperatingSystem).Caption)"
"  build $([Environment]::OSVersion.Version)"

Section "Before installing"
$before = Get-Runtimes
"  .NET desktop runtimes: " + $(if ($before) { $before -join ', ' } else { 'none' })
$cls = Get-CimClass -Namespace root\wmi -ClassName RW_GMWMI -ErrorAction SilentlyContinue
"  firmware mailbox class: " + $(if ($cls) { 'present' } else { 'absent - expected in a virtual machine' })

Section "Running Setup"
$setup = 'C:\Setup\Nextcalibur-win-Setup.exe'
if (-not (Test-Path $setup)) { Write-Host "  $setup is missing - is the folder mapped?" -ForegroundColor Red; exit 1 }
"  $setup"
"  watch for a prompt about the .NET runtime; allow it"
$sw = [Diagnostics.Stopwatch]::StartNew()
# Not -Wait. Velopack's Setup starts the application and stays alive alongside
# it, so waiting on Setup means waiting for somebody to close the window, which
# once turned a nine-second install into a reported 803 seconds.
$proc = Start-Process $setup -PassThru
while (-not $proc.HasExited -and $sw.Elapsed.TotalSeconds -lt 180) {
    if (Get-Process -Name Nextcalibur -ErrorAction SilentlyContinue) { break }
    Start-Sleep -Milliseconds 500
}
$sw.Stop()
"  application appeared after $([int]$sw.Elapsed.TotalSeconds) seconds"

Section "After installing"
$after = Get-Runtimes
"  .NET desktop runtimes: " + $(if ($after) { $after -join ', ' } else { 'none' })
$new = $after | Where-Object { $_ -notin $before }
# The package carries its own runtime, so "none" here is the expected answer
# rather than a failure. It was a failure only while the installer was expected
# to fetch one - and that expectation is what turned out not to work.
$new = $after | Where-Object { $_ -notin $before }
if ($new) { Write-Host "  Setup installed a runtime: $($new -join ', ')" -ForegroundColor Yellow }
else { "  no shared runtime, as expected - the package carries its own" }

$installed = Get-ChildItem "$env:LOCALAPPDATA\Nextcalibur" -Recurse -Filter 'Nextcalibur.exe' -ErrorAction SilentlyContinue |
    Select-Object -First 1
"  installed to: " + $(if ($installed) { $installed.FullName } else { 'NOT FOUND' })

Section "Starting it"
if ($installed) {
    Start-Process $installed.FullName
    Start-Sleep -Seconds 12
    $p = @(Get-Process -Name Nextcalibur -ErrorAction SilentlyContinue)
    if ($p.Count -gt 0) {
        Write-Host "  running, $($p.Count) process" -ForegroundColor Green
        "  window title: " + ($p[0].MainWindowTitle)
    } else {
        Write-Host "  NOT running - it exited or never started" -ForegroundColor Red
    }
}

Section "Anything it complained about"
Get-WinEvent -FilterHashtable @{LogName='Application'; StartTime=(Get-Date).AddMinutes(-10)} -ErrorAction SilentlyContinue |
    Where-Object { $_.Message -match 'Nextcalibur' } |
    Select-Object -First 5 | ForEach-Object {
        Write-Host ("  {0} {1}" -f $_.TimeCreated.ToString('HH:mm:ss'), ($_.Message -split "`n")[0]) -ForegroundColor Yellow
    }

Write-Host ""
Write-Host "Report the four sections above." -ForegroundColor Cyan
Write-Host "What matters: whether the runtime got installed, whether the window opened,"
Write-Host "and what the banner in the window says."
