/// <summary>
///     Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
/// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

/// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
/// another source.  Any references used in the completion of the assignment are cited in my README file.
/// Purpose: The purpose of this document is to start the application
/// </summary>
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CS4540_tetris.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CS4540_tetris.Hubs;

namespace CS4540_tetris
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IISOptions>(op =>
            {
                op.ForwardClientCertificate = false;
            });

            services.AddMvc();

            services.AddDbContext<GameDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("GameDataContext")));

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<GameHub>("/chatHub");
                endpoints.MapHub<ChatRoomHub>("/Multi_Player");
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
