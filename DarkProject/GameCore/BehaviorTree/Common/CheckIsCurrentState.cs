using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    internal class CheckIsCurrentState : Node
    {
        object enemyState;

        public CheckIsCurrentState(object enemyState)
        {
            this.enemyState = enemyState;
        }
         
        public override NodeState Evaluate()
        {
            var currentState = GetData("currentState");
            if (enemyState.Equals(currentState))
                return NodeState.SUCCESS;
            return NodeState.FAILURE;
        }
    }
}
