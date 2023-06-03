using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Sword : Weapon
    {
        public Sword() : base(
            0.5f, 
            0.25f, 
            20, 
            new WeaponAttacks[] { ChosenUndead.WeaponAttacks.FirstAttack, ChosenUndead.WeaponAttacks.SecondAttack, ChosenUndead.WeaponAttacks.ThirdAttack, ChosenUndead.WeaponAttacks.FourthAttack})
        {

        }
    }
}
