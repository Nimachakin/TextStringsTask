using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MostGamesData.DatabaseModels;

namespace MostGamesWeb
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            // Gets database context object via IServiceProvider interface 
            // and writes sample data in database if it is empty.
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    TextContext context = services.GetRequiredService<TextContext>();
                    await context.InitializeAsync();
                }
                catch(Exception)
                {

                }
            }
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
