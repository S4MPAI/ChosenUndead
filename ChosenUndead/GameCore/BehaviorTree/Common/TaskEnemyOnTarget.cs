using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChosenUndead
{
    public class TaskEnemyOnTarget : Node
    {
        private Enemy enemy;

        private Entity target;

        public TaskEnemyOnTarget(Enemy enemy, Entity target) : base()
        {
            this.enemy = enemy;
            this.target = target;
        }

        public override NodeState Evaluate()
        {
            var hitBoxEnemy = enemy.HitBox;
            var hitBoxTarget = target.HitBox;
            var orientation = hitBoxTarget.Center.X - hitBoxEnemy.Center.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var rightDirection = hitBoxTarget.Left - hitBoxEnemy.Right;
            var leftDirection = hitBoxTarget.Right - hitBoxEnemy.Left;

            SetDataOnMainElement("velocityX", 0.0f);
            SetDataOnMainElement("orientation", orientation);
            SetDataOnMainElement("currentState", EntityAction.Idle);

            var distance = GetShortestDistance(rightDirection, leftDirection);

            if (distance > 0 || distance < 0)
            {
                SetDataOnMainElement("velocityX", enemy.WalkSpeed * (distance > 0 ? 1 : -1));
                SetDataOnMainElement("orientation", distance > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
                SetDataOnMainElement("currentState", EntityAction.Run);
            }


            return NodeState.RUNNING;
        }

        private float GetShortestDistance(float rightDirection, float leftDirection)
        {
            if (Math.Abs(rightDirection) > Math.Abs(leftDirection))
                return LimitInLowerValues(leftDirection);

            return LimitInUpperValues(rightDirection);
        }

        public float LimitInUpperValues(float number) => number >= 0 ? number : 0;

        public float LimitInLowerValues(float number) => number <= 0 ? number : 0;
    }
}
