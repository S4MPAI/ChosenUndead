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
        private static Player _instance;
        

        private string _animationKey;

        protected override float walkSpeed { get; init; } = 110f;

        protected override float gravitySpeed { get; init; } = 25f;

        protected override float jumpSpeed { get; init; } = -300f;

        private Player(Map map, AnimationManager animationManager, int hitBoxWidth, int attackWidth) : 
            base(map, animationManager, hitBoxWidth, attackWidth)
        {
        }

        public static Player GetInstance(Map map, AnimationManager animationManager, int hitBoxWidth, int attackWidth)
        {
            return _instance ??= new Player(map, animationManager, hitBoxWidth, attackWidth);
        }

        public override void Update(GameTime gameTime)
        {
            _elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            InputManager.Update(gameTime);

            Move();
            CollisionWithMap();

            if (Velocity.Y != 0)_animationKey = "Jump";
            else if (Velocity.X != 0) _animationKey = "Run";
            else _animationKey = "Idle";

            _animationManager.SetAnimation(_animationKey);
            _animationManager.Update(gameTime);

            Position += Velocity;
            _orientation = Velocity.X != 0 ? (Velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally) : _orientation;
            Velocity.X = 0;
            
        }

        private void Move()
        {
            if (Velocity.Y < 10)
                Velocity.Y += gravitySpeed * _elapsedTime;

            if (InputManager.LeftPressed)
                Velocity.X = -walkSpeed * _elapsedTime;
            if (InputManager.RightPressed)
                Velocity.X = walkSpeed * _elapsedTime;

            if (InputManager.JumpPressed && !_hasJumped)
            {
                Velocity.Y = jumpSpeed * _elapsedTime;
                _hasJumped = true;
            }

            
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _animationManager.Draw(Position, spriteBatch, _orientation);
        }
    }
}
