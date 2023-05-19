using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ChosenUndead
{
    public class Animation
    {
        private readonly Texture2D texture;

        public readonly int FrameWidth;

        public readonly int FrameHeight;

        private readonly List<Rectangle> frames = new();

        public readonly int FramesCount;

        private int currentFrame;

        private float frameTime;

        private double frameTimeLeft;

        //private bool _active = true;

        public Animation(Texture2D texture, int framesX, float frameTime)
        {
            this.texture = texture;
            this.frameTime = frameTime;
            frameTimeLeft = this.frameTime;
            FramesCount = framesX;
            FrameWidth = this.texture.Width / framesX;
            FrameHeight = this.texture.Height;

            for (int i = 0; i < FramesCount; i++)
            {
                frames.Add(new(FrameWidth * i, 0, FrameWidth, FrameHeight));
            }
        }

        //public void Stop()
        //{
        //    _active = false;
        //}

        //public void Start()
        //{
        //    _active = true;
        //}

        public void Reset()
        {
            currentFrame = 0;
            frameTimeLeft = frameTime;
        }

        public void ChangeFrameTime(float frameTime)
        {
            Reset();
            this.frameTime = frameTime;
        }

        public void Update(GameTime gameTime)
        {
            //if (!_active) return;

            frameTimeLeft -= gameTime.ElapsedGameTime.TotalSeconds;

            if (!(frameTimeLeft <= 0)) return;
            
            frameTimeLeft += frameTime;
            currentFrame = (currentFrame + 1) % FramesCount;
        }

        public bool IsEnded() => FramesCount == currentFrame + 1;

        public void Draw(SpriteBatch spriteBatch, Vector2 pos, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            spriteBatch.Draw(texture, pos, frames[currentFrame], Color.White, 0, Vector2.Zero, Vector2.One, spriteEffect, 1);
        }
    }
}
