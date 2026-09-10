# Sandbox-Vendor-Probe.ps1 - watch what the vendor's Control Center installs,
# and what it leaves behind when it is removed. Reads only.
#
#   powershell -ExecutionPolicy Bypass -File C:\Tools\Sandbox-Vendor-Probe.ps1 -Label before
#   ... install the vendor software ...
#   powershell -ExecutionPolicy Bypass -File C:\Tools\Sandbox-Vendor-Probe.ps1 -Label installed
#   ... uninstall it ...
#   powershell -ExecutionPolicy Bypass -File C:\Tools\Sandbox-Vendor-Probe.ps1 -Label removed
#   powershell -ExecutionPolicy Bypass -File C:\Tools\Sandbox-Vendor-Probe.ps1 -Diff before,installed
#
# Run in a disposable sandbox, never on a machine anybody depends on.
#
# The question this exists for: Nextcalibur's readings stopped when the vendor
# software was uninstalled from the development laptop, and the explanation -
# that its installer widens the security descriptor on the firmware data block
# and its uninstaller narrows it again - was inference. The descriptor was only
# ever read after the fact. A sandbox that has never had either program on it
# can answer the question directly.
#
# The sandbox has no such data block behind it, so nothing here proves what the
# software does to the hardware. What it can show is what the installer writes,
# which is the half that was guessed at.

param(
    [string]$Label,
    [string[]]$Diff,
    [string]$OutDir = "$env:USERPROFILE\Desktop\vendor-probe"
)

$ErrorActionPreference = 'Continue'
$MailboxGuid = '644c5791-b7b0-4123-a90b-e93876e0daad'

function Get-State {
    $state = [ordered]@{}

    # The security descriptors on kernel-WMI data blocks. The one that matters is
    # the mailbox's, but the count is worth having too: an installer that adds
    # several is doing something broader than one interface.
    $secPath = 'HKLM:\SYSTEM\CurrentControlSet\Control\WMI\Security'
    $sec = Get-ItemProperty $secPath -ErrorAction SilentlyContinue
    $names = @()
    if ($sec) { $names = @($sec.PSObject.Properties.Name | Where-Object { $_ -notlike 'PS*' }) }
    $state['wmi-security-count'] = $names.Count
    $state['wmi-security-has-mailbox-guid'] = ($names -contains $MailboxGuid)
    if ($names -contains $MailboxGuid) {
        try {
            $sd = (Get-ItemProperty $secPath -Name $MailboxGuid).$MailboxGuid
            $raw = New-Object Security.AccessControl.RawSecurityDescriptor ($sd, 0)
            $state['wmi-security-mailbox-sddl'] = $raw.GetSddlForm('All')
        } catch { $state['wmi-security-mailbox-sddl'] = 'unreadable' }
    }

    foreach ($root in @(
        'HKLM:\SOFTWARE\WOW6432Node\CASPER EXCALIBUR',
        'HKLM:\SOFTWARE\CASPER EXCALIBUR')) {
        try {
            $keys = @(Get-Item $root -ErrorAction Stop) + @(Get-ChildItem $root -Recurse -ErrorAction SilentlyContinue)
            foreach ($k in $keys) {
                foreach ($n in $k.GetValueNames()) {
                    $v = $k.GetValue($n)
                    if ($v -is [byte[]]) { $v = "bytes[$($v.Length)]" }
                    elseif ($v -is [array]) { $v = $v -join '|' }
                    $state["reg\$($k.Name)\$n"] = "$v"
                }
            }
        } catch { }
    }

    foreach ($s in Get-CimInstance Win32_SystemDriver -ErrorAction SilentlyContinue) {
        if ($s.Name -match 'Control|Excalibur|Casper|RW') {
            $state["driver\$($s.Name)"] = "$($s.State)/$($s.StartMode)/$($s.PathName)"
        }
    }
    foreach ($s in Get-CimInstance Win32_Service -ErrorAction SilentlyContinue) {
        if ($s.Name -match 'Control|Excalibur|Casper') {
            $state["service\$($s.Name)"] = "$($s.State)/$($s.StartMode)/$($s.PathName)"
        }
    }

    foreach ($dir in @("${env:ProgramFiles(x86)}\CASPER EXCALIBUR", "$env:ProgramFiles\CASPER EXCALIBUR")) {
        if (-not (Test-Path $dir)) { continue }
        foreach ($f in Get-ChildItem $dir -Recurse -File -ErrorAction SilentlyContinue) {
            $state["file\$($f.FullName)"] = "$($f.Length)"
        }
    }

    foreach ($f in Get-ChildItem "$env:windir\System32\drivers" -ErrorAction SilentlyContinue) {
        if ($f.Name -match 'Control|Excalibur|Casper') { $state["sys\$($f.Name)"] = "$($f.Length)" }
    }

    foreach ($line in (powercfg /list 2>$null)) {
        if ($line -match 'GUID:\s+(\S+)\s+\((.+?)\)') { $state["powerplan\$($Matches[2])"] = $Matches[1] }
    }

    foreach ($t in (Get-ScheduledTask -ErrorAction SilentlyContinue)) {
        if ($t.TaskName -match 'Control|Excalibur|Casper') { $state["task\$($t.TaskName)"] = $t.State }
    }

    foreach ($runKey in @('HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Run',
                          'HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Run')) {
        $r = Get-ItemProperty $runKey -ErrorAction SilentlyContinue
        if ($r) {
            foreach ($n in ($r.PSObject.Properties.Name | Where-Object { $_ -notlike 'PS*' })) {
                $state["startup\$runKey\$n"] = "$($r.$n)"
            }
        }
    }

    # WMI classes the installer may compile into the repository.
    foreach ($c in @('RW_GMWMI', 'GMC_WMIEvent')) {
        $exists = $null -ne (Get-CimClass -Namespace root\wmi -ClassName $c -ErrorAction SilentlyContinue)
        $state["wmiclass\$c"] = $exists
    }

    return $state
}

