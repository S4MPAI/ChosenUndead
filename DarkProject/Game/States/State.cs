using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkProject.Game.States
{
    public abstract class State
    {
        #region Fields

        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        protected ChosenUndead _game;

        #endregion

        #region Methods

        public State(ChosenUndead game, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = contentManager;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public abstract void Update(GameTime gameTime);

        #endregion


    }
}
