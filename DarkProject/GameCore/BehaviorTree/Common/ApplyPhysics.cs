using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class ApplyPhysics : Node
    {
        private Enemy enemy;

        public ApplyPhysics(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override NodeState Evaluate()
        {
            var velocityX = (float)(GetData("velocityX") ?? 0.0f);
            var velocityY = (float)(GetData("velocityY") ?? 0.0f);

            velocityY = enemy.SetGravity(velocityY);
            var velocity = enemy.CollisionWithMap(new Vector2(velocityX, velocityY));
            SetData("velocityY", velocity.Y);

            enemy.Orientation = (SpriteEffects)GetData("orientation");
            enemy.Velocity = velocity;
            enemy.Position += velocity * Time.ElapsedSeconds;

            return NodeState.RUNNING;
        }
    }
}
