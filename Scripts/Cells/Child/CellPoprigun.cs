using Godot;
using System;
using System.Collections.Generic;

namespace NPR13.Scripts.Cells.Child
{
    public partial class CellPoprigun : Cell
    {
        private ColorRect _cellUniqueIllumination;

        public override void _Ready()
        {
            base._Ready();
            _cellUniqueIllumination = GetNode<ColorRect>("CellUniqueIllumination");
        }

        public CellPoprigun()
        {
            MineZone = GetMineZone();
        }

        public override Vector2I[] GetMineZone()
        {
            var res = new List<Vector2I>();

            for (int x = -2; x <= 2; x++)
            {
                for (int y = -2; y <= 2; y++)
                {
                    if (Math.Abs(x) <= 1 && Math.Abs(y) <= 1) continue;
                    res.Add(new Vector2I(x, y));
                }
            }

            return res.ToArray();
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
