﻿using AutoMapper;

using System.Reflection;

namespace WineManagement.AppStarts
{
	public static class AutoMapperConfig
	{
		public static void ConfigureAutoMapper(this IServiceCollection services)
		{
			MapperConfiguration mapperConfiguration = new MapperConfiguration(mc =>
			{
				//mc.ConfigStoreModule();
				mc.AddMaps(Assembly.GetExecutingAssembly());
				//mc.ConfigMessage();
			});
			IMapper mapper = mapperConfiguration.CreateMapper();
			services.AddSingleton(mapper);
		}
	}
}
