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
    public enum EntityAction
    {
        Idle,
        Run,
        Jump,
        Death
    }

    public abstract class Entity : Component
    {
        public Vector2 Center { get; }

        public Vector2 Velocity;

        public Rectangle HitBox
        {
            get => new Rectangle(
            (int)(Position.X + Center.X - hitBoxWidth / 2),
            (int)Position.Y,
            hitBoxWidth,
            (int)(Center.Y * 2));
        }

        public Rectangle AttackBox
        {
            get => new Rectangle(
            (int)(Position.X + Center.X - (int)orientation * attackWidth),
            (int)Position.Y,
            attackWidth,
            (int)(Center.Y * 2));
        }

        public bool IsAttacking { get => weapon.IsAttacking(); }

        protected Weapon weapon { get; set; }

        public float Damage { get => weapon.Damage; }

        protected AnimationManager<object> animationManager;

        protected SpriteEffects orientation;

        protected EntityAction state;

        protected Level map;

        protected abstract float MaxHp { get; }

        protected float Hp { get; set; }

        protected abstract float walkSpeed { get;}

        protected float gravitySpeed { get; } = 15f;

        protected abstract float jumpSpeed { get; }

        protected float elapsedTime;

        protected bool hasJumped = true;

        protected int hitBoxWidth { get; }

        protected int attackWidth { get; }

        public Entity(Level map, Weapon weapon, AnimationManager<object> animationManager, int hitBoxWidth, int attackWidth = 0) : this(hitBoxWidth, attackWidth)
        {
            this.map = map;
            this.weapon = weapon;
            this.animationManager = animationManager;
            Hp = MaxHp;
            Center = new Vector2(this.animationManager.CurrentAnimation.FrameWidth / 2, this.animationManager.CurrentAnimation.FrameHeight / 2);

            foreach (var attack in weapon.WeaponAttacks)
                animationManager.ChangeFrameTime(attack, weapon.attackCooldown);
        }

        private Entity(int hitBoxWidth, int attackWidth)
        {
            this.hitBoxWidth = hitBoxWidth;
            this.attackWidth = attackWidth;
        }

        public override void Update(GameTime gameTime)
        {
            CollisionWithMap();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationManager.Draw(Position, spriteBatch, orientation);
        }

        public abstract void GiveDamage(float damage);

        public abstract bool IsDead();

        #region Collision

        protected virtual void CollisionWithMap()
        {
            var leftTile = (int)Math.Floor((float)HitBox.Left / map.TileSize);
            var rightTile = (int)Math.Ceiling((float)HitBox.Right / map.TileSize) - 1;
            var topTile = (int)Math.Floor((float)HitBox.Top / map.TileSize);
            var bottomTile = (int)Math.Ceiling((float)HitBox.Bottom / map.TileSize) - 1;

            for (int y = topTile; y <= bottomTile; y++)
            {
                for (int x = leftTile; x <= rightTile; x++)
                {
                    if (map.IsHaveCollision(x, y))
                    {
                        var bounds = map.GetBounds(x, y);

                        if ((IsTouchingLeft(bounds) && Velocity.X > 0) || (IsTouchingRight(bounds) && Velocity.X < 0))
                            Velocity.X = 0;

                        if (IsTouchingTop(bounds) && Velocity.Y > 0)
                        {
                            if (HitBox.Bottom > bounds.Top + bounds.Height / 5)
                                Position.Y -= Velocity.Y;

                            Velocity.Y = 0;
                            hasJumped = false;
                        }

                        if (IsTouchingBottom(bounds) && Velocity.Y < 0)
                        {
                            Velocity.Y = 0;
                            hasJumped = true;
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

        #endregion
    }
}
