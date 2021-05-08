using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSOLiberty.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CSOLiberty
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            InitDbIfEmpty(host);

            host.Run();
        }

        static void InitDbIfEmpty(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var provider = scope.ServiceProvider;

            try 
            { 
                DbInit.Init(provider.GetRequiredService<AppDbContext>()); 
            }
            catch(Exception e)
            {
                provider.GetRequiredService<ILogger<Program>>().LogError(e, "Failed to create db!");
                
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
