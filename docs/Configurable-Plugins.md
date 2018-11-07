# Configurable Plugin
Each plugin can be configurable. Configurable plugin get configuration from database.
User will edit configuration throught UI or db directly.

To enable configuration for a plugin, you must inerith `IConfigurablePlugin`.

This let you:

- define default configuration (persisted at first usage)
- get condfiguration from database

Configuration is provided during startup so application must be restarted to reload (atm).
