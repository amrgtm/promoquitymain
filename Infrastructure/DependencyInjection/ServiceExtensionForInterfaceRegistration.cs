using Application.Interfaces;
using Application.Interfaces.Seeder;
using Infrastructure.Logging;
using Infrastructure.Repositories;
using Infrastructure.SeedData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using ApplicationService.Interface;
using ApplicationService.Implementation;
using Application.Interfaces.Default;
using Infrastructure.Common;
namespace Infrastructure.DependencyInjection
{
    public static class ServiceExtensionForInterfaceRegistration
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<,,,,>), typeof(GenericRepository<,,,,>));
            services.AddTransient<IPermissionDataSeeder, PermissionDataSeeder>();
            services.AddTransient<IRoleDataSeeder, RoleDataSeeder>();
            services.AddTransient<IUserDataSeeder, UserDataSeeder>();
            services.AddTransient<IUserRoleSeeder, UserRoleSeeder>();
            services.AddHostedService<SeedDataHostedService>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<ILoggerProvider, DatabaseLoggerProvider>();


            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IUserRolesRepository, UserRolesRepository>();
            services.AddScoped<IImageMasterRepository, ImageMasterRepository>();
            services.AddScoped<ICompanyProfileRepository, CompanyProfileRepository>();
            services.AddScoped<IHomeContentRepository, HomeContentRepository>();
            services.AddScoped<IHomeContentMidRepository, HomeContentMidRepository>();
            services.AddScoped<INewsAnnounceRepository, NewsAnnounceRepository>();
            services.AddScoped<IOperationalKPIRepository, OperationalKPIRepository>();
            services.AddScoped<IMarketKPIRepository, MarketKPIRepository>();
            services.AddScoped<IFinancialKPIRepository, FinancialKPIRepository>();
            services.AddScoped<IValuationKPIRepository, ValuationKPIRepository>();
            services.AddScoped<IDownloadRepository, DownloadRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IFaqRepository, FaqRepository>();
            services.AddScoped<IPromoConfigRepository, PromoConfigRepository>();


            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRolesService, UserRolesService>();
            services.AddScoped<IImageMasterService, ImageMasterService>();
            services.AddScoped<ICompanyProfileService, CompanyProfileService>();
            services.AddScoped<IHomeContentService, HomeContentService>();
            services.AddScoped<IHomeContentMidService, HomeContentMidService>();
            services.AddScoped<INewsAnnounceService, NewsAnnounceService>();
            services.AddScoped<IOperationalKPIService, OperationalKPIService>();
            services.AddScoped<IMarketKPIService, MarketKPIService>();
            services.AddScoped<IFinancialKPIService, FinancialKPIService>();
            services.AddScoped<IValuationKPIService, ValuationKPIService>();
            services.AddScoped<IDownloadService, DownloadService>();
            services.AddScoped<IBlogService, BlogService>();

            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<IPromoConfigService, PromoConfigService>();

            services.AddHttpClient();
            return services;
        }
    }
}
