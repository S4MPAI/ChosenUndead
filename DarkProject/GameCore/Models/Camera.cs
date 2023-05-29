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

        public Point VisionWindowSize { get => new Point((int)(windowSize.X / Scale) - 1, (int)(windowSize.Y / Scale)); }

        public Vector2 WindowPos { get; private set; }

        public readonly float Scale;

        public Matrix Transform { get; private set; }

        public Camera(Point windowSize, float scale)
        {
            this.windowSize = windowSize;
            Scale = scale;
        }

        public void Follow(Entity target, Map map)
        {
            var dx = MathHelper.Clamp(target.Center.X, VisionWindowSize.X / 2, map.MapSize.X - VisionWindowSize.X / 2);
            var dy = MathHelper.Clamp(target.Center.Y, VisionWindowSize.Y / 2, map.MapSize.Y - VisionWindowSize.Y / 2);

            WindowPos = new(dx - VisionWindowSize.X / 2, dy - VisionWindowSize.Y / 2);

            var position = Matrix.CreateTranslation(
                -dx,
                -dy,
                0);

            var offset = Matrix.CreateTranslation(
                                windowSize.X / 2,
                                windowSize.Y / 2,
                                0);

            var scale = Matrix.CreateScale(Scale, Scale, 0);

            Transform = position * scale * offset;
        }
    }
}
