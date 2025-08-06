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

            CreateGameField();
        }


        private void CreateGameField()
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

            PlaceMines();
            CalculateNumbers();
        }

        public void PlaceMines()
        {
            var random = new RandomNumberGenerator();
            random.Randomize();

            for (int i = 0; i < mineCount; i++)
            {
                var pos = new Vector2I(
                    random.RandiRange(0, fieldWidth - 1),
                    random.RandiRange(0, fieldHeight - 1)
                );

                if (cells.TryGetValue(pos, out var cell))
                {
                    if (cell.IsMine)
                    {
                        i--;
                        continue;
                    }

                    cell.SetMine();
                }
            }

            GD.Print($"Размещено {mineCount} мин");
        }

        private void CalculateNumbers()
        {
            foreach (var kvp in cells)
            {
                var pos = kvp.Key;
                var cell = kvp.Value;

                if (!cell.IsMine)
                {
                    cell.SetAdjacentMines(CountAdjacentMines(pos, cell));
                }
            }
            GD.Print("Подсчитаны соседние мины");
        }

        private int CountAdjacentMines(Vector2I pos, Cell cell)
        {
            int count = 0;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;

                    var checkPos = pos + new Vector2I(x, y);
                    if (CheckMine(checkPos)) count++;
                }
            }

            return count;
        }

        private bool CheckMine(Vector2I checkPos)
        {
            return cells.TryGetValue(checkPos, out var tmpCell) && tmpCell.IsMine;
        }

        private void RevealCellAfDoubleClicked(Vector2I pos)
        {
            if (!cells.TryGetValue(pos, out var cell) || cell.IsFlagged || cell.IsMine) return;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;
                    RevealCellAfClicked(pos + new Vector2I(x, y));
                }
            }
        }

        private void RevealCellAfClicked(Vector2I pos)
        {
            if (!cells.TryGetValue(pos, out var cell) || cell.IsFlagged || cell.IsMine || cell.IsRevealed) return;

            cell.SetRevealed();
            cell.UpdateVisual();

            if (cell.AdjacentMines == 0)
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0) continue;
                        RevealCellAfClicked(pos + new Vector2I(x, y));
                    }
                }
            }
        }

        public void GameOver()
        {
            GD.Print("Game Over!");
            foreach (var cell in cells.Values)
            {
                if (cell.IsMine)
                {
                    cell.UpdateVisual();
                }
            }
        }
    }
}