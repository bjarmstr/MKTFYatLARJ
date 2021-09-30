using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MKTFY.Repositories;
using System;

namespace MKTFY.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Create a Service Scope so we can get a services collection
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;  // services collection
                try
                {
                    // Get an ApplicationDbContext instance so we can perform the migrations on it
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    // Perform the migrations
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // Output an error log to the configured logging service
                    // By default the logging service just outputs to the console
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
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
