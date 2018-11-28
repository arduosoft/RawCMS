FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app


# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY  dist .
ENTRYPOINT ["dotnet", "RawCMS.dll"]