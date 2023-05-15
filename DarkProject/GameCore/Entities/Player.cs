using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Player : Entity
    {
        protected float time;

        protected override float walkSpeed { get; } = 150f;

        private const float MaxJumpTime = 0.4f;

        private const float JumpLaunchVelocity = -400.0f;
        
        private const float GravityAcceleration = 2000.0f;
        
        private const float MaxFallSpeed = 400.0f;
        
        private const float JumpControlPower = 1.5f;

        private bool isJumping;
        private bool wasJumping;
        private float jumpTime;

        protected override float MaxHp => 50;

        public bool IsInteract { get; private set; }

        private static Player instance;

        private Player(Map map) : base(map, new Sword(), Art.GetPlayerAnimations(), 32, 32)
        {
        }

        public static Player GetInstance(Map map = null)
        {
            if (instance == null)
                instance = new Player(map);

            if (map != null)
                instance.map = map;

            return instance;
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            IsInteract = InputManager.InteractionPressed;
            isJumping = InputManager.JumpPressed;

            Move();
            base.Update(gameTime);

            weapon.Update(gameTime, isOnGround ? InputManager.AttackPressed : false);
            SetAnimation();

            animationManager.Update(gameTime);
            velocity = weapon.IsAttack() ? Vector2.Zero : velocity;

            //Position += weapon.CurrentAttack != WeaponAttack.None ? Vector2.Zero : Velocity;
            Position += velocity * elapsedTime;
            orientation = velocity.X != 0 ? (velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally) : orientation;
            velocity.X = 0;

        }

        private void Move()
        {
            velocity.Y = MathHelper.Clamp(velocity.Y + GravityAcceleration * elapsedTime, -MaxFallSpeed, MaxFallSpeed);
            velocity.Y = DoJump(velocity.Y);

            if (InputManager.LeftPressed)
                velocity.X = -walkSpeed;
            if (InputManager.RightPressed)
                velocity.X = walkSpeed;
        }

        private float DoJump(float velocityY)
        {
            if (isJumping)
            {
                if ((isOnGround && !wasJumping) || jumpTime > 0.0f)
                {
                    jumpTime += elapsedTime;
                }

                if (0.0f < jumpTime && jumpTime <= MaxJumpTime)
                {
                    velocityY = JumpLaunchVelocity * (1.0f - (float)Math.Pow(jumpTime / MaxJumpTime, JumpControlPower));
                }
                else
                {
                    jumpTime = 0.0f;
                }
            }
            else
            {
                jumpTime = 0.0f;
            }
            wasJumping = isJumping;

            return velocityY;
        }

        private void SetAnimation()
        {
            if (weapon.IsAttack())
                animationManager.SetAnimation(weapon.CurrentAttack);
            else
            {
                if (!isOnGround) state = EntityAction.Jump;
                else if (velocity.X != 0 && isOnGround) state = EntityAction.Run;
                else state = EntityAction.Idle;

                animationManager.SetAnimation(state);
            }
        }

        public override bool IsDead() => state == EntityAction.Death && animationManager.IsCurrentAnimationEnded();

        public override void GiveDamage(float damage)
        {
            throw new NotImplementedException();
        }
    }
}
