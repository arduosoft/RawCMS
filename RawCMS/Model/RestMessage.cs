using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RawCMS.Model
{

    public enum RestStatus
    {
        OK,
        KO,
        CompletedWithErrors
    }
        public class RestMessage
    {

        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }

    public class RestMessage<T>
    {
        public List<RestMessage> Errors { get; set; } = new List<RestMessage>();
        public List<RestMessage> Warnings { get; set; } = new List<RestMessage>();
        public List<RestMessage> Infos { get; set; } = new List<RestMessage>();

        public RestStatus Status { get; set; }

        public T Data { get; set; }

        public RestMessage(T item)
        {
            this.Data = item;
        }

    }
}
