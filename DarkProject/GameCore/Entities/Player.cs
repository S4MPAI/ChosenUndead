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
        
        private EntityAction animationEntityKey;

        protected float time;

        protected override float walkSpeed { get; init; } = 150f;

        protected override float gravitySpeed { get; init; } = 15f;

        protected override float jumpSpeed { get; init; } = -250f;

        private Player(Map map, AnimationManager<object> animationManager, int hitBoxWidth, int attackWidth) : 
            base(map, new Sword(),animationManager, hitBoxWidth, attackWidth)
        {
        }

        public static Player GetInstance(Map map, AnimationManager<object> animationManager, int hitBoxWidth, int attackWidth)
        {
            return instance ??= new Player(map, animationManager, hitBoxWidth, attackWidth);
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            InputManager.Update(gameTime);
            
            Move();
            CollisionWithMap();

            weapon.Update(gameTime, hasJumped ? false : InputManager.AttackPressed);

            if (weapon.IsAttack())
                animationManager.SetAnimation(weapon.CurrentAttack);
            else
            {
                if (Velocity.Y != 0) animationEntityKey = EntityAction.Jump;
                else if (Velocity.X != 0) animationEntityKey = EntityAction.Run;
                else animationEntityKey = EntityAction.Idle;

                animationManager.SetAnimation(animationEntityKey);
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
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationManager.Draw(Position, spriteBatch, orientation);
        }
    }
}
