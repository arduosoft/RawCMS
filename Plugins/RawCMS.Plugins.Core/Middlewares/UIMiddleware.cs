using System;
using System.Collections.Generic;
using System.IO;
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


            if (context.Request.Path.Value.StartsWith("/app") && context.Request.Path.Value.EndsWith("/"))
            {
                var indexPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins", "RawCMS.Plugins.Core", "UICore", "Index.html");//TODO: make it dinamic?
                context.Response.WriteAsync(File.ReadAllText(indexPath));
            }
            else
            {
                await next(context);
            }
        }
    }
}
