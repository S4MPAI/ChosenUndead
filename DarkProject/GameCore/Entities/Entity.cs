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
        public float Speed = 1f;

        public Vector2 Velocity;

        public Rectangle HitBox 
        { 
            get => new Rectangle(
            (int)(Position.X + _center.X - _hitBoxWidth / 2),
            (int)Position.Y, 
            _hitBoxWidth,
            (int)(_center.Y * 2)); 
        }

        public Rectangle AttackBox
        {
            get => new Rectangle(
            (int)(Position.X + _center.X - _attackWidth / 2),
            (int)Position.Y, 
            _attackWidth,
            (int)(_center.Y * 2));
        }

        protected AnimationManager _animationManager;

        protected SpriteEffects _orientation;

        protected Map _map;

        protected float _previousBottom;

        protected bool isOnGround;
        
        protected readonly Vector2 _center;

        protected readonly int _height;

        protected bool _hasJumped = true;

        protected int _hitBoxWidth { get; }

        protected int _attackWidth { get; }

        public Entity(Map map, Texture2D texture, int hitBoxWidth, int attackWidth = 0) : this(hitBoxWidth, attackWidth)
        {
            _map = map;
            _texture = texture;
            _center = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public Entity(Map map, AnimationManager animationManager, int hitBoxWidth, int attackWidth = 0) : this(hitBoxWidth, attackWidth)
        {
            _map = map;
            _animationManager = animationManager;
            _center = new Vector2(_animationManager.CurrentAnimation.FrameWidth / 2, _animationManager.CurrentAnimation.FrameHeight / 2);
        }

        private Entity(int hitBoxWidth, int attackWidth)
        {
            _hitBoxWidth = hitBoxWidth;
            _attackWidth = attackWidth;
        }

        protected virtual void Collision()
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

                        var intersection = Rectangle.Intersect(HitBox, bounds);

                        if (intersection.IsEmpty) continue;

                        if (HitBox.Left == intersection.Left && HitBox.Bottom > bounds.Top + bounds.Height / 5 && Velocity.X < 0)
                            Velocity.X = 0;

                        if (HitBox.Right == intersection.Right && HitBox.Bottom > bounds.Top + bounds.Height / 5 && Velocity.X > 0)
                            Velocity.X = 0;
                        
                        if (HitBox.Bottom == intersection.Bottom && Velocity.Y > 0)
                        {
                            Velocity.Y = 0;
                            _hasJumped = false;
                        }

                        if (HitBox.Top == intersection.Top && Velocity.Y < 0)
                        {
                            Velocity.Y = 0;
                            _hasJumped = true;
                        }
                            
                    }
                }
            }

            _previousBottom = HitBox.Bottom;
        }

        //protected bool IsTouchingLeft(Rectangle r)
        //{
        //    var intersection = Rectangle.Intersect(HitBox, r);
        //    if (intersection.IsEmpty) return false;
        //    return HitBox.Left == intersection.Left;
        //}

        //protected bool IsTouchingRight(Rectangle r)
        //{

        //}

        //protected bool IsTouchingTop(Rectangle r) =>
        //    HitBox.Bottom >= r.Top - 1 &&
        //    HitBox.Bottom <= r.Top + (r.Height / 2) &&
        //    HitBox.Right >= r.Left + r.Width / 5 &&
        //    HitBox.Left <= r.Right - r.Width / 5;

        //protected bool IsTouchingBottom(Rectangle r) =>
        //    HitBox.Top <= r.Bottom + (r.Height / 5) &&
        //    HitBox.Top >= r.Bottom - 1 &&
        //    HitBox.Right >= r.Left + r.Width / 5 &&
        //    HitBox.Left <= r.Right - r.Width / 5;
    }
}
