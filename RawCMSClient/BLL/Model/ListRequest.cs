using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMSClient.BLL.Model
{
    public class ListRequest:BaseRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Id { get; set; }



    }
}
