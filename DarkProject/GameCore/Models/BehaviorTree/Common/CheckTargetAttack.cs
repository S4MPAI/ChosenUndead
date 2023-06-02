using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class CheckTargetAttack : Node
    {
        private Enemy currentEnemy;
        private Entity target;

        public CheckTargetAttack(Enemy currentEnemy, Entity target)
        {
            this.currentEnemy = currentEnemy;
            this.target = target;
        }

        public override NodeState Evaluate()
        {
            if (currentEnemy.AttackBox.Intersects(target.HitBox))
            {
                SetDataOnMainElement("isAttack", true);
                return NodeState.SUCCESS;
            }

            SetDataOnMainElement("isAttack", false);
            return NodeState.FAILURE;
        }
    }
}
