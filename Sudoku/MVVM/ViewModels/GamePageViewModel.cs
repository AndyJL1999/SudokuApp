
using CommunityToolkit.Mvvm.Input;
using Sudoku.MVVM.Views;

namespace Sudoku.MVVM.ViewModels
{
    public partial class GamePageViewModel
    {

        public GamePageViewModel()
        {

        }

        [RelayCommand]
        private async Task GoToMain()
        {
            await Shell.Current.GoToAsync("..", true);
        }
    }
}
