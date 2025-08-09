using Godot;

public partial class Hud : CanvasLayer
{
    [Signal]
    public delegate void RestartGameEventHandler();

    private Button _restartButton;

    public override void _Ready()
    {
        _restartButton = GetNode<Button>("TopPanel/RestartButton");
        _restartButton.Pressed += OnRestartPressed;
    }

    private void OnRestartPressed()
    {
        EmitSignal(SignalName.RestartGame);
    }
}
