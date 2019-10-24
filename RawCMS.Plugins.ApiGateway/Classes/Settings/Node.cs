using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.ApiGateway.Classes.Settings
{
    public class Node
    {
        /// <summary>
        /// Terget Host value
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Target Port value
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Target scheme value
        /// </summary>
        public string Scheme { get; set; }
        /// <summary>
        /// Enable target node
        /// </summary>
        public bool Enable { get; set; }

        public Node()
        {
            Enable = true;
        }

    }
}
