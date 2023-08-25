using CommunityToolkit.Maui.Views;
using Sudoku.MVVM.ViewModels;

namespace Sudoku.MVVM.Views.PopUps;

public partial class ResultPopUp : Popup
{
	public ResultPopUp(GamePageViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}