using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    internal class TaskEnemyShootOnTarget : Node
    {
        private Animation bulletAnim;
        private float damage;
        private float speed;
        private Entity target;
        private Enemy enemy;
        private float delay;
        private float lastTime;
        private float attackCooldown { get; }

        public TaskEnemyShootOnTarget(Animation bulletAnim, float damage, float speed, float attackCooldown, Enemy enemy, Entity target)
        {
            this.bulletAnim = bulletAnim;
            this.damage = damage;
            this.speed = speed;
            this.enemy = enemy;
            this.target = target;
            this.attackCooldown = attackCooldown;
            lastTime = Time.TotalSeconds;
        }

        public override NodeState Evaluate()
        {
            SetDataOnMainElement("velocityX", 0.0f);
            var velocity = Vector2.Normalize(target.CenterPos - enemy.CenterPos);
            SetDataOnMainElement("orientation", velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);

            if ((delay -= Time.TotalSeconds - lastTime) > 0)
            {
                SetDataOnMainElement("currentState", EntityAction.Idle);
                lastTime = Time.TotalSeconds;
                return NodeState.RUNNING;
            }
            
            if ((GetData("currentState")?.Equals(Attacks.DistanceAttack) ?? false) && (GetData("isAnimEnded")?.Equals(true) ?? false))
            {
                delay = attackCooldown;
                lastTime = Time.TotalSeconds;
                var bullet = new Bullet(bulletAnim.Copy(), damage, speed);
                bullet.Position = enemy.CenterPos + velocity * 5;
                bullet.Velocity = velocity;
                EntityManager.AddBullet(bullet);
                SetDataOnMainElement("currentState", EntityAction.Idle);
            }
            else
                SetDataOnMainElement("currentState", Attacks.DistanceAttack);

            return NodeState.RUNNING;
        }
    }
}
