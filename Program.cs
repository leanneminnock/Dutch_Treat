using Dutch_Treat.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dutch_Treat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            if(args.Length == 1 && args[0].ToLower() == "/seed")
            {
                RunSeeding(host);
            }
            else
            {
                host.Run();
            }
        }

        private static void RunSeeding(IWebHost host)
        {
            // GetService creates an instance and tries to fulfil all of its dependancies
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using(var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.SeedAsync().Wait();
            }
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(AddConfuguration)
                .UseStartup<Startup>();

        private static void AddConfuguration(WebHostBuilderContext ctx, IConfigurationBuilder bldr)
        {
            bldr.Sources.Clear();
            bldr.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("config.json")
                                                             .AddEnvironmentVariables();
        }
    }
}
