﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class GettingDamageStatus : PlayerState
    {
        public GettingDamageStatus(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
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
        }
    }
}
