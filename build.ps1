$ErrorActionPreference = "Stop"

Set-Location -Path $PSScriptRoot

Write-Host "==> Building container..."
docker build -t crawler "$PSScriptRoot\Crawler"
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

Write-Host "`n==> Running container..."
docker run --rm crawler $args
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

exit 0
