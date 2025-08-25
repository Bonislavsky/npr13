using Godot;

namespace NPR13.Scripts.HUDS
{
    public partial class Hud : CanvasLayer
    {
        private Button _topPanelRestartButton;

        private Control _gameOverPanel;
        private Button _gameOverRestartButton;
        private Label _gameOverThxLabel;

        public override void _Ready()
        {
            _topPanelRestartButton = GetNode<Button>("TopPanel/RestartButton");
            _gameOverPanel = GetNode<Control>("GameOverPanel");
            _gameOverRestartButton = GetNode<Button>("GameOverPanel/Panel/VBoxContainer/PlayAgainButton");
            _gameOverThxLabel = GetNode<Label>("GameOverPanel/Panel/VBoxContainer/ThankYouLabel");
            _gameOverPanel.Visible = false;

            InitializeSignals();
        }

        public void HideGameOverPanel()
        {
            _gameOverPanel.Visible = false;
        }

        public void ShowGameOverPanel(string txt = "проиграл? запускай новую")
        {
            _gameOverPanel.Visible = true;
            _gameOverThxLabel.Text = txt;
        }
    }
}

