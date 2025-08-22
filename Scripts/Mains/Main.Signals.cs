using Godot;
using NPR13.Scripts.Cells;
using System.Linq;

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
            _hud.RestartGame += Restart;
        }

        private void OnInitializeClick(Vector2I pos)
        {
            CreateGameField(pos);
            gameInitialized = true;
            InitializeClick -= OnInitializeClick;
            RevealCellAfClicked(pos);
        }

        private void OnCellClicked(Vector2I pos)
        {
            if (!gameInitialized)
            {
                EmitSignal(SignalName.InitializeClick, pos);
                return;
            }

            if (!cells.TryGetValue(pos, out var cell) || cell.IsFlagged) return;

            if (cell.IsMine)
            {
                GameOver();
                return;
            }
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
                return;
            }
            else
            {
                RevealCellAfDoubleClicked(pos);
            }
        }

        private void OnCellMouseEntered(Vector2I pos)
        {
            ToggleBacklight(pos, false);
        }

        private void OnCellMouseExited(Vector2I pos)
        {
            ToggleBacklight(pos, true);
        }

        public void Restart()
        {
            _hud.HideGameOverPanel();

            revealedCells = 0;
            gameInitialized = false;
            InitializeClick += OnInitializeClick;

            foreach (var position in cells.Keys.ToArray())
            {
                if (cells.ContainsKey(position)) 
                {
                    Cell cell = cells[position];
                    cell.DefaultValue();
                    cell.ResetVisual();
                }
            }
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventKey keyEvent && keyEvent.Pressed)
            {
                if (keyEvent.AltPressed && keyEvent.Keycode == Key.Key3)
                {
                    if (_cellCreatorInstance == null)
                    {
                        _cellCreatorInstance = _cellCreatorScene.Instantiate<Window>();
                        GetTree().CurrentScene.AddChild(_cellCreatorInstance);
                    }
                    else
                    {
                        _cellCreatorInstance.QueueFree();
                        _cellCreatorInstance = null;
                    }
                }
            }
        }
    }
}
