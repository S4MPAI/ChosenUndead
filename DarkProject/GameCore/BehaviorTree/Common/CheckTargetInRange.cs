using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    internal class CheckTargetInRange : Node
    {
        private Enemy enemy;

        private Entity target;

        private float fovRange;

        public CheckTargetInRange(Enemy enemy, Entity target, float fovRange) : base()
        {
            this.enemy = enemy;
            this.target = target;
            this.fovRange = fovRange;
        }

        public override NodeState Evaluate()
        {
            var vectorDistance = enemy.CenterPos - target.CenterPos;

            if (vectorDistance.LengthSquared() > fovRange * fovRange)
                return NodeState.FAILURE;
            return NodeState.SUCCESS;
        }
    }
}
