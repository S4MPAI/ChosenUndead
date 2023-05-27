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

        public readonly string[] Phrases;

        protected Board board = Art.GetBoardForNpc();

        protected int currentPhrase;

        protected bool isTargetIntersect;

        public readonly string Name;

        protected override float walkSpeed => 60;

        public NPC(Map map, string name, string[] phrases) : base(map, Art.GetNpcAnimations(name), 64, null, 0)
        {
            target = Player.GetInstance();
            Phrases = phrases;
            board.ChangeText(phrases[0]);
            Name = name;
        }

        public override void Update(GameTime gameTime)
        {
            var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);

            if (target.HitBox.Intersects(HitBox))
            {
                isTargetIntersect = true;
                if ((phraseTimeLeft -= elapsedTime) <= 0 && currentPhrase < Phrases.Length - 1)
                    board.ChangeText(Phrases[++currentPhrase]);
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
