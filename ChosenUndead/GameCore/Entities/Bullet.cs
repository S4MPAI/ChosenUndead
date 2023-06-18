using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChosenUndead
{
    public class Bullet : Sprite
    {
        private Animation bulletAnim;

        public Vector2 Velocity;

        private int Width;

        private int Height;

        public float FlightTime { get; private set; }

        public float Damage { get; }
        public float Speed { get; }

        public new Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        public Bullet(Texture2D texture, float damage, float speed) : base(texture, 1)
        {
            Damage = damage;
            Speed = speed;
            Width = texture.Width;
            Height = texture.Height;
        }

        public Bullet(Animation anim, float damage, float speed) : base(null, 1)
        {
            bulletAnim = anim;
            Damage = damage;
            Speed = speed;
            Width = bulletAnim.FrameWidth;
            Height = bulletAnim.FrameHeight;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var orientation = (Velocity.X >= 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (texture != null)
                spriteBatch.Draw(texture, Position, color);
            else if (bulletAnim != null)
                bulletAnim.Draw(spriteBatch, Position, orientation);
        }

        public override void Update()
        {
            FlightTime += Time.ElapsedSeconds;
            Position += Velocity * Speed * Time.ElapsedSeconds;
            bulletAnim.Update();
        }
    }
}
