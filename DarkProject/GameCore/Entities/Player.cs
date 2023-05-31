using ChosenUndead.GameCore.Models.StateMachine;
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
        public override float walkSpeed => 170f;
        public override float walkSpeedAttackCoef => 0.3f;

        public const float rollSpeedCoef = 1.3f;

        public const float MaxJumpTime = 0.62f;

        public const float JumpLaunchVelocity = -400.0f;

        public const float JumpControlPower = 0.4f;

        public int Keys { get; private set; }

        public int MaxHealingQuartz { get; private set; }

        public int HealingQuartzLeft { get; private set; }

        public StateMachine stateMachine { get; private set; }
        public WalkingStatus WalkingStatus { get; private set; }
        public JumpingStatus JumpingStatus { get; private set; }
        public RollingStatus RollingStatus { get; private set; }
        public DeathStatus DeathStatus { get; private set; }

        public override float maxHp => startMaxHp + VitalityBuffCount * vitalityBuffCoef;
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
            stateMachine = new StateMachine();
            WalkingStatus = new WalkingStatus(this, stateMachine);
            JumpingStatus = new JumpingStatus(this, stateMachine);
            RollingStatus = new RollingStatus(this, stateMachine);
            DeathStatus = new DeathStatus(this, stateMachine);
            stateMachine.Initialize(WalkingStatus);
            
        }

        public static Player GetInstance(Map map = null)
        {
            if (instance == null)
                instance = new Player(map);

            if (map != null)
                instance.map = map;

            return instance;
        }

        public override bool IsDeadFull() => state == EntityAction.Death && AnimationManager.IsCurrentAnimationEnded();

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

        public override void Update()
        {
            stateMachine.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            stateMachine.Draw(spriteBatch);
        }

        public override void GiveDamage(float damage)
        {
            base.GiveDamage(damage);
        }
    }
}
