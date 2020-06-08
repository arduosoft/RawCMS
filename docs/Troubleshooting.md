## On your local

1. Check configuration.

API and UI are two different application and must be linked.

![](https://github.com/arduosoft/RawCMS/blob/master/asset/docimages/architecture.png?raw=true)

In future api and UI will be separated to implement a standalone (but scalable or splittable) web application.
Now API and UI are separated and lives in different universes. That implies we need to configure two different settings

### UI settings

/env/env.json

```
{
  "api": {
    "baseUrl": "http://localhost:28436" <== This is the API url without trailing slash at the end
  },
  "login": {
    "grant_type": "password",
    "scope": "openid",
    "client_id": "raw.client", <= this must match the value in _congif collection, the row for auth plugin. This is the default value, valid for dev
    "client_secret": "raw.secret" <= this must match the value in _congif collection, the row for auth plugin. This is the default value, valid for dev
  }
}

```

### API settings

All settings are stored into \_config table and are editable (doesn't support the hot reload, so you need to reboot APPI after change).
The only settings we have on file is the connection string.

appsettings.json

```
{
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Warning"
    }
  },

  "PluginPath": "../../../Plugins",
  "MongoSettings": {
    "ConnectionString": "mongodb://localhost:27017/rawCms"
  }
}
```

The port or generally the baseUrl must point to the correct API url. Just check ISSExpress tray icon and open on browser to check it.

2. Check API configuration.
   Standard port from dev is 28436. You should run the RawCMS config and have this settings:

![](https://github.com/arduosoft/RawCMS/blob/master/asset/docimages/projectsettings.png?raw=true)

![](https://github.com/arduosoft/RawCMS/blob/master/asset/docimages/runsettings.png?raw=true)

3. Log Files
   the log file paths are configured in

/cong/NLog.Development.config

You can find here the location of log files.

4. asking for help

- for specific question add a reply on issue thread is the perfect way. This makes the issue transparent and helps build the community
- for general question not related to the task, we can ask on gitter or general slack channel
- for problem with codebase or so on, just use direct communicaton. Of course, anything we learn of use must be added to doc
  - any problem must be transformed into issues
  - any news must be translated to documentation

## Common errors

### Failed to load resource: net::ERR_CONNECTION_REFUSED

the base url is not correct, or API is not started yet
