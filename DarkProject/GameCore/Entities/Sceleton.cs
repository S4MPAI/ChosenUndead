using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Sceleton : Enemy
    {
        public Sceleton(Map map) : base(
            map,
            Art.GetSceletonAnimations(),
            32,
            new Weapon(1, 1, 10, new[] { WeaponAttack.FirstAttack, WeaponAttack.SecondAttack }),
            32)
        {
        }

        protected override float walkSpeed { get; } = 100f;

        protected override float maxHp => 30;

        protected override float walkSpeedAttackCoef => 0.5f;

        //public override void Update(GameTime gameTime)
        //{
        //    if (state == EntityAction.Death)
        //    {
        //        if (!animationManager.IsCurrentAnimationEnded())
        //            animationManager.Update(gameTime);
                
        //        return;
        //    }

        //    base.Update(gameTime);
        //    animationManager.Update(gameTime);
        //}
    }
}
