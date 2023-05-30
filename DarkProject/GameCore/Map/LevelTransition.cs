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

        private Board board = Art.GetBoardForLevelTransition();

        public static event Action<LevelTransition> LevelChanged;

        public LevelTransition(Rectangle tilePosition, int levelIndex) : base(texture: null, tilePosition)
        {
            target = Player.GetInstance();
            LevelIndex = levelIndex;
        }

        public override void Update()
        {
            isTargetIntersect = false;
            base.Update();

            if (target.HitBox.Intersects(Rectangle))
            {
                isTargetIntersect = true;

                if (target.IsInteract)
                    LevelChanged(this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isTargetIntersect)
            {
                board.Position = new Vector2(target.HitBox.Center.X - board.Rectangle.Width / 2, target.HitBox.Top - board.Rectangle.Height);
                board.Draw(spriteBatch);
            }
                
        }
    }
}
