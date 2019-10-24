using RawCMS.Plugins.ApiGateway.Classes.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.ApiGateway.Classes
{
    public class ApiGatewayConfig
    {
        public ApiGatewayConfig()
        {
            Balancer.Add(new BalancerOption
            {
                Enable = false,
                Host = "localhost:64516",
                Nodes = new Node[] {
                    new Node {
                        Host = "google.com",
                        Port = 443,
                        Scheme = "https",
                        Enable = true
                    },
                     new Node {
                        Host = "amazon.com",
                        Port = 443,
                        Scheme = "https",
                        Enable = true
                    }
                },
                Path = "^(.*)$",
                Policy = "RoundRobin",
                Port = 64516,
                Scheme = "http"
            });

            Proxy.Add(new ProxyOption
            {
                Enable = false,
                Host = "localhost:64516",
                Node = new Node
                {
                    Host = "google.com",
                    Port = 443,
                    Scheme = "https"
                },
                Path = "^(.*)$",
                Port = 64516,
                Scheme = "http"
            });
            Cache = new CacheOption
            {
                Enable = true,
                Duration = 600,
                MaximumBodySize = 100 * 1024 * 1024,
                SizeLimit = 64 * 1024 * 1024,
                UseCaseSensitivePaths = false
            };
        }

        public List<BalancerOption> Balancer { get; set; } = new List<BalancerOption>();
        public List<ProxyOption> Proxy { get; set; } = new List<ProxyOption>();
        public CacheOption Cache { get; set; }
    }
}
