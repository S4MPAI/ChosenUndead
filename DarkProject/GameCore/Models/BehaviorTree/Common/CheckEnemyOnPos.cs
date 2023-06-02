using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    internal class CheckEnemyOnPos : Node
    {
        private Entity entity;
        private Vector2 pos;

        public CheckEnemyOnPos(Entity entity, Vector2 pos)
        {
            this.entity = entity;
            this.pos = pos;
        }

        public override NodeState Evaluate()
        {
            if (entity.HitBox.Contains(pos))
                return NodeState.SUCCESS;
            return NodeState.FAILURE;
        }
    }
}
