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

        public override float MaxHp => float.MaxValue;

        protected const float phraseTime = 1.5f;

        protected float phraseTimeLeft = phraseTime;

        public readonly string[] Phrases;

        protected Board board = Art.GetBoardForNpc();

        protected int currentPhrase;

        protected bool isTargetIntersect;

        public readonly string Name;

        public override float WalkSpeed => 60;

        public override float walkSpeedAttackCoef => 1f;

        public NPC(Map map, string name, string[] phrases) : base(map, Art.GetNpcAnimations(name), 64, null, 0)
        {
            target = Player.GetInstance();
            Phrases = phrases;
            board.ChangeText(phrases[0]);
            Name = name;
        }

        public override void Update()
        {
            var elapsedTime = Time.ElapsedSeconds;

            if (target.HitBox.Intersects(HitBox))
            {
                isTargetIntersect = true;
                if ((phraseTimeLeft -= elapsedTime) <= 0 && currentPhrase < Phrases.Length - 1)
                {
                    board.ChangeText(Phrases[++currentPhrase]);
                    phraseTimeLeft = phraseTime;
                }
                    
            }
            else
            {
                isTargetIntersect = false;
                currentPhrase = 0;
                phraseTimeLeft = phraseTime;
                board.ChangeText(Phrases[currentPhrase]);
            }

            Velocity.Y = SetGravity(Velocity.Y);
            Velocity = CollisionWithMap(Velocity);
            Position += Velocity * Time.ElapsedSeconds;
            AnimationManager.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (isTargetIntersect)
            {
                board.Position = new Vector2(HitBox.Center.X - board.Rectangle.Width / 2, HitBox.Top - board.Rectangle.Height);
                board.Draw(spriteBatch);
            }
        }
    }
}
