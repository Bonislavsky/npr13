using Godot;

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
    }
}
    