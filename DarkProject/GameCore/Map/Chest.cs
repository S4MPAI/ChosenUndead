using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public enum ChestItem
    {
        Attack = 1,
        Vitality,
        HealingQuartz,
        Key
    }

    public class Chest : Decoration
    {
        private readonly ChestItem item;

        private readonly Player target;

        public bool IsOpen = false;

        public Chest(ChestItem item, Rectangle tilePosition) : base(Art.GetChestAnimation(item), tilePosition)
        {
            this.item = item;
            target = Player.GetInstance();
        }

        public override void Update()
        {
            if (target.IsInteract && !IsOpen && Rectangle.Intersects(target.HitBox))
            {
                IsOpen = true;
                target.AddItem(item);
            }
                
            if (IsOpen) animation?.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
