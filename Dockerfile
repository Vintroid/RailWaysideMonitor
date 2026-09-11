FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY RailWaysideMonitor.csproj .
RUN dotnet restore
COPY . /src
RUN dotnet publish RailWaysideMonitor.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RailWaysideMonitor.dll"]
