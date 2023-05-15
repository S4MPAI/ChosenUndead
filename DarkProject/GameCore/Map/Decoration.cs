using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Decoration : MapEntity
    {
        private Animation animation { get; }

        public Decoration(Animation animation, Rectangle tilePosition)
        {
            this.animation = animation;

            Rectangle = new Rectangle(
                tilePosition.X + tilePosition.Width / 2 - animation.FrameWidth / 2,
                tilePosition.Y + tilePosition.Height - animation.FrameHeight,
                animation.FrameWidth,
                animation.FrameHeight);
            Position = new Vector2(Rectangle.X, Rectangle.Y);
        }

        public Decoration(Texture2D texture, Rectangle tilePosition)
        {
            base.texture = texture;

            if (texture == null) 
                Rectangle = tilePosition;
            else
                Rectangle = new Rectangle(
                    tilePosition.X + tilePosition.Width / 2 - texture.Width / 2,
                    tilePosition.Y + tilePosition.Height - texture.Height,
                    texture.Width,
                    texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            if (animation != null)
                animation.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, Rectangle, Color.White);
            else if (animation != null)
                animation.Draw(spriteBatch, Position);
        }
    }
}
