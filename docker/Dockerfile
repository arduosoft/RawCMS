FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.2 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:3.1.200 AS build
WORKDIR /src
COPY ./docker/config/api/buildscript.sh ./build.unix.sh
COPY . .

RUN chmod +x ./build.unix.sh && bash /src/build.unix.sh

FROM base AS final
WORKDIR /app
COPY --from=build /dist/. .
ENTRYPOINT ["dotnet", "RawCMS.dll"]
