using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace QuoteOfTheDay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting up");
                int hour = int.Parse(Environment.GetEnvironmentVariable("Hour"));
                IHost host = CreateHostBuilder(args).Build();
                host.Services.UseScheduler(scheduler => {
                    scheduler
                        .Schedule<QotdTask>()
                        .DailyAtHour(hour);
                });
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally 
            {
                Log.CloseAndFlush();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices(services => {
                    services.AddScheduler();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var port = System.Environment.GetEnvironmentVariable("PORT");
                    webBuilder.UseUrls($"http://*:{port}");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
