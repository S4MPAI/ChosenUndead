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

        static PlayerInterface()
        {
            player = Player.GetInstance();
            var healthBarTextures = Art.GetHealthBars(); 
            healthBar = new ProgressBar(healthBarTextures.bar, healthBarTextures.progressBar, healthBarTextures.progressBarBorder, player.MaxHp, new Vector2(), 1.5f);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            healthBar.Draw(spriteBatch);
        }

        public static void Update()
        {
            healthBar.Update(player.Hp, player.MaxHp);
        }
    }
}
