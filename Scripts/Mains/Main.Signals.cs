using Godot;

namespace NPR13.Scripts.Mains
{
    public partial class Main
    {
        [Signal]
        public delegate void InitializeClickEventHandler(Vector2I pos);

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
            {
                Vector2I pos = new Vector2I((int)mouseEvent.Position.X, (int)mouseEvent.Position.Y);
                EmitSignal(SignalName.InitializeClick, pos);
            }
        }

        private void InitializeSignals()
        {
        }

        private void OnInitializeClick(Vector2I pos)
        {
            // ТУТ БУДЕТ ЛОГИКА СОЗЛАНИЯ поля с начальной позиции
            InitializeClick -= OnInitializeClick;
        }

        private void OnCellClicked(Vector2I pos)
        {
            GD.Print($"Клик по клетке {pos}");
            if (!cells.TryGetValue(pos, out var cell) || cell.IsFlagged) return;

            if (cell.IsMine)
            {
                GameOver();
            }
            else
            {
                RevealCellAfClicked(pos);
            }
        }

        private void OnCellRightClicked(Vector2I pos)
        {
            if (cells.TryGetValue(pos, out var cell))
            {
                cell.ToggleFlag();
                cell.UpdateVisual();
            }
        }

        private void OnCellDoubleClicked(Vector2I pos)
        {
            if (!cells.TryGetValue(pos, out var cell) || cell.IsFlagged) return;
            if (cell.IsMine)
            {
                GameOver();
            }
            else
            {
                RevealCellAfDoubleClicked(pos);
            }
        }
    }
}
