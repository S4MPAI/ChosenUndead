using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ChosenUndead
{
    public class Button : Sprite
    {
        #region Fields

        private MouseState currentMouse;

        private readonly SpriteFont font;

        private bool isHovering;

        private MouseState previousMouse;

        #endregion

        #region Properties

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }

        public string Text { get; set; }

        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont spriteFont) : base(texture)
        {
            font = spriteFont;
            PenColour = Color.Silver;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (isHovering)
                colour = PenColour;

            spriteBatch.Draw(texture, Rectangle, Color.White);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = Rectangle.X + Rectangle.Width / 2 - font.MeasureString(Text).X / 2;
                var y = Rectangle.Y + Rectangle.Height / 2 - font.MeasureString(Text).Y / 2;
                spriteBatch.DrawString(font, Text, new Vector2(x, y), colour);
            }
        }

        public override void Update()
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var currentMouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            isHovering = false;

            if (currentMouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;

                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                    Click?.Invoke(this, new EventArgs());
            }
        }

        #endregion
    }
}
