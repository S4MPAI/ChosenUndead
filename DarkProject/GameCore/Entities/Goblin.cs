using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Goblin : Enemy
    {
        protected override float maxHp => 50f;

        protected override float walkSpeed => 170f;

        public Goblin(Map map, Entity target = null) : 
            base(NeuralNetworkManager.GetGoblinNeuralNetwork(), map, Art.GetGoblinAnimations(), 30, new Weapon(1, 0.2f, 15, new[]{WeaponAttack.FirstAttack}), 60, target)
        {
        }
    }
}
