﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class AnimationManager<TAnimationKey>
    {
        public Animation CurrentAnimation { get; private set; }

        private readonly Dictionary<TAnimationKey, Animation> anims = new();

        public void AddAnimation(TAnimationKey animationName, Animation animation)
        {
            anims.Add(animationName, animation);
            CurrentAnimation ??= animation;
        }

        public void SetAnimation(TAnimationKey key)
        {
            if (anims.TryGetValue(key, out var value))
            {
                if (value != CurrentAnimation) value.Reset();
                CurrentAnimation = value;
                //CurrentAnimation.Start();
            }
            else
            {
                //CurrentAnimation.Stop();
                CurrentAnimation.Reset();
            }
        }

        public void ChangeFrameTime(TAnimationKey key, float animationTime)
        {
            if(anims.TryGetValue(key,out var value))
            {
                value.ChangeFrameTime(animationTime / value.FramesCount);
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
