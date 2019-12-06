/// <summary>
///     Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
/// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

/// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
/// another source.  Any references used in the completion of the assignment are cited in my README file.
/// Purpose: The purpose of this document is where the whole program begins helping to strat the application
/// </summary>
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
        public static Dictionary<string, int> rooms = new Dictionary<string, int>();

        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await CreateDbIfNotExistsAsync(host);

            host.Run();
        }

        private static async Task CreateDbIfNotExistsAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                //try to make and initialize databases
                try
                {
                    var scorecontext = services.GetRequiredService<GameDataContext>();
                    var usercontext = services.GetRequiredService<UserContext>();
                    var userManager = services.GetRequiredService<UserManager<GameUser>>();
                   await DbInitializer.InitializeAsync(scorecontext, usercontext, userManager);
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
