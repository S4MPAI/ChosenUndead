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
        public Vector2 TextureCenter { get; }

        protected Vector2 velocity;

        public Rectangle HitBox
        {
            get => new Rectangle(
            (int)(Position.X + TextureCenter.X - hitBoxWidth / 2),
            (int)Position.Y,
            hitBoxWidth,
            (int)(TextureCenter.Y * 2));
        }

        public Rectangle AttackBox
        {
            get => new Rectangle(
            (int)(Position.X + TextureCenter.X - (int)orientation * attackWidth),
            (int)Position.Y,
            attackWidth,
            (int)(TextureCenter.Y * 2));
        }

        public bool IsAttacking { get => weapon.IsAttacking(); }

        protected Weapon weapon { get; set; }

        public float Damage { get => weapon.Damage; }

        protected AnimationManager<object> animationManager;

        protected SpriteEffects orientation;

        protected EntityAction state;

        protected Map map;

        protected abstract float MaxHp { get; }

        protected float Hp { get; set; }

        protected abstract float walkSpeed { get;}

        protected float elapsedTime;

        protected bool isOnGround = true;

        protected int hitBoxWidth { get; }

        protected int attackWidth { get; }

        public Entity(Map map, Weapon weapon, AnimationManager<object> animationManager, int hitBoxWidth, int attackWidth = 0) : this(hitBoxWidth, attackWidth)
        {
            this.map = map;
            this.weapon = weapon;
            this.animationManager = animationManager;
            Hp = MaxHp;
            TextureCenter = new Vector2(this.animationManager.CurrentAnimation.FrameWidth / 2, this.animationManager.CurrentAnimation.FrameHeight / 2);

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
            var velocity = this.velocity * elapsedTime;
            isOnGround = false;
            var leftTile = (int)Math.Floor((float)HitBox.Left / map.TileSize);
            var rightTile = (int)Math.Ceiling((float)HitBox.Right / map.TileSize) - 1;
            var topTile = (int)Math.Floor((float)HitBox.Top / map.TileSize);
            var bottomTile = (int)Math.Ceiling((float)HitBox.Bottom / map.TileSize);

            for (int y = topTile; y <= bottomTile; y++)
            {
                for (int x = leftTile; x <= rightTile; x++)
                {
                    if (map.IsHaveCollision(x, y))
                    { 
                        var bounds = map.GetBounds(x, y);

                        if ((velocity.X > 0 && IsTouchingLeft(bounds, velocity.X)) || (velocity.X < 0 && IsTouchingRight(bounds, velocity.X)))
                            this.velocity.X = 0;

                        if (velocity.Y > 0 && IsTouchingTop(bounds, velocity.Y))
                        {
                            //if (HitBox.Bottom > bounds.Top + bounds.Height / 5)
                            //    Position.Y -= velocity.Y;

                            this.velocity.Y = 0;
                            isOnGround = true;
                        }

                        if (velocity.Y < 0 && IsTouchingBottom(bounds, velocity.Y))
                        {
                            this.velocity.Y = 0;
                        }

                    }
                }
            }
        }

        protected bool IsTouchingLeft(Rectangle bounds, float velocityX) => 
            HitBox.Right + velocityX >= bounds.Left && 
            HitBox.Left < bounds.Left && 
            HitBox.Bottom > (bounds.Top + bounds.Height / 5) && 
            HitBox.Top < (bounds.Bottom - bounds.Height / 5);

        protected bool IsTouchingRight(Rectangle bounds, float velocityX) => 
            HitBox.Left + velocityX <= bounds.Right &&
            HitBox.Right > bounds.Right &&
            HitBox.Bottom > (bounds.Top + bounds.Height / 5) &&
            HitBox.Top < (bounds.Bottom - bounds.Height / 5);

        protected bool IsTouchingBottom(Rectangle bounds, float velocityY) => 
            HitBox.Top + velocityY <= bounds.Bottom &&
            HitBox.Bottom > bounds.Bottom &&
            HitBox.Right > (bounds.Left + bounds.Width / 5) &&
            HitBox.Left < (bounds.Right - bounds.Width / 5);

        protected bool IsTouchingTop(Rectangle bounds, float velocityY) =>
            HitBox.Bottom + velocityY >= bounds.Top &&
            HitBox.Top < bounds.Top &&
            HitBox.Right > (bounds.Left + bounds.Width / 5) &&
            HitBox.Left < (bounds.Right - bounds.Width / 5);

        #endregion
    }
}
