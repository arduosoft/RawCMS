## Docker publish

publish on dist filder. This step produce binaries that will be published as artifact into docker images.

```
dotnet publish RawCMS\RawCMS.csproj -o ../dist
```

Build the image. This can be done as usual. This is part of the release on docker hub.
```
docker build -t rawcms .
```

# Run locally on dokcer

To run the single container just use docker run. This require all env parameters have to be passed as command line argument or env file.

```
docker run rawcms -p 80:8081
```

Run via docker compose. This is easy using docker-compose.yml into project root. running

```
local.bat
```
will be started a volatile enviroment with mongodb+ rawcms (available on http://localhost:54321)


# Dockerhub deployment

Through appeveyour project is compiled and deployed after a success pull request on master branch. 