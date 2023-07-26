
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
        private SudokuCell _chosenCell;
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
            if(ChosenCell != null && ChosenCell.IsInteractable == true)
            { 
                ChosenCell.CellColor = Colors.AliceBlue;
            }

            ChosenCell = cell;
            cell.CellColor = Colors.Gray;
        }

        [RelayCommand]
        private void CheckCell(char chosenNumber)
        {
            if(ChosenCell != null)
            {
                if (ChosenCell.CellNumber == chosenNumber.ToString())
                {
                    ChosenCell.CellColor = Colors.LimeGreen;
                    ChosenCell.IsInteractable = false;
                }
                else
                {
                    ChosenCell.CellColor = Colors.Red;
                }
            }
        }

        private void SetBoard()
        {
            SudokuPattern = new ObservableCollection<SudokuCell>();

            for(int i = 1; i <= 9; i++)
            {
                for(int j = 1; j <= 9; j++)
                {
                    SudokuPattern.Add(new SudokuCell
                    {
                        CellColor = Colors.AliceBlue,
                        IsInteractable = true,
                        CellNumber = $"{j}"
                    });
                }
            }
        }
    }
}
