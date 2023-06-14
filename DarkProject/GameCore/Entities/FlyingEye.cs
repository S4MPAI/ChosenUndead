using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class FlyingEye : EnemyBTree
    {
        public override float MaxHp => throw new NotImplementedException();

        public override float WalkSpeed => throw new NotImplementedException();

        public override float walkSpeedAttackCoef => throw new NotImplementedException();

        public FlyingEye(Map map, Entity target = null) : 
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
