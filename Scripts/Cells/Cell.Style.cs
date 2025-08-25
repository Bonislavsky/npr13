using Godot;

namespace NPR13.Scripts.Cells
{
    public partial class Cell
    {
        protected void InitializeStyle()
        {
            _label.AddThemeColorOverride("font_outline_color", Colors.Black);
            _label.AddThemeConstantOverride("outline_size", 3);
        }
    }
}
