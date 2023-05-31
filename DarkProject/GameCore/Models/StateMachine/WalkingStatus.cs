using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class WalkingStatus : Status
    {
        private bool isJumping;

        public WalkingStatus(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            player.AnimationManager.Draw(player.Position, spriteBatch, player.Orientation);
        }

        public override void Enter()
        {
            base.Enter();
            speed = player.walkSpeed;
            player.AnimationManager.SetAnimation(EntityAction.Idle);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
            isJumping = Input.JumpPressed;
        }

        public override void LogicUpdate()
        {
            if (!player.IsOnGround || isJumping)
                stateMachine.ChangeState(player.JumpingStatus);
        }

        public override void PhysicsUpdate()
        {
            velocity = SetGravityAndCollision(velocity);
            player.Position += velocity * Time.ElapsedSeconds;
        }

        public override void DisplayUpdate()
        {
            base.DisplayUpdate();
            if (velocity.X != 0)
                player.AnimationManager.SetAnimation(EntityAction.Run);
            else
                player.AnimationManager.SetAnimation(EntityAction.Idle);

            player.AnimationManager.Update();
        }
    }
}
