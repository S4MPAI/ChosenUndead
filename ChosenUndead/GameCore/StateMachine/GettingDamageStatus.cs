using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class GettingDamageStatus : PlayerState
    {
        private SoundEffectInstance hurtSound = Sound.GetPlayerSound("Hurt").CreateInstance();
        public GettingDamageStatus(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
            hurtSound.Volume = 1;
        }

        public override void DisplayUpdate()
        {
            base.DisplayUpdate();
            player.AnimationManager.Update();
        }

        public override void Enter()
        {
            base.Enter();
            player.AnimationManager.SetAnimation(EntityAction.Hurt);
            hurtSound.Play();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            if (player.AnimationManager.IsCurrentAnimationEnded())
                stateMachine.ChangeState(player.WalkingStatus);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            velocity = SetGravityAndCollision(velocity);
            player.Position += velocity * Time.ElapsedSeconds;
            player.Velocity = velocity;
            player.Stamina += Player.StaminaRecovery * Time.ElapsedSeconds;
        }
    }
}
