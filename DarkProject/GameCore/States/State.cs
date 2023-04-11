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
    public abstract class State
    {
        #region Fields

        protected ContentManager _content;

        protected readonly ChosenUndeadGame _game;

        #endregion

        #region Methods

        public State(ChosenUndeadGame game, ContentManager content)
        {
            _game = game;
            _content = content;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public abstract void Update(GameTime gameTime);

        #endregion


    }
}
