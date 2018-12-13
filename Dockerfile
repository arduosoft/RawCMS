FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY  dist .
#ENTRYPOINT ["dotnet", "RawCMS.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet RawCMS.dll