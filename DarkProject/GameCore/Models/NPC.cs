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

        protected Board board = Art.GetBoardForNpc();

        protected int currentPhrase;

        protected bool isTargetIntersect;

        protected override float walkSpeed => 60;

        public NPC(Map map, AnimationManager<object> animationManager, string[] phrases) : base(map, animationManager, 64, null, 0)
        {
            target = Player.GetInstance();
            this.phrases = phrases;
            board.ChangeText(phrases[0]);
        }

        public override void Update(GameTime gameTime)
        {
            var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);

            if (target.HitBox.Intersects(HitBox))
            {
                isTargetIntersect = true;
                if ((phraseTimeLeft -= elapsedTime) <= 0 && currentPhrase < phrases.Length - 1)
                    board.ChangeText(phrases[++currentPhrase]);
            }
            else
            {
                isTargetIntersect = false;
                currentPhrase = 0;
                phraseTimeLeft = phraseTime;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if (isTargetIntersect)
            {
                board.Position = new Vector2(HitBox.Center.X - board.Rectangle.Width / 2, HitBox.Top - board.Rectangle.Height);
                board.Draw(gameTime, spriteBatch);
            }
        }
    }
}
