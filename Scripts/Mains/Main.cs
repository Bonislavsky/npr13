using Godot;
using NPR13.Scripts.Cells;
using NPR13.Scripts.HUDS;
using System;
using System.Collections.Generic;

namespace NPR13.Scripts.Mains
{
    public partial class Main : Node
    {
        #region tools
        private PackedScene _cellCreatorScene;
        private Window _cellCreatorInstance;
        #endregion

        private PackedScene _cellScene;
        private PackedScene _cellPoprigunScene;
        private PackedScene _cellBishopScene;

        private Panel _panelBackground;
        private GridContainer _gridContainer;
        private Hud _hud;

        private int fieldWidth = 16;
        private int fieldHeight = 16;
        private int mineCount = 40;
        private int revealedCells = 0;
        private Random _random = new Random();

        private Dictionary<Vector2I, Cell> cells = new Dictionary<Vector2I, Cell>();

        public override void _Ready()
        {
            _cellCreatorScene = GD.Load<PackedScene>("res://Scenes/CellCreator.tscn");

            _cellBishopScene = GD.Load<PackedScene>("res://Scenes/CellBishop.tscn");
            _cellPoprigunScene = GD.Load<PackedScene>("res://Scenes/CellPoprigun.tscn");
            _cellScene = GD.Load<PackedScene>("res://Scenes//Cell.tscn");
            _gridContainer = GetNode<GridContainer>("GridContainer");
            _panelBackground = GetNode<Panel>("PanelBackground");
            _hud = GetNode<Hud>("HUD");

            InitializeSignals();
            FillingContainer();
            CallDeferred(nameof(UpdatePanelBackground));
        }
    }
} 