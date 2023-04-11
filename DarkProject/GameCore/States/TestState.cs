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

        public TestState(ChosenUndeadGame game, ContentManager content) : base(game, content)
        {
            
            _player = EntityManager.GetPlayer();
            _player.Position = new Vector2(game.Window.ClientBounds.Width / 2, 400);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            _player.Draw(gameTime, spriteBatch);

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
