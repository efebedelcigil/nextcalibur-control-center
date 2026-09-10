# Sandbox-Autopsy.ps1 - a full before/after of what an installer does to a
# machine. Reads only; writes nothing but its own snapshots.
#
#   powershell -ExecutionPolicy Bypass -File C:\Tools\Sandbox-Autopsy.ps1 -Label clean
#   ... install ...
#   powershell -ExecutionPolicy Bypass -File C:\Tools\Sandbox-Autopsy.ps1 -Label installed
#   ... uninstall ...
#   powershell -ExecutionPolicy Bypass -File C:\Tools\Sandbox-Autopsy.ps1 -Label removed
#
# Snapshots go to C:\Out, mapped writable to the host, so the analysis happens on
# the raw capture rather than on somebody reading numbers aloud.
#
# Run in a disposable sandbox. The point is to establish exactly what is being
# replaced: which files arrive, where, what signed them, which registry values
# appear, which services and drivers are registered - and then which of those
# survive an uninstall. A clone is only faithful if you know what the original
# does.
#
# Sizes, hashes, versions and paths are facts about a machine. No vendor file is
# read for its contents, and nothing of anybody's product is copied anywhere.

param(
    [Parameter(Mandatory)][string]$Label,
    [string]$OutDir = 'C:\Out'
)

$ErrorActionPreference = 'Continue'
$ProgressPreference = 'SilentlyContinue'

# Where an installer can plausibly leave something. Depth is capped per root: a
# full walk of System32 costs minutes and answers nothing extra, because what
# matters is a file appearing, and new files appear near the top of these trees.
$FileRoots = @(
    @{ Path = "${env:ProgramFiles(x86)}";                       Depth = 3 },
    @{ Path = "$env:ProgramFiles";                              Depth = 3 },
    @{ Path = "$env:ProgramData";                               Depth = 3 },
    @{ Path = "$env:windir\System32\drivers";                   Depth = 1 },
    @{ Path = "$env:windir\System32\wbem";                      Depth = 1 },
    @{ Path = "$env:windir\INF";                                Depth = 1 },
    @{ Path = "$env:windir\Temp";                               Depth = 2 },
    @{ Path = "$env:LOCALAPPDATA";                              Depth = 3 },
    @{ Path = "$env:APPDATA";                                   Depth = 3 },
    @{ Path = "$env:USERPROFILE\Desktop";                       Depth = 2 },
    @{ Path = "$env:ProgramData\Microsoft\Windows\Start Menu";  Depth = 4 }
)

# Hashing everything would take longer than the test. Hashes are for the things
# a clone has to match or avoid: executable code and the data files beside it.
$HashExtensions = @('.exe', '.dll', '.sys', '.ini', '.pow', '.mof', '.cat', '.inf')

function Get-Files {
    $out = @{}
    foreach ($root in $FileRoots) {
        if (-not (Test-Path $root.Path)) { continue }
        $files = Get-ChildItem -LiteralPath $root.Path -Recurse -File -Force -Depth $root.Depth -ErrorAction SilentlyContinue
        foreach ($f in $files) {
            $record = [ordered]@{
                size     = $f.Length
                modified = $f.LastWriteTimeUtc.ToString('s')
            }
            if ($HashExtensions -contains $f.Extension.ToLowerInvariant() -and $f.Length -lt 50MB) {
                # -ErrorAction Stop, or the catch never fires: a file that vanishes mid-scan
                # (Edge's updater deletes its own as it goes) raises a non-terminating
                # error, which prints and carries on rather than being caught.
                try { $record.sha256 = (Get-FileHash -LiteralPath $f.FullName -Algorithm SHA256 -ErrorAction Stop).Hash } catch { }
                $v = $f.VersionInfo
                if ($v.FileVersion) { $record.version = "$($v.FileVersion)".Trim() }
                if ($v.CompanyName) { $record.company = "$($v.CompanyName)".Trim() }
                try {
                    $sig = Get-AuthenticodeSignature -LiteralPath $f.FullName -ErrorAction Stop
                    if ($sig.Status -ne 'NotSigned') {
                        $record.signature = "$($sig.Status)"
                        if ($sig.SignerCertificate) { $record.signer = $sig.SignerCertificate.Subject.Split(',')[0] }
                    }
                } catch { }
            }
            $out[$f.FullName] = $record
        }
    }
    return $out
}

