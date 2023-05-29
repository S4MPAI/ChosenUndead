using Microsoft.Xna.Framework;
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
        protected override float walkSpeed { get; } = 170f;
        protected override float walkSpeedAttackCoef => 0.25f;
        protected float rollSpeedCoef => 1.3f;

        private const float MaxJumpTime = 0.62f;

        private const float JumpLaunchVelocity = -400.0f;

        private const float JumpControlPower = 0.4f;

        private bool wasJumping;
        private float jumpTime;
        private bool isJumping;

        public int Keys { get; private set; }

        public int MaxHealingQuartz { get; private set; }

        public int HealingQuartzLeft { get; private set; }

        protected override float maxHp => startMaxHp + VitalityBuffCount * vitalityBuffCoef;
        protected const float startMaxHp = 50f;
        public int VitalityBuffCount { get; private set; }
        private const float vitalityBuffCoef = 10f;

        public override float Damage { get => weapon.Damage + AttackBuffCount * attackBuffCoef; }
        public int AttackBuffCount { get; private set; }
        private const float attackBuffCoef = 1f;

        public bool IsInteract { get; private set; }

        private static Player instance;

        private Player(Map map) : base(map, Art.GetPlayerAnimations(), 32, new Sword(), 32)
        {
            HealingQuartzLeft = MaxHealingQuartz;
        }

        public static Player GetInstance(Map map = null)
        {
            if (instance == null)
                instance = new Player(map);

            if (map != null)
                instance.map = map;

            return instance;
        }

        public override bool IsDeadFull() => state == EntityAction.Death && animationManager.IsCurrentAnimationEnded();

        public void AddItem(ChestItem buff)
        {
            switch (buff)
            {
                case ChestItem.Attack:
                    AttackBuffCount++;
                    break;
                case ChestItem.Vitality:
                    VitalityBuffCount++;
                    break;
                case ChestItem.HealingQuartz:
                    MaxHealingQuartz++;
                    break;
                case ChestItem.Key:
                    Keys++;
                    break;
                default:
                    break;
            }
        }

        public void SetInventory(int attackBuffCount, int vitalityBuffCount, int maxHealingQuartz, int keys) => 
            (AttackBuffCount, VitalityBuffCount, MaxHealingQuartz, Keys) = (attackBuffCount, vitalityBuffCount, maxHealingQuartz, keys);

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
            Velocity *= weapon.IsAttack() ? walkSpeedAttackCoef : 1;

            Position += Velocity * elapsedTime;
            orientation = Velocity.X != 0 ? (Velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally) : orientation;
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

        public override void GiveDamage(float damage)
        {
            base.GiveDamage(damage);
        }
    }
}
