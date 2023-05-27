using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public enum ChestBuff
    {
        Attack = 1,
        Vitality
    }

    public class Chest : Decoration
    {
        private readonly ChestBuff buff;

        private readonly Player target;

        public bool IsOpen = false;

        public Chest(ChestBuff buff, Rectangle tilePosition) : base(Art.GetChestAnimation(buff), tilePosition)
        {
            this.buff = buff;
            target = Player.GetInstance();
        }

        public override void Update(GameTime gameTime)
        {
            if (target.IsInteract && !IsOpen && Rectangle.Intersects(target.HitBox))
            {
                IsOpen = true;
                target.AddBuff(buff);
            }
                
            if (IsOpen) animation?.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
