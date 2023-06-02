using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Sequence : Node
    {
        public Sequence() : base() { }

        public Sequence(List<Node> nodes) : base(nodes) { }

        public override NodeState Evaluate()
        {
            var isAnyChildIsRunning = false;

            foreach (var child in Children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        isAnyChildIsRunning = true;
                        continue;
                }
            }

            state = isAnyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}
