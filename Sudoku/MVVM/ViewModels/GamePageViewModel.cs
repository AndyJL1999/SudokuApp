
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sudoku.MVVM.Models;
using Sudoku.MVVM.Views;
using System.Collections.ObjectModel;

namespace Sudoku.MVVM.ViewModels
{
    public partial class GamePageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<SudokuCell> _sudokuPattern;
        [ObservableProperty]
        private string _numberChoice;

        public GamePageViewModel()
        {
            SetBoard();
            NumberChoice = "123456789";
        }

        [RelayCommand]
        private async Task GoToMain()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        [RelayCommand]
        private void ModifyCell(SudokuCell cell)
        {
            cell.CellColor = Colors.LimeGreen;
            cell.IsInteractable = false;
        }

        private void SetBoard()
        {
            SudokuPattern = new ObservableCollection<SudokuCell>();

            for(int i = 1; i <= 81; i++)
            {
                SudokuPattern.Add(new SudokuCell
                {
                    CellColor = Colors.AliceBlue,
                    IsInteractable = true,
                    CellNumber = $"{i}"
                });
            }
        }
    }
}
