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

        private EntityManager _entityManager { get; }

        public TestState(ChosenUndeadGame game, ContentManager content) : base(game, content)
        {
            _entityManager = new EntityManager(_map);
            _player = _entityManager.GetPlayer();
            _player.Position = new Vector2(16, 16);

            Tiles.Content = _content;
            _map.Generate(new[,]
            {
                //{0, 0, 0, 0, 0, 0 },
                //{0, 0, 0, 0, 0, 0 },
                //{0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 1 },
                {0, 0, 0, 0, 0, 0, 0, 1 },
                {0, 0, 0, 0, 0, 0, 0, 1 },
                {0, 0, 0, 0, 0, 0, 0, 1 },
                {0, 0, 0, 0, 0, 0, 0, 1 },
                {0, 0, 0, 0, 0, 0, 0, 1},
                { 0, 0, 0, 0, 0, 0, 1, 1},
                {0, 0, 1, 1, 1, 1, 1, 1 }
            }, 24);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            _player.Draw(gameTime, spriteBatch);
            _map.Draw(spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
        }
    }
}
