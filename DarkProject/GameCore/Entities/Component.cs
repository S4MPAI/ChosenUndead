using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChosenUndead
{
    public abstract class Component
    {
        protected Texture2D _texture;

        public Vector2 Position;

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
