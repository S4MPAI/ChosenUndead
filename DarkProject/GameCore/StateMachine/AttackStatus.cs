using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class AttackStatus : PlayerState
    {
        private bool isAttack;

        private WeaponAttacks lastAttack;

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
            player.Stamina -= Player.AttackStaminaCost;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            if (player.Weapon.CurrentAttack != lastAttack)
                base.HandleInput();
            isAttack = Input.AttackPressed;
        }

        public override void LogicUpdate()
        {
            if (!player.Weapon.IsAttack() ||
                lastAttack != player.Weapon.CurrentAttack && player.Stamina - Player.AttackStaminaCost < Player.AttackStaminaCost)
                stateMachine.ChangeState(player.WalkingStatus);

            if (lastAttack != player.Weapon.CurrentAttack && player.Stamina - Player.AttackStaminaCost > Player.AttackStaminaCost)
                player.Stamina -= Player.AttackStaminaCost;
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            velocity = SetGravityAndCollision(velocity);
            lastAttack = player.Weapon.CurrentAttack;
            player.Weapon.Update(isAttack);

            player.Velocity = velocity;
            player.Position += velocity * Time.ElapsedSeconds;



            player.AnimationManager.SetAnimation(player.Weapon.CurrentAttack);
        }
    }
}
