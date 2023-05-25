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

        private bool isOpen = false;

        public Chest(ChestBuff buff, Rectangle tilePosition) : base(Art.GetChestAnimation(buff), tilePosition)
        {
            this.buff = buff;
            target = Player.GetInstance();
        }

        public override void Update(GameTime gameTime)
        {
            if (target.IsInteract && !isOpen && Rectangle.Intersects(target.HitBox))
            {
                isOpen = true;
                target.AddBuff(buff);
            }
                
            if (isOpen) animation?.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
