﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead.GameCore.Models.StateMachine
{
    public class HealingStatus : PlayerState
    {
        public HealingStatus(Player player, ChosenUndead.StateMachine stateMachine) : base(player, stateMachine)
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
            player.AnimationManager.SetAnimation(EntityAction.Healing);
            player.Velocity.X = 0;
            speed = 1;
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
        }
    }
}