function Get-RegistryTree([string]$root, [int]$maxDepth) {
    $out = @{}
    try { $base = Get-Item -LiteralPath $root -ErrorAction Stop } catch { return $out }
    $queue = New-Object System.Collections.Queue
    $queue.Enqueue(@{ Key = $base; Depth = 0 })
    while ($queue.Count -gt 0) {
        $item = $queue.Dequeue()
        $k = $item.Key
        try {
            foreach ($n in $k.GetValueNames()) {
                $v = $k.GetValue($n)
                if ($v -is [byte[]]) { $v = "bytes[$($v.Length)]" }
                elseif ($v -is [array]) { $v = ($v -join '|') }
                $name = if ($n) { $n } else { '(default)' }
                $out["$($k.Name)\$name"] = "$v"
            }
            if ($item.Depth -lt $maxDepth) {
                foreach ($sub in $k.GetSubKeyNames()) {
                    try { $queue.Enqueue(@{ Key = $k.OpenSubKey($sub); Depth = $item.Depth + 1 }) } catch { }
                }
            }
        } catch { }
    }
    return $out
}

function Get-System {
    $out = [ordered]@{}

    foreach ($s in Get-CimInstance Win32_Service -ErrorAction SilentlyContinue) {
        $out["service\$($s.Name)"] = "$($s.State)/$($s.StartMode)/$($s.PathName)"
    }
    foreach ($d in Get-CimInstance Win32_SystemDriver -ErrorAction SilentlyContinue) {
        $out["driver\$($d.Name)"] = "$($d.State)/$($d.StartMode)/$($d.PathName)"
    }
    foreach ($t in Get-ScheduledTask -ErrorAction SilentlyContinue) {
        $out["task\$($t.TaskPath)$($t.TaskName)"] = "$($t.State)"
    }
    foreach ($line in (powercfg /list 2>$null)) {
        if ($line -match 'GUID:\s+(\S+)\s+\((.+?)\)') { $out["powerplan\$($Matches[2])"] = $Matches[1] }
    }
    foreach ($ns in @('root\wmi', 'root\cimv2')) {
        foreach ($c in (Get-CimClass -Namespace $ns -ErrorAction SilentlyContinue)) {
            if ($c.CimClassName -match '^(RW_|GMC_|CASPER|Excalibur|TF_|Quanta)') {
                $out["wmiclass\$ns\$($c.CimClassName)"] = 'present'
            }
        }
    }
    # Installers sometimes add a publisher certificate so their driver loads
    # without a prompt. That outlives an uninstall and is worth knowing about.
    foreach ($store in @('TrustedPublisher', 'Root', 'CA')) {
        foreach ($c in (Get-ChildItem "Cert:\LocalMachine\$store" -ErrorAction SilentlyContinue)) {
            $out["cert\$store\$($c.Thumbprint)"] = $c.Subject.Split(',')[0]
        }
    }
    foreach ($f in (Get-NetFirewallRule -ErrorAction SilentlyContinue)) {
        if ($f.DisplayName -match 'Control|Excalibur|Casper') {
            $out["firewall\$($f.DisplayName)"] = "$($f.Enabled)/$($f.Direction)/$($f.Action)"
        }
    }
    return $out
}

Write-Host "Capturing '$Label' - this takes a couple of minutes." -ForegroundColor Cyan
$started = Get-Date

$snapshot = [ordered]@{
    label    = $Label
    taken    = (Get-Date).ToString('s')
    machine  = (Get-CimInstance Win32_OperatingSystem).Caption
    files    = Get-Files
    registry = [ordered]@{}
    system   = Get-System
}

$registryRoots = @(
    @{ Path = 'HKLM:\SOFTWARE'; Depth = 2 },
    @{ Path = 'HKLM:\SYSTEM\CurrentControlSet\Control\WMI\Security'; Depth = 0 },
    @{ Path = 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Run'; Depth = 0 },
    @{ Path = 'HKLM:\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run'; Depth = 0 },
    @{ Path = 'HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Run'; Depth = 0 },
    @{ Path = 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall'; Depth = 1 },
    @{ Path = 'HKLM:\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall'; Depth = 1 },
    @{ Path = 'HKLM:\SYSTEM\CurrentControlSet\Services'; Depth = 1 }
)

foreach ($r in $registryRoots) {
    foreach ($kv in (Get-RegistryTree $r.Path $r.Depth).GetEnumerator()) {
        $snapshot.registry[$kv.Key] = $kv.Value
    }
}

if (-not (Test-Path $OutDir)) { New-Item -ItemType Directory -Path $OutDir -Force | Out-Null }
$path = Join-Path $OutDir "autopsy-$Label.json"
$snapshot | ConvertTo-Json -Depth 6 -Compress | Out-File $path -Encoding utf8

$elapsed = [int]((Get-Date) - $started).TotalSeconds
Write-Host "Wrote $path" -ForegroundColor Green
Write-Host ("  {0} files, {1} registry values, {2} system entries, in {3}s" -f $snapshot.files.Count, $snapshot.registry.Count, $snapshot.system.Count, $elapsed)
