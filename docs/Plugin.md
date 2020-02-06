# Build New Plugin

To build your own plugin please follow below steps:

- Create you project named RawCMS.Plugins.\{plugin name} inside Plugins folder
- Add references to RawCMS.Library and RawCMS.Plugins.Core
- Add all library reference of the core plugin. ** this is mandatory **
- Create plugin class that inerith from RawCMS.Library.Core.Extension.Plugin

```json
   
    public class KeyStorePlugin : RawCMS.Library.Core.Extension.Plugin, IConfigurablePlugin<KeyStoreSettings>
    {
        public override string Name => "KeyStore";

        public override string Description => "Add KeyStore capabilities";

        private readonly KeyStoreSettings config;
		private readonly AppEngine appEngine;

        public KeyStorePlugin(AppEngine appEngine, KeyStoreSettings config, ILogger logger) : base(appEngine, logger)
        {
            this.config = config;			
        }
        public override void Init()
        {
            Logger.LogInformation("KeyStore plugin loaded");
        }

        private KeyStoreService keyStoreService = new KeyStoreService();

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<KeyStoreService, KeyStoreService>();
        }        

        public override void Configure(IApplicationBuilder app)
        {
        }

        public override void ConfigureMvc(IMvcBuilder builder)
        {
        }

        public override void Setup(IConfigurationRoot configuration)
        {
        }
    }
```
This is an example of base plugin

- Reference you plugin class on Startup class of RawCMS project (Only for now)
- Implement ConfigureService method on plugin for DI
- Implement Configure method on plugin

# get configuration
Follow the example where we want an instance of KeyStoreSettings class from config.


1. add `IConfigurablePlugin<KeyStoreSettings>` inetithance. This tell you will need this and you depency will be loaded.
2. ask for an instance adding  KeyStoreSettings into the constructor parameter



# working with main bundle
Plugin compiles and produce dll into /plugin folder into RawCMS folder (view on prebuild event for RawCMS project). This folder is scan by default at startup taking from `"PluginPath": "../../../Plugins",`

Path can be relative to the app path or absolute.

During release on  public dockerhub appsettings.json is replaced with appsettings.Docker.json, this to remove the constraint of running docker image with flag `ASPNETCORE_ENVIRONMENT=Docker`
