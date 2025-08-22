using Godot;

namespace NPR13.Scripts.Cells.Child
{
    public abstract partial class CellChild : Cell
    {
        protected ColorRect _cellUniqueIllumination;

        public override void _Ready()
        {
            base._Ready();
            _cellUniqueIllumination = GetNode<ColorRect>("CellUniqueIllumination");
        }

        public CellChild()
        {
            MineZone = GetMineZone();
        }

        public override void ResetVisual()
        {
            base.ResetVisual();
            _cellUniqueIllumination.Visible = false;
        }

        public override void UpdateVisual()
        {
            base.UpdateVisual();
            _cellUniqueIllumination.Visible = IsRevealed && AdjacentMines > 0;
        }
    }
}
