# Sandbox-Diagnose-Setup.ps1 - find out why Setup installed nothing.
#
#   powershell -ExecutionPolicy Bypass -File C:\Tools\Sandbox-Diagnose-Setup.ps1
#
# Run inside the same sandbox, after Sandbox-Check.ps1 has reported that the
# runtime was not installed. Five minutes of doing nothing looks like a download
# that never arrived, so connectivity is tested first and separately: a failure
# to reach Microsoft's servers and a failure in the installer are different
# problems with the same symptom.

$ErrorActionPreference = 'Continue'
function Section([string]$t) { Write-Host ""; Write-Host "== $t" -ForegroundColor Cyan }

Section "Can this sandbox reach the internet at all"
foreach ($host_ in @('aka.ms', 'download.visualstudio.microsoft.com', 'dotnet.microsoft.com')) {
    try {
        $r = Invoke-WebRequest -Uri "https://$host_" -Method Head -TimeoutSec 15 -UseBasicParsing
        Write-Host ("  {0,-40} {1}" -f $host_, $r.StatusCode) -ForegroundColor Green
    } catch {
        Write-Host ("  {0,-40} {1}" -f $host_, $_.Exception.Message) -ForegroundColor Red
    }
}

Section "Logs anything left behind in the last hour"
$since = (Get-Date).AddHours(-1)
$roots = @($env:TEMP, $env:LOCALAPPDATA, "$env:ProgramData")
$logs = foreach ($r in $roots) {
    Get-ChildItem $r -Recurse -Include *.log, *.txt -ErrorAction SilentlyContinue -Depth 3 |
        Where-Object { $_.LastWriteTime -gt $since -and $_.Length -gt 0 }
}
if (-not $logs) { "  nothing" }
foreach ($l in ($logs | Select-Object -First 8)) {
    Write-Host ""
    Write-Host ("  --- {0}  ({1} bytes)" -f $l.FullName, $l.Length) -ForegroundColor Yellow
    Get-Content $l.FullName -Tail 15 | ForEach-Object { "      $_" }
}

Section "Running Setup again, with its own log"
$log = "$env:USERPROFILE\Desktop\setup-verbose.log"
$setup = 'C:\Setup\Nextcalibur-win-Setup.exe'
"  $setup --verbose"
"  log: $log"
$sw = [Diagnostics.Stopwatch]::StartNew()
$p = Start-Process $setup -ArgumentList '--verbose', '--log', "`"$log`"" -Wait -PassThru
$sw.Stop()
"  exit code: $($p.ExitCode)   after $([int]$sw.Elapsed.TotalSeconds)s"

if (Test-Path $log) {
    Section "What the installer said"
    Get-Content $log -Tail 40 | ForEach-Object { "  $_" }
} else {
    Section "No log was written"
    "  the installer did not get far enough to write one"
}

Section "Where things stand now"
$dir = "$env:ProgramFiles\dotnet\shared\Microsoft.WindowsDesktop.App"
"  .NET desktop runtimes: " + $(if (Test-Path $dir) { (Get-ChildItem $dir -Directory).Name -join ', ' } else { 'none' })
$app = Get-ChildItem "$env:LOCALAPPDATA\Nextcalibur" -Recurse -Filter 'Nextcalibur.exe' -ErrorAction SilentlyContinue | Select-Object -First 1
"  application: " + $(if ($app) { $app.FullName } else { 'not installed' })
