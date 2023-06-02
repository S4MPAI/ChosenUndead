﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public enum WeaponAttacks
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

        public WeaponAttacks[] WeaponAttacks { get; private set; }

        public WeaponAttacks CurrentAttack { get; set; }

        private bool isDamageReg { get; set; }

        public bool IsDamaged { get; set; }

        public Weapon()
        {

        }

        public Weapon(float attackCooldown, float stunCooldown, float damage, WeaponAttacks[] weaponAttacks)
        {
            this.attackCooldown = attackCooldown;
            attackCooldownLeft = attackCooldown;
            this.stunCooldown = stunCooldown;
            this.Damage = damage;
            this.WeaponAttacks = weaponAttacks;
        }

        public virtual void Update(bool isFire)
        {
            var elapsedTime = Time.ElapsedSeconds;

            if (CurrentAttack == ChosenUndead.WeaponAttacks.Stun)
                stunCooldownLeft -= elapsedTime;
            else if (WeaponAttacks.Contains(CurrentAttack))
                attackCooldownLeft -= elapsedTime;

            if (attackCooldownLeft <= 0 || (CurrentAttack == ChosenUndead.WeaponAttacks.None && isFire))
            {
                if (CurrentAttack != WeaponAttacks[^1] && isFire)
                {
                    CurrentAttack = CurrentAttack + 1;
                    isDamageReg = false;
                }
                else
                {
                    CurrentAttack = ChosenUndead.WeaponAttacks.Stun;
                    isDamageReg = true;
                    stunCooldownLeft = stunCooldown;
                }

                attackCooldownLeft = attackCooldown;
            }

            IsDamaged = IsDamageReg();

            if (stunCooldownLeft <= 0 && CurrentAttack == ChosenUndead.WeaponAttacks.Stun)
                CurrentAttack = ChosenUndead.WeaponAttacks.None;
        }

        protected virtual bool IsDamageReg()
        {
            if (attackCooldownLeft < attackCooldown / 2 && !isDamageReg)
                return isDamageReg = true;

            return false;
        }

        public virtual bool IsAttack() => CurrentAttack != ChosenUndead.WeaponAttacks.None && CurrentAttack != ChosenUndead.WeaponAttacks.Stun;
    }
}
