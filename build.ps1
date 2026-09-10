# build.ps1 - build, publish and package Nextcalibur into a Setup.exe
#
#   .\build.ps1                 build + package
#   .\build.ps1 -Version 0.2.0  package a specific version
#
# Requires: .NET 8 SDK, and the Velopack CLI (dotnet tool install -g vpk)

param([string]$Version = "0.4.0")

$ErrorActionPreference = 'Stop'
$env:PATH += ";$env:USERPROFILE\.dotnet\tools"
Set-Location $PSScriptRoot

Write-Host "==> Building" -ForegroundColor Cyan
dotnet build Nextcalibur.sln -c Release --nologo /p:Version=$Version

Write-Host "==> Publishing (self-contained, win-x64)" -ForegroundColor Cyan
Remove-Item publish -Recurse -Force -ErrorAction SilentlyContinue
# -p:Version keeps the assembly version and the package version in step; they
# drifted apart once already.
dotnet publish src\Nextcalibur.App\Nextcalibur.App.csproj `
    -c Release -o publish --nologo /p:Version=$Version
Remove-Item publish\*.pdb, publish\*.xml -ErrorAction SilentlyContinue

Write-Host "==> Packaging Setup.exe" -ForegroundColor Cyan

# Velopack refuses to pack a version that already exists in the releases folder,
# which means a second run of this script for the same version fails rather than
# rebuilding it. Building the same version twice is the normal case while
# working on it, so the previous attempt is cleared first. Earlier versions stay,
# because the delta is built against them.
Remove-Item "releases\Nextcalibur-$Version-*.nupkg" -ErrorAction SilentlyContinue
vpk pack `
    --packId Nextcalibur `
    --packVersion $Version `
    --packDir publish `
    --mainExe Nextcalibur.exe `
    --packTitle "Nextcalibur Control Center" `
    --packAuthors "Efe Bedelcigil" `
    --icon src\Nextcalibur.App\Assets\app.ico `
    -o releases

Write-Host "==> Done" -ForegroundColor Green
Get-ChildItem releases | Select-Object Name, @{n='MB';e={[math]::Round($_.Length/1MB,1)}}
