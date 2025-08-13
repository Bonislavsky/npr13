using Godot;

namespace NPR13.Scripts.HUDS
{
    public partial class Hud : CanvasLayer
    {
        private Button _topPanelRestartButton;

        private Control _gameOverPanel;
        private Button _gameOverRestartButton;

        public override void _Ready()
        {
            _topPanelRestartButton = GetNode<Button>("TopPanel/RestartButton");
            _gameOverPanel = GetNode<Control>("GameOverPanel");
            _gameOverRestartButton = _gameOverPanel.GetNode<Button>("PlayAgainButton");
            _gameOverPanel.Visible = false;



            InitializeSignals();
        }

        public void HideGameOverPanel()
        {
            _gameOverPanel.Visible = false;
        }

        public void ShowGameOverPanel()
        {
            _gameOverPanel.Visible = true;
        }
    }
}

