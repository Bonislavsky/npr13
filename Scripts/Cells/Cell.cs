using Godot;
using NPR13.Scripts.Cells.Enums;

namespace NPR13.Scripts.Cells
{
    public partial class Cell : Panel
    {
        public Vector2I GridPosition { get; private set; }
        public bool IsMine { get; private set; }
        public bool IsFlagged { get; private set; }
        public bool IsRevealed { get; private set; }
        public int AdjacentMines { get; private set; }
        public CellType CellType { get; private set; }

        private Label label;

        public override void _Ready()
        {
            label = GetNode<Label>("CellLabel");
            InitializeSignals();
        }

        public void Initialize(Vector2I pos)
        {
            GridPosition = pos;
            CellType = CellType.Default;
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
        public void SetRevealed()
        {
            if (IsFlagged) return;
            IsRevealed = true;
        }
    }
}
