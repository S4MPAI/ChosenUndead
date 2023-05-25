using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class BonfireSave : Decoration
    {
        public bool isTargetIntersect;

        private readonly Player target;

        public const float textCooldown = 2f;

        public float textCooldownLeft = 0f;

        private readonly string startText;

        private readonly string saveText = "Вы сохранились";

        private readonly Board board = Art.GetBoardForBonfireSave();

        public static event Action<BonfireSave> PlayerSaved;

        public BonfireSave(Animation animation, Rectangle tilePosition) : base(animation, tilePosition)
        {
            target = Player.GetInstance();
            startText = board.Text;
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            isTargetIntersect = false;

            if((textCooldownLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds) < 0)
                board.ChangeText(startText);

            if (target.HitBox.Intersects(Rectangle))
            {
                isTargetIntersect = true;

                if (target.IsInteract)
                {
                    PlayerSaved(this);
                    board.ChangeText(saveText);
                    textCooldownLeft = textCooldown;
                }
                    
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (isTargetIntersect)
            {
                board.Position = new Vector2(target.HitBox.Center.X - board.Rectangle.Width / 2, target.HitBox.Top - board.Rectangle.Height);
                board.Draw(gameTime, spriteBatch);
            }

        }
    }
}
