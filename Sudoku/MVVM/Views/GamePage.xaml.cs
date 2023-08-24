using Sudoku.MVVM.ViewModels;

namespace Sudoku.MVVM.Views;

public partial class GamePage : ContentPage
{

	public GamePage(GamePageViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

}