using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class DeathState : State
    {
        private Video deathVideo;

        private static VideoPlayer videoPlayer = new VideoPlayer();

        private float videoTime;

        private List<Component> components;

        public DeathState(ChosenUndeadGame game, ContentManager content) : base(game, content)
        {
            deathVideo = Art.GetVideo("Death");
            videoTime = 5f;
            videoPlayer.Volume = 0.3f;

            var buttonTexture = content.Load<Texture2D>("Controls/menuButton");
            var buttonFont = Art.GetFont("Font");

            var centerX = (ChosenUndeadGame.WindowSize.X - buttonTexture.Width) / 2;
            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(centerX, 600),
                Text = "Возродиться"
            };
            newGameButton.Click += (sender, e) => game.LoadSave();

            var exitInMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(centerX, 700),
                Text = "Выйти в меню"
            };
            exitInMenuButton.Click += (sender, e) => game.ChangeState(new StartMenu(game, content));

            components = new List<Component>
            {
                newGameButton,
                exitInMenuButton
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var videoTexture = videoPlayer.GetTexture();

            spriteBatch.Begin();

            spriteBatch.Draw(videoTexture, new Rectangle(0, 0, 1600, 900), Color.White);

            foreach (var component in components)
                component.Draw(spriteBatch);

            spriteBatch.End();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update()
        {
            if ((videoTime -= Time.ElapsedSeconds) > 0)
                videoPlayer.Play(deathVideo);
            else
                videoPlayer.Stop();
            
            foreach (var component in components)
                component.Update();
        }
    }
}
