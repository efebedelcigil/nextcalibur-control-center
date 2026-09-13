<#
.SYNOPSIS
    Refuses to build a release against a .NET channel that is out of support,
    and - for a build that carries its own runtime - one that is behind the
    latest patch.

.DESCRIPTION
    Nextcalibur is framework-dependent since 0.5.4: the runtime is installed
    on the machine, patched by Windows, and kept current by the application
    itself (DotNetRuntimeDependency). What a release still has to be sure of
    is that the channel it targets is one Microsoft is still fixing - after
    a channel's end of life nobody patches it at all, and shipping against
    it would quietly leave every machine on an unfixable runtime.

    It also still handles the older shape: if the published output carries a
    runtime, the patch it carries must be the newest one, because nothing on
    a machine ever updates that. 0.5.3 shipped .NET 8.0.30 five days after
    8.0.31 fixed five CVEs, and this check exists because of it.

.PARAMETER Channel
    The .NET channel the application targets, e.g. 8.0.

.PARAMETER Path
    Optional: the published application. When it carries a runtime, the
    patch is checked as well.

.EXAMPLE
    tools\Check-Runtime.ps1 -Channel 8.0 -Path publish\Nextcalibur.exe
#>
[CmdletBinding()]
param(
    [string]$Channel = "8.0",
    [string]$Path
)

$ErrorActionPreference = "Stop"

$index = Invoke-RestMethod "https://raw.githubusercontent.com/dotnet/core/main/release-notes/releases-index.json"
$entry = $index.'releases-index' | Where-Object { $_.'channel-version' -eq $Channel }
if (-not $entry) { throw "Channel $Channel is not in Microsoft's release index." }

$latest = [version]$entry.'latest-release'
Write-Host "Channel $Channel : latest $latest ($($entry.'latest-release-date'); security release: $($entry.'security'))"
Write-Host "Support phase:    $($entry.'support-phase'), end of life $($entry.'eol-date')"

if ($entry.'support-phase' -eq 'eol') {
    throw "The .NET $Channel channel reached its end of life on $($entry.'eol-date'). Nothing patches it any more, on any machine. Retarget before releasing."
}

$daysLeft = ([datetime]$entry.'eol-date' - (Get-Date)).Days
if ($daysLeft -lt 120) {
    Write-Warning "The .NET $Channel channel goes out of support in $daysLeft days ($($entry.'eol-date')). Plan the retarget."
}

if ($Path) {
    if (-not (Test-Path $Path)) { throw "There is nothing at $Path to check." }

    # A self-contained build stamps the runtime's own version into what it
    # carries; a framework-dependent one does not, and there is nothing to
    # check - the machine's runtime is the machine's business.
    $escaped = ($Channel.ToCharArray() | ForEach-Object { [regex]::Escape([string]$_) }) -join '\x00?'
    $pattern = "$escaped\x00?\.\x00?((?:\d\x00?)+)\+"
    $latin1 = [System.Text.Encoding]::GetEncoding(28591)

    $files = if (Test-Path $Path -PathType Container) {
        Get-ChildItem $Path -Recurse -Include *.exe, *.dll | Sort-Object Length -Descending | Select-Object -First 5
    } else {
        @(Get-Item $Path)
    }

    $found = @{}
    foreach ($file in $files) {
        $text = $latin1.GetString([System.IO.File]::ReadAllBytes($file.FullName))
        foreach ($m in [regex]::Matches($text, $pattern)) {
            $patch = $m.Groups[1].Value -replace "\x00", ""
            $found["$Channel.$patch"] = $true
        }
    }

    # The desktop assemblies keep their 8.0.0 identity across the channel's
    # life, so anything above that is the runtime's own patch - and a
    # framework-dependent build has nothing above it.
    $carried = $found.Keys | ForEach-Object { [version]$_ } | Where-Object { $_.Build -gt 0 } | Sort-Object -Descending | Select-Object -First 1

    if (-not $carried) {
        Write-Host "The build carries no runtime of its own (framework-dependent): the machine's runtime is what runs it."
    }
    elseif ($carried -lt $latest) {
        throw ("The build carries .NET $carried but $latest is out ($($entry.'latest-release-date')). " +
               "Nothing on a machine patches a runtime carried inside an application. Install the newest " +
               "$Channel SDK on the build machine (setup-dotnet with check-latest: true) and build again.")
    }
    else {
        Write-Host "The runtime it carries ($carried) is current."
    }
}

Write-Host "The runtime channel is supported."
