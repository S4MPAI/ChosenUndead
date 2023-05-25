using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public enum WeaponAttack
    {
        None, 
        FirstAttack,
        SecondAttack,
        ThirdAttack, 
        FourthAttack,
        Stun
    }

    public class Weapon
    {
        public readonly float attackCooldown;

        public readonly float stunCooldown;

        public float attackRegTimeLeft { get => MathHelper.Clamp(attackCooldownLeft - attackCooldown/2, 0, attackCooldown / 2); }

        protected float attackCooldownLeft { get; set; }

        protected float stunCooldownLeft { get; set; }

        public float Damage { get; private set; }

        public WeaponAttack[] WeaponAttacks { get; private set; }

        public WeaponAttack CurrentAttack { get; set; }

        private bool isDamaged { get; set; }

        public Weapon()
        {

        }

        public Weapon(float attackCooldown, float stunCooldown, float damage, WeaponAttack[] weaponAttacks)
        {
            this.attackCooldown = attackCooldown;
            attackCooldownLeft = attackCooldown;
            this.stunCooldown = stunCooldown;
            this.Damage = damage;
            this.WeaponAttacks = weaponAttacks;
        }

        public virtual void Update(GameTime gameTime, bool isFire)
        {
            var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (CurrentAttack == WeaponAttack.Stun)
                stunCooldownLeft -= elapsedTime;
            else if (WeaponAttacks.Contains(CurrentAttack))
                attackCooldownLeft -= elapsedTime;

            if (attackCooldownLeft <= 0 || (CurrentAttack == WeaponAttack.None && isFire))
            {
                if (CurrentAttack != WeaponAttacks[^1] && isFire)
                {
                    CurrentAttack = CurrentAttack + 1;
                    isDamaged = false;
                }
                else
                {
                    CurrentAttack = WeaponAttack.Stun;
                    stunCooldownLeft = stunCooldown;
                }

                attackCooldownLeft = attackCooldown;
            }

            if (stunCooldownLeft <= 0 && CurrentAttack == WeaponAttack.Stun)
                CurrentAttack = WeaponAttack.None;
        }

        public virtual bool IsDamaged()
        {
            if (attackCooldownLeft < attackCooldown / 2 && !isDamaged)
            {
                isDamaged = true;
                return true;
            }

            return false;
        }

        public virtual bool IsAttack() => CurrentAttack != WeaponAttack.None && CurrentAttack != WeaponAttack.Stun;
    }
}
