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
    public class Sceleton : EnemyBTree
    {
        public override float WalkSpeed { get; } = 70f;

        public override float MaxHp => 100;

        public override float walkSpeedAttackCoef => 0.3f;

        protected override float targetDistance => 275;

        protected float rangedAttackCooldown = 2f;

        protected float closeAttackDistance => 175;

        public Sceleton(Map map) : base(
            map,
            Art.GetSceletonAnimations(),
            32,
            new Weapon(1.5f, 1.2f, 30, new[] { Attacks.FirstAttack, Attacks.SecondAttack }),
            48)
        {
        }

        protected override Node SetupTree()
        {
            return new Parallel(new List<Node>
            {
                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new Selector(new List<Node>
                        {
                            new CheckIsCurrentState(Attacks.DistanceAttack),
                            new Sequence(new List<Node>
                            {
                                new CheckTargetInRange(this, target, targetDistance),
                                new Inverter(new List<Node>
                                {
                                    new CheckTargetInRange(this, target, closeAttackDistance),
                                }),
                            })
                        }),
                        new TaskEnemyShootOnTarget(Art.GetBulletAnimation("scelet", 3, 0.1f), 25, 200, rangedAttackCooldown, this, target)
                    }),

                    new Sequence(new List<Node>
                    {
                        new Inverter(new List<Node>
                        {
                            new CheckTargetInRange(this, target, targetDistance),
                        }),
                        new TaskGoEnemyOnPos(this, startPos)
                    }),
                    new Parallel(new List<Node>
                    {
                        new Selector(new List<Node>
                        {
                            new Sequence(new List<Node>
                            {
                                new TaskEnemyOnTarget(this, target),
                                new CheckLimitOnTerritory(this, map),
                            }),
                            new TaskIdling(this),
                        }),

                        new Parallel(new List<Node>
                        {
                            new CheckTargetAttack(this, target),
                            new TaskAttack(Weapon, walkSpeedAttackCoef)
                        })
                    })
                }),
                new ApplyPhysics(this),
                new UpdateAnimation(AnimationManager)
            });
        }
    }
}
