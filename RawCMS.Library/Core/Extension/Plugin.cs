using Microsoft.Extensions.Logging;
using RawCMS.Library.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Core.Extension
{
    public abstract class Plugin: IRequireApp, IInitable
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        AppEngine engine;
        ILogger logger;
        public void setLambdaManager(AppEngine manager)
        {
            this.engine = manager;
            logger = this.engine.GetLogger(this);
        }

        public virtual void OnApplicationStart()
        {
            logger.LogInformation($"Plugin {Name} is notified about app starts");
        }

        public abstract void Init();
    }
}
