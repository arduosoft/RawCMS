using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Core.Extension
{
    /// <summary>
    /// RawCMS plugin definitio
    /// </summary>
    public abstract class Plugin : IRequireApp, IInitable
    {
        public virtual int Priority => 1;
        public abstract string Name { get; }
        public abstract string Description { get; }
        public ILogger Logger { get => logger; private set => logger = value; }
        public AppEngine Engine { get => engine;  }

        AppEngine engine;
        ILogger logger;

        public void SetAppEngine(AppEngine manager)
        {
            this.engine = manager;
            Logger = this.Engine.GetLogger(this);
        }

        /// <summary>
        /// startup application event 
        /// </summary>
        public virtual void OnApplicationStart()
        {
            Logger.LogInformation($"Plugin {Name} is notified about app starts");
        }

        public abstract void Init();

        /// <summary>
        /// this allow plugin to register its own services
        /// </summary>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            //DO NOTHING
        }

        /// <summary>
        /// this allow the plugin to interact with appengine and application builder
        /// </summary>
        /// <param name="app"></param>
        /// <param name="appEngine"></param>
        public virtual void Configure(IApplicationBuilder app, AppEngine appEngine)
        {
            //DO NOTHING
        }

        /// <summary>
        /// this metod receive configuration to allow plugin configure itself
        /// </summary>
        /// <param name="configuration"></param>
        public virtual void Setup(IConfigurationRoot configuration)
        {
            //DO NOTHING
        }
    }
}