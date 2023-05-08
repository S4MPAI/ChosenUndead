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
    public class Sceleton : Entity
    {
        private SoundEffectInstance damageSound;

        private SoundEffectInstance deathSound;

        public Sceleton(Level map, AnimationManager<object> anims, int hitBoxWidth, int attackWidth = 30) : base(
            map,
            new Weapon(1, 1, 20, new[] { WeaponAttack.FirstAttack, WeaponAttack.SecondAttack }),
            anims,
            hitBoxWidth,
            attackWidth)
        {
            damageSound = Content.Load<SoundEffect>("Entities/Sceleton/damageSound").CreateInstance();
            deathSound = Content.Load<SoundEffect>("Entities/Sceleton/deathSound").CreateInstance();
        }

        protected override float walkSpeed { get; } = 100f;

        protected override float jumpSpeed { get; } = 10f;

        protected override float MaxHp => 30;

        public override void Update(GameTime gameTime)
        {
            if (state == EntityAction.Death)
            {
                if (!animationManager.IsCurrentAnimationEnded())
                    animationManager.Update(gameTime);
                
                return;
            }

            base.Update(gameTime);
            animationManager.Update(gameTime);
        }

        public override void GiveDamage(float damage)
        {
            if (state == EntityAction.Death) return;

            Hp -= damage;

            if (Hp <= 0)
            {
                deathSound.Play();
                state = EntityAction.Death;
                animationManager.SetAnimation(state);
            }
            else
            {
                damageSound.Stop(false);
                damageSound.Play();
            }
                
        }

        public override bool IsDead() => state == EntityAction.Death && animationManager.IsCurrentAnimationEnded();
    }
}
