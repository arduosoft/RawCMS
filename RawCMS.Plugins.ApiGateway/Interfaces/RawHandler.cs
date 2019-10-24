using Microsoft.AspNetCore.Http;
using RawCMS.Plugins.ApiGateway.Classes.Balancer.Handles;
using RawCMS.Plugins.ApiGateway.Classes.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RawCMS.Plugins.ApiGateway.Interfaces
{
    public abstract class RawHandler
    {
        internal const int DefaultBufferSize = 4096;

        public abstract HandlerProtocolType HandlerRequestType { get; }

        public abstract Task HandleRequest(HttpContext context, Node node, int? bufferSize = null, bool Chuncked = false, TimeSpan? keepAlive = null);

        /// <summary>
        /// compute uri of remote request basing on context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="scheme"></param>
        /// <returns></returns>
        internal string GetUri(HttpContext context, string host, int? port, string scheme)
        {
            var urlPort = "";
            if (port.HasValue
                && !(port.Value == 443 && "https".Equals(scheme, StringComparison.InvariantCultureIgnoreCase))
                && !(port.Value == 80 && "http".Equals(scheme, StringComparison.InvariantCultureIgnoreCase))
                )
            {
                urlPort = ":" + port.Value;
            }
            return $"{scheme}://{host}{urlPort}{context.Request.PathBase}{context.Request.Path}{context.Request.QueryString}";
        }
    }
}
