using Godot;
using System;

namespace NPR13.Scripts.Cells
{
    public partial class Cell
    {
        public virtual void UpdateVisual()
        {
            ResetVisual();

            if (IsFlagged)
            {
                _label.Text = "🚩";
                _backlightLabel.Text = "";
            }
            else if (IsRevealed)
            {
                if (AdjacentMines > 0)
                {
                    _label.Text = AdjacentMines.ToString();
                    _label.Modulate = GetNumberColor(AdjacentMines);                  
                }
                _texture.Texture = GD.Load<Texture2D>("res://Arts/cell_back.png");
            }
        }

        public virtual void ResetVisual()
        {
            _label.Text = "";
            _label.Modulate = Colors.White;
            Modulate = Colors.White;
            _backlight.Visible = false;
            _backlightLabel.Text = "?";
        }

        public void GameOverVisual()
        {
            if(IsMine && IsFlagged)
            {
                Modulate = Colors.Green;
            }
            else if(!IsMine && IsFlagged)
            {
                Modulate = Colors.Red;
            }
            else if (IsMine)
            {
                _label.Text = "💣";
            }
        }

        protected Color GetNumberColor(int number)
        {
            return number switch
            {
                1 => Colors.Blue,
                2 => Colors.DarkGreen,
                3 => Colors.DarkRed,
                4 => Colors.Purple,
                5 => Colors.Brown,
                6 => Colors.Pink,
                7 => Colors.Black,
                8 => Colors.Gray,
                _ => Colors.Black
            };
        }

        public void AnimateClick(Action callback)
        {
            if (!_animationsEnabled)
            {
                callback?.Invoke();
                return;
            }
          
            var tween = CreateTween();
            PivotOffset = new Vector2(16, 16);

            tween.TweenProperty(this, "scale:x", 0.0f, 0.1f);

            tween.TweenCallback(Callable.From(()=> 
            {
                callback?.Invoke();
            }
            ));

            tween.TweenProperty(this, "scale:x", 1.0f, 0.1f);

        }
    }
}
