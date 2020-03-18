# Build New Plugin

## The project file template

Each plugin project must start from following project structure.

```xml
<Project Sdk="Microsoft.NET.Sdk">

	<PropertyGroup>
		<TargetFramework>netcoreapp2.1</TargetFramework>
		<IsPlugin>true</IsPlugin>
		<UseNETCoreGenerator>true</UseNETCoreGenerator>
	</PropertyGroup>

	<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
		<OutputPath></OutputPath>
	</PropertyGroup>

	<ItemGroup>
		<PackageReference Include="IdentityServer4" Version="2.2.0" />
		<PackageReference Include="IdentityServer4.AccessTokenValidation" Version="2.6.0" />
		<PackageReference Include="IdentityServer4.AspNetIdentity" Version="2.1.0" />
		<PackageReference Include="IdentityServer4.Contrib.LocalAccessTokenValidation" Version="1.0.1" />
		<PackageReference Include="Jint" Version="2.11.58" />
		<PackageReference Include="McMaster.NETCore.Plugins" Version="0.2.4" />
		<PackageReference Include="McMaster.NETCore.Plugins.Sdk" Version="0.2.4" />
		<PackageReference Include="Microsoft.AspNetCore" Version="2.0.3" />
		<PackageReference Include="Microsoft.AspNetCore.Authentication.OpenIdConnect" Version="2.1.2" />
		<PackageReference Include="Microsoft.AspNetCore.Authorization" Version="2.1.2" />
		<PackageReference Include="Microsoft.AspNetCore.Identity" Version="2.1.3" />
		<PackageReference Include="Microsoft.AspNetCore.Rewrite" Version="2.2.0" />
		<PackageReference Include="Microsoft.Extensions.Configuration" Version="2.2.0" />

		<PackageReference Include="Microsoft.AspNetCore.Authorization" Version="2.1.2" />
		<PackageReference Include="Microsoft.Extensions.Options" Version="2.2.0" />
		<PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="2.1.1" />
		<PackageReference Include="MongoDB.Driver" Version="2.6.0" />
		<PackageReference Include="MongoDB.Driver.Core" Version="2.6.0" />
		<PackageReference Include="Newtonsoft.Json" Version="12.0.3" />


		<PackageReference Include="Microsoft.AspNetCore.Diagnostics" Version="2.1.1" />
		<PackageReference Include="Microsoft.AspNetCore.Identity" Version="2.1.3" />
		<PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.1.1" />
		<PackageReference Include="Microsoft.AspNetCore.Server.IISIntegration" Version="2.1.1" />
		<PackageReference Include="Microsoft.AspNetCore.Server.Kestrel" Version="2.1.1" />
		<PackageReference Include="Microsoft.AspNetCore.Server.Kestrel.Https" Version="2.1.1" />
		<PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="2.1.1" />
		<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="2.1.1" />
		<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="2.1.1" />
		<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="2.1.1" />
		<PackageReference Include="Microsoft.Extensions.Configuration.Binder" Version="2.1.1" />
		<PackageReference Include="Microsoft.Extensions.Logging.Console" Version="2.1.1" />
		<PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="2.1.1" />
		<PackageReference Include="Microsoft.VisualStudio.Web.BrowserLink" Version="2.1.1" />
		<PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="2.1.1" />
		<PackageReference Include="NLog" Version="4.5.6" />
		<PackageReference Include="NLog.Config" Version="4.5.6" />
		<PackageReference Include="NLog.Web.AspNetCore" Version="4.5.4" />
		<PackageReference Include="Swashbuckle.AspNetCore" Version="2.5.0" />
		<PackageReference Include="Swashbuckle.AspNetCore.Swagger" Version="4.0.1" />
		<PackageReference Include="Swashbuckle.AspNetCore.SwaggerGen" Version="4.0.1" />
		<PackageReference Include="Swashbuckle.AspNetCore.SwaggerUi" Version="4.0.1" />
	</ItemGroup>

	<ItemGroup>
		<ProjectReference Include="..\..\RawCMS.Library\RawCMS.Library.csproj" />
	</ItemGroup>


	<Target Name="PublishUI" AfterTargets="Build">
		<RemoveDir Directories="$(OutDir)\UI" />
		<ItemGroup>
			<Files Include="$(ProjectDir)UI\**" />
		</ItemGroup>
		<Copy SourceFiles="@(Files)" DestinationFiles="@(Files->'$(OutDir)\UI\%(RecursiveDir)%(Filename)%(Extension)')" />
	</Target>
</Project>
```

## Folder Structure for the project

A standard structure for a plugin is the following

```
+	UI
+	Configuration
+	Extensions
+	Lambdas
+	Middlewares
+	Model
+	Services
PluginClass.cs
```

