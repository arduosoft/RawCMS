using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RawCMS.Library.Core.Extension
{
    public abstract class Middleware<T>: IConfigurableMiddleware<T>
    {
        public RequestDelegate next { get; private set; }
        public ILogger logger { get; private set; }
        public T pluginConfig { get; private set; }
        public Middleware(RequestDelegate requestDelegate, ILogger logger, T config)
        {
            this.next = requestDelegate;
            this.logger = logger;
            this.pluginConfig = config;
        }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract Task InvokeAsync(HttpContext context);
    }
}
