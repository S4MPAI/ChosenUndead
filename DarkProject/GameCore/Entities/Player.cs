﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Player : Entity
    {
        protected override float walkSpeed { get; } = 160f;

        private const float MaxJumpTime = 0.5f;

        private const float JumpLaunchVelocity = -450.0f;

        private const float JumpControlPower = 1.5f;

        private bool wasJumping;
        private float jumpTime;
        private bool isJumping;

        protected override float maxHp => startMaxHp + attackBuffCount * attackBuffCoef;
        protected const float startMaxHp = 50f;
        public int vitalityBuffCount { get; private set; }
        private const float vitalityBuffCoef = 10f;

        public override float Damage { get => weapon.Damage + vitalityBuffCount * vitalityBuffCoef; }
        public int attackBuffCount { get; private set; }
        private const float attackBuffCoef = 1f;

        public bool IsInteract { get; private set; }

        private static Player instance;

        private Player(Map map) : base(map, Art.GetPlayerAnimations(), 32, new Sword(), 32)
        {
        }

        public static Player GetInstance(Map map = null)
        {
            if (instance == null)
                instance = new Player(map);

            if (map != null)
                instance.map = map;

            return instance;
        }

        public override bool IsDead() => state == EntityAction.Death && animationManager.IsCurrentAnimationEnded();

        public void AddBuff(ChestBuff buff)
        {
            if (buff == ChestBuff.Attack) 
                attackBuffCount++;
            else if (buff == ChestBuff.Vitality) 
                vitalityBuffCount++;
        }

        public override void Update(GameTime gameTime)
        {
            Velocity.X = 0;
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            IsInteract = InputManager.InteractionPressed;

            Move();
            CollisionWithMap();
            isJumping = InputManager.JumpPressed;

            weapon.Update(gameTime, isOnGround ? InputManager.AttackPressed : false);
            SetAnimation();

            animationManager.Update(gameTime);
            Velocity = weapon.IsAttack() ? Vector2.Zero : Velocity;

            //Position += weapon.CurrentAttack != WeaponAttack.None ? Vector2.Zero : Velocity;
            Position += Velocity * elapsedTime;
            orientation = Velocity.X != 0 ? Velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally : orientation;
        }

        private void Move()
        {
            Velocity.Y = SetGravity();
            Velocity.Y = DoJump(Velocity.Y);

            if (InputManager.LeftPressed)
                Velocity.X = -walkSpeed;
            if (InputManager.RightPressed)
                Velocity.X = walkSpeed;
        }

        private float DoJump(float velocityY)
        {
            if (isJumping && !isUnderTop)
            {
                if (isOnGround && !wasJumping || jumpTime > 0.0f)
                {
                    jumpTime += elapsedTime;
                }

                if (0.0f < jumpTime && jumpTime <= MaxJumpTime)
                {
                    velocityY = JumpLaunchVelocity * (1.0f - (float)Math.Pow(jumpTime / MaxJumpTime, JumpControlPower));
                }
                else
                {
                    jumpTime = 0.0f;
                }
            }
            else
            {
                jumpTime = 0.0f;
            }
            wasJumping = isJumping;

            return velocityY;
        }

        private void SetAnimation()
        {
            if (weapon.IsAttack())
                animationManager.SetAnimation(weapon.CurrentAttack);
            else
            {
                if (!isOnGround) state = EntityAction.Jump;
                else if (Velocity.X != 0 && isOnGround) state = EntityAction.Run;
                else state = EntityAction.Idle;

                animationManager.SetAnimation(state);
            }
        }
    }
}
