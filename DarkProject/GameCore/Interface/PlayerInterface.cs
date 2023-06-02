using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public static class PlayerInterface
    {
        private static Player player;
        private static ProgressBar healthBar;
        private static ProgressBar staminaBar;

        static PlayerInterface()
        {
            player = Player.GetInstance();
            var barTextures = Art.GetBars(); 
            healthBar = new ProgressBar(barTextures.bar, barTextures.progressBar, barTextures.progressBarBorder, Color.Red, player.MaxHp, new Vector2(), 30f, 1.5f);
            staminaBar = new ProgressBar(barTextures.bar, barTextures.progressBar, barTextures.progressBarBorder, Color.Aqua, Player.MaxStamina, new Vector2(0, 75),Player.StaminaRecovery, 1.5f);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            healthBar.Draw(spriteBatch);
            staminaBar.Draw(spriteBatch);
        }

        public static void Update()
        {
            healthBar.Update(player.Hp, player.MaxHp);
            staminaBar.Update(player.Stamina, Player.MaxStamina);
        }
    }
}
