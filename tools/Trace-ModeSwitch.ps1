# Trace-ModeSwitch.ps1 - capture what changes when the vendor software's Display
# Mode button is pressed. Reads only; nothing here reconfigures a device.
#
#   .\Trace-ModeSwitch.ps1 -Label before-hybrid
#   .\Trace-ModeSwitch.ps1 -Diff before-hybrid after-click-hybrid
#
# Watch-Graphics.ps1 answers "which chip is driving the panel". This answers the
# other half: where the choice is written down. The vendor's own registry keys
# were checked first and hold no mode value, so the search has to be wide -
# whole subtrees, the install directory's timestamps, the driver's state - and
# the diff is what makes a wide capture readable.
#
# A switch that shows up in none of these is itself the answer: the setting
# lives in firmware, reached through the kernel driver, and Windows never sees
# it until the next boot enumerates the bus differently.

param(
    [string]$Label,
    [string[]]$Diff,
    [string]$OutDir = "$PSScriptRoot\trace"
)

$ErrorActionPreference = 'Continue'

$registryRoots = @(
    'HKLM:\SOFTWARE\WOW6432Node\CASPER EXCALIBUR',
    'HKLM:\SOFTWARE\CASPER EXCALIBUR',
    'HKLM:\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}',
    'HKLM:\SYSTEM\CurrentControlSet\Control\GraphicsDrivers',
    'HKLM:\SYSTEM\CurrentControlSet\Services\nvlddmkm',
    'HKLM:\SYSTEM\CurrentControlSet\Services\ControlCenter64',
    'HKLM:\SYSTEM\CurrentControlSet\Services\ControlCenterC64'
)

function Get-RegistryValues([string]$root) {
    $out = @{}
    try { $keys = @(Get-Item $root -ErrorAction Stop) + @(Get-ChildItem $root -Recurse -ErrorAction SilentlyContinue) }
    catch { return $out }

    foreach ($k in $keys) {
        $path = $k.Name
        foreach ($name in $k.GetValueNames()) {
            $v = $k.GetValue($name)
            # Binary blobs are compared by content, not printed in full.
            if ($v -is [byte[]]) { $v = "bytes[$($v.Length)]:" + [BitConverter]::ToString($v[0..([Math]::Min(31, $v.Length - 1))]) }
            elseif ($v -is [array]) { $v = $v -join '|' }
            $out["$path\$name"] = "$v"
        }
    }
    return $out
}

function Get-VendorFiles {
    $out = @{}
    foreach ($dir in @(
        "${env:ProgramFiles(x86)}\CASPER EXCALIBUR",
        "$env:ProgramFiles\CASPER EXCALIBUR",
        "$env:ProgramData\CASPER EXCALIBUR")) {
        if (-not (Test-Path $dir)) { continue }
        Get-ChildItem $dir -Recurse -File -ErrorAction SilentlyContinue | ForEach-Object {
            $out[$_.FullName] = '{0} {1}' -f $_.Length, $_.LastWriteTime.ToString('yyyy-MM-dd HH:mm:ss')
        }
    }
    return $out
}

function Get-Drivers {
    $out = @{}
    foreach ($n in @('ControlCenter64', 'ControlCenterC64', 'nvlddmkm', 'igfx')) {
        $s = Get-CimInstance Win32_SystemDriver -Filter "Name='$n'" -ErrorAction SilentlyContinue
        if ($s) { $out["driver\$n"] = "$($s.State)/$($s.StartMode)/$($s.PathName)" }
    }
    foreach ($d in Get-PnpDevice -Class Display -ErrorAction SilentlyContinue) {
        $out["pnp\$($d.InstanceId)"] = "$($d.Status)/$($d.ConfigManagerErrorCode)/$($d.Present)"
    }
    foreach ($v in Get-CimInstance Win32_VideoController -ErrorAction SilentlyContinue) {
        $res = if ($v.CurrentHorizontalResolution) { "$($v.CurrentHorizontalResolution)x$($v.CurrentVerticalResolution)@$($v.CurrentRefreshRate)" } else { 'none' }
        $out["video\$($v.PNPDeviceID)"] = "$($v.Name)/$res/$($v.DriverVersion)"
    }
    return $out
}

function Get-State {
    $state = @{}
    foreach ($r in $registryRoots) {
        foreach ($kv in (Get-RegistryValues $r).GetEnumerator()) { $state[$kv.Key] = $kv.Value }
    }
    foreach ($kv in (Get-VendorFiles).GetEnumerator()) { $state["file\" + $kv.Key] = $kv.Value }
    foreach ($kv in (Get-Drivers).GetEnumerator()) { $state[$kv.Key] = $kv.Value }
    return $state
}

if (-not (Test-Path $OutDir)) { New-Item -ItemType Directory -Path $OutDir | Out-Null }

if ($Diff) {
    if ($Diff.Count -ne 2) { Write-Host "Give two labels to compare." -ForegroundColor Red; exit 1 }
    $a = Get-Content "$OutDir\$($Diff[0]).json" -Raw | ConvertFrom-Json
    $b = Get-Content "$OutDir\$($Diff[1]).json" -Raw | ConvertFrom-Json

    $ka = @{}; $a.PSObject.Properties | ForEach-Object { $ka[$_.Name] = $_.Value }
    $kb = @{}; $b.PSObject.Properties | ForEach-Object { $kb[$_.Name] = $_.Value }

    $changed = 0
    foreach ($k in ($ka.Keys + $kb.Keys | Sort-Object -Unique)) {
        $va = $ka[$k]; $vb = $kb[$k]
        if ("$va" -eq "$vb") { continue }
        $changed++
        if ($null -eq $va) { Write-Host "  + $k = $vb" -ForegroundColor Green }
        elseif ($null -eq $vb) { Write-Host "  - $k (was $va)" -ForegroundColor Red }
        else { Write-Host "  ~ $k`n      $va`n   -> $vb" -ForegroundColor Yellow }
    }
    if ($changed -eq 0) {
        Write-Host "Nothing changed between $($Diff[0]) and $($Diff[1])." -ForegroundColor Cyan
        Write-Host "That is a result: the choice is not kept anywhere Windows can see."
    } else {
        Write-Host "`n$changed difference(s)." -ForegroundColor Cyan
    }
    exit 0
}

if (-not $Label) { Write-Host "Give -Label or -Diff." -ForegroundColor Red; exit 1 }

$state = Get-State
$state | ConvertTo-Json -Depth 3 | Out-File "$OutDir\$Label.json" -Encoding utf8
Write-Host "Captured $($state.Count) values as '$Label'." -ForegroundColor Green
