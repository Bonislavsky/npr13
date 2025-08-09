using Godot;
using NPR13.Scripts.Cells;
using System.Collections.Generic;

namespace NPR13.Scripts.Mains
{
    public partial class Main : Node
    {
        private PackedScene _cellScene;
        private GridContainer _gridContainer;
        private Hud _hud;

        private int fieldWidth = 16;
        private int fieldHeight = 16;
        private int mineCount = 40;

        private Dictionary<Vector2I, Cell> cells = new Dictionary<Vector2I, Cell>();

        public override void _Ready()
        {
            _cellScene = GD.Load<PackedScene>("res://Scenes//Cell.tscn");
            _gridContainer = GetNode<GridContainer>("GridContainer");
            _hud = GetNode<Hud>("HUD");

            InitializeSignals();
            FillingContainer();
        }

        private void FillingContainer()
        {
            _gridContainer.Columns = fieldWidth;
            cells.Clear();

            for (int x = 0; x < fieldWidth; x++)
            {
                for (int y = 0; y < fieldHeight; y++)
                {
                    var pos = new Vector2I(x, y);
                    var cellInstance = _cellScene.Instantiate<Cell>();

                    cellInstance.Initialize(pos);
                    cellInstance.Name = $"cell_{x}-{y}";

                    cellInstance.CellClicked += OnCellClicked;
                    cellInstance.CellRightClicked += OnCellRightClicked;
                    cellInstance.CellDoubleClicked += OnCellDoubleClicked;

                    _gridContainer.AddChild(cellInstance);
                    cells[pos] = cellInstance;
                }
            }
        }
    }
} 