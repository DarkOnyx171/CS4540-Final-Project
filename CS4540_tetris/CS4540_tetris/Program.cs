using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS4540_tetris.Areas.Identity.Data;
using CS4540_tetris.Data;
using CS4540_tetris.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CS4540_tetris
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var scorecontext = services.GetRequiredService<ScoreContext>();
                    var usercontext = services.GetRequiredService<UserContext>();
                    var userManager = services.GetRequiredService<UserManager<GameUser>>();
                    DbInitializer.Initialize(scorecontext, usercontext, userManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
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
