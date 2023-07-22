using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]
    public class SudokuCell
    {
        public Color CellColor { get; set; }
        public string CellNumber { get; set; }
        public bool IsInteractable { get; set; }
    }
}
