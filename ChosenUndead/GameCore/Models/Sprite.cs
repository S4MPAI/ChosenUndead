using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Sprite : Component
    {
        protected Texture2D texture;

        private float scale;
        protected Color color = Color.White;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(texture.Width * scale), (int)(texture.Height * scale));
            }
        }

        public Sprite(Texture2D texture, Color color, float scale = 1)
        {
            this.texture = texture;
            this.scale = scale;
            this.color = color;
        }

        public Sprite(Texture2D texture, float scale = 1)
        {
            this.texture = texture;
            this.scale = scale;
        }

        public override void Update()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, color, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle part)
        {
            spriteBatch.Draw(texture, Position, part, color, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }
    }
}
