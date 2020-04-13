using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Extension;
using RawCMS.Library.Service;
using RawCMS.Plugins.Core.Configuration;

namespace RawCMS.Plugins.Core.Middlewares
{
    public class UIMiddleware : Middleware<UIPluginConfig>
    {
        public override string Name => nameof(UIMiddleware);

        public override string Description => nameof(UIMiddleware);

        protected AppEngine appEngine;
        protected UIService uiService;

        public UIMiddleware(RequestDelegate requestDelegate, ILogger logger, UIPluginConfig config, AppEngine appEngine, UIService uiService) :
            base(requestDelegate, logger, config)
        {
            this.appEngine = appEngine;
            this.uiService = uiService;
        }

        public override async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value.StartsWith("/app") && !Path.HasExtension(context.Request.Path))
            {
                var indexPath = Path.Combine(Path.GetDirectoryName(appEngine.CorePlugin.PluginPath), "UICore", "index.html");

                var index = File.ReadAllText(indexPath);
                index = index
                    .Replace("<!--CSS-->", uiService.GetCSStHtml())
                    .Replace("<!--SCRIPTS-->", uiService.GetJavascriptHtml()).Replace(".jss", ".js");

                await context.Response.WriteAsync(index);
            }
            else
            {
                await next(context);
            }
        }
    }
}