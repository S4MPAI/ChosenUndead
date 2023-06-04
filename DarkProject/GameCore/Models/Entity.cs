using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
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
        public Vector2 TextureSize { get; }

        public Vector2 CenterPos { get => Position + TextureSize / 2; }

        public Vector2 Velocity;

        public Rectangle HitBox
        {
            get => new Rectangle(
            (int)(CenterPos.X - hitBoxWidth / 2),
            (int)Position.Y,
            hitBoxWidth,
            (int)TextureSize.Y);
        }

        public Rectangle AttackBox
        {
            get => new Rectangle(
            (int)(CenterPos.X - (int)Orientation * attackWidth),
            (int)Position.Y,
            attackWidth,
            (int)TextureSize.Y);
        }

        public bool IsImmune;

        public bool IsDead => Hp == 0;

        public float AttackRegTimeLeft { get => Weapon.attackRegTimeLeft; }

        public Weapon Weapon { get;protected set; }

        public virtual float Damage { get => Weapon.Damage; }

        public bool IsAttacking => Weapon.IsDamaged;

        public AnimationManager<object> AnimationManager { get; private set; }

        public SpriteEffects Orientation;

        protected Map map;

        public abstract float MaxHp { get; }

        public float Hp { get; protected set; }

        public abstract float WalkSpeed { get; }

        public abstract float walkSpeedAttackCoef { get; }

        public bool IsOnGround { get; private set; } = true;

        public bool IsUnderTop { get; private set; } = false;

        protected const float GravityAcceleration = 1500.0f;

        protected const float MaxFallSpeed = 275.0f;

        protected int hitBoxWidth { get; }

        protected int attackWidth { get; }

        public Entity(Map map, AnimationManager<object> animationManager, int hitBoxWidth, Weapon weapon = null, int attackWidth = 0)
        {
            this.hitBoxWidth = hitBoxWidth;
            this.attackWidth = attackWidth;

            this.map = map;
            Weapon = weapon ?? new Weapon(0, 0, 0, new Attacks[] {});
            this.AnimationManager = animationManager;
            Hp = MaxHp;
            TextureSize = new Vector2(this.AnimationManager.CurrentAnimation.FrameWidth, this.AnimationManager.CurrentAnimation.FrameHeight);

            foreach (var attack in Weapon.WeaponAttacks)
                animationManager.ChangeFrameTime(attack, Weapon.attackCooldown);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            AnimationManager.Draw(Position, spriteBatch, Orientation);
        }

        public float SetGravity(float velocityY) => MathHelper.Clamp(velocityY + GravityAcceleration * Time.ElapsedSeconds, -MaxFallSpeed, MaxFallSpeed);

        public virtual void AddHp(float hpSize)
        {
            Hp += hpSize;

            if (Hp <= 0)
            {
                Hp = 0;
                AnimationManager.SetAnimation(EntityAction.Death);
            }
                
            else if (Hp > MaxHp)
                Hp = MaxHp;
        }

        #region Collision

        public virtual bool IsDeadFull() => Hp == 0 && AnimationManager.IsCurrentAnimationEnded();

        public virtual Vector2 CollisionWithMap(Vector2 velocity)
        {
            velocity *= Time.ElapsedSeconds;
            var leftTile = (int)Math.Floor((float)HitBox.Left / map.TileSize) - 1;
            var rightTile = (int)Math.Ceiling((float)HitBox.Right / map.TileSize);
            var topTile = (int)Math.Floor((float)HitBox.Top / map.TileSize);
            var bottomTile = (int)Math.Ceiling((float)HitBox.Bottom / map.TileSize);

            IsOnGround = false;
            IsUnderTop = false;

            for (int y = topTile; y <= bottomTile; y++)
            {
                for (int x = leftTile; x <= rightTile; x++)
                {
                    if (map.IsHaveCollision(x, y))
                    {
                        var bounds = map.GetBounds(x, y);

                        if (velocity.X > 0 && IsTouchingLeft(bounds, velocity.X) || velocity.X < 0 && IsTouchingRight(bounds, velocity.X))
                            velocity.X = 0;

                        if (velocity.Y > 0 && IsTouchingTop(bounds, velocity.Y))
                        {
                            velocity.Y = 0;
                            IsOnGround = true;
                        }

                        if (velocity.Y < 0 && IsTouchingBottom(bounds, velocity.Y))
                        {
                            velocity.Y = 0;
                            IsUnderTop = true;
                        }

                    }
                }
            }

            return velocity / Time.ElapsedSeconds;
        }

        protected bool IsTouchingLeft(Rectangle bounds, float velocityX) =>
            HitBox.Right + velocityX >= bounds.Left &&
            HitBox.Left < bounds.Left &&
            HitBox.Bottom > bounds.Top + bounds.Height / 5 &&
            HitBox.Top < bounds.Bottom - bounds.Height / 5;

        protected bool IsTouchingRight(Rectangle bounds, float velocityX) =>
            HitBox.Left + velocityX <= bounds.Right &&
            HitBox.Right > bounds.Right &&
            HitBox.Bottom > bounds.Top + bounds.Height / 5 &&
            HitBox.Top < bounds.Bottom - bounds.Height / 5;

        protected bool IsTouchingBottom(Rectangle bounds, float velocityY) =>
            HitBox.Top + velocityY <= bounds.Bottom &&
            HitBox.Bottom > bounds.Bottom &&
            HitBox.Right > bounds.Left + bounds.Width / 5 &&
            HitBox.Left < bounds.Right - bounds.Width / 5;

        protected bool IsTouchingTop(Rectangle bounds, float velocityY) =>
            HitBox.Bottom + velocityY >= bounds.Top &&
            HitBox.Top < bounds.Top &&
            HitBox.Right > bounds.Left + bounds.Width / 5 &&
            HitBox.Left < bounds.Right - bounds.Width / 5;

        //protected Rectangle GetIntersectionDepthAttackWithHitBox(Entity entity) => Rectangle.Intersect(entity.HitBox, HitBox);

        #endregion
    }
}
