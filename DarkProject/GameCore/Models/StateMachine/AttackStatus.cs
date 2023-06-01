using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class AttackStatus : Status
    {
        private bool isAttack; 

        public AttackStatus(Player player, StateMachine stateMachine) : base(player, stateMachine)
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
            speed = player.WalkSpeed * player.walkSpeedAttackCoef; 
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
            isAttack = Input.AttackPressed;
        }

        public override void LogicUpdate()
        {
            if (!player.Weapon.IsAttack())
                stateMachine.ChangeState(player.WalkingStatus);
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            velocity = SetGravityAndCollision(velocity);
            player.Velocity = velocity;
            player.Position += velocity * Time.ElapsedSeconds;
            player.Weapon.Update(isAttack);
            player.AnimationManager.SetAnimation(player.Weapon.CurrentAttack);
        }
    }
}
