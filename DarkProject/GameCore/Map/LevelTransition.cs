﻿using Microsoft.Xna.Framework;
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

        public Player Target;

        private Board board = Art.GetBoardForLevelTransition();

        public static event Action<LevelTransition> LevelChanged;

        public LevelTransition(Rectangle tilePosition, int levelIndex) : base(texture: null, tilePosition)
        {
            Target = Player.GetInstance();
            LevelIndex = levelIndex;
        }

        public override void Update(GameTime gameTime)
        {
            isTargetIntersect = false;
            base.Update(gameTime);

            if (Target.HitBox.Intersects(Rectangle))
            {
                isTargetIntersect = true;

                if (Target.IsInteract)
                    LevelChanged(this);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isTargetIntersect)
            {
                board.Position = new Vector2(Target.HitBox.Center.X - board.Rectangle.Width / 2, Target.HitBox.Top - board.Rectangle.Height);
                board.Draw(gameTime, spriteBatch);
            }
                
        }
    }
}