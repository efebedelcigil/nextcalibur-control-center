# Write-IntegrityManifest.ps1 - the release's list of file hashes
#
#   tools\Write-IntegrityManifest.ps1 -Path publish
#
# Writes Nextcalibur.integrity.json into the publish folder: every file in it,
# by name, with its SHA-256. The application compares its own files with this
# list at start and every hour (src\Nextcalibur.Core\Security\Integrity.cs),
# and stops writing to the firmware when they differ.
#
# Run after anything that changes a file - signing included - and before
# packaging. Fails, and so fails the build, when the executable is not on it.

param([Parameter(Mandatory = $true)][string]$Path)

$ErrorActionPreference = 'Stop'
$name = 'Nextcalibur.integrity.json'
$manifest = Join-Path $Path $name

$hashes = [ordered]@{}
Get-ChildItem -LiteralPath $Path -File | Where-Object { $_.Name -ne $name } | Sort-Object Name | ForEach-Object {
    $hashes[$_.Name] = (Get-FileHash -LiteralPath $_.FullName -Algorithm SHA256).Hash
}

if (-not $hashes.Contains('Nextcalibur.exe')) {
    Write-Error "Nextcalibur.exe is not in $Path; nothing to vouch for."
}
if (Get-ChildItem -LiteralPath $Path -Directory) {
    # The check reads names in its own folder only; a subfolder would go unchecked.
    Write-Error "$Path has subfolders; the integrity list covers files beside the executable only."
}

$json = $hashes | ConvertTo-Json
[IO.File]::WriteAllText((Resolve-Path $Path).Path + "\$name", $json, (New-Object System.Text.UTF8Encoding $false))
Write-Host "Integrity list: $($hashes.Count) file(s) -> $manifest"
