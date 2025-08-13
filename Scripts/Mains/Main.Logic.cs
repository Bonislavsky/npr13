using Godot;
using NPR13.Scripts.Cells;
using System.Collections.Generic;
using System.Linq;

namespace NPR13.Scripts.Mains
{
    public partial class Main
    {
        private void CreateGameField(Vector2I safePos)
        {
            PlaceMines(safePos);
            CalculateNumbers();
        }

        private void PlaceMines(Vector2I safePos)
        {
            var safeZone = CreateSafeZone(safePos);
            var random = new RandomNumberGenerator();
            random.Randomize();

            for (int i = 0; i < mineCount; i++)
            {
                var pos = new Vector2I(
                    random.RandiRange(0, fieldWidth - 1),
                    random.RandiRange(0, fieldHeight - 1)
                );

                if(safeZone.Any(p => p.X == pos.X && p.Y == pos.Y))
                {
                    i--;
                    continue;
                }

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
        }

        private List<Vector2I> CreateSafeZone(Vector2I safePos)
        {
            List<Vector2I> safeZone = new List<Vector2I>();

            for (int x = -1; x <= 1; x++) 
            {
                for (int y = -1; y <= 1; y++)
                {
                    safeZone.Add(safePos + new Vector2I(x, y));
                }
            }

            return safeZone;
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
                    if (cells.TryGetValue(checkPos, out var tmpCell) && tmpCell.IsMine) count++;
                }
            }

            return count;
        }

        private void RevealCellAfDoubleClicked(Vector2I pos)
        {
            
            if (!cells.TryGetValue(pos, out var cell) || cell.IsFlagged) return;

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
            if (!cells.TryGetValue(pos, out var cell) || cell.IsFlagged || cell.IsRevealed) return;
            cell.SetRevealed();
            cell.UpdateVisual();

            if (cell.IsMine) 
            {
                GameOver(); 
            }

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

        private void ToggleBacklight(Vector2I pos, bool isDisable)
        {
            if (cells.TryGetValue(pos, out var valCell) && !valCell.IsRevealed) return;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;
                    var checkPos = pos + new Vector2I(x, y);
                    if (cells.TryGetValue(checkPos, out var tmpCell) && !tmpCell.IsRevealed )
                    {
                        if (isDisable) tmpCell.DisableBacklightVisibility();
                        else tmpCell.EnableBacklightVisibility();
                    }
                }
            }
        }

        public void GameOver()
        {
            foreach (var cell in cells.Values)
            {
                if (cell.IsMine || cell.IsFlagged)
                {
                    cell.GameOverVisual();
                }
            }
            _hud.ShowGameOverPanel();
        }
    }
}
