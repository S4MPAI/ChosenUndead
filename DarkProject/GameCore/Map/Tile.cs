using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public enum Collision
    {
        Passable,
        Impassable,
    }

    public abstract class MapEntity : Component
    {
        public Rectangle Rectangle { get; protected set; }

        public Collision Collision { get; protected set; }
    }

    public class Tile : MapEntity
    {
        public Tile(int i, Rectangle rectangle, Collision Collision) 
        {
            if (i == 0) 
                texture = null;
            else 
                texture = Art.GetTileTexture(i);

            this.Collision = Collision;
            Rectangle = rectangle;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }


}
