using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    internal class TaskAttack : Node
    {
        private Weapon weapon;

        private float walkSpeedAttackCoef;


        public TaskAttack(Weapon weapon, float walkSpeedAttackCoef = 1) : base()
        {
            this.weapon = weapon;
            this.walkSpeedAttackCoef = walkSpeedAttackCoef;
        }

        public override NodeState Evaluate()
        {
            weapon.Update((bool)GetData("isAttack"));

            if (weapon.IsAttack())
            {
                SetDataOnMainElement("velocityX", (float)GetData("velocityX") * walkSpeedAttackCoef);
                SetDataOnMainElement("currentState", weapon.CurrentAttack);
            }

            return NodeState.RUNNING;
        }
    }
}
