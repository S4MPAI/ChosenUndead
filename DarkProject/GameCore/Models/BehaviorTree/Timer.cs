using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead.GameCore.Models.BehaviorTree
{
    internal class Timer : Node
    {
        private readonly float delay;
        private float time;

        public delegate void TickEnded();
        public event TickEnded onTickEnded;

        public Timer(float delay, TickEnded onTickEnded = null) : base()
        {
            this.delay = delay;
            time = this.delay;
            this.onTickEnded = onTickEnded;
        }
        public Timer(float delay, List<Node> children, TickEnded onTickEnded = null)
            : base(children)
        {
            this.delay = delay;
            time = this.delay;
            this.onTickEnded = onTickEnded;
        }

        public override NodeState Evaluate()
        {
            if (!HasChildren) return NodeState.FAILURE;
            if (time <= 0)
            {
                time = delay;
                state = children[0].Evaluate();
                if (onTickEnded != null)
                    onTickEnded();
                state = NodeState.SUCCESS;
            }
            else
            {
                time -= Time.ElapsedSeconds;
                state = NodeState.RUNNING;
            }
            return state;
        }
    }
}
