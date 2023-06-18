using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using Newtonsoft.Json;

namespace ChosenUndead
{
    public class ChosenUndeadGame : Game
    {
        private const string saveFile = "../../../Content/Data/PlayerSaves/save.json"; 

        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        public static Point WindowSize = new(1600, 900);

        public State currentState;

        private State nextState;

        public bool isPause = false;

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
            currentState.Exit();
            nextState = state;
            nextState.Initialize();
            ChangeStateCooldownLeft = ChangeStateCooldown;
        }

        public void ChangeLevel(LevelTransition transition)
        {
            var currentLevel = currentState as PlayState;
            currentLevel.SaveChanges();
            Save();
            var level = Levels[transition.LevelIndex - 1];
            level.spawnpointNumber = Array.IndexOf(Levels, currentLevel) + 1;
            
            ChangeState(level);
            var player = Player.GetInstance();
            player.Reset();
        }

        public void SaveCompleted(BonfireSave bonfire)
        {
            Save();
            if (currentState is PlayState play)
                play.SaveChanges();
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            Art.Initialize(Content);
            Sound.Initialize(Content);
            camera = new Camera(WindowSize, 4f);
            Map.SetLevelChanged(ChangeLevel);
            Map.SetSaveCompleted(SaveCompleted);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = WindowSize.X;
            graphics.PreferredBackBufferHeight = WindowSize.Y;

            graphics.ApplyChanges();

            base.Initialize();
        }

        private PlayState[] LoadLevels()
        {
            var windowSize = camera.VisionWindowSize;

            return new PlayState[]
            {
                new PlayState(this, Content, 1, Art.GetForestBackgrounds(windowSize), "light1"),
                new PlayState(this, Content, 2, null, "light1"),
                new PlayState(this, Content, 3, Art.GetForestBackgrounds(windowSize), "light1")
            };
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState = new StartMenu(this, Content);
            
            Levels = LoadLevels();
        }

        protected override void Update(GameTime gameTime)
        {
            Time.Update(gameTime);
            ChangeStateCooldownLeft -= Time.ElapsedSeconds;

            if (nextState != null)
                (currentState, nextState) = (nextState, null);

            if (ChangeStateCooldownLeft <= 0)
            {
                Input.Update();

                currentState.Update();

                //if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                  //  Exit();
            }
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (ChangeStateCooldownLeft <= 0)
                currentState.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        public void Save()
        {
            var player = Player.GetInstance();

            var data = new PlayerData()
            {
                X = (int)player.Position.X,
                Y = (int)player.Position.Y,
                PlayerLevelIndex = Array.IndexOf(Levels, currentState),
                AttackBuffCount = player.AttackBuffCount,
                VitalityBuffCount = player.VitalityBuffCount,
                MaxHealingQuartz = player.MaxHealingQuartz
            };
            var jsonStringSave = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(saveFile, jsonStringSave);
        }

        public void LoadSave(bool isNewSave = false)
        {
            var player = Player.GetInstance();

            if (isNewSave)
            {
                var playerData = new PlayerData()
                {
                    X = -32,
                    Y = 380,
                    PlayerLevelIndex = 0,
                    MaxHealingQuartz = 1
                };
                var jsonSave = JsonConvert.SerializeObject(playerData, Formatting.Indented);
                File.WriteAllText(saveFile, jsonSave);

                foreach (var level in Levels)
                    level.ClearProgress();
            }

            var jsonString = File.ReadAllText(saveFile);
            var data = JsonConvert.DeserializeObject<PlayerData>(jsonString);

            if (isNewSave)
                ChangeState(new TrainingState(this, Content));
            else
                ChangeState(Levels[data.PlayerLevelIndex]);

            player.Position = new Vector2(data.X, data.Y);
            player.SetInventory(data.AttackBuffCount, data.VitalityBuffCount, data.MaxHealingQuartz, data.Keys);
            player.Reset();
            
        }

        public void SetFirstLevel() => ChangeState(Levels[0]);
    }
}