using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public abstract class Entity : Component
    {
        protected AnimationManager _animationManager;

        protected Vector2 _position;

        public float Speed = 1f;

        public Vector2 Velocity;

        public SpriteEffects orientation;

        public float hitBoxRadius { get; }
        public float attackRadius { get; }

        public Entity(Texture2D texture, float hitBoxRadius = 0, float attackRadius = 0) : this(hitBoxRadius, attackRadius)
        {
            _texture = texture;
            
        }

        public Entity(AnimationManager animationManager, float hitBoxRadius = 0, float attackRadius = 0) : this(hitBoxRadius, attackRadius)
        {
            _animationManager = animationManager;
        }

        private Entity(float hitBoxRadius, float attackRadius)
        {
            this.hitBoxRadius = hitBoxRadius;
            this.attackRadius = attackRadius;
        }
    }
}
