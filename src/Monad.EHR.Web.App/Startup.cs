
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;

using System;
using Monad.EHR.Web.App.Filters;
using Monad.EHR.Common.Logger;
using Monad.EHR.Common.Utility;
using Monad.EHR.Infrastructure.DependencyResolver;
using Monad.EHR.Web.App.Middlewares;
using Monad.EHR.Web.App.Policies;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Authentication.JwtBearer;

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
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // public object JwtBearerDefaults { get; private set; }

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
            services.AddInstance<TokenAuthOptions>(TokenAuthOptions.GetInstance());
            //services.AddAuthorization(auth =>
            //{
            //    auth.AddPolicy(TokenAuthOptions.Scheme,
            //       policy => policy.Requirements.Add(new TokenAuthRequirement()));
            //});

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme??)
                    .RequireAuthenticatedUser().Build());
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            DependencyInstaller.InjectDependencies(services, this.Configuration);
            _logger.LogInformation("Configuring Services");
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configure the HTTP request pipeline.
           // app.Use(new TokenReaderMiddleware().Process);
            app.UseTokenAuthAuthentication();
           // app.Use(new MessageLoggingMiddleware().Process);
            app.UseSession();
            app.UseStaticFiles();
            app.UseIdentity()
         .UseMvc(routes =>
         {
             routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
         });
            AutoMapperBootStrapper.Bootstrap();
        }
    }
}
