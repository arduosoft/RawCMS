using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Client.BLL.Model
{
    public class BaseRequest
    {
        public string Token { get; set; }
        public string Collection { get; set; }
        public string RawQuery { get; set; }

    }

}
