using Godot;

namespace NPR13.Scripts.Cells
{
    public partial class Cell : Panel
    {
        public Vector2I GridPosition { get; private set; }
        public bool IsMine { get; private set; }
        public bool IsFlagged { get; private set; }
        public bool IsRevealed { get; private set; }
        public int AdjacentMines { get; private set; }
        public Vector2I[] MineZone { get; protected set; }

        protected Label _label;
        protected ColorRect _backlight;

        public Cell()
        {
            MineZone = GetMineZone();
        }

        public override void _Ready()
        {
            _label = GetNode<Label>("CellLabel");
            _backlight = GetNode<ColorRect>("CellBacklight");

            InitializeSignals();
        }

        public void Initialize(Vector2I pos)
        {
            GridPosition = pos;
            DefaultValue();
        }

        public void DefaultValue()
        {
            IsMine = false;
            IsFlagged = false;
            IsRevealed = false;
        }

        public void SetMine() => IsMine = true;
        public void SetAdjacentMines(int count) => AdjacentMines = count;
        public void ToggleFlag() => IsFlagged = !IsFlagged;

        public void DisableBacklightVisibility() => _backlight.Visible = false;
        public void EnableBacklightVisibility() => _backlight.Visible = true;

        public void SetRevealed()
        {
            if (IsFlagged) return;
            IsRevealed = true;
        }  

        public virtual Vector2I[] GetMineZone()
        {
            return
            [
                new Vector2I(-1, -1),
                new Vector2I(-1,  0),
                new Vector2I(-1,  1),
                new Vector2I( 0, -1),
                new Vector2I( 0,  1),
                new Vector2I( 1, -1),
                new Vector2I( 1,  0),
                new Vector2I( 1,  1)
            ];
        }
    }
}
