FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine
WORKDIR /app
COPY  dist .
COPY  dist/appsettings.Docker.json ./appsettings.json
#ENTRYPOINT ["dotnet", "RawCMS.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet RawCMS.dll
