using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public abstract class Enemy : Entity
    {
        public Entity Target { get; private set; }

        protected const float targetDistance = 450f;

        public Enemy(Map map, AnimationManager<object> animationManager, int hitBoxWidth, Weapon weapon, int attackWidth = 0, Entity target = null) : base(map, animationManager, hitBoxWidth, weapon, attackWidth)
        {
            Target = target ?? Player.GetInstance();
        }

        public override void Update()
        {
            if (Target.IsDead || Math.Abs(GetDistance()) > targetDistance)
                AnimationManager.SetAnimation(EntityAction.Idle);
            else if (state != EntityAction.Death)
            {
                //elapsedTime = Time.ElapsedSeconds;

                Velocity = Vector2.Zero;
                

                Weapon.Update(true);

                base.Update();
                LimitOnTerritory();
                //Position += Velocity * elapsedTime;
                Orientation = Velocity.X != 0 ? Velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally : Orientation;

                SetAnimation();
            }

            AnimationManager.Update();
        }

        private float GetDistance()
        {
            var right = Target.HitBox.Left - HitBox.Right;
            var left = Target.HitBox.Right - HitBox.Left;

            if (Math.Abs(right) < Math.Abs(left))
                return MathHelper.Clamp(right, 0, targetDistance);
            return MathHelper.Clamp(left, -targetDistance, 0);
        }

        public void ChangeTarget(Entity target)
        {
            Target = target;
        }

        protected virtual void SetAnimation()
        {
            if (Weapon.IsAttack())
                AnimationManager.SetAnimation(Weapon.CurrentAttack);
            else
            {
                if (!IsOnGround) state = EntityAction.Jump;
                else if (Velocity.X != 0 && IsOnGround) state = EntityAction.Run;
                else state = EntityAction.Idle;

                AnimationManager.SetAnimation(state);
            }
        }

        protected void LimitOnTerritory()
        {
            var tileY = (int)Math.Ceiling((float)HitBox.Bottom / map.TileSize);

            if ((!map.IsHaveCollision(HitBox.Right / map.TileSize, tileY) && Velocity.X > 0) || (!map.IsHaveCollision(HitBox.Left / map.TileSize, tileY) && Velocity.X < 0))
                Velocity.X = 0;
        }
    }
}
