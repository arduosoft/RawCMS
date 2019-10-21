using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.ApiGateway.Classes.Settings
{
    public class ProxyOption
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Scheme { get; set; }
        public string Path { get; set; }
        public Node Node { get; set; }
        public bool Enable { get; set; }
    }
}
