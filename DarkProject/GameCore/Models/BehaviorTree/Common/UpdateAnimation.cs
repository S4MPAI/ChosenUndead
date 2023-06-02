using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    internal class UpdateAnimation : Node
    {
        private AnimationManager<object> animationManager;

        public UpdateAnimation(AnimationManager<object> animationManager) : base()
        {
            this.animationManager = animationManager;
        }

        public override NodeState Evaluate()
        {
            animationManager.Update();
            var currentState = GetData("currentState");
            animationManager.SetAnimation(currentState);
            return NodeState.RUNNING;
        }
    }
}
