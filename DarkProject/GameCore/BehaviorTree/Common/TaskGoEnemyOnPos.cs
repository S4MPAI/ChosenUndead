using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    internal class TaskGoEnemyOnPos : Node
    {
        private Enemy enemy;
        private Vector2 pos;

        public TaskGoEnemyOnPos(Enemy enemy, Vector2 pos) : base()
        {
            this.enemy = enemy;
            this.pos = pos;
        }

        public override NodeState Evaluate()
        {
            var difference = pos.X - enemy.CenterPos.X;

            if (enemy.HitBox.Contains(pos))
            {
                SetDataOnMainElement("currentState", EntityAction.Idle);
                SetDataOnMainElement("velocityX", 0.0f);
                SetDataOnMainElement("orientation", enemy.Orientation);
            }
            else
            {
                SetDataOnMainElement("currentState", EntityAction.Run);
                SetDataOnMainElement("velocityX", enemy.WalkSpeed * (difference > 0 ? 1 : -1));
                SetDataOnMainElement("orientation", difference > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            }

            return NodeState.RUNNING;
        }
    }
}
