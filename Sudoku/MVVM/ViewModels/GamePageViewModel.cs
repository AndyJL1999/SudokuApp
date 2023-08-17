
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sudoku.MVVM.Models;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;

namespace Sudoku.MVVM.ViewModels
{
    public partial class GamePageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<SudokuCell> _sudokuPattern = new ObservableCollection<SudokuCell>();
        [ObservableProperty]
        private SudokuCell _chosenCell;
        [ObservableProperty]
        private string _numberChoice;
        [ObservableProperty]
        private string _errorCounter;
        [ObservableProperty]
        private string _clock;

        private Random _rnd;
        private int _errorNum;
        private int _clockNum;
        private Timer _timer;

        public GamePageViewModel()
        {
            _rnd = new Random();
            _timer = new Timer(1000);
            _clockNum = 0;

            ErrorCounter = "Mistakes: 0/3";
            NumberChoice = "123456789";
            Clock = "Time: 0:00";

            SetBoard();

            _timer.Elapsed += (sender, e) => HandleTimer();
            _timer.Start();
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
        private async Task CheckCell(char chosenNumber)
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
                    _errorNum++;

                    ChosenCell.CellColor = Colors.Red;
                    ErrorCounter = $"Mistakes: {_errorNum}/3";

                    if (_errorNum == 3) //After 3 errors -> end game
                    {
                        var playAgain = await Shell.Current.DisplayAlert("Game Over", "Do you want to play again?", "Yes", "No");

                        if(playAgain == true)
                        {
                            SetBoard();
                            ErrorCounter = $"Mistakes: {_errorNum}/3";
                        }
                        else
                        {
                            await GoToMain();
                        }
                    }
                }
            }
        }

        private void SetBoard()
        {
            _errorNum = 0;

            string viablePattern = "123456789";

            for (int i = 0; i < 9; i++)
            {
                viablePattern = new(viablePattern.OrderBy(x => _rnd.Next()).ToArray());

                for (int j = 0; j < 9; j++)
                {
                    SudokuPattern.Add(new SudokuCell
                    {
                        CellColor = Colors.AliceBlue,
                        IsInteractable = true,
                        CellNumber = $"{viablePattern[j]}"
                    });
                }
            }
        }


        void HandleTimer()
        {
            _clockNum++;
            
            Clock = $"Time: " + TimeSpan.FromSeconds(_clockNum).ToString("mm':'ss");
        }

    }
}
