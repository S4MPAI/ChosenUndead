﻿using System;
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
            2f, 
            25, 
            new WeaponAttack[] {WeaponAttack.FirstAttack, WeaponAttack.SecondAttack, WeaponAttack.ThirdAttack, WeaponAttack.FourthAttack})
        {

        }
    }
}
