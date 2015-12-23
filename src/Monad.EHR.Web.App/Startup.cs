
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
using Monad.EHR.Web.App.Security;
using System.IdentityModel.Tokens;

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

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }
        private TokenAuthOptions tokenOptions = null;
        private RsaSecurityKey key;


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
            var appSettings = Configuration.Get<AppSettings>();

            key = RSAKeyUtils.GetKey();
            tokenOptions = new TokenAuthOptions("ExampleAudience", "ExampleIssuer", key);
            services.AddInstance<TokenAuthOptions>(tokenOptions);

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(new string[] { "Bearer" })
                    .RequireAuthenticatedUser()
                    .AddRequirements(new TokenAuthRequirement())
                    .Build());
            });
           
            DependencyInstaller.InjectDependencies(services, this.Configuration);
            _logger.LogInformation("Configuring Services");
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configure the HTTP request pipeline.
            app.Use(new TokenReaderMiddleware().Process);
            app.Use(new MessageLoggingMiddleware().Process);

            app.UseJwtBearerAuthentication(options =>
            {
                options.TokenValidationParameters.IssuerSigningKey = key;
                options.TokenValidationParameters.ValidAudience = tokenOptions.Audience;
                options.TokenValidationParameters.ValidIssuer = tokenOptions.Issuer;

                // When receiving a token, check that we've signed it.
                options.TokenValidationParameters.ValidateSignature = true;

                // When receiving a token, check that it is still valid.
                options.TokenValidationParameters.ValidateLifetime = true;

                // This defines the maximum allowable clock skew - i.e. provides a tolerance on the token expiry time 
                // when validating the lifetime. As we're creating the tokens locally and validating them on the same 
                // machines which should have synchronised time, this can be set to zero. Where external tokens are
                // used, some leeway here could be useful.
                options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(0);
            });
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
