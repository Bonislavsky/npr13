using Godot;

namespace NPR13.Scripts.Cells
{
    public partial class Cell
    {
        [Signal]
        public delegate void CellClickedEventHandler(Vector2I pos);
        [Signal]
        public delegate void CellRightClickedEventHandler(Vector2I pos);
        [Signal]
        public delegate void CellDoubleClickedEventHandler(Vector2I pos);
        [Signal]
        public delegate void CellMouseEnteredEventHandler(Vector2I pos);
        [Signal]
        public delegate void CellMouseExitedEventHandler(Vector2I pos);

        private void InitializeSignals()
        {
            GuiInput += OnCellGuiInput;
            MouseEntered += () => EmitSignal(SignalName.CellMouseEntered, GridPosition);
            MouseExited += () => EmitSignal(SignalName.CellMouseExited, GridPosition);
        }

        private void OnCellGuiInput(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
            {
                if (mouseEvent.ButtonIndex == MouseButton.Left && IsRevealed && mouseEvent.DoubleClick)
                {
                    EmitSignal(SignalName.CellDoubleClicked, GridPosition);
                }
                else if (mouseEvent.ButtonIndex == MouseButton.Left && !IsRevealed)
                {
                    EmitSignal(SignalName.CellClicked, GridPosition);
                }
                else if (mouseEvent.ButtonIndex == MouseButton.Right && !IsRevealed)
                {
                    EmitSignal(SignalName.CellRightClicked, GridPosition);
                }
            }
        }
    }
}
