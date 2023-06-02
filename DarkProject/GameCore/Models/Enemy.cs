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
        protected Entity target { get; set; }

        protected const float targetDistance = 200f;

        protected Vector2 startPos;

        public Enemy(Map map, AnimationManager<object> animationManager, int hitBoxWidth, Weapon weapon, int attackWidth = 0, Entity target = null) : base(map, animationManager, hitBoxWidth, weapon, attackWidth)
        {
            this.target = target ?? Player.GetInstance();
        }

        private float GetDistance()
        {
            var right = target.HitBox.Left - HitBox.Right;
            var left = target.HitBox.Right - HitBox.Left;

            if (Math.Abs(right) < Math.Abs(left))
                return MathHelper.Clamp(right, 0, targetDistance);
            return MathHelper.Clamp(left, -targetDistance, 0);
        }

        public void ChangeTarget(Entity target)
        {
            this.target = target;
        }

        public virtual void SetStartPosition(Vector2 pos)
        {
            Position = pos;
            startPos = CenterPos;
        }
    }
}
