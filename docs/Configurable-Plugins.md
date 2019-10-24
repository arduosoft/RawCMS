# Configurable Plugin
Each plugin can be configurable. Configurable plugin get configuration from database.
User will edit configuration throught UI or db directly.

To enable configuration for a plugin, you must inerith `IConfigurablePlugin`.

This let you:

- define default configuration (persisted at first usage): RawCMS willcreate a new instance of the configuration class and this will be stored to the db. Just fill the default values using default property values or costructors.
- get condfiguration from database: after the configuration is stored to database the first time, this will be reloaded. If you change it manually, after an application restart the value will be used.

Configuration is provided during startup so application must be restarted to reload (atm).

