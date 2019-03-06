$ErrorActionPreference = "Stop"

Write-Host "==> Building container..."
docker build -t crawler "$PSScriptRoot\Crawler"
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

Write-Host "`n==> Running container..."
dotnet run --rm crawler --project "$PSScriptRoot\Crawler"
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

exit 0