if (-not (Test-Path $OutDir)) { New-Item -ItemType Directory -Path $OutDir -Force | Out-Null }

if ($Diff) {
    if ($Diff.Count -ne 2) { Write-Host "Give two labels." -ForegroundColor Red; exit 1 }
    $a = @{}; (Get-Content "$OutDir\$($Diff[0]).json" -Raw | ConvertFrom-Json).PSObject.Properties | ForEach-Object { $a[$_.Name] = "$($_.Value)" }
    $b = @{}; (Get-Content "$OutDir\$($Diff[1]).json" -Raw | ConvertFrom-Json).PSObject.Properties | ForEach-Object { $b[$_.Name] = "$($_.Value)" }

    $n = 0
    foreach ($k in ($a.Keys + $b.Keys | Sort-Object -Unique)) {
        if ($a[$k] -eq $b[$k]) { continue }
        $n++
        if (-not $a.ContainsKey($k)) { Write-Host "  + $k = $($b[$k])" -ForegroundColor Green }
        elseif (-not $b.ContainsKey($k)) { Write-Host "  - $k (was $($a[$k]))" -ForegroundColor Red }
        else { Write-Host "  ~ $k`n      $($a[$k])`n   -> $($b[$k])" -ForegroundColor Yellow }
    }
    Write-Host ""
    Write-Host "$n difference(s) between $($Diff[0]) and $($Diff[1])." -ForegroundColor Cyan
    exit 0
}

if (-not $Label) { Write-Host "Give -Label or -Diff." -ForegroundColor Red; exit 1 }

$state = Get-State
$state | ConvertTo-Json -Depth 3 | Out-File "$OutDir\$Label.json" -Encoding utf8
Write-Host "Captured $($state.Count) values as '$Label'." -ForegroundColor Green
Write-Host "  mailbox descriptor present: $($state['wmi-security-has-mailbox-guid'])"
if ($state['wmi-security-mailbox-sddl']) { Write-Host "  $($state['wmi-security-mailbox-sddl'])" }
