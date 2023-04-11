using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ChosenUndead
{
    internal class StartMenu : State
    {
        private List<Component> _sprites;

        private Texture2D _background;

        public StartMenu(ChosenUndeadGame game, ContentManager content) : base(game, content)
        {
            _background = _content.Load<Texture2D>("Backgrounds/menuBackground");
            var buttonTexture = _content.Load<Texture2D>("Controls/menuButton");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((game.Window.ClientBounds.Width - buttonTexture.Width) / 2, 400),
                Text = "Новая игра"
            };

            //newGameButton.Click += (sender, e) => game.ChangeState(new TestState(_game, _graphicsDevice, _content));

            var optionGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((game.Window.ClientBounds.Width - buttonTexture.Width) / 2, 500),
                Text = "Настройки"
            };

            //optionGameButton.Click +=

            var exitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((game.Window.ClientBounds.Width - buttonTexture.Width) / 2, 600),
                Text = "Выйти"
            };

            exitGameButton.Click += (sender, e) => game.Exit();

            var testGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((game.Window.ClientBounds.Width - buttonTexture.Width) / 2, 700),
                Text = "Тесты"
            };

            testGameButton.Click += (sender, e) => game.ChangeState(new TestState(_game, content));

            _sprites = new List<Component>()
            {
                newGameButton,
                optionGameButton,
                exitGameButton,
                testGameButton
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);

            foreach (var component in _sprites)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //Remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _sprites)
                component.Update(gameTime);
        }
    }
}
