using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.ApiGateway.Classes.Settings
{
    public class BalancerOption
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
        /// Taget Hosts description
        /// </summary>
        public Node[] Nodes { get; set; }
        /// <summary>
        /// Policy name for balancing. You can use RoundRobin or RequestCount
        /// </summary>
        public string Policy { get; set; }
        /// <summary>
        /// Enable Balancing Middleware
        /// </summary>
        public bool Enable { get; set; }
    }
}
