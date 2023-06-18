using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class HealingStatus : PlayerState
    {
        private SoundEffectInstance healingSound = Sound.GetPlayerSound("Healing").CreateInstance();

        public HealingStatus(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
            healingSound.Volume = 0.3f;
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
            player.AnimationManager.SetAnimation(EntityAction.Healing);
            player.Velocity.X = 0;
            player.HealingQuartzLeft--;
            speed = 1;
            healingSound.Play();
        }

        public override void Exit()
        {
            base.Exit();
            player.AddHp(Player.HealingSize);
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            if (player.AnimationManager.IsCurrentAnimationEnded())
                stateMachine.ChangeState(player.WalkingStatus);
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            player.Stamina += Player.StaminaRecovery * Time.ElapsedSeconds;
        }
    }
}
