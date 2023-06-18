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
    public class Final : PlayState
    {
        private Texture2D background;
        public Final(ChosenUndeadGame game, ContentManager content, int levelNumber, List<ScrollingBackground> backgrounds, string soundName = null) : base(game, content, levelNumber, backgrounds, soundName)
        {
            background = content.Load<Texture2D>("Backgrounds/Final");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }

        public override void Exit()
        {

        }

        public override void Initialize()
        {

        }

        public override void Update()
        {
            sound?.Play();
        }
    }
}
