using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ChosenUndead
{
    public abstract class Component
    {
        public Vector2 Position;

        public static ContentManager Content;

        public abstract void Update();

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
