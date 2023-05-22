using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public abstract class Enemy : Entity
    {
        public Enemy(Map map, AnimationManager<object> animationManager, int hitBoxWidth, Weapon weapon, int attackWidth = 0) : base(map, animationManager, hitBoxWidth, weapon, attackWidth)
        {
        }
    }
}
