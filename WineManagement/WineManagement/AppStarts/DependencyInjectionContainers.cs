using BusinessLayer.Service;
using BusinessLayer.Service.Interface;
using DataLayer.Repository;
using DataLayer.Repository.Interface;
using System.Diagnostics;

namespace WineManagement.AppStarts
{
	public static class DependencyInjectionContainers
	{
        public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {
            // use DI here
            services.AddScoped<IWineRepository, WineRepository>();
            services.AddScoped<IWineService, WineService>();
            services.AddScoped<IWineBatchRepository, WineBatchRepository>();
            services.AddScoped<IWineBatchService, WineBatchService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();


            // auto mapper
            services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

            services.AddHttpContextAccessor();

            return services;
        }
    }

}
