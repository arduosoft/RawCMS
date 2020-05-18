using System;
using System.Linq;
using System.Collections.Generic;
using RawCMS.Plugins.LogCollecting.Model;
using RawCMS.Plugins.LogCollecting.Models;

namespace RawCMS.Plugins.LogCollecting.Controllers

{
    public class LogQueue
    {
        private Queue<LogEntity> queque = new Queue<LogEntity>();
        public List<QueueLoad> QueueLoad { get; set; }
        public long MaxProcessedItems { get; set; }
        public long MaxQueueSize { get; set; }
        public long RescheduleThereshold { get; set; }
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
            AppendLoadValue();
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
            while (i < count && queque.Count > 0)
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

        public void AppendLoadValue()
        {
            if (this.QueueLoad.Count > 100)
            {
                this.QueueLoad.RemoveAt(0);
            }

            this.QueueLoad.Add(new QueueLoad()
            {
                MaxSize = this.MaxQueueSize,
                QueueSize = this.MaxQueueSize,
                Time = DateTime.Now
            });
        }

        public List<LogStatistic> GetQueueStatistic(string applicationId = null)
        {
            var array = this.queque.ToArray();
            if (!string.IsNullOrEmpty(applicationId))
            {
                array = array.Where(x => x.ApplicationId.Equals(applicationId)).ToArray();
            }
            var time = DateTime.Now;
            return array.GroupBy(x => x.ApplicationId).Select(x => new LogStatistic { ApplicationId = x.Key, Count = x.Key.Count(), Time = time }).ToList();
        }
    }
}