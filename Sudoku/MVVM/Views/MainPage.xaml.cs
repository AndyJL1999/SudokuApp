using Sudoku.MVVM.ViewModels;

namespace Sudoku.MVVM.Views;

public partial class MainPage : ContentPage
{

	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

}

