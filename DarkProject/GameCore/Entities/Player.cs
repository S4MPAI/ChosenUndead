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

        private InputManager _inputManager;
        
        private string _animationKey;

        private Player(Map map, AnimationManager animationManager, int hitBoxRadius, int attackRadius, InputManager inputManager = null) : 
            base(map, animationManager, hitBoxRadius, attackRadius)
        {
            _inputManager = inputManager ?? new();
        }

        public static Player GetInstance(Map map, AnimationManager animationManager, int hitBoxRadius, int attackRadius, InputManager inputManager = null)
        {
            return _instance ??= new Player(map, animationManager, hitBoxRadius, attackRadius, inputManager);
        }

        public override void Update(GameTime gameTime)
        {
            _inputManager.Update(gameTime);

            Move();
            Collision();

            _animationManager.SetAnimation(_animationKey);
            _animationManager.Update(gameTime);

            Position += Velocity;
            _orientation = Velocity.X != 0 ? (Velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally) : _orientation;
            Velocity.X = 0;
            
        }

        private void Move()
        {
            if (Velocity.Y < 10)
                Velocity.Y += 0.4f;

            if (_inputManager.LeftPressed)
                Velocity.X = -Speed;
            if (_inputManager.RightPressed)
                Velocity.X = Speed;

            if (_inputManager.JumpPressed && !_hasJumped)
            {
                Position.Y -= 3f;
                Velocity.Y = -4f;
                _hasJumped = true;
                _animationManager.SetAnimation("Jump");
            }

            if (Velocity.X != 0) _animationKey = "Run";
            else _animationKey = "Idle";
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _animationManager.Draw(Position, spriteBatch, _orientation);
        }
    }
}
