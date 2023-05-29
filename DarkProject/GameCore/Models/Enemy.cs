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
        public Entity Target { get; private set; }

        protected const float targetDistance = 450f;

        private float performance;

        public NeuralNetwork brain { get; set; }

        public float[] input;

        public float[] output;

        public Enemy(NeuralNetwork neuralNetwork, Map map, AnimationManager<object> animationManager, int hitBoxWidth, Weapon weapon, int attackWidth = 0, Entity target = null) : base(map, animationManager, hitBoxWidth, weapon, attackWidth)
        {
            Target = target ?? Player.GetInstance();
            brain = neuralNetwork;
            input = new float[brain.layers[0]];
            output = new float[brain.layers[^1]];
        }

        public override void Update(GameTime gameTime)
        {
            if (Target.IsDead || Math.Abs(GetDistance()) > targetDistance)
                animationManager.SetAnimation(EntityAction.Idle);
            else if (state != EntityAction.Death)
            {
                elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Velocity = Vector2.Zero;
                LoadInputs();
                output = brain.FeedForward(input);
                bool isFire = output[1] > 0;

                weapon.Update(gameTime, isFire);


                Move(output[0], weapon.IsAttack());

                base.Update(gameTime);
                LimitOnTerritory();
                Position += Velocity * elapsedTime;
                orientation = Velocity.X != 0 ? Velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally : orientation;

                SetAnimation();
            }

            var distance = Math.Abs(GetDistance());
            if (weapon.IsDamaged && distance > attackWidth * 3 && distance < targetDistance) performance -= 150;

            brain.Fitness = targetDistance - distance + performance + (performance == 0?-targetDistance:0);

            animationManager.Update(gameTime);
        }

        private float GetDistance()
        {
            var right = Target.HitBox.Left - HitBox.Right;
            var left = Target.HitBox.Right - HitBox.Left;

            if (Math.Abs(right) < Math.Abs(left))
                return MathHelper.Clamp(right, 0, targetDistance);
            return MathHelper.Clamp(left, -targetDistance, 0);
        }

        public void ChangeTarget(Entity target)
        {
            Target = target;
        }

        public virtual void AddAttackPerformance()
        {
            performance += 150;
        }

        public virtual void AddGetDamagedPerformance()
        {
            performance -= 50;
        }

        protected virtual void SetAnimation()
        {
            if (weapon.IsAttack())
                animationManager.SetAnimation(weapon.CurrentAttack);
            else
            {
                if (!isOnGround) state = EntityAction.Jump;
                else if (Velocity.X != 0 && isOnGround) state = EntityAction.Run;
                else state = EntityAction.Idle;

                animationManager.SetAnimation(state);
            }
        }

        protected virtual void LoadInputs()
        {
            input[0] = GetDistance();
            input[1] = Target.AttackRegTimeLeft;
            input[2] = Rectangle.Intersect(AttackBox, Target.HitBox).Width;
            input[3] = Rectangle.Intersect(HitBox, Target.AttackBox).Width;
        }

        protected virtual void Move(float velocityCoef, bool isAttacked)
        {
            Velocity.X = walkSpeed * (velocityCoef != 0 ? (velocityCoef > 0 ? 1 : -1) : 0) * (isAttacked ? walkSpeedAttackCoef : 1);
        }

        protected void LimitOnTerritory()
        {
            var tileY = (int)Math.Ceiling((float)HitBox.Bottom / map.TileSize);

            if ((!map.IsHaveCollision(HitBox.Right / map.TileSize, tileY) && Velocity.X > 0) || (!map.IsHaveCollision(HitBox.Left / map.TileSize, tileY) && Velocity.X < 0))
                Velocity.X = 0;
        }
    }
}
