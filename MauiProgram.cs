﻿using FastCost.DAL;
using FastCost.Services;
using FastCost.Views;
using Microsoft.Extensions.Logging;

namespace FastCost;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<CostRepository>();
        builder.Services.AddSingleton<CategoryRepository>();
        builder.Services.AddSingleton<AllCostsService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
