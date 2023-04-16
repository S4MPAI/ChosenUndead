using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class AnimationManager
    {
        public Animation CurrentAnimation { get; private set; }

        private readonly Dictionary<string, Animation> _anims = new();

        public void AddAnimation(string animationName, Animation animation)
        {
            _anims.Add(animationName, animation);
            CurrentAnimation ??= animation;
        }

        public void SetAnimation(string key)
        {
            if (_anims.TryGetValue(key, out var value))
            {
                CurrentAnimation = value;
                //CurrentAnimation.Start();
            }
            else
            {
                //CurrentAnimation.Stop();
                CurrentAnimation.Reset();
            }
        }

        public void Update(GameTime gameTime)
        {
            CurrentAnimation.Update(gameTime);
        }

        public void Draw(Vector2 position, SpriteBatch spriteBatch, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            CurrentAnimation.Draw(spriteBatch, position, spriteEffect);
        }
    }
}
