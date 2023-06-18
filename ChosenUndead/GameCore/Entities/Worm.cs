using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    //TODO: work with Worm
    public class Worm : EnemyBTree
    {
        public override float MaxHp => throw new NotImplementedException();

        public override float WalkSpeed => throw new NotImplementedException();
        
        public override float walkSpeedAttackCoef => throw new NotImplementedException();

        protected override float targetDistance => throw new NotImplementedException();

        public Worm(Map map, Entity target = null) : 
            base(map, Art.GetFlyingEyeAnimations(), 30, new Weapon(), 30, target)
        {
            Start();
        }

        protected override Node SetupTree()
        {
            throw new NotImplementedException();
        }
    }
}
