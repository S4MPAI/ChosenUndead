﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ChosenUndead
{
    internal class StartMenu : State
    {
        private List<Component> sprites;

        private Texture2D background;

        public StartMenu(ChosenUndeadGame game, ContentManager content) : base(game, content)
        {
            background = base.content.Load<Texture2D>("Backgrounds/menuBackground");
            var buttonTexture = base.content.Load<Texture2D>("Controls/menuButton");
            var buttonFont = base.content.Load<SpriteFont>("Fonts/Font");
            var centerX = (game.Window.ClientBounds.Width - buttonTexture.Width) / 2;

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(centerX, 300),
                Text = "Новая игра"
            };

            newGameButton.Click += (sender, e) =>
            {
                game.DeleteSave();
                game.LoadSave();
            };

            var continueButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(centerX, 400),
                Text = "Продолжить"
            };

            continueButton.Click += (sender, e) => game.LoadSave();

            var optionGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(centerX, 500),
                Text = "Настройки"
            };

            //optionGameButton.Click +=

            var exitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(centerX, 600),
                Text = "Выйти"
            };

            exitGameButton.Click += (sender, e) => game.Exit();

            sprites = new List<Component>()
            {
                newGameButton,
                continueButton,
                optionGameButton,
                exitGameButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            foreach (var component in sprites)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //Remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in sprites)
                component.Update(gameTime);
        }
    }
}
