using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;
using System.IO;

namespace RawCMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = new WebHostBuilder()
                .UseNLog()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseSetting("detailedErrors", "true")
                .Build();

            host.Run();
        }
    }
}