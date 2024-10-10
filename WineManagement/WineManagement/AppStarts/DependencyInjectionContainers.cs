using BusinessLayer.Service;
using BusinessLayer.Service.Interface;
using DataLayer.GenericRepository;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Repository.Interface;
using DataLayer.UnitOfWork;
using Microsoft.CodeAnalysis.Host;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WineManagement.AppStarts
{
	public static class DependencyInjectionContainers
	{

		public static void InstallService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddRouting(options =>
			{
				options.LowercaseUrls = true; ;
				options.LowercaseQueryStrings = true;
			});
			services.AddDbContext<WineManagementSystemContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DBDefault"));
			});

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			// use DI here
			services.AddScoped<IWineRepository, WineRepository>();
			services.AddScoped<IWineService, WineService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IWineBatchRepository, WineBatchRepository>();
			services.AddScoped<IWineBatchService, WineBatchService>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IAuthServices, AuthServices>();
			services.AddScoped<IAccountService, AccountServices>();
		}




		public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {
            // use DI here
            services.AddScoped<IWineRepository, WineRepository>();
            services.AddScoped<IWineService, WineService>();
            services.AddScoped<IWineBatchRepository, WineBatchRepository>();
            services.AddScoped<IWineBatchService, WineBatchService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IAuthServices, AuthServices>();
			services.AddScoped<IAccountService, AccountServices>();


			// auto mapper
			services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

            services.AddHttpContextAccessor();

            return services;
        }
    }

}