## Plugin UI

The content of UI folder will be served as static file from the directory:

`/app/modules/<modulename>`

The <modulename> placeholder is the slug of the plugin. Automatically is computed by the DLL name (RawCMS.Plugins.MyPlugin => myplugin), but can be specified on plugin config.

The UI plugin component must have the following structure

```
+	config.js
+	assets
+	components
+	services
+	utils
+	views
```

The config.js file init the configuration on the frontend side, see the example:

```js
const _configCoreModule = {
  namespaced: true,
  name: "core",

  getters: {
    menuItems() {
      return [
        { icon: "mdi-home", text: "Home", route: "home" }
        /* ... */
      ];
    }
  },
  getRoutes() {
    return [
      {
        path: "/login",
        name: "login",
        component: async (res, rej) => await LoginView(res, rej),
        meta: {
          requiresAuth: false
        }
      }
      /* ... */
    ];
  },
  state: {
    /* ... */
  },
  mutations: {
    /* ... */
  },
  actions: {
    /* ... */
  }
};

export const configCoreModule = _configCoreModule;
export default _configCoreModule;
```

The menuItems getter provide men� items for the app, The getRoutes method provides routes for app.

## Plugin class

You can have one or more Plugin classes. One per project\dll is recommended for a better portability of components.

To build your own plugin please follow below steps:

- Fork this repo
- Create you project named RawCMS.Plugins.\{plugin name} inside Plugins folder
- Overraid defaults using project tempate on this page
- Add references to RawCMS.Library and RawCMS.Plugins.Core
- Create plugin class that inerith from RawCMS.Library.Core.Extension.Plugin

The following is the example of KeyStorePlugin.

Given a settings class, KeyStoreSettings in this example, you must declare a class that implements RawCMS.Library.Core.Extension.Plugin, IConfigurablePlugin<ClassConfig>

```json

    public class KeyStorePlugin : RawCMS.Library.Core.Extension.Plugin, IConfigurablePlugin<KeyStoreSettings>
    {
        public override string Name => "KeyStore";

        public override string Description => "Add KeyStore capabilities";

        public KeyStorePlugin(AppEngine appEngine, KeyStoreSettings config, ILogger logger) : base(appEngine, logger)
        {
            //init here
        }


        public override void ConfigureServices(IServiceCollection services)
        {
            //Add your plugin services here
        }

        public override void Configure(IApplicationBuilder app)
        {
			//configure app here
        }

        public override void ConfigureMvc(IMvcBuilder builder)
        {
			//alter mvc config here
        }

        public override void Setup(IConfigurationRoot configuration)
        {
			//setup configuration
        }
    }
```

For the UI part you may need some additional settings:

```cs
 public class UIPlugin : RawCMS.Library.Core.Extension.Plugin, IConfigurablePlugin<UIPluginConfig>
    {

        public override string Slug => "core";


        public override UIMetadata GetUIMetadata()
        {
            var metadata = new UIMetadata();

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type =UIResourceRequirementType.CSS,
                ResourceUrl= "https://fonts.googleapis.com/css?family=Roboto:100,300,400,500,700,900",
                Order=0
            });


            //JS

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://cdn.jsdelivr.net/npm/vue@2.x/dist/vue.js",
                Order =1000
            });

            return metadata;
        }
    }
```

### get configuration

Follow the example where we want an instance of KeyStoreSettings class from config.

1. add `IConfigurablePlugin<KeyStoreSettings>` inetithance. This tell you will need this and you depency will be loaded.
2. ask for an instance adding KeyStoreSettings into the constructor parameter

### working with main bundle

Plugin compiles and produce dll into /plugin folder into RawCMS folder (view on prebuild event for RawCMS project). This folder is scan by default at startup taking from `"PluginPath": "../../../Plugins",`

Path can be relative to the app path or absolute.

During release on public dockerhub appsettings.json is replaced with appsettings.Docker.json, this to remove the constraint of running docker image with flag `ASPNETCORE_ENVIRONMENT=Docker`

### Build and deplyments technicalities

During the build each project build as a regular project on its own output folder (bin\xxx\debug...). During the build the project task copy also static files without need of adding them as content on the solution. WE DONT HAVE TO ADD THEM MANUALLY.

At startup app engine looks recursively on the Plugin folder, finding all plugin.connfig files and identifies the binary folders to be used for the assembly loading.

On dev\debugging the UI files copied on the bin foldera are ignored. The application serves by convention\configuration the folder inside the visual studio project, so you can change the js source code and such changes reflect immeddiately on the application.

**note:** on some cases browser chaches the js file, just open the file in a new tab and CTRL+F5
