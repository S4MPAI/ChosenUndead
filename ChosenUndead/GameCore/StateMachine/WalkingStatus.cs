using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class WalkingStatus : PlayerState
    {
        private bool isJumping;
        private bool isAttack;
        private bool isRolling;
        private bool isHealing;
        private SoundEffectInstance walkSound = Sound.GetPlayerSound("Walk").CreateInstance();
        public WalkingStatus(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Enter()
        {
            base.Enter();
            speed = player.WalkSpeed;
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
            isAttack = Input.AttackPressed;
            isRolling = Input.RollingPressed;
            player.IsInteract = Input.InteractionPressed;
            isHealing = Input.HealingPressed;
        }

        public override void LogicUpdate()
        {
            if (!player.IsOnGround || isJumping && player.Stamina >= Player.JumpStaminaCost)
                stateMachine.ChangeState(player.JumpingStatus);
            if (isAttack && player.Weapon.CurrentAttack != Attacks.Stun && player.Stamina >= Player.AttackStaminaCost)
                stateMachine.ChangeState(player.AttackStatus);
            if (isRolling && velocity.X != 0 && player.Stamina >= Player.RollStaminaCost)
                stateMachine.ChangeState(player.RollingStatus);
            if (isHealing && player.HealingQuartzLeft != 0)
                stateMachine.ChangeState(player.HealingStatus);
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            player.Stamina += Player.StaminaRecovery * Time.ElapsedSeconds;
            velocity = SetGravityAndCollision(velocity);
            player.Velocity = velocity;
            player.Position += velocity * Time.ElapsedSeconds;
            player.Weapon.Update(false);
        }

        public override void DisplayUpdate()
        {
            base.DisplayUpdate();
            if (velocity.X != 0)
            {
                walkSound.Play();
                player.AnimationManager.SetAnimation(EntityAction.Run);
            }
            else
                player.AnimationManager.SetAnimation(EntityAction.Idle);

            player.AnimationManager.Update();
        }
    }
}
