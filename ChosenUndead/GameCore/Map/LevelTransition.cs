using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class LevelTransition : Decoration
    {
        public int LevelIndex;

        public bool isTargetIntersect;

        private readonly Player target;

        private int keysToEnter { get; }

        private string text;
        private Board board = Art.GetBoardForLevelTransition();

        private bool isOpen = true;

        public static event Action<LevelTransition> LevelChanged;

        public LevelTransition(Rectangle tilePosition, int levelIndex, int keysToEnter = 0) : base(Art.GetLevelTransitionAnimation(), tilePosition)
        {
            target = Player.GetInstance();
            LevelIndex = levelIndex;
            this.keysToEnter = keysToEnter;
            text = board.Text;
            
        }

        public override void Update()
        {
            isTargetIntersect = false;
            base.Update();

            if (target.HitBox.Intersects(Rectangle))
            {
                isTargetIntersect = true;

                isOpen = target.Keys >= keysToEnter;

                if (target.IsInteract && isOpen)
                    LevelChanged(this);
                if (!isOpen) board.ChangeText($"Нужен {keysToEnter} ключ");
                else board.ChangeText(text);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (isTargetIntersect)
            {
                board.Position = new Vector2(target.HitBox.Center.X - board.Rectangle.Width / 2, target.HitBox.Top - board.Rectangle.Height);
                Art.SetPositionInMapBounds(board);
                board.Draw(spriteBatch);
            }
                
        }
    }
}
