using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Core.Extension
{
    public abstract class Plugin : IRequireApp, IInitable
    {
        public virtual int Priority => 1;
        public abstract string Name { get; }
        public abstract string Description { get; }
        public ILogger Logger { get => logger; private set => logger = value; }

        AppEngine engine;
        ILogger logger;

        public void SetAppEngine(AppEngine manager)
        {
            this.engine = manager;
            Logger = this.engine.GetLogger(this);
        }

        public virtual void OnApplicationStart()
        {
            Logger.LogInformation($"Plugin {Name} is notified about app starts");
        }

        public abstract void Init();

        public virtual void ConfigureServices(IServiceCollection services)
        {
            //DO NOTHING
        }

        public virtual void Configure(IApplicationBuilder app, AppEngine appEngine)
        {
            //DO NOTHING
        }
    }
}