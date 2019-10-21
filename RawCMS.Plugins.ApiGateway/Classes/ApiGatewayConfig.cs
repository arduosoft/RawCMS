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
            Balancer.Add(new BalancerOptions
            {
                Enable = true,
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
        }

        public List<BalancerOptions> Balancer { get; set; } = new List<BalancerOptions>();
    }
}
