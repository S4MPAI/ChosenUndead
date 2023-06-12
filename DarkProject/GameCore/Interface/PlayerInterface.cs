using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ChosenUndead
{
    public static class PlayerInterface
    {
        private static Player player;
        private static ProgressBar healthBar;
        private static ProgressBar staminaBar;
        private static Sprite healingQuartz;
        private static SpriteFont font;

        static PlayerInterface()
        {
            player = Player.GetInstance();
            var barTextures = Art.GetBars(); 
            healthBar = new ProgressBar(barTextures.bar, barTextures.progressBar, barTextures.progressBarBorder, new Color(229, 0, 46), player.MaxHp, new Vector2(), 30f, 1.5f);
            staminaBar = new ProgressBar(barTextures.bar, barTextures.progressBar, barTextures.progressBarBorder, new Color(5, 166, 225), Player.MaxStamina, new Vector2(0, 75),Player.StaminaRecovery, 1.5f);
            healingQuartz = new Sprite(Art.GetInterfaceTexture("Quartz"));
            healingQuartz.Position = ChosenUndeadGame.WindowSize.ToVector2() - healingQuartz.Rectangle.Size.ToVector2();
            font = Art.GetFont("Interface");
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            healthBar.Draw(spriteBatch);
            staminaBar.Draw(spriteBatch);
            healingQuartz.Draw(spriteBatch);

            var text = player.HealingQuartzLeft.ToString();
            var x = font.MeasureString(text).X;
            var y = font.MeasureString(text).Y;
            spriteBatch.DrawString(font, text, healingQuartz.Rectangle.Center.ToVector2() - new Vector2(x, y) / 2, Color.Black);
        }

        public static void Update()
        {
            healthBar.Update(player.Hp, player.MaxHp);
            staminaBar.Update(player.Stamina, Player.MaxStamina);
            healingQuartz.Update();
        }
    }
}
