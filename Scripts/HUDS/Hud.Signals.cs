using Godot;

namespace NPR13.Scripts.HUDS
{
    public partial class Hud
    {
        [Signal]
        public delegate void RestartGameEventHandler();

        private void InitializeSignals()
        {
            _gameOverRestartButton.Pressed += OnRestartPressed;
            _topPanelRestartButton.Pressed += OnRestartPressed;
        }

        private void OnRestartPressed()
        {
            EmitSignal(SignalName.RestartGame);
        }
    }
}
