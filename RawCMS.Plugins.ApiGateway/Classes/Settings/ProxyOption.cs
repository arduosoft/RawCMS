using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.ApiGateway.Classes.Settings
{
    public class ProxyOption
    {
        /// <summary>
        /// Request Host value
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Request Port value
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Request scheme value
        /// </summary>
        public string Scheme { get; set; }
        /// <summary>
        /// Request Path filer. Use regular expression for filter path
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Target Host descriptions
        /// </summary>
        public Node Node { get; set; }
        /// <summary>
        /// Enable Balancing Middleware
        /// </summary>
        public bool Enable { get; set; }
    }
}
