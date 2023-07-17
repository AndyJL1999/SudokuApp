using Sudoku.MVVM.Views;

namespace Sudoku;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(GamePage), typeof(GamePage));
	}
}
