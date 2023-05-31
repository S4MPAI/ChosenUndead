using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class JumpingStatus : Status
    {
        private bool isJumping;

        private bool wasJumping;

        private float jumpTime;

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
            player.AnimationManager.Draw(player.Position, spriteBatch, player.Orientation);
        }

        public override void Enter()
        {
            base.Enter();
            speed = player.walkSpeed;
            player.AnimationManager.SetAnimation(EntityAction.Jump);
            isJumping = true;
        }

        public override void Exit()
        {
            base.Exit();
            velocity = new Vector2();
            isJumping = false;
            wasJumping = false;
            jumpTime = 0f;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            isJumping = Input.JumpPressed;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (player.IsOnGround && velocity.Y >= 0)
                stateMachine.ChangeState(player.WalkingStatus);
        }

        public override void PhysicsUpdate()
        {
            velocity.Y = player.SetGravity(velocity.Y);
            velocity.Y = DoJump(velocity.Y);
            velocity = player.CollisionWithMap(velocity);
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
