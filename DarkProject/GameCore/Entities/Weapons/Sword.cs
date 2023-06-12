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
            0.65f, 
            0.25f, 
            20, 
            new Attacks[] { ChosenUndead.Attacks.FirstAttack, ChosenUndead.Attacks.SecondAttack, ChosenUndead.Attacks.ThirdAttack, ChosenUndead.Attacks.FourthAttack})
        {

        }
    }
}
