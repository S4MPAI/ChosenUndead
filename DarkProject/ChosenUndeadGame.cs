using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.CompilerServices;

namespace ChosenUndead
{
    public class ChosenUndeadGame : Game
    {
        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        public static int ScreenWidth;

        public static int ScreenHeight;

        public State currentState;

        private State nextState;

        public Camera camera { get; private set; }

        public ChosenUndeadGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ChangeState(State state) => nextState = state;

        protected override void Initialize()
        {
            IsMouseVisible = true;
            EntityManager.Initialize(Content);
            Component.Content = Content;
            graphics.IsFullScreen = false;
            ScreenWidth = 1600;
            ScreenHeight = 900;

            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;

            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState = new StartMenu(this, Content);
            camera = new Camera();
        }

        protected override void Update(GameTime gameTime)
        {
            if (nextState != null)
                (currentState, nextState) = (nextState, null);


            currentState.Update(gameTime);

            currentState.PostUpdate(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            currentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}