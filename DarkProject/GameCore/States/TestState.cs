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
        private readonly Player _player;

        private readonly Map _map = new();

        public TestState(ChosenUndeadGame game, ContentManager content) : base(game, content)
        {
            MapEntity.Content = _content;

            var mapPath = "Content/Maps/1.txt";
            using var fileStream = TitleContainer.OpenStream(mapPath);
                _map.Generate(fileStream, 24);

            _player = _map.Player;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.Immediate, transformMatrix: _game.camera.Transform);

            _map.Draw(gameTime, spriteBatch);
            

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _map.Update(gameTime);
            _game.camera.Follow(_player);
        }
    }
}
