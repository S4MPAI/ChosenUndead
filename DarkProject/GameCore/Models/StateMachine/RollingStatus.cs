using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead.GameCore.Models.StateMachine
{
    public class RollingStatus : Status
    {
        private float rollingTimeLeft;

        public RollingStatus(Player player, ChosenUndead.StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        

        public override void Enter()
        {
            base.Enter();
            speed = player.WalkSpeed * Player.RollSpeedCoef;
            velocity.X = player.Velocity.X > 0 ? speed : -speed;
            rollingTimeLeft = Player.MaxRollingTime;
            player.AnimationManager.SetAnimation(EntityAction.Roll);
            player.IsImmune = true;
        }

        public override void Exit()
        {
            base.Exit();
            rollingCoolDownLeft = Player.RollingCooldown;
        }

        public override void HandleInput()
        {
            
        }

        public override void LogicUpdate()
        {
            rollingTimeLeft -= Time.ElapsedSeconds;
            if (rollingTimeLeft <= 0)
            {
                stateMachine.ChangeState(player.WalkingStatus);
            }
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            velocity = SetGravityAndCollision(velocity);
            player.Velocity = velocity;
            player.Position += velocity * Time.ElapsedSeconds;

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
    }
}
