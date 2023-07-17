using Sudoku.MVVM.ViewModels;
using Sudoku.MVVM.Views;

namespace Sudoku;

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

		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<MainPageViewModel>();

        builder.Services.AddTransient<GamePage>();
        builder.Services.AddTransient<GamePageViewModel>();

        return builder.Build();
	}
}
