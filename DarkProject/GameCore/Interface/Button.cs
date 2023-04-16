using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ChosenUndead
{
    public class Button : Component
    {
        #region Fields

        private MouseState _currentMouse;

        private readonly SpriteFont _font;

        private bool _isHovering;

        private MouseState _previousMouse;

        #endregion

        #region Properties

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public string Text { get; set; }

        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont spriteFont)
        {
            _texture = texture;
            _font = spriteFont;
            PenColour = Color.Silver;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (_isHovering)
                colour = PenColour;

            spriteBatch.Draw(_texture, Rectangle, Color.White);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = Rectangle.X + Rectangle.Width / 2 - _font.MeasureString(Text).X / 2;
                var y = Rectangle.Y + Rectangle.Height / 2 - _font.MeasureString(Text).Y / 2;
                spriteBatch.DrawString(_font, Text, new Vector2(x, y), colour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var currentMouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (currentMouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    Click?.Invoke(this, new EventArgs());
            }
        }

        #endregion
    }
}
