using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class TestState : State
    {
        private readonly Player player;

        private readonly Map map = new();

        public TestState(ChosenUndeadGame game, ContentManager content) : base(game, content)
        {
            MapEntity.Content = base.content;

            var mapPath = "Content/Maps/1.txt";
            using var fileStream = TitleContainer.OpenStream(mapPath);
                map.Generate(fileStream, 24);

            player = map.Player;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.Immediate, transformMatrix: game.camera.Transform);

            map.Draw(gameTime, spriteBatch);
            

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            map.Update(gameTime);
            game.camera.Follow(player);
        }
    }
}
