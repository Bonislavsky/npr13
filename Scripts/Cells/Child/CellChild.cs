using Godot;
using System.Linq;

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

        public override Vector2I[] GetRevealZone()
        {
            var standardNeighbors = new Vector2I[]
            {
                new Vector2I(-1, -1), new Vector2I(-1, 0), new Vector2I(-1, 1),
                new Vector2I(0, -1),  new Vector2I(0, 1),
                new Vector2I(1, -1),  new Vector2I(1, 0),  new Vector2I(1, 1)
            };
            return standardNeighbors.Union(GetMineZone()).ToArray();
        }
    }
}
