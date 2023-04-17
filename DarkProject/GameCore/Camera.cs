using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChosenUndead.ChosenUndeadGame;

namespace ChosenUndead
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Entity target)
        {
            var position = Matrix.CreateTranslation(
                -target.Position.X - target.HitBox.Width ,
                -target.Position.Y - target.HitBox.Height,
                0);

            var offset = Matrix.CreateTranslation(
                                ScreenWidth / 2,
                                ScreenHeight / 2,
                                0);

            var scale = Matrix.CreateScale(1.6f, 1.6f, 0);

            Transform = position * scale * offset;
        }
    }
}
