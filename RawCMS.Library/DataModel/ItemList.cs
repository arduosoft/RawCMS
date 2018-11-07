using Newtonsoft.Json.Linq;

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
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}