using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMSClient.BLL.Model
{
    public class BaseRequest
    {
        public string Token { get; set; }
        public string Collection { get; set; }
        public string RawQuery { get; set; } = "{}";

    }

}
