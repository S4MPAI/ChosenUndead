using DarkProject.Game.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DarkProject.Game.States
{
    internal class StartMenu : State
    {
        private List<Component> _components;

        private Texture2D _background;

        public StartMenu(ChosenUndead game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            _background = content.Load<Texture2D>("Backgrounds/menuBackground");
            var buttonTexture = _content.Load<Texture2D>("Controls/menuButton");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((game.Window.ClientBounds.Width - buttonTexture.Width) / 2, 300),
                Text = "Новая игра"
            };

            newGameButton.Click += (sender, e) => game.ChangeState(new GameState(_game, _graphicsDevice, _content));

            var optionGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((game.Window.ClientBounds.Width - buttonTexture.Width) / 2, 400),
                Text = "Настройки"
            };

            //optionGameButton.Click +=

            var exitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((game.Window.ClientBounds.Width - buttonTexture.Width) / 2, 500),
                Text = "Выйти"
            };

            exitGameButton.Click += (sender, e) => game.Exit();

            _components = new List<Component>()
            {
                newGameButton,
                optionGameButton,
                exitGameButton
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //Remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
