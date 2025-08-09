using Godot;

namespace NPR13.Scripts.HUDS
{
    public partial class Hud : CanvasLayer
    {
        private Button _restartButton;

        public override void _Ready()
        {
            _restartButton = GetNode<Button>("TopPanel/RestartButton");
        }
    }
}

