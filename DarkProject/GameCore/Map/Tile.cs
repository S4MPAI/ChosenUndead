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

        public static ContentManager Content { protected get; set; }

        
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

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Rectangle, Color.White);
        }
    }

    public class Decoration : MapEntity
    {
        public event EventHandler EntityEnter;

        private Animation _animation { get; }

        private Rectangle _rectangle { get; }

        private Entity _followedEntity { get; }

        public Decoration(Animation animation, Rectangle tilePosition, Entity entity = null)
        {
            _animation = animation;

            _rectangle = new Rectangle(
                tilePosition.X + tilePosition.Width / 2 - animation.FrameWidth / 2,
                tilePosition.Y + tilePosition.Height - animation.FrameHeight,
                animation.FrameWidth, 
                animation.FrameHeight);
            Position = new Vector2(_rectangle.X, _rectangle.Y);

            _followedEntity = entity;
        }

        public Decoration(Texture2D texture, Rectangle tilePosition, Entity entity = null)
        {
           _texture = texture;

            _rectangle = new Rectangle(
                tilePosition.X + tilePosition.Width / 2 - texture.Width / 2,
                tilePosition.Y + tilePosition.Height - texture.Height,
                texture.Width,
                texture.Height);

            _followedEntity = entity;
        }

        public override void Update(GameTime gameTime)
        {
            if (_animation != null)
                _animation.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Rectangle, Color.White);
            else if (_animation != null)
                _animation.Draw(spriteBatch, Position);
        }
    }
}
