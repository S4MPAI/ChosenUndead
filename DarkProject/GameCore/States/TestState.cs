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

        private EntityManager EntityManager { get; }

        public TestState(ChosenUndeadGame game, ContentManager content) : base(game, content)
        {
            EntityManager = new EntityManager(_map);
            _player = EntityManager.GetPlayer();
            _player.Position = new Vector2(16, 16);

            MapEntity.Content = _content;
            _map.Generate(new[,]
            {
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
                {0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
                {0, 0, 1, 1, 1, 1, 1, 1, 1, 1 }
            }, 24);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.Immediate, transformMatrix: _game.camera.Transform);

            _map.Draw(gameTime, spriteBatch);
            _player.Draw(gameTime, spriteBatch);
            

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
            _game.camera.Follow(_player);
        }
    }
}
