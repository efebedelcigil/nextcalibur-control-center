<#
.SYNOPSIS
    Refuses to ship a self-contained build whose .NET runtime is behind the
    latest patch, or whose channel is out of support.

.DESCRIPTION
    A self-contained application carries its own runtime. Nothing on the
    machine ever patches those files - not Windows Update, not the .NET
    updater - so a release built against an old runtime pack carries that
    runtime's known vulnerabilities until the next release, on every machine
    that installs it.

    That is not hypothetical: 0.5.3 shipped .NET 8.0.30 on 13 September 2026,
    five days after 8.0.31 fixed five CVEs. The build machine had 8.0.30
    preinstalled and "8.0.x" was satisfied by it.

    So the release checks its own output: the version the runtime stamps into
    the binaries it carries, against Microsoft's release index. Behind the
    latest patch, or on a channel past its end of life, and the release stops
    here.

.PARAMETER Path
    The published application: the single-file executable, or a folder.

.PARAMETER Channel
    The .NET channel to check, e.g. 8.0.

.EXAMPLE
    tools\Check-BundledRuntime.ps1 -Path publish\Nextcalibur.exe
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$Path,
    [string]$Channel = "8.0"
)

$ErrorActionPreference = "Stop"

# The runtime writes its informational version - "8.0.31+<commit>" - into the
# assemblies it carries, so the question is answered by the artifact rather
# than by the build's intentions. A single-file bundle holds those strings
# both as ASCII and as UTF-16, so the pattern allows a NUL after each
# character and the digits are put back together afterwards.
$escaped = ($Channel.ToCharArray() | ForEach-Object { [regex]::Escape([string]$_) }) -join '\x00?'
$pattern = "$escaped\x00?\.\x00?((?:\d\x00?)+)\+"

$files = if (Test-Path $Path -PathType Container) {
    Get-ChildItem $Path -Recurse -Include *.exe, *.dll | Sort-Object Length -Descending | Select-Object -First 5
} else {
    @(Get-Item $Path)
}

# Latin-1: every byte becomes one character, so a byte pattern can be matched
# as text. GetEncoding(28591) rather than [Encoding]::Latin1, which Windows
# PowerShell 5.1 does not have.
$latin1 = [System.Text.Encoding]::GetEncoding(28591)
$found = @{}
foreach ($file in $files) {
    $text = $latin1.GetString([System.IO.File]::ReadAllBytes($file.FullName))
    foreach ($m in [regex]::Matches($text, $pattern)) {
        $patch = $m.Groups[1].Value -replace "\x00", ""
        $found["$Channel.$patch"] = $true
    }
}

if ($found.Count -eq 0) {
    throw "No .NET $Channel runtime version was found in $Path. This check does not pass a build it cannot read."
}

# The highest patch is the runtime's own: the desktop assemblies keep their
# 8.0.0 identity across the channel's life, so the lowest would always read
# 8.0.0 and the check would never pass. In a self-contained publish every
# runtime pack comes from the same release, so the highest is that release.
$bundled = ($found.Keys | ForEach-Object { [version]$_ } | Sort-Object -Descending)[0]
Write-Host "Bundled .NET runtime: $bundled  (found: $(($found.Keys | Sort-Object) -join ', '))"

$index = Invoke-RestMethod "https://raw.githubusercontent.com/dotnet/core/main/release-notes/releases-index.json"
$entry = $index.'releases-index' | Where-Object { $_.'channel-version' -eq $Channel }
if (-not $entry) { throw "Channel $Channel is not in Microsoft's release index." }

$latest = [version]$entry.'latest-release'
Write-Host "Latest published:     $latest ($($entry.'latest-release-date'); security release: $($entry.'security'))"
Write-Host "Support phase:        $($entry.'support-phase'), end of life $($entry.'eol-date')"

if ($entry.'support-phase' -eq 'eol') {
    throw "The .NET $Channel channel reached its end of life on $($entry.'eol-date'). A self-contained build on it would carry an unpatched runtime for ever. Retarget before releasing."
}

if ($bundled -lt $latest) {
    throw ("The build carries .NET $bundled but $latest is out ($($entry.'latest-release-date')). " +
           "A self-contained release is the only thing that patches these files. Install the newest " +
           "$Channel SDK on the build machine (setup-dotnet with check-latest: true) and build again.")
}

# Notice in time, so the retarget is planned rather than discovered.
$daysLeft = ([datetime]$entry.'eol-date' - (Get-Date)).Days
if ($daysLeft -lt 120) {
    Write-Warning "The .NET $Channel channel goes out of support in $daysLeft days ($($entry.'eol-date')). Plan the retarget."
}

Write-Host "The bundled runtime is current."
