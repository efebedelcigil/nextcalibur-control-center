# build.ps1 - build, publish and package Nextcalibur into a Setup.exe
#
#   .\build.ps1                 build + package
#   .\build.ps1 -Version 0.2.0  package a specific version
#
# Requires: .NET 8 SDK, and the Velopack CLI (dotnet tool install -g vpk)

param([string]$Version = "0.3.0")

$ErrorActionPreference = 'Stop'
$env:PATH += ";$env:USERPROFILE\.dotnet\tools"
Set-Location $PSScriptRoot

Write-Host "==> Building" -ForegroundColor Cyan
dotnet build Nextcalibur.sln -c Release --nologo /p:Version=$Version

Write-Host "==> Publishing (framework-dependent, win-x64)" -ForegroundColor Cyan
Remove-Item publish -Recurse -Force -ErrorAction SilentlyContinue
# -p:Version keeps the assembly version and the package version in step; they
# drifted apart once already.
dotnet publish src\Nextcalibur.App\Nextcalibur.App.csproj `
    -c Release -o publish --nologo /p:Version=$Version
Remove-Item publish\*.pdb, publish\*.xml -ErrorAction SilentlyContinue

Write-Host "==> Packaging Setup.exe" -ForegroundColor Cyan
vpk pack `
    --packId Nextcalibur `
    --packVersion $Version `
    --packDir publish `
    --mainExe Nextcalibur.exe `
    --packTitle "Nextcalibur Control Center" `
    --packAuthors "Efe Bedelcigil" `
    --framework net8.0-x64-desktop `
    --icon src\Nextcalibur.App\Assets\app.ico `
    -o releases

Write-Host "==> Done" -ForegroundColor Green
Get-ChildItem releases | Select-Object Name, @{n='MB';e={[math]::Round($_.Length/1MB,1)}}
