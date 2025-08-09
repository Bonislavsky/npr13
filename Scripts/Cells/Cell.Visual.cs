using Godot;

namespace NPR13.Scripts.Cells
{
    public partial class Cell
    {
        public void UpdateVisual()
        {
            _label.Text = "";
            _label.Modulate = Colors.White;

            if (IsFlagged)
            {
                _label.Text = "🚩";
            }
            else if (IsRevealed)
            {
                if (AdjacentMines > 0)
                {
                    _label.Text = AdjacentMines.ToString();
                    _label.Modulate = GetNumberColor(AdjacentMines);
                }
                Modulate = Colors.WebGray;
            }
        }

        public void ResetVisual()
        {
            _label.Text = "";
            _label.Modulate = Colors.White;
            Modulate = Colors.White;
        }

        public void GameOverVisual()
        {
            if(IsMine && IsFlagged)
            {
                Modulate = Colors.Green;
            }
            else if(!IsMine && IsFlagged)
            {
                Modulate = Colors.Red;
            }
            else if (IsMine)
            {
                _label.Text = "💣";
            }
        }

        private Color GetNumberColor(int number)
        {
            return number switch
            {
                1 => Colors.Blue,
                2 => Colors.Green,
                3 => Colors.Red,
                4 => Colors.Purple,
                5 => Colors.Brown,
                6 => Colors.Pink,
                7 => Colors.Black,
                8 => Colors.Gray,
                _ => Colors.Black
            };
        }
    }
}
