using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMSClient.BLL.Model
{
    public class ListRequest:BaseRequest
    {
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
        public string Id { get; set; }



    }
}
