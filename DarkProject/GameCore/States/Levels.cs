using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Level1 : PlayState
    {
        private readonly List<ScrollingBackground> backgrounds;

        public Level1(ChosenUndeadGame game, ContentManager content) : base(game, content, 1)
        {
            backgrounds = Art.GetForestBackgrounds(game.camera.VisionWindowSize);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.Immediate, transformMatrix: game.camera.Transform);
            

            foreach (var bg in backgrounds)
                bg.Draw(gameTime, spriteBatch, game.camera.WindowPos);

            map.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (var bg in backgrounds)
                bg.Update(gameTime);
        }
    }

    public class Level2 : PlayState
    {
        public Level2(ChosenUndeadGame game, ContentManager content) : base(game, content, 2)
        {

        }
    }

    public class Level3 : PlayState
    {
        public Level3(ChosenUndeadGame game, ContentManager content) : base(game, content, 3)
        {

        }
    }
}
