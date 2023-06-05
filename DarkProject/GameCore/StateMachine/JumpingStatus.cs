using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace ChosenUndead
{
    public class JumpingStatus : PlayerState
    {
        private bool isJumping;

        private bool wasJumping;

        private float jumpTime;

        private SoundEffectInstance jumpSound = Sound.GetPlayerSound("Jump").CreateInstance();

        private SoundEffectInstance landingSound = Sound.GetPlayerSound("Landing").CreateInstance();

        public JumpingStatus(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void DisplayUpdate()
        {
            base.DisplayUpdate();
            player.AnimationManager.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Enter()
        {
            base.Enter();
            speed = player.WalkSpeed;
            player.AnimationManager.SetAnimation(EntityAction.Jump);
            isJumping = true;
            if (Input.JumpPressed && player.Stamina >= Player.JumpStaminaCost)
            {
                jumpSound.Play();
                player.Stamina -= Player.JumpStaminaCost;
            }
        }

        public override void Exit()
        {
            base.Exit();
            velocity = new Vector2();
            isJumping = false;
            wasJumping = false;
            jumpTime = 0f;
            landingSound.Play();
        }

        public override void HandleInput()
        {
            base.HandleInput();
            isJumping = Input.JumpPressed;
        }

        public override void LogicUpdate()
        {
            if (player.IsOnGround && velocity.Y >= 0)
                stateMachine.ChangeState(player.WalkingStatus);
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            velocity.Y = player.SetGravity(velocity.Y);
            velocity.Y = DoJump(velocity.Y);
            velocity = player.CollisionWithMap(velocity);
            player.Velocity = velocity;
            player.Position += velocity * Time.ElapsedSeconds;
        }

        private float DoJump(float velocityY)
        {
            if (isJumping && !player.IsUnderTop)
            {
                if (player.IsOnGround && !wasJumping || jumpTime > 0.0f)
                    jumpTime += Time.ElapsedSeconds;

                if (0.0f < jumpTime && jumpTime <= Player.MaxJumpTime)
                    velocityY = Player.JumpLaunchVelocity * (1.0f - (float)Math.Pow(jumpTime / Player.MaxJumpTime, Player.JumpControlPower));
                else
                    jumpTime = 0.0f;
            }
            else
                jumpTime = 0.0f;

            wasJumping = isJumping;

            return velocityY;
        }
    }
}
