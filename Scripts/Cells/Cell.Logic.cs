using Godot;
using System.Collections.Generic;
using System.Linq;

namespace NPR13.Scripts.Cells
{
    public partial class Cell
    {
        public virtual Vector2I[] GetMineZone()
        {
            return
            [
                new Vector2I(-1, -1),
                new Vector2I(-1, 0),
                new Vector2I(-1, 1),
                new Vector2I(0, -1),
                new Vector2I(0, 1),
                new Vector2I(1, -1),
                new Vector2I(1, 0),
                new Vector2I(1, 1)
            ];
        }

        public virtual Vector2I[] GetSafeZone()
        {
            return GetMineZone().Concat(new List<Vector2I>
            {
                new Vector2I(0, 0),
                new Vector2I(2, 2),
                new Vector2I(-2, 2),
                new Vector2I(-2, -2),
                new Vector2I(2, -2)
            }).ToArray();
        }

        public void Initialize(Vector2I pos)
        {
            GridPosition = pos;
        }

        public void DefaultValue()
        {
            IsMine = false;
            IsFlagged = false;
            IsRevealed = false;
            _texture.Texture = GD.Load<Texture2D>("res://Arts/cell_front.png");
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

        /// <summary>
        /// ONLY for tools
        /// NOT USE in game logic
        /// </summary>
        public void SetNotRevealed() => IsRevealed = false;
    }
}
