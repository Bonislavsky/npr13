using Godot;

namespace NPR13.Scripts.Cells
{
    public partial class Cell
    {
        public void UpdateVisual()
        {
            label.Text = "";
            label.Modulate = Colors.White;

            if (IsFlagged)
            {
                label.Text = "🚩";
            }
            else if (IsMine)
            {
                label.Text = "💣";
            }
            else if (IsRevealed)
            {
                if (AdjacentMines > 0)
                {
                    label.Text = AdjacentMines.ToString();
                    label.Modulate = GetNumberColor(AdjacentMines);
                }
                Modulate = Colors.WebGray;
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
