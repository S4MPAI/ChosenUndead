using Microsoft.Xna.Framework;
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

            var newGameButton = Art.GetButton("Новая игра");
            newGameButton.Click += (sender, e) => game.LoadSave(true);
            var continueButton = Art.GetButton("Продолжить");
            continueButton.Click += (sender, e) => game.LoadSave();
            var exitGameButton = Art.GetButton("Выйти");
            exitGameButton.Click += (sender, e) => game.Exit();

            var centerX = (game.Window.ClientBounds.Width - newGameButton.Rectangle.Width) / 2;

            newGameButton.Position = new Vector2 (centerX, 300);
            continueButton.Position = new Vector2(centerX, 400);
            exitGameButton.Position = new Vector2(centerX, 500);

            sprites = new List<Component>()
            {
                newGameButton,
                continueButton,
                exitGameButton
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            foreach (var component in sprites)
                component.Draw(spriteBatch);

            spriteBatch.End();
        }

        public override void Update()
        {
            foreach (var component in sprites)
                component.Update();
        }
    }
}
