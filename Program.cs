using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel;
using Coravel.Scheduling.Schedule;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using QuoteOfTheDay.Context;
using QuoteOfTheDay.Context.Repository;

namespace QuoteOfTheDay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Ensure that the sqllite has been created
            using(var client = new QotdDbContext())
            {
                client.Database.EnsureCreated();
            }

            IHost host = CreateHostBuilder(args).Build();
            host.Services.UseScheduler(scheduler => {

                scheduler
                    .Schedule<QotdTask>()
                    .EveryTenSeconds();

            });


            host.Run();
            webHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScheduler();
                    services.Configure<BotConfiguration>(hostContext.Configuration.GetSection("QotdBot"));
                    services.AddTransient<QotdTask>();
                    

                    services.AddEntityFrameworkSqlite().AddDbContext<QotdDbContext>();
                    services.AddScoped<IRepository<Chat>, ChatRepository>();
                });



    }
}
