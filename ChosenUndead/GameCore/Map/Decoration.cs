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
        protected Animation animation { get; }

        public Decoration(Animation animation, Rectangle tilePosition) : base(null)
        {
            this.animation = animation;

            Rectangle = new Rectangle(
                tilePosition.X + tilePosition.Width / 2 - animation.FrameWidth / 2,
                tilePosition.Y + tilePosition.Height - animation.FrameHeight,
                animation.FrameWidth,
                animation.FrameHeight);
            Position = new Vector2(Rectangle.X, Rectangle.Y);
        }

        public Decoration(Texture2D texture, Rectangle tilePosition) : base(texture)
        {
            if (texture == null) 
                Rectangle = tilePosition;
            else
                Rectangle = new Rectangle(
                    tilePosition.X + tilePosition.Width / 2 - texture.Width / 2,
                    tilePosition.Y + tilePosition.Height - texture.Height,
                    texture.Width,
                    texture.Height);
        }

        public override void Update()
        {
            if (animation != null)
                animation.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, Rectangle, Color.White);
            else if (animation != null)
                animation.Draw(spriteBatch, Position);
        }
    }
}
