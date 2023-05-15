using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Camera
    {
        private Point windowSize;

        public Camera(Point windowSize)
        {
            this.windowSize = windowSize;
        }

        public Matrix Transform { get; private set; }

        public void Follow(Entity target, Map map)
        {
            var position = Matrix.CreateTranslation(
                -target.Position.X - target.HitBox.Width ,
                -target.Position.Y - target.HitBox.Height,
                0);

            var offset = Matrix.CreateTranslation(
                                windowSize.X / 2,
                                windowSize.Y / 2,
                                0);

            var scale = Matrix.CreateScale(3f, 3f, 0);

            Transform = position * scale * offset;
        }
    }
}
