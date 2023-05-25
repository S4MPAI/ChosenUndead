using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public abstract class Enemy : Entity
    {
        protected Entity target;

        protected const float targetDistance = 75f;

        public NeuralNetwork brain { get; }

        public float[] input;

        public float[] output;

        public Enemy(NeuralNetwork neuralNetwork, Map map, AnimationManager<object> animationManager, int hitBoxWidth, Weapon weapon, int attackWidth = 0, Entity target = null) : base(map, animationManager, hitBoxWidth, weapon, attackWidth)
        {
            this.target = target ?? Player.GetInstance();
            brain = neuralNetwork;
            input = new float[brain.layers[0]];
            output = new float[brain.layers[^1]];
        }

        public override void Update(GameTime gameTime)
        {
            if (targetDistance < Math.Abs(target.Position.X - Position.X))
                animationManager.SetAnimation(EntityAction.Idle);
            else if (state != EntityAction.Death)
            {
                elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Velocity = Vector2.Zero;
                LoadInputs();
                output = brain.FeedForward(input);
                bool isFire = output[1] > 0;
                weapon.Update(gameTime, isFire);

                if (isFire)
                    animationManager.SetAnimation(weapon.CurrentAttack);
                else if ((output[0] > 0.01 || output[0] < -0.01) && !weapon.IsAttack())
                    Move(output[0]);
                else
                    animationManager.SetAnimation(EntityAction.Idle);

                base.Update(gameTime);
                LimitOnTerritory();
                Position += Velocity * elapsedTime;
                orientation = Velocity.X != 0 ? Velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally : orientation;
            }

            animationManager.Update(gameTime);
        }

        protected virtual void LoadInputs()
        {
            input[0] = target.Position.X - Position.X;
            input[1] = target.AttackRegTimeLeft;
            input[2] = Rectangle.Intersect(AttackBox, target.HitBox).Width;
            input[3] = Rectangle.Intersect(HitBox, target.AttackBox).Width;
        }

        protected virtual void Move(float velocityCoef)
        {
            Velocity.X = walkSpeed * velocityCoef;
            animationManager.SetAnimation(EntityAction.Run);
        }

        protected void LimitOnTerritory()
        {
            var tileY = (int)Math.Ceiling((float)HitBox.Bottom / map.TileSize);

            if (map.IsHaveCollision(HitBox.Right / map.TileSize, tileY) || map.IsHaveCollision(HitBox.Left / map.TileSize, tileY))
                Velocity.X = 0;
        }
    }
}
