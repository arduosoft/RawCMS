using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.ApiGateway.Classes.Settings
{
    public class BalancerData
    {
        public Dictionary<int, long> Scores { get; set; } = new Dictionary<int, long>();
        public long LastServed { get; set; }
    }
}
