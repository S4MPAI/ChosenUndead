using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChosenUndead
{
    public abstract class Entity : Component
    {
        protected abstract float walkSpeed { get; init; }

        protected abstract float gravitySpeed { get; init; }

        protected abstract float jumpSpeed { get; init; }

        protected float _elapsedTime;

        public Vector2 Velocity;

        public Rectangle HitBox
        {
            get => new Rectangle(
            (int)(Position.X + Center.X - _hitBoxWidth / 2),
            (int)Position.Y,
            _hitBoxWidth,
            (int)(Center.Y * 2));
        }

        public Rectangle AttackBox
        {
            get => new Rectangle(
            (int)(Position.X + Center.X - _attackWidth / 2),
            (int)Position.Y,
            _attackWidth,
            (int)(Center.Y * 2));
        }

        protected AnimationManager _animationManager;

        protected SpriteEffects _orientation;

        protected Map _map;

        public Vector2 Center { get; }

        protected bool _hasJumped = true;

        protected int _hitBoxWidth { get; }

        protected int _attackWidth { get; }

        public Entity(Map map, Texture2D texture, int hitBoxWidth, int attackWidth = 0) : this(hitBoxWidth, attackWidth)
        {
            _map = map;
            _texture = texture;
            Center = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public Entity(Map map, AnimationManager animationManager, int hitBoxWidth, int attackWidth = 0) : this(hitBoxWidth, attackWidth)
        {
            _map = map;
            _animationManager = animationManager;
            Center = new Vector2(_animationManager.CurrentAnimation.FrameWidth / 2, _animationManager.CurrentAnimation.FrameHeight / 2);
        }

        private Entity(int hitBoxWidth, int attackWidth)
        {
            _hitBoxWidth = hitBoxWidth;
            _attackWidth = attackWidth;
        }

        protected virtual void CollisionWithMap()
        {
            var leftTile = (int)Math.Floor((float)HitBox.Left / _map.TileSize);
            var rightTile = (int)Math.Ceiling((float)HitBox.Right / _map.TileSize) - 1;
            var topTile = (int)Math.Floor((float)HitBox.Top / _map.TileSize);
            var bottomTile = (int)Math.Ceiling((float)HitBox.Bottom / _map.TileSize) - 1;

            for (int y = topTile; y <= bottomTile; y++)
            {
                for (int x = leftTile; x <= rightTile; x++)
                {
                    if (_map.IsHaveCollision(x, y))
                    {
                        var bounds = _map.GetBounds(x, y);

                        if ((IsTouchingLeft(bounds) && Velocity.X > 0) || (IsTouchingRight(bounds) && Velocity.X < 0))
                            Velocity.X = 0;

                        if (IsTouchingTop(bounds) && Velocity.Y > 0)
                        {
                            if (HitBox.Bottom > bounds.Top + bounds.Height / 5)
                                Position.Y -= Velocity.Y;

                            Velocity.Y = 0;
                            _hasJumped = false;
                        }

                        if (IsTouchingBottom(bounds) && Velocity.Y < 0)
                        {
                            Velocity.Y = 0;
                            _hasJumped = true;
                        }

                    }
                }
            }
        }

        protected bool IsTouchingLeft(Rectangle bounds) => 
            HitBox.Right + Velocity.X > bounds.Left && 
            HitBox.Left < bounds.Left && 
            HitBox.Bottom > bounds.Top + bounds.Height / 5 && 
            HitBox.Top < bounds.Bottom - bounds.Height / 5;

        protected bool IsTouchingRight(Rectangle bounds) => 
            HitBox.Left + Velocity.X < bounds.Right &&
            HitBox.Right > bounds.Right &&
            HitBox.Bottom > bounds.Top + bounds.Height / 5 &&
            HitBox.Top < bounds.Bottom - bounds.Height / 5;

        protected bool IsTouchingBottom(Rectangle bounds) => 
            HitBox.Top + Velocity.Y < bounds.Bottom &&
            HitBox.Bottom > bounds.Bottom &&
            HitBox.Right > bounds.Left + bounds.Width / 5 &&
            HitBox.Left < bounds.Right - bounds.Width / 5;

        protected bool IsTouchingTop(Rectangle bounds) =>
            HitBox.Bottom + Velocity.Y > bounds.Top &&
            HitBox.Top < bounds.Top &&
            HitBox.Right > bounds.Left + bounds.Width / 5 &&
            HitBox.Left < bounds.Right - bounds.Width / 5;
    }
}
