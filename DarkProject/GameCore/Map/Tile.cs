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

    public abstract class MapEntity : Sprite
    {
        protected MapEntity(Texture2D texture) : base(texture)
        {
        }

        public new Rectangle Rectangle { get; protected set; }

        public Collision Collision { get; protected set; } = Collision.Passable;
    }

    public class Tile : MapEntity
    {
        public Tile(int i, Rectangle rectangle, Collision Collision) : base(Art.GetTileTexture(i))
        {
            this.Collision = Collision;
            Rectangle = rectangle;
        }

        public override void Update()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }


}
