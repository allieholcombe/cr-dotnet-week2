﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using SonOfCod.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SonOfCod
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFramework()
                .AddDbContext<SOCContext>(options =>
                    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddIdentity<Admin, IdentityRole>()
                .AddEntityFrameworkStores<SOCContext>()
                .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<SOCContext>();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<SOCContext>().EnsureSeedData();
                }

            app.UseStaticFiles();

            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async (appContext) =>
            {
                await appContext.Response.WriteAsync("Hello World!");
            });
        }
    }
}