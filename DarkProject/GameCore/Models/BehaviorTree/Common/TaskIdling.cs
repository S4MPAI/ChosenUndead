using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    internal class TaskIdling : Node
    {
        private Enemy enemy;

        public TaskIdling(Enemy enemy) : base()
        {
            this.enemy = enemy;
        }

        public override NodeState Evaluate()
        {
            SetDataOnMainElement("velocityX", 0.0f);
            SetDataOnMainElement("currentState", EntityAction.Idle);
            SetDataOnMainElement("orientation", enemy.Orientation);

            return NodeState.RUNNING;
        }
    }
}
