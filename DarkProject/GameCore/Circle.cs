using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChosenUndead;

public struct Circle
{
    public Vector2 Center;

    public readonly int Radius;

    public int Left => (int)Center.X - Radius;

    public int Right => (int)Center.X + Radius;

    public int Top => (int)Center.Y - Radius;

    public int Bottom => (int)Center.Y + Radius;

    public Circle(Vector2 position, int radius)
    {
        Center = position;
        Radius = radius;
    }

    public bool Intersects(Rectangle rectangle)
    {
        Vector2 v = new Vector2(MathHelper.Clamp(Center.X, rectangle.Left, rectangle.Right),
            MathHelper.Clamp(Center.Y, rectangle.Top, rectangle.Bottom));

        Vector2 direction = Center - v;
        float distanceSquared = direction.LengthSquared();

        return distanceSquared > 0 && distanceSquared < Radius * Radius;
    }

    public bool Intersects(Circle circle)
    {
        var radius = Radius + circle.Radius;

        return Vector2.DistanceSquared(Center, circle.Center) < radius * radius;
    }

    public Vector2 GetIntersectionDepth(Rectangle rectangle)
    {
        if (!Intersects(rectangle)) return Vector2.Zero;

        var distance = Center - rectangle.Center.ToVector2();
        var minDistance = new Vector2(Radius + rectangle.Width, Radius + rectangle.Height);

        distance.X = (distance.X > 0 ? minDistance.X : -minDistance.X) - distance.X;
        distance.Y = (distance.Y > 0 ? minDistance.Y : -minDistance.Y) - distance.Y;

        return distance;
    }
}