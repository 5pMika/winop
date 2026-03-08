$pkg = Get-AppxPackage -Name '*Winop*' | Select-Object -First 1
if ($pkg) {
    Start-Process 'explorer.exe' "shell:AppsFolder\$($pkg.PackageFamilyName)!App"
    Write-Host 'Launching Winop...' -ForegroundColor Green
} else {
    Write-Host 'Winop is not installed. Run build-and-run.bat first, or deploy from Visual Studio (F5).' -ForegroundColor Yellow
    exit 1
}
