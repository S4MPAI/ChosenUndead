using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace ChosenUndead
{
    public class Pause : State
    {
        private List<Button> buttons;

        private Texture2D background;

        public Pause(ChosenUndeadGame game, ContentManager content) : base(game, content)
        {
            background = new Texture2D(game.GraphicsDevice, 1, 1);
            background.SetData(new Color[] { Color.White });

            var centerX = ChosenUndeadGame.WindowSize.X / 2;
            var continueButton = Art.GetButton("Продолжить");
            continueButton.Click += (s, e) => game.isPause = false;
            var exitButton = Art.GetButton("Выйти в меню");
            exitButton.Click += (sender, e) => game.ChangeState(new StartMenu(game, content));

            continueButton.Position = new Vector2(centerX - continueButton.Rectangle.Width / 2, 400);
            exitButton.Position = new Vector2(centerX - exitButton.Rectangle.Width / 2, 600);

            buttons = new List<Button> 
            { 
                continueButton, 
                exitButton 
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(Point.Zero, ChosenUndeadGame.WindowSize), new Color(Color.Black, 0.7f));

            foreach (var button in buttons)
                button.Draw(spriteBatch);

            spriteBatch.End();
        }

        public override void Update()
        {
            foreach (var button in buttons)
                button.Update();
        }
    }
}
