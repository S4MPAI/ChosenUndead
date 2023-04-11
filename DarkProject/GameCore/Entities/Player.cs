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

        private Player(AnimationManager animationManager, float hitBoxRadius, float attackRadius, InputManager inputManager = null) : 
            base(animationManager, hitBoxRadius, attackRadius)
        {
            _inputManager = inputManager == null ? new() : inputManager;
        }

        public static Player GetInstance(AnimationManager animationManager, float hitBoxRadius, float attackRadius, InputManager inputManager = null)
        {
            if (_instance == null)
                _instance = new Player(animationManager, hitBoxRadius, attackRadius, inputManager);
            return _instance;
        }

        public override void Update(GameTime gameTime)
        {
            _inputManager.Update(gameTime);

            if (_inputManager.LeftPressed)
                Velocity.X = -Speed;
            if (_inputManager.RightPressed)
                Velocity.X = Speed;

            if (Velocity.X != 0) _animationManager.SetAnimation("Run");
            else _animationManager.SetAnimation("Idle");

            _animationManager.Update(gameTime);

            Position += Velocity;
            orientation = Velocity.X != 0 ? (Velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally) : orientation;

            Velocity = Vector2.Zero;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _animationManager.Draw(Position, spriteBatch, orientation);
        }
    }
}
