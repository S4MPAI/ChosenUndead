using ChosenUndead.GameCore;
using ChosenUndead.GameCore.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ChosenUndead
{
    public class ChosenUndeadGame : Game
    {
        private const string saveFile = "save.json"; 

        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        public static Point WindowSize;

        public State currentState;

        private State nextState;

        public PlayState[] Levels { get; private set; }

        public Camera camera { get; private set; }

        public float ChangeStateCooldown = 0.25f;

        public float ChangeStateCooldownLeft = 0f;

        public ChosenUndeadGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ChangeState(State state)
        {
            nextState = state;
            nextState.Initialise();
            ChangeStateCooldownLeft = ChangeStateCooldown;
        }

        public void ChangeLevel(LevelTransition transition)
        {
            var level = Levels[transition.LevelIndex - 1];
            level.spawnpointNumber = Array.IndexOf(Levels, currentState) + 1;
            
            ChangeState(level);
        }

        public void SaveCompleted(BonfireSave bonfire)
        {
            Save();
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            Art.Initialize(Content);
            Component.Content = Content;
            Map.SetLevelChanged(ChangeLevel);
            Map.SetSaveCompleted(SaveCompleted);
            Levels = LoadLevels();
            graphics.IsFullScreen = false;
            WindowSize = new(1600, 900);

            graphics.PreferredBackBufferWidth = WindowSize.X;
            graphics.PreferredBackBufferHeight = WindowSize.Y;

            graphics.ApplyChanges();

            base.Initialize();
        }

        private PlayState[] LoadLevels()
        {
            return new PlayState[]
            {
                new Level1(this, Content),
                new Level2(this, Content),
                new Level3(this, Content)
            };
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState = new StartMenu(this, Content);
            camera = new Camera(WindowSize);
        }

        protected override void Update(GameTime gameTime)
        {
            ChangeStateCooldownLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (nextState != null)
                (currentState, nextState) = (nextState, null);

            if (ChangeStateCooldownLeft <= 0)
            {
                InputManager.Update(gameTime);

                currentState.Update(gameTime);

                currentState.PostUpdate(gameTime);

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
            }
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (ChangeStateCooldownLeft <= 0)
                currentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }

        public void Save()
        {
            var player = Player.GetInstance();

            var data = new PlayerData()
            {
                X = (int)player.Position.X,
                Y = (int)player.Position.Y,
                PlayerLevelIndex = Array.IndexOf(Levels, currentState)
            };
            var jsonStringSave = JsonSerializer.Serialize(data);
            File.WriteAllText(saveFile, jsonStringSave);
        }

        public void LoadSave()
        {
            var player = Player.GetInstance();

            if (!File.Exists(saveFile))
            {
                var playerData = new PlayerData()
                {
                    X = -24,
                    Y = 154,
                    PlayerLevelIndex = 0
                };
                var jsonSave = JsonSerializer.Serialize(playerData);
                File.WriteAllText(saveFile, jsonSave);
            }

            var jsonString = File.ReadAllText(saveFile);
            var data = JsonSerializer.Deserialize<PlayerData>(jsonString);
            ChangeState(Levels[data.PlayerLevelIndex]);
            player.Position = new Vector2(data.X, data.Y);
        }

        public void DeleteSave()
        {
            File.Delete(saveFile);
        }
    }
}