using Sudoku.MVVM.ViewModels;

namespace Sudoku.MVVM.Views;

public partial class GamePage : ContentPage
{

	public GamePage(GamePageViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		((GamePageViewModel)BindingContext).StartTimer();
	}
}