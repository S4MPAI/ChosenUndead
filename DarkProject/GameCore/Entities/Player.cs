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
        public override float WalkSpeed => 170f;
        public override float walkSpeedAttackCoef => 0.3f;

        public const float RollSpeedCoef = 0.9f;

        public const float MaxRollingTime = 0.6f;

        public const float MaxJumpTime = 0.62f;

        public const float JumpLaunchVelocity = -400.0f;

        public const float JumpControlPower = 0.7f;

        public const float MaxStamina = 100f;

        private float stamina = 100f;
        public float Stamina { get
            {
                return stamina;
            }
            set
            {
                if (value > MaxStamina)
                    stamina = MaxStamina;
                else
                    stamina = value;
            }
        }

        public const float StaminaRecovery = 25f;

        public const float RollStaminaCost = 30f;

        public const float AttackStaminaCost = 8f;

        public const float JumpStaminaCost = 10f;

        public int Keys { get; private set; }

        public int MaxHealingQuartz { get; private set; }
        public int HealingQuartzLeft { get; set; }

        public const float HealingSize = 40f;

        public StateMachine stateMachine { get; }
        public WalkingStatus WalkingStatus { get; }
        public JumpingStatus JumpingStatus { get; }
        public RollingStatus RollingStatus { get; }
        public DeathStatus DeathStatus { get; }
        public AttackStatus AttackStatus { get; }
        public HealingStatus HealingStatus { get; }
        public GettingDamageStatus GettingDamageStatus { get; }

        public bool IsGettingDamage { get; private set; } 
        public override float MaxHp => startMaxHp + VitalityBuffCount * vitalityBuffCoef;
        protected const float startMaxHp = 100f;
        public int VitalityBuffCount { get; private set; }
        private const float vitalityBuffCoef = 25f;

        public override float Damage { get => Weapon.Damage + AttackBuffCount * attackBuffCoef; }
        public int AttackBuffCount { get; private set; }
        private const float attackBuffCoef = 5f;

        public bool IsInteract;

        private static Player instance;

        private Player(Map map) : base(map, Art.GetPlayerAnimations(), 32, new Sword(), 32)
        {
            HealingQuartzLeft = MaxHealingQuartz;
            stateMachine = new StateMachine();
            WalkingStatus = new WalkingStatus(this, stateMachine);
            JumpingStatus = new JumpingStatus(this, stateMachine);
            RollingStatus = new RollingStatus(this, stateMachine);
            AttackStatus = new AttackStatus(this, stateMachine);
            DeathStatus = new DeathStatus(this, stateMachine);
            HealingStatus = new HealingStatus(this, stateMachine);
            GettingDamageStatus = new GettingDamageStatus(this, stateMachine);
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

        public void RevivePlayer()
        {
            stateMachine.ChangeState(WalkingStatus);
        }

        public override bool IsDeadFull() => Hp == 0 && AnimationManager.IsCurrentAnimationEnded();

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
                    HealingQuartzLeft++;
                    break;
                case ChestItem.Key:
                    Keys++;
                    break;
                default:
                    break;
            }
        }

        public void SetInventory(int attackBuffCount, int vitalityBuffCount, int maxHealingQuartz, int keys) 
        {
            AttackBuffCount = attackBuffCount;
            VitalityBuffCount = vitalityBuffCount;
            MaxHealingQuartz = maxHealingQuartz;
            HealingQuartzLeft = MaxHealingQuartz;
            Keys = keys;
        }

        public override void Update()
        {
            stateMachine.Update();
            IsGettingDamage = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            stateMachine.Draw(spriteBatch);
        }

        public override void AddHp(float hpSize)
        {
            

            if (!IsImmune || hpSize >= 0)
            {
                if (hpSize < 0)
                    IsGettingDamage = true;

                base.AddHp(hpSize);
            }
        }

        public void Reset()
        {
            Hp = MaxHp;
            HealingQuartzLeft = MaxHealingQuartz;
            stateMachine.ChangeState(WalkingStatus);
        }
    }
}
