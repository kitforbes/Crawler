FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore /nologo

# Copy and build everything else
COPY . ./
RUN dotnet publish -c Release -o out /nologo
ENTRYPOINT ["dotnet", "out/Crawler.dll"]
