using Godot;
using NPR13.Scripts.Cells;
using NPR13.Scripts.Cells.Child;
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
                    var cellInstance = CreateRandomCell();
                    cellInstance.Initialize(pos);
                    cellInstance.Name = $"cell_{x}-{y}";

                    cellInstance.CellClicked += OnCellClicked;
                    cellInstance.CellRightClicked += OnCellRightClicked;
                    cellInstance.CellDoubleClicked += OnCellDoubleClicked;

                    cellInstance.CellMouseEntered += OnCellMouseEntered;
                    cellInstance.CellMouseExited += OnCellMouseExited;

                    _gridContainer.AddChild(cellInstance);
                    cells[pos] = cellInstance;
                }
            }
        }

        private Cell CreateRandomCell()
        {
            float rand = _random.NextSingle();

            if (rand < 0.1f)        
                return _cellPoprigunScene.Instantiate<CellPoprigun>();
            else if (rand < 0.2f)  
                return _cellBishopScene.Instantiate<CellBishop>(); 
            else                    
                return _cellScene.Instantiate<Cell>();
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventKey keyEvent && keyEvent.Pressed)
            {
                if (keyEvent.AltPressed && keyEvent.Keycode == Key.Key3)
                {
                    if (_cellCreatorInstance == null)
                    {
                        _cellCreatorInstance = _cellCreatorScene.Instantiate<Window>();
                        GetTree().CurrentScene.AddChild(_cellCreatorInstance);
                    }
                    else
                    {
                        _cellCreatorInstance.QueueFree();
                        _cellCreatorInstance = null;
                    }
                }
            }
        }
    }
} 