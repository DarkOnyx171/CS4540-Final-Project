using System;
using CS4540_tetris.Areas.Identity.Data;
using CS4540_tetris.Models;
using Learning_Outcome_Tracker.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CS4540_tetris.Areas.Identity.IdentityHostingStartup))]
namespace CS4540_tetris.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<UserContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("UserContextConnection")));
                
                //Jaecee added email verification for players
                services.AddDefaultIdentity<GameUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<UserContext>()
                    .AddEntityFrameworkStores<UserContext>()
                    .AddDefaultTokenProviders().AddDefaultUI();
                services.AddTransient<IEmailSender, EmailSender>();
            });
        }
    }
}