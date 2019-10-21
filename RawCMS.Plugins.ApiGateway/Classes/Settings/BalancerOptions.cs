using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.ApiGateway.Classes.Settings
{
    public class BalancerOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Scheme { get; set; }
        public string Path { get; set; }
        public Node[] Nodes { get; set; }
        public string Policy { get; set; }
        public bool Enable { get; set; }
    }
}
