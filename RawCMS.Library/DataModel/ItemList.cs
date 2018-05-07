using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.DataModel
{
    public class ItemList
    {
        public JArray Items { get; set; } = new JArray();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public ItemList(JArray items, int totalCount, int pageNumber, int pageSize)
        {
            this.Items = items;
            this.TotalCount = totalCount;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
    }
}
