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
    public class Tiles
    {
        protected Texture2D texture;

        public Rectangle Rectangle { get; protected set; }

        public static ContentManager Content { protected get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }

    public class CollisionTiles : Tiles
    {
        public CollisionTiles(int i, Rectangle rectangle) 
        {
            texture = Content.Load<Texture2D>("Tiles/tile" + i);
            Rectangle = rectangle;
        }
    }
}
