using Newtonsoft.Json;
using RawCMS.Plugins.LogCollecting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;

namespace RawCMS.Test
{
    public class LogTest
    {
        [Fact]
        public void Test()
        {
            var rand = new Random();
            var start = DateTime.Now;
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44318/");
            client.DefaultRequestHeaders.Add("content-type", "application/json");
            client.DefaultRequestHeaders.Add("accept", "application/json");
            
            
            do
            {
                var logs = Enumerable.Range(1, rand.Next(50, 100)).Select(index => new LogEntity
                {
                    ApplicationId = "8626f875-6278-4249-a4f9-5482ddde50b1",
                    Date = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Message = $"Message generated from LogTest",
                    Severity = (LogEntitySeverity)rand.Next(0,6)
                })
                 .ToList();
                var content = new StringContent(JsonConvert.SerializeObject(logs), Encoding.UTF8, "application/json");
                var resp = client.PostAsync("api/logingress/8626f875-6278-4249-a4f9-5482ddde50b1", content).Result;

                System.Threading.Thread.Sleep(1000);

            } while (DateTime.Now.Subtract(start).TotalSeconds < 30);
        }
    }
}
