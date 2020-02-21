using System;
using System.Collections.Generic;
using System.Text;
using RawCMS.Plugins.FullText.Core;
using RawCMS.Plugins.FullText.Models;
namespace RawCMS.Plugins.FullText.Controllers

{
    public class LogQueue
    {
        protected LogQueueService service;
        public LogQueue(LogQueueService service)
        {
            this.service = service;
        }
        Queue<LogEntity> queque = new Queue<LogEntity>();
        public List<QueueLoad> QueueLoad { get; set; }
        public int MaxProcessedItems { get; set; }
        public int MaxQueueSize { get; set; }
        public int RescheduleThereshold { get; set; }
        private static string lockObj = "";
        public long Count
        {
            get { return queque.Count; }
        }
        public LogQueue()
        {
            MaxProcessedItems = 10000;
            MaxQueueSize = 100000;
            QueueLoad = new List<QueueLoad>();
            AppendLoadValue(0, MaxQueueSize);
            RescheduleThereshold = 1000;
        }
        
        public void Enqueue(LogEntity le)
        {
            queque.Enqueue(le);
        }
        public List<LogEntity> Dequeue(int count)
        {
            List<LogEntity> result = new List<LogEntity>();
            LogEntity newElem;
            int i = 0;
            while (i < count)
            {
                newElem = queque.Dequeue();
                if (newElem != null)
                {
                    result.Add(newElem);
                }
                else
                {
                    break;
                }
                i++;
            }
            return result;
        }
        public void AppendLoadValue(long count, int maxQueueSize)
        {
            if (this.QueueLoad.Count > 100)
            {
                this.QueueLoad.RemoveAt(0);
            }

            this.QueueLoad.Add(new QueueLoad()
            {
                MaxSize = maxQueueSize,
                QueueSize = (int)count,
                Time = DateTime.Now
            });
        }
        
    }
}
