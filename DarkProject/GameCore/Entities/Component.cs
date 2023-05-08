using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ChosenUndead
{
    public abstract class Component
    {
        protected Texture2D texture;

        public Vector2 Position;

        public static ContentManager Content;

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
