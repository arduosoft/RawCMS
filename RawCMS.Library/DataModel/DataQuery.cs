using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.DataModel
{
    public class DataQuery
    {
        public string RawQuery { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
