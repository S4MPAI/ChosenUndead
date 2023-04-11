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
        public readonly Texture2D Texture;

        public readonly int FrameWidth;

        public readonly int FrameHeight;

        private readonly List<Rectangle> _frames = new();

        private readonly int _framesCount;

        private int _currentFrame;

        private readonly double _frameTime;

        private double _frameTimeLeft;

        //private bool _active = true;

        public Animation(Texture2D texture, int frameWidth, int frameHeight, int framesCount, float frameTime)
        {
            Texture = texture;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            _frameTime = frameTime;
            _frameTimeLeft = _frameTime;
            _framesCount = framesCount;
            var framesCountX = Texture.Width / frameWidth;
            var framesCountY = Texture.Height / frameHeight;
            var currentFrameCount = 0;

            for (int i = 0; i < framesCountY; i++)
                for (int j = 0; j < framesCountX; j++)
                {
                    if (++currentFrameCount > _framesCount) break;

                    _frames.Add(new Rectangle(j * frameWidth, i * frameHeight, frameWidth, frameHeight));
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
            _currentFrame = 0;
            _frameTimeLeft = _frameTime;
        }

        public void Update(GameTime gameTime)
        {
            //if (!_active) return;

            _frameTimeLeft -= gameTime.ElapsedGameTime.TotalSeconds;

            if (_frameTimeLeft <= 0)
            {
                _frameTimeLeft += _frameTime;
                _currentFrame = (_currentFrame + 1) % _framesCount;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            spriteBatch.Draw(Texture, pos, _frames[_currentFrame], Color.White, 0, Vector2.Zero, Vector2.One, spriteEffect, 1);
        }
    }
}
