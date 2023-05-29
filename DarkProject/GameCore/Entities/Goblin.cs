using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Goblin : Enemy
    {
        protected override float maxHp => 45f;

        protected override float walkSpeed => 150f;

        protected override float walkSpeedAttackCoef => 0.5f;

        public Goblin(Map map, Entity target = null) : 
            base(NeuralNetworkManager.GetGoblinNeuralNetwork(), map, Art.GetGoblinAnimations(), 30, new Weapon(1, 1f, 15, new[]{WeaponAttack.FirstAttack}), 60, target)
        {
        }
    }
}
