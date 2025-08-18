using Godot;
using NPR13.Scripts.Cells;
using System.Collections.Generic;

public partial class CellCreator : Window
{
    // для роботы очистки клеточек надо в классе cell закоментировать "&& !IsRevealed" при обычном клике
    private PackedScene _cellScene;
    private GridContainer _gridContainer;
    private TextEdit _coordsText;
    private Button _copyButton;

    private int fieldWidth = 7;
    private int fieldHeight = 7;
    private Dictionary<Vector2I, Cell> cells = new Dictionary<Vector2I, Cell>();


    public override void _Ready()
    {
        _cellScene = GD.Load<PackedScene>("res://Scenes//Cell.tscn");
        _gridContainer = GetNode<GridContainer>("Constructor");
        _coordsText = GetNode<TextEdit>("TextEdit");
        _copyButton = GetNode<Button>("ButtonCopy");

        FillingContainer();

        _coordsText.Editable = false;
        _copyButton.Pressed += () => DisplayServer.ClipboardSet(_coordsText.Text);
        CloseRequested += () => QueueFree();
    }

    private void FillingContainer()
    {
        _gridContainer.Columns = fieldWidth;
        cells.Clear();

        for (int x = -3; x <= 3; x++)
        {
            for (int y = -3; y <= 3; y++)
            {
                var pos = new Vector2I(x, y);
                var cellInstance = _cellScene.Instantiate<Cell>();
                cellInstance.Initialize(pos);

                cellInstance.CellClicked += OnCellClicked;

                _gridContainer.AddChild(cellInstance);
                cells[pos] = cellInstance;
            }
        }

        if (cells.ContainsKey(Vector2I.Zero))
        {
            OnCellClicked(Vector2I.Zero);
        }
    }

    private void OnCellClicked(Vector2I pos)
    {
        cells.TryGetValue(pos, out var cell);
        if (!cell.IsRevealed)
        {
            cell.SetRevealed();
            cell.UpdateVisual();
            _coordsText.Text += $"new Vector2I({cell.GridPosition.X}, {cell.GridPosition.Y}),\n";
        }
        else
        {
            cell.SetNotRevealed();
            cell.UpdateVisual();
            _coordsText.Text = _coordsText.Text.Replace($"new Vector2I({cell.GridPosition.X}, {cell.GridPosition.Y}),\n", "");
        }
    }
}


