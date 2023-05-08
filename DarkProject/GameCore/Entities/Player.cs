using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Player : Entity
    {
        private static Player instance;

        protected float time;

        protected override float walkSpeed { get; } = 150f;

        protected override float jumpSpeed { get; } = -250f;

        protected override float MaxHp => 50;

        private Player(Level map, AnimationManager<object> animationManager, int hitBoxWidth, int attackWidth) : 
            base(map, new Sword(),animationManager, hitBoxWidth, attackWidth)
        {
        }

        public static Player GetInstance(Level map, AnimationManager<object> animationManager, int hitBoxWidth, int attackWidth)
        {
            if (instance == null) 
                return instance = new Player(map, animationManager, hitBoxWidth, attackWidth);
                
            instance.map = map;
            return instance;
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            InputManager.Update(gameTime);
            
            Move();
            base.Update(gameTime);

            weapon.Update(gameTime, hasJumped ? false : InputManager.AttackPressed);

            if (weapon.IsAttack())
                animationManager.SetAnimation(weapon.CurrentAttack);
            else
            {
                if (Velocity.Y != 0) state = EntityAction.Jump;
                else if (Velocity.X != 0) state = EntityAction.Run;
                else state = EntityAction.Idle;

                animationManager.SetAnimation(state);
            }

            animationManager.Update(gameTime);
            Velocity = weapon.IsAttack() ? Vector2.Zero : Velocity;

            //Position += weapon.CurrentAttack != WeaponAttack.None ? Vector2.Zero : Velocity;
            Position += Velocity;
            orientation = Velocity.X != 0 ? (Velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally) : orientation;
            Velocity.X = 0;
            
        }

        private void Move()
        {
            if (Velocity.Y < 10)
                Velocity.Y += gravitySpeed * elapsedTime;

            if (InputManager.LeftPressed)
                Velocity.X = -walkSpeed * elapsedTime;
            if (InputManager.RightPressed)
                Velocity.X = walkSpeed * elapsedTime;

            if (InputManager.JumpPressed && !hasJumped)
            {
                Velocity.Y = jumpSpeed * elapsedTime;
                hasJumped = true;
            }

            
        }

        public override bool IsDead() => state == EntityAction.Death && animationManager.IsCurrentAnimationEnded();

        public override void GiveDamage(float damage)
        {
            throw new NotImplementedException();
        }
    }
}
