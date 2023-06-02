﻿using Microsoft.Xna.Framework;
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
    public class Sceleton : EnemyBTree
    {
        public Sceleton(Map map) : base(
            map,
            Art.GetSceletonAnimations(),
            32,
            new Weapon(1, 1, 10, new[] { WeaponAttacks.FirstAttack, WeaponAttacks.SecondAttack }),
            32)
        {
        }

        public override float WalkSpeed { get; } = 100f;

        public override float MaxHp => 30;

        public override float walkSpeedAttackCoef => 0.5f;

        protected override Node SetupTree()
        {
            throw new NotImplementedException();
        }

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
