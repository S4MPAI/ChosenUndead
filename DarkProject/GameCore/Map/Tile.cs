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

    public class MapEntity : Component
    {
        public Rectangle Rectangle { get; protected set; }

        public Collision Collision { get; protected set; }

        public static ContentManager Content { protected get; set; }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null) 
                spriteBatch.Draw(_texture, Rectangle, Color.White);
        }
    }

    public class Tile : MapEntity
    {
        public Tile(int i, Rectangle rectangle, Collision Collision) 
        {
            if (i == 0) 
                _texture = null;
            else 
                _texture = Content.Load<Texture2D>("Tiles/tile" + i);

            this.Collision = Collision;
            Rectangle = rectangle;
        }
    }
}
