using Godot;
using NPR13.Scripts.Cells;
using System.Collections.Generic;

namespace NPR13.Scripts.Mains
{
    public partial class Main : Node
    {
        private PackedScene cellScene;
        private GridContainer gridContainer;

        private int fieldWidth = 16;
        private int fieldHeight = 16;
        private int mineCount = 40;

        private Dictionary<Vector2I, Cell> cells = new Dictionary<Vector2I, Cell>();

        public override void _Ready()
        {
            cellScene = GD.Load<PackedScene>("res://Scenes//Cell.tscn");
            gridContainer = GetNode<GridContainer>("GridContainer");

            InitializeSignals();
            FillingContainer();
        }

        private void FillingContainer()
        {
            gridContainer.Columns = fieldWidth;
            cells.Clear();

            for (int x = 0; x < fieldWidth; x++)
            {
                for (int y = 0; y < fieldHeight; y++)
                {
                    var pos = new Vector2I(x, y);
                    var cellInstance = cellScene.Instantiate<Cell>();

                    cellInstance.Initialize(pos);
                    cellInstance.Name = $"cell_{x}-{y}";

                    cellInstance.CellClicked += OnCellClicked;
                    cellInstance.CellRightClicked += OnCellRightClicked;
                    cellInstance.CellDoubleClicked += OnCellDoubleClicked;

                    gridContainer.AddChild(cellInstance);
                    cells[pos] = cellInstance;
                }
            }
        }
    }
} 