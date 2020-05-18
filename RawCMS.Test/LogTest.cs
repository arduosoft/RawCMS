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


            do
            {
                var logs = Enumerable.Range(1, rand.Next(50, 100)).Select(index => new LogEntity
                {
                    Date = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Message = $"Message generated from LogTest",
                    Severity = (LogEntitySeverity)rand.Next(0, 6)
                })
                 .ToList();
                var content = new StringContent(JsonConvert.SerializeObject(logs), Encoding.UTF8, "application/json");
                var resp = client.PostAsync("api/logingress/8626f875-6278-4249-a4f9-5482ddde50b1", content).Result;

                var resp2 = client.PostAsync("api/logingress/4187e72b-4565-4100-8319-92471b338af1", content).Result;

                System.Threading.Thread.Sleep(1000);

            } while (DateTime.Now.Subtract(start).TotalSeconds < 30);
        }
    }
}
