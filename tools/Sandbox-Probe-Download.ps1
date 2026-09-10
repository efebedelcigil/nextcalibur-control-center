# Sandbox-Probe-Download.ps1 - is it the network or the installer?
#
#   powershell -ExecutionPolicy Bypass -File C:\Tools\Sandbox-Probe-Download.ps1
#
# The installer's progress bar sits still. That has two very different causes
# and they need different fixes, so this measures the download the installer
# would be doing, directly: same file, same machine, no installer in between.
#
# Fast here and stuck there means the installer. Stuck here too means the
# network between this machine and Microsoft's CDN - which is not something the
# application can fix, and is an argument for not needing the download at all.

$ErrorActionPreference = 'Continue'
function Section([string]$t) { Write-Host ""; Write-Host "== $t" -ForegroundColor Cyan }

Section "Whatever the installer logged"
$log = "$env:USERPROFILE\Desktop\setup-verbose.log"
if (Test-Path $log) {
    Get-Content $log -Tail 30 | ForEach-Object { "  $_" }
} else {
    "  no log at $log"
}

Section "Downloading the .NET desktop runtime directly"
$url = 'https://aka.ms/dotnet/8.0/windowsdesktop-runtime-win-x64.exe'
$out = "$env:TEMP\probe-runtime.exe"
Remove-Item $out -ErrorAction SilentlyContinue
"  $url"

$sw = [Diagnostics.Stopwatch]::StartNew()
try {
    $req = [Net.HttpWebRequest]::Create($url)
    $req.Timeout = 30000
    $req.ReadWriteTimeout = 30000
    $resp = $req.GetResponse()
    "  responded in $([int]$sw.Elapsed.TotalSeconds)s, size $([math]::Round($resp.ContentLength/1MB,1)) MB"
    "  from $($resp.ResponseUri.Host)"

    $stream = $resp.GetResponseStream()
    $file = [IO.File]::Create($out)
    $buffer = New-Object byte[] 81920
    $total = 0
    $lastReport = 0
    while (($read = $stream.Read($buffer, 0, $buffer.Length)) -gt 0) {
        $file.Write($buffer, 0, $read)
        $total += $read
        if ($sw.Elapsed.TotalSeconds - $lastReport -ge 5) {
            $lastReport = $sw.Elapsed.TotalSeconds
            $mb = [math]::Round($total/1MB, 1)
            $rate = [math]::Round(($total/1MB)/$sw.Elapsed.TotalSeconds, 2)
            "    $mb MB after $([int]$sw.Elapsed.TotalSeconds)s  =  $rate MB/s"
        }
        if ($sw.Elapsed.TotalSeconds -gt 120) { "  stopping at two minutes"; break }
    }
    $file.Close(); $stream.Close(); $resp.Close()
    $sw.Stop()

    $mb = [math]::Round($total/1MB, 1)
    $rate = if ($sw.Elapsed.TotalSeconds -gt 0) { [math]::Round(($total/1MB)/$sw.Elapsed.TotalSeconds, 2) } else { 0 }
    Write-Host "  got $mb MB in $([int]$sw.Elapsed.TotalSeconds)s = $rate MB/s" -ForegroundColor Green
}
catch {
    $sw.Stop()
    Write-Host "  failed after $([int]$sw.Elapsed.TotalSeconds)s: $($_.Exception.Message)" -ForegroundColor Red
}

Section "Verdict"
"  A few MB/s here means the network is fine and the installer is the problem."
"  Kilobytes or a stall means the download itself, which no change to the"
"  application can fix - only not needing it."
