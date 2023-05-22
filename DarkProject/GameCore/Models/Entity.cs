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
        public Vector2 TextureSize { get; }

        public Vector2 Center { get => Position + TextureSize / 2; }

        public Vector2 Velocity;

        public Rectangle HitBox
        {
            get => new Rectangle(
            (int)(Center.X - hitBoxWidth / 2),
            (int)Position.Y,
            hitBoxWidth,
            (int)TextureSize.Y);
        }

        public Rectangle AttackBox
        {
            get => new Rectangle(
            (int)(Center.X - (int)orientation * attackWidth),
            (int)Position.Y,
            attackWidth,
            (int)TextureSize.Y);
        }

        public bool IsAttacking { get => weapon.IsAttacking(); }

        protected Weapon weapon { get; set; }

        public float Damage { get => weapon.Damage; }

        protected AnimationManager<object> animationManager;

        protected SpriteEffects orientation;

        protected EntityAction state;

        protected Map map;

        protected abstract float maxHp { get; }

        protected float Hp { get; set; }

        protected abstract float walkSpeed { get; }

        protected float elapsedTime;

        protected bool isOnGround = true;

        protected bool isUnderTop = false;

        protected int hitBoxWidth { get; }

        protected int attackWidth { get; }

        public Entity(Map map, AnimationManager<object> animationManager, int hitBoxWidth, Weapon weapon = null, int attackWidth = 0) : this(hitBoxWidth, attackWidth)
        {
            this.map = map;
            this.weapon = weapon ?? new Weapon(0, 0, 0, new WeaponAttack[] {});
            this.animationManager = animationManager;
            Hp = maxHp;
            TextureSize = new Vector2(this.animationManager.CurrentAnimation.FrameWidth, this.animationManager.CurrentAnimation.FrameHeight);

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

        public virtual void GiveDamage(float damage)
        {
            if (state == EntityAction.Death) return;

            Hp -= damage;

            if (Hp <= 0)
            {
                state = EntityAction.Death;
                animationManager.SetAnimation(state);
            }
            else
            {
            }
        }

        public virtual bool IsDead() => state == EntityAction.Death && animationManager.IsCurrentAnimationEnded();

        #region Collision

        protected virtual void CollisionWithMap()
        {
            var velocity = Velocity * elapsedTime;
            var leftTile = (int)Math.Floor((float)HitBox.Left / map.TileSize) - 1;
            var rightTile = (int)Math.Ceiling((float)HitBox.Right / map.TileSize);
            var topTile = (int)Math.Floor((float)HitBox.Top / map.TileSize);
            var bottomTile = (int)Math.Ceiling((float)HitBox.Bottom / map.TileSize);

            isOnGround = false;
            isUnderTop = false;

            for (int y = topTile; y <= bottomTile; y++)
            {
                for (int x = leftTile; x <= rightTile; x++)
                {
                    if (map.IsHaveCollision(x, y))
                    {
                        var bounds = map.GetBounds(x, y);

                        if (velocity.X > 0 && IsTouchingLeft(bounds, velocity.X) || velocity.X < 0 && IsTouchingRight(bounds, velocity.X))
                            Velocity.X = 0;

                        if (velocity.Y > 0 && IsTouchingTop(bounds, velocity.Y))
                        {
                            Velocity.Y = 0;
                            isOnGround = true;
                        }

                        if (velocity.Y < 0 && IsTouchingBottom(bounds, velocity.Y))
                        {
                            Velocity.Y = 0;
                            isUnderTop = true;
                        }

                    }
                }
            }
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

        #endregion
    }
}
