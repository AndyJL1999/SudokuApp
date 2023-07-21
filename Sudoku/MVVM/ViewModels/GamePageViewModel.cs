
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sudoku.MVVM.Views;
using System.Collections.ObjectModel;

namespace Sudoku.MVVM.ViewModels
{
    public partial class GamePageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _sudokuPattern;

        public GamePageViewModel()
        {
            SudokuPattern = "123456789123456789123456789123456789123456789123456789123456789123456789123456789";
        }

        [RelayCommand]
        private async Task GoToMain()
        {
            await Shell.Current.GoToAsync("..", true);
        }

    }
}
