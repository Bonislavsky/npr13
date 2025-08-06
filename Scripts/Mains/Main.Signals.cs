using Godot;

namespace NPR13.Scripts.Mains
{
    public partial class Main
    {
        [Signal]
        public delegate void InitializeClickEventHandler(Vector2I pos);

        private bool gameInitialized = false;

        private void InitializeSignals()
        {
            InitializeClick += OnInitializeClick;
        }

        private void OnInitializeClick(Vector2I pos)
        {
            CreateGameField(pos);
            gameInitialized = true;
            InitializeClick -= OnInitializeClick;
        }

        private void OnCellClicked(Vector2I pos)
        {
            if (!gameInitialized)
            {
                EmitSignal(SignalName.InitializeClick, pos);
                RevealCellAfClicked(pos);
                return;
            }

            if (!cells.TryGetValue(pos, out var cell) || cell.IsFlagged) return;

            if (cell.IsMine) GameOver();     
            else RevealCellAfClicked(pos);           
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
