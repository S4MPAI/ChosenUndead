using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead.GameCore.Entities
{
    internal class NightBorne : EnemyBTree
    {
        public override float MaxHp => throw new NotImplementedException();

        public override float WalkSpeed => throw new NotImplementedException();

        public override float walkSpeedAttackCoef => throw new NotImplementedException();

        public NightBorne(Map map, Entity target = null) : base(map, Art.GetNightBorneAnimations(), 30, new Weapon(), 30, target)
        {
            Start();
        }

        protected override Node SetupTree()
        {
            throw new NotImplementedException();
        }
    }
}
