using Microsoft.Xna.Framework.Audio;
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

        private Attacks lastAttack;

        private SoundEffect attackSound = Sound.GetPlayerSound("Attack");

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
            attackSound.Play();
            speed = player.WalkSpeed * player.walkSpeedAttackCoef;
            player.Stamina -= Player.AttackStaminaCost;
        }

        public override void Exit()
        {
            base.Exit();
            player.Weapon.SetNoneAttack();
        }

        public override void HandleInput()
        {
            if (player.Weapon.CurrentAttack != lastAttack)
                base.HandleInput();
            isAttack = Input.AttackPressed;
            lastAttack = player.Weapon.CurrentAttack;
            player.Weapon.Update(isAttack);
        }

        public override void LogicUpdate()
        {
            if (!player.Weapon.IsAttack() ||
                (lastAttack != player.Weapon.CurrentAttack && player.Stamina - Player.AttackStaminaCost < Player.AttackStaminaCost))
                stateMachine.ChangeState(player.WalkingStatus);

            if (lastAttack != player.Weapon.CurrentAttack && player.Weapon.IsAttack() && (player.Stamina - Player.AttackStaminaCost) > Player.AttackStaminaCost)
            {
                player.Stamina -= Player.AttackStaminaCost;
                attackSound.Play();
            }
                
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            velocity = SetGravityAndCollision(velocity);

            player.Velocity = velocity;
            player.Position += velocity * Time.ElapsedSeconds;



            player.AnimationManager.SetAnimation(player.Weapon.CurrentAttack);
        }
    }
}
