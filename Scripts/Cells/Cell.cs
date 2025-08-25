using Godot;

namespace NPR13.Scripts.Cells
{
    public partial class Cell : Panel
    {
        public bool _animationsEnabled = true;

        public Vector2I GridPosition { get; private set; }
        public bool IsMine { get; private set; }
        public bool IsFlagged { get; private set; }
        public bool IsRevealed { get; private set; }
        public int AdjacentMines { get; private set; }
        public Vector2I[] MineZone { get; protected set; }

        protected Label _label;
        protected ColorRect _backlight;
        protected TextureRect _texture;
        protected Label _backlightLabel;

        public Cell()
        {
            MineZone = GetMineZone();
        }

        public override void _Ready()
        {
            _label = GetNode<Label>("CellLabel");
            _backlight = GetNode<ColorRect>("CellBacklight");
            _texture = GetNode<TextureRect>("CellTexture");
            _backlightLabel = GetNode<Label>("CellBacklight/Label");

            InitializeStyle();
            InitializeSignals();
            DefaultValue();
        }

    }
}
