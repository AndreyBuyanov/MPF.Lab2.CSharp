using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;

namespace App
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
            services.AddControllersWithViews();
            services.AddTransient<MySqlDatabase>(_ => new MySqlDatabase(Configuration["ConnectionStrings:mariadb"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "add_url",
                    pattern: "admin/url/add",
                    new { controller = "Admin", action = "AddUrl" });
                endpoints.MapControllerRoute(
                    name: "remove_url",
                    pattern: "admin/url/remove/{id:int}",
                    new { controller = "Admin", action = "RemoveUrlById" });
                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "admin/login",
                    new { controller = "Admin", action = "Login" });
                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "admin/logout",
                    new { controller = "Admin", action = "Logout" });
                endpoints.MapControllerRoute(
                    name: "admin_default",
                    pattern: "/admin/{action=Index}",
                    new { controller = "Admin" });
                endpoints.MapControllerRoute(
                    name: "go",
                    pattern: "go/{url}",
                    new { controller = "Home", action = "Go" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}