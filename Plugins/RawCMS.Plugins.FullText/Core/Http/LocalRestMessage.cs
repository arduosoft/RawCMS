using System.Collections.Generic;
using RawCMS.Library.JavascriptClient;

namespace RawCMS.Plugins.FullText.Core.Http
{
    public class LocalRestMessage<T>
    {
        public List<LocalError> Errors { get; set; } = new List<LocalError>();
        public List<LocalError> Warnings { get; set; } = new List<LocalError>();
        public List<LocalError> Infos { get; set; } = new List<LocalError>();

        public RestStatus Status { get; set; }

        public T Data { get; set; }

        public LocalRestMessage(T item)
        {
            Data = item;
        }
    }
}