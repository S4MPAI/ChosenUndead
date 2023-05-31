using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Goblin : Enemy
    {
        public override float maxHp => 45f;

        public override float walkSpeed => 150f;

        public override float walkSpeedAttackCoef => 0.5f;

        public Goblin(Map map, Entity target = null) : 
            base(map, Art.GetGoblinAnimations(), 30, new Weapon(1, 1f, 15, new[]{WeaponAttack.FirstAttack}), 60, target)
        {
        }
    }
}
