
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
        private string _board;

        public GamePageViewModel()
        {
            _rnd = new Random();
            _timer = new Timer(1000);
            _clockNum = 0;

            ErrorCounter = "Mistakes: 0/3";
            NumberChoice = "123456789";
            Clock = "Time: 0:00";

            _board = SudokuSolver.InitializeSudoku();
            SetBoard();

            _timer.Elapsed += (sender, e) => HandleTimer();
        }

        [RelayCommand]
        private async Task GoToMain()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        [RelayCommand]
        private void ModifyCell(SudokuCell cell)
        {
            // If cell isn't null and is interactable without a red color value -> change cell color to AliceBlue (Default Color for cells)
            if(ChosenCell != null && ChosenCell.IsInteractable == true && ChosenCell.CellColor != Colors.Red)
            { 
                ChosenCell.CellColor = Colors.AliceBlue;
            }

            ChosenCell = cell;

            if (cell.CellColor != Colors.Red) // Leave selected cell red if that is it's color value, otherwise change selected cell to gray
            {
                cell.CellColor = Colors.Gray;
            }
        }

        [RelayCommand]
        private async Task CheckCell(char chosenNumber)
        {
            if(ChosenCell != null)
            {
                // Get index of cell to match up with _board number
                int cellIndex = SudokuPattern.IndexOf(ChosenCell);

                // Get correct number from _board using cellIndex and see if it matches chosenNumber
                // If true change cell to green and make it non-interactable
                // else change cell to red and increment mistakes counter
                if (_board[cellIndex].ToString() == chosenNumber.ToString())
                {
                    ChosenCell.CellColor = Colors.LimeGreen;
                    ChosenCell.IsInteractable = false;
                }
                else
                {
                    _errorNum++;

                    ChosenCell.CellColor = Colors.Red;
                    ErrorCounter = $"Mistakes: {_errorNum}/3";
                }

                // Give blank cells the same value as the chosenNumber regardless whether its right or wrong
                ChosenCell.CellNumber = chosenNumber.ToString();

                if (_errorNum == 3) // After 3 errors -> end game
                {
                    _timer.Stop(); // Stop clock

                    var playAgain = await Shell.Current.DisplayAlert("Game Over", "Do you want to play again?", "Yes", "No");

                    if (playAgain == true)
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

        private void SetBoard()
        {
            int[] blanksIndexes = GenerateBlanksIndexes(); // Get an array of random indexes ranging from 0 - 48
            _errorNum = 0;

            for (int i = 0; i < _board.Length; i++)
            {
                if (blanksIndexes.Contains(i)) // If the value of i is found within blanksIndexes -> give cell a blank CellNumber
                {
                    SudokuPattern.Add(new SudokuCell
                    {
                        CellColor = Colors.AliceBlue,
                        IsInteractable = true,
                        CellNumber = $" "
                    });

                }
                else // else fill in CellNumber normally
                {
                    SudokuPattern.Add(new SudokuCell
                    {
                        CellColor = Colors.AliceBlue,
                        IsInteractable = false,
                        CellNumber = $"{_board[i]}"
                    });
                }
                
                
            }
        }

        private int[] GenerateBlanksIndexes()
        {
            int randNum;
            var blanks = new int[48]; // Defining an int array with a size of 48

            for(int i = 0; i < blanks.Length; i++)
            {
                randNum = _rnd.Next(_board.Length);

                if (blanks.Contains(randNum) == false) // If blanks array doesn't already contain this index value -> add it to array
                    blanks[i] = randNum;
                else // else decrement i so that we can run the process again
                    i--;
            }

            return blanks;
        }

        // Used from OnNavigatedTo method in Gamepage.xaml.cs
        public void StartTimer()
        {
            _timer.Start();
        }

        // Updates the clock
        void HandleTimer()
        {
            _clockNum++;
            
            Clock = $"Time: " + TimeSpan.FromSeconds(_clockNum).ToString("mm':'ss");
        }

    }
}
