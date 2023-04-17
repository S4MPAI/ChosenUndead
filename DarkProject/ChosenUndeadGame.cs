using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.CompilerServices;

namespace ChosenUndead
{
    public class ChosenUndeadGame : Game
    {
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        public static int ScreenWidth;

        public static int ScreenHeight;

        public State _currentState;

        private State _nextState;

        public Camera camera { get; private set; }

        public ChosenUndeadGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ChangeState(State state) => _nextState = state;

        protected override void Initialize()
        {
            IsMouseVisible = true;
            EntityManager.Initialize(Content);
            _graphics.IsFullScreen = false;
            ScreenWidth = 1600;
            ScreenHeight = 900;

            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new StartMenu(this, Content);
            camera = new Camera();
        }

        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
                (_currentState, _nextState) = (_nextState, null);


            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}