using Godot;
using System.Linq;

namespace NPR13.Scripts.Cells.Child
{
    public partial class CellBishop : CellChild
    {
        public override Vector2I[] GetMineZone()
        {
            return
            [
                new Vector2I(-1, 1),
                new Vector2I(-2, 2),
                new Vector2I(-3, 3),
                new Vector2I(1, 1),
                new Vector2I(2, 2),
                new Vector2I(3, 3),
                new Vector2I(1, -1),
                new Vector2I(2, -2),
                new Vector2I(3, -3),
                new Vector2I(-1, -1),
                new Vector2I(-2, -2),
                new Vector2I(-3, -3),
            ];
        }

        public override Vector2I[] GetSafeZone()
        {
            return GetMineZone().Concat(new Vector2I[]
            {
                new Vector2I(0, 0),
                new Vector2I(0, -1),
                new Vector2I(-1, 0),
                new Vector2I(0, 1),
                new Vector2I(1, 0),
                new Vector2I(0, 2),
                new Vector2I(0, -2),
                new Vector2I(2, 0),
                new Vector2I(-2, 0),
            }).ToArray();
        }
    }
}
    