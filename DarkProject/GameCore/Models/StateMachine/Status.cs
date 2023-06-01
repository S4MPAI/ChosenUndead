using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public abstract class Status
    {
        protected Player player;

        protected StateMachine stateMachine;

        protected float speed;

        protected Vector2 velocity;

        public static float rollingCoolDownLeft;

        protected Status(Player player, StateMachine stateMachine)
        {
            this.player = player;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            
        }

        public void Update()
        {
            HandleInput();
            PhysicsUpdate();
            DisplayUpdate();
            LogicUpdate();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            player.AnimationManager.Draw(player.Position, spriteBatch, player.Orientation);
        }

        public virtual void HandleInput()
        {
            velocity.X = 0;
            if (Input.LeftPressed)
                velocity.X = -speed;
            if (Input.RightPressed)
                velocity.X = speed;
        }

        public virtual void PhysicsUpdate()
        {
            rollingCoolDownLeft -= Time.ElapsedSeconds;
        }

        public virtual void LogicUpdate()
        {
            if (player.IsDead && this != player.DeathStatus)
                stateMachine.ChangeState(player.DeathStatus);
        }

        public virtual void DisplayUpdate()
        {
            player.Orientation = velocity.X != 0 ? (velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally) : player.Orientation;
        }

        protected Vector2 SetGravityAndCollision(Vector2 velocity)
        {
            velocity.Y = player.SetGravity(velocity.Y);
            velocity = player.CollisionWithMap(velocity);

            return velocity;
        }

        public virtual void Exit()
        {

        }
    }
}
