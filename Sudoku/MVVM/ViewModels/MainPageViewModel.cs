using CommunityToolkit.Mvvm.Input;
using Sudoku.MVVM.Views;

namespace Sudoku.MVVM.ViewModels;

public partial class MainPageViewModel
{
    public MainPageViewModel()
    {

    }

    [RelayCommand]
    private async Task GoToGame()
    {
        await Shell.Current.GoToAsync(nameof(GamePage));
    }
}
