using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core.Extension;
using RawCMS.Plugins.Core.Configuration;

namespace RawCMS.Plugins.Core.Middlewares
{
    public class UIMiddleware : Middleware<UIPluginConfig>
    {
        public override string Name => nameof(UIMiddleware);

        public override string Description => nameof(UIMiddleware);


        public UIMiddleware(RequestDelegate requestDelegate, ILogger logger, UIPluginConfig config) :
            base(requestDelegate, logger, config)
        {   
            
        }

        public override async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value.StartsWith("/app"))
            {
                await context.Response.WriteAsync("Hello from 2nd delegate.");
            }
            await next(context);
        }
    }
}
