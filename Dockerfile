FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY  dist .
ENTRYPOINT ["dotnet", "RawCMS.dll"]