try {
    $env:ASPNETCORE_ENVIRONMENT = 'Development'
    $env:ASPNETCORE_URLS = 'http://localhost:5000;https://localhost:5001'
    Write-Host "Starting Inkzen.Api session."
    Push-Location "$PSScriptRoot\Inkzen.Api\bin\Debug"
    Start-Process dotnet -ArgumentList Inkzen.Api.dll -NoNewWindow -Wait -PassThru
}
finally {
    Write-Host "Closed Inkzen.Api session."
    Pop-Location
    # [Environment]::Exit(0)
}
