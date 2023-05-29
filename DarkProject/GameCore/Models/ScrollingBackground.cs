using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class ScrollingBackground : Component
    {


        private bool constantSpeed;

        private float scrollingSpeed;

        private List<Sprite> sprites;

        private readonly Player player;

        private float speed;

        private Point windowSize;

        private float scale;

        public ScrollingBackground(Texture2D texture, float scrollingSpeed, Point windowSize, bool constantSpeed = false)
        {
            player = Player.GetInstance();
            this.scrollingSpeed = scrollingSpeed;
            this.constantSpeed = constantSpeed;
            this.windowSize = windowSize;
            scale = Math.Min((float)windowSize.X / texture.Width, (float)windowSize.Y / texture.Height);

            sprites = new List<Sprite>();
            for (int i = 0; i < 2; i++)
            {
                sprites.Add(new Sprite(texture, scale)
                {
                    Position = new Vector2(i * texture.Width * scale - 1, windowSize.Y - texture.Height * scale)
                });
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 windowPos)
        {
            foreach (var sprite in sprites)
            {
                sprite.Position += windowPos;
                sprite.Draw(gameTime, spriteBatch);
                sprite.Position -= windowPos;
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var sprite in sprites)
                sprite.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            ApplySpeed(gameTime);

            CheckPosition();
        }

        private void ApplySpeed(GameTime gameTime)
        {
            speed = (float)(scrollingSpeed * gameTime.ElapsedGameTime.TotalSeconds);

            if (player.Velocity.X > 0 || !constantSpeed)
                speed *= player.Velocity.X;

            foreach (var sprite in sprites)
                sprite.Position.X -= speed;
        }

        private void CheckPosition()
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                var sprite = sprites[i];

                if (sprite.Rectangle.Right <= 0)
                {
                    var index = i - 1;

                    if (index < 0)
                        index = sprites.Count - 1;

                    sprite.Position.X = sprites[index].Rectangle.Right;
                }

                if (sprite.Rectangle.Left >= windowSize.X)
                {
                    var index = i + 1;

                    if (index >= sprites.Count)
                        index = 0;

                    sprite.Position.X = sprites[index].Rectangle.Left - sprite.Rectangle.Width;
                }
            }
        }
    }
}
