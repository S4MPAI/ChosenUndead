using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class ProgressBar
    {
        private Sprite backProgressBar;

        private Sprite progressBar;

        private Sprite frontProgressBar;

        private float maxValue;

        private float currentValue;

        private float targetValue;

        private float size;

        private readonly float animationSpeed = 20f;

        private Color color;

        private Rectangle part;

        public ProgressBar(Texture2D bPB, Texture2D pB, Texture2D fPB, Color barColor, float max, Vector2 pos, float animSpeed = 20f, float size = 1f) 
        {
            animationSpeed = animSpeed;
            color = barColor;
            maxValue = max;
            backProgressBar = new Sprite(bPB, size);
            progressBar = new Sprite(pB, color, size);
            frontProgressBar = new Sprite(fPB, size);
            part.Height = pB.Height;
            this.size = size;
            var center = pos;
            center.X += Math.Max(Math.Max(bPB.Width, pB.Width), fPB.Width) / 2 * size;
            center.Y += Math.Max(Math.Max(bPB.Height, pB.Height), fPB.Height) / 2 * size;

            backProgressBar.Position = center - new Vector2(bPB.Width, bPB.Height) / 2 * size;
            progressBar.Position = center - new Vector2(pB.Width, pB.Height) / 2 * size;
            frontProgressBar.Position = center - new Vector2(fPB.Width, fPB.Height) / 2 * size;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            backProgressBar.Draw(spriteBatch);
            progressBar.Draw(spriteBatch, part);
            frontProgressBar.Draw(spriteBatch);
        }

        public void Update(float value)
        {
            targetValue = value;

            if (targetValue < currentValue)
            {
                currentValue -= animationSpeed * Time.ElapsedSeconds;

                if (currentValue < targetValue) 
                    currentValue = targetValue;
                part.Width = (int)(currentValue / maxValue * progressBar.Rectangle.Width / size);
            }
            else
            {
                currentValue += animationSpeed * Time.ElapsedSeconds;

                if (currentValue > targetValue) 
                    currentValue = targetValue;
                part.Width = (int)(currentValue / maxValue * progressBar.Rectangle.Width / size);
            }
        }

        public void Update(float value, float maxValue)
        {
            this.maxValue = maxValue;
            Update(value);
        }
    }
}
