using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Attempt.Sentry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(c =>
                {
                    c.SetBasePath(Directory.GetCurrentDirectory());
                    c.AddJsonFile("appsettings.json", optional: false);
                    c.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSentry(o =>
                    {
                        o.MinimumBreadcrumbLevel = LogLevel.Information;
                        o.MinimumEventLevel = LogLevel.Warning;
                    });
                });
    }
}
