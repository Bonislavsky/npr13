using Godot;
using System.Collections.Generic;
using System.Linq;

namespace NPR13.Scripts.Mains
{
    public partial class Main
    {
        private void CreateGameField(Vector2I safePos)
        {
            revealedCells = 0;
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

        private List<Vector2I> CreateSafeZone(Vector2I centerPos)
        {
            var safePositions = new HashSet<Vector2I> { centerPos };
            var cell = cells[centerPos];

            foreach (var offset in cell.GetSafeZone())
            {
                safePositions.Add(centerPos + offset);
            }

            return safePositions.ToList();
        }

        private void CalculateNumbers()
        {
            foreach (var kvp in cells)
            {
                var pos = kvp.Key;
                var cell = kvp.Value;

                if (!cell.IsMine)
                {
                    cell.SetAdjacentMines(CountAdjacentMines(pos));
                }
            }
        }

        private int CountAdjacentMines(Vector2I pos)
        {
            var cell = cells[pos];
            int count = 0;
            
            foreach(var posM in cell.MineZone)
            {
                var checkPos = pos + posM;
                if (cells.TryGetValue(checkPos, out var tmpCell) && tmpCell.IsMine) count++;
            }

            return count;
        }

        private void RevealCellAfDoubleClicked(Vector2I pos)
        {
            if (!cells.TryGetValue(pos, out var cell) || cell.IsFlagged) return;

            foreach (var posM in cell.MineZone)
            {
                RevealCellAfClicked(pos + posM);
            }
        }

        private void RevealCellAfClicked(Vector2I pos)
        {
            if (!cells.TryGetValue(pos, out var cell) || cell.IsFlagged || cell.IsRevealed) return;

            cell.SetRevealed();
            cell.UpdateVisual();
            revealedCells++;

            if (cell.IsMine) 
            {
                GameOver();
                return;
            }            

            if (cell.AdjacentMines == 0)
            {
                foreach (var offset in cell.MineZone)
                {
                    RevealCellAfClicked(pos + offset);
                }
            }

            CheckWinCondition();
        }

        private void ToggleBacklight(Vector2I pos, bool isDisable)
        {
            if (cells.TryGetValue(pos, out var cell) && !cell.IsRevealed) return;

            foreach (var posM in cell.MineZone)
            {
                var checkPos = pos + posM;
                if (cells.TryGetValue(checkPos, out var tmpCell) && !tmpCell.IsRevealed)
                {
                    if (isDisable) tmpCell.DisableBacklightVisibility();
                    else tmpCell.EnableBacklightVisibility();
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

        private void CheckWinCondition()
        {
            if (revealedCells == fieldWidth * fieldHeight - mineCount) 
            {
                GameOver();
            }        
        }
    }
}
