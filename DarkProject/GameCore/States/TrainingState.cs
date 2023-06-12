using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class TrainingState : State
    {
        Texture2D background;

        public TrainingState(ChosenUndeadGame game, ContentManager content) : base(game, content)
        {
            background = content.Load<Texture2D>("Backgrounds/training");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            spriteBatch.End();
        }

        public override void Update()
        {
            if (Input.InteractionPressed)
                game.SetFirstLevel();
        }
    }
}
