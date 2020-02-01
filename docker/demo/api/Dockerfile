FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ./docker/config/api/buildscript.sh ./build.sh
COPY . .

RUN  chmod +x ./build.sh && ./build.sh


FROM base AS final
WORKDIR /app
COPY --from=build /dist .
ENTRYPOINT ["dotnet", "RawCMS.dll"]
