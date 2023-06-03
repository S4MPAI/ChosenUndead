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

        private float boardTime = 3f;

        public bool IsOpen = false;

        private Board board;

        public Chest(ChestItem item, Rectangle tilePosition) : base(Art.GetChestAnimation(item), tilePosition)
        {
            this.item = item;
            target = Player.GetInstance();
            board = Art.GetBoardForChest(item);
            board.Position = new Vector2(tilePosition.X - board.Rectangle.Width / 2, tilePosition.Top - board.Rectangle.Height);
        }

        public override void Update()
        {
            if (target.IsInteract && !IsOpen && Rectangle.Intersects(target.HitBox))
            {
                IsOpen = true;
                target.AddItem(item);
            }
                
            if (IsOpen)
            {
                animation?.Update();
                if ((boardTime -= Time.ElapsedSeconds) > 0 && Rectangle.Intersects(target.HitBox))
                    board.Update();
            } 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (boardTime > 0 && IsOpen)
                board.Draw(spriteBatch);
        }
    }
}
