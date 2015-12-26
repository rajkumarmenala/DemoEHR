using Monad.EHR.Common.StateManagement;
using Monad.EHR.Domain.Entities.Identity;
using Monad.EHR.Domain.Interfaces;
using Monad.EHR.Domain.Interfaces.Identity;
using Monad.EHR.Infrastructure.Data;
using Monad.EHR.Infrastructure.Data.Identity;
using Monad.EHR.Infrastructure.StateManagement.Cache.Providers;
using Monad.EHR.Services.Business;
using Monad.EHR.Services.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Monad.EHR.Infrastructure.DependencyResolver
{
    public static class DependencyInstaller
    {
        public static void InjectDependencies(IServiceCollection services, IConfiguration configuration)
        {
            InjectDependenciesForDAL(services, configuration);
            InjectDependenciesForBL(services);
            services.AddSingleton<ICacheProvider, InMemoryCache>();
        }

        private static void InjectDependenciesForDAL(IServiceCollection services, IConfiguration configuration)
        {

            services
              .AddEntityFramework()
              .AddSqlServer()
              .AddDbContext<CustomDBContext>(options => options.UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<CustomDBContext>()
                    .AddDefaultTokenProviders();

            services.AddTransient<IRoleStore<Role>, CustomRoleStore>();
            services.AddTransient<IUserStore<User>, CustomUserStore>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<IActivityRepository, ActivityRepository>();
            services.AddTransient<IActivityRoleRepository, ActivityRoleRepository>();
            services.AddTransient<IIdentityRepository, CustomUserStore>();
			services.AddTransient<IActivityRepository, ActivityRepository>();
            services.AddTransient<IResourceRepository, ResourceRepository>();
			services.AddTransient<IResourceTypeRepository, ResourceTypeRepository>();
            services.AddTransient<IRoleRightRepository, RoleRightRepository>();

			            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IMedicationsRepository, MedicationsRepository>();
            services.AddTransient<IProblemsRepository, ProblemsRepository>();
            services.AddTransient<IBPRepository, BPRepository>();
            services.AddTransient<IPatientHeightRepository, PatientHeightRepository>();
            services.AddTransient<IWeightRepository, WeightRepository>();

        }

        private static void InjectDependenciesForBL(IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IActivityService, ActivityService>();
            //services.AddTransient<IRoleService, RoleService>();
			services.AddTransient<IRoleService, RoleService>();
			            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IMedicationsService, MedicationsService>();
            services.AddTransient<IProblemsService, ProblemsService>();
            services.AddTransient<IBPService, BPService>();
            services.AddTransient<IPatientHeightService, PatientHeightService>();
            services.AddTransient<IWeightService, WeightService>();

        }
    }
}
