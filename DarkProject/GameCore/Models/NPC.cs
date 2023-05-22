using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class NPC : Entity
    {
        protected Player target;

        protected override float maxHp => float.MaxValue;

        protected const float phraseTime = 3f;

        protected float phraseTimeLeft = phraseTime;

        protected readonly string[] phrases;

        protected override float walkSpeed => 0;

        public NPC(Map map, AnimationManager<object> animationManager, int hitBoxWidth, string[] phrases) : base(map, animationManager, hitBoxWidth, null, 0)
        {
            target = Player.GetInstance();
            this.phrases = phrases;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
