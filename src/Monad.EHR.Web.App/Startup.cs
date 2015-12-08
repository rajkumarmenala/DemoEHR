
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Logging;
using System;
using Microsoft.AspNet.Http;
using Monad.EHR.Web.App.Troubleshooters;
using Monad.EHR.Web.App.Filters;
﻿using Monad.EHR.Common.Logger;
using Monad.EHR.Common.Utility;
using Monad.EHR.Infrastructure.DependencyResolver;

namespace Monad.EHR.Web.App
{
   public class Startup
    {
       private readonly ILogger _logger;
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            // Setup configuration sources.
            _logger = LogHelper.CreateLogger<Startup>();
            var builder = new ConfigurationBuilder()
                 .SetBasePath(appEnv.ApplicationBasePath)
              .AddJsonFile("config.json")
              .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // To do 
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCaching();

            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromSeconds(10);
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new GlobalExceptionFilter());
            });
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            DependencyInstaller.InjectDependencies(services, this.Configuration);
            _logger.LogInformation("Configuring Services");
        }
        
         // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configure the HTTP request pipeline.
            app.Use(new MessageLoggingMiddleware().Process);
            app.UseSession();
            app.UseStaticFiles()
         .UseIdentity()
          .UseMvc(routes =>
         {
             routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
         });
        }
    }

}
