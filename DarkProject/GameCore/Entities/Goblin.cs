using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChosenUndead
{
    public class Goblin : EnemyBTree
    {
        public override float MaxHp => 65f;

        public override float WalkSpeed => 150f;

        public override float walkSpeedAttackCoef => 0.5f;

        public Goblin(Map map, Entity target = null) : 
            base(map, Art.GetGoblinAnimations(), 30, new Weapon(1f, 1.2f, 30, new[]{WeaponAttacks.FirstAttack}), 55, target)
        {
            Start();
        }

        protected override Node SetupTree()
        {
            return new Parallel(new List<Node>
            {
                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new Inverter(new List<Node>
                        {
                            new CheckTargetInFovRange(this, target, targetDistance),
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
