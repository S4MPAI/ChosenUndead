﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Audio;

namespace ChosenUndead
{
    public class PlayState : State
    {
        protected Player player;
        private readonly int levelNumber;
        private readonly List<ScrollingBackground> backgrounds;
        protected readonly Map map = new();
        private string savePath => $"../../../Content/Data/LevelsData/level{levelNumber}.json";
        private string mapPath => $"../../../Content/Maps/{levelNumber}.txt";
        private LevelData levelData;

        private static Pause pause;

        public int spawnpointNumber;

        public SoundEffectInstance sound;

        public PlayState(ChosenUndeadGame game, ContentManager content, int levelNumber, List<ScrollingBackground> backgrounds, string soundName = null) : base(game, content)
        {
            this.levelNumber = levelNumber;
            this.backgrounds = backgrounds;
            pause = new Pause(game, content);

            if (soundName != null) sound = Sound.GetStateSound(soundName);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.Immediate, transformMatrix: game.camera.Transform);

            if (backgrounds != null )
                foreach (var bg in backgrounds)
                    bg.Draw(spriteBatch, game.camera.WindowPos);

            map.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(sortMode: SpriteSortMode.Immediate);

            PlayerInterface.Draw(spriteBatch);

            spriteBatch.End();

            if (game.isPause)
                pause.Draw(spriteBatch);
        }
        
        public override void Update()
        {
            sound?.Play();
            game.isPause = Input.EscapePressed ? !game.isPause : game.isPause;

            if (game.isPause)
            {
                pause.Update();
                return;
            }

            map.Update();
            game.camera.Follow(player, map);

            if (backgrounds != null)
                foreach (var bg in backgrounds)
                    bg.Update();

            if (player.IsDeadFull())
                game.ChangeState(new DeathState(game, content));

            PlayerInterface.Update();
        }

        public override void Initialize()
        {
            base.Initialize();
            

            using var fileStream = new StreamReader(mapPath); 
            Art.SetMap(map);
            map.Generate(fileStream, 32, spawnpointNumber);
            

            if (File.Exists(savePath))
            {
                levelData = LoadLevelData();

                var npcS = levelData.Npcs.Select(x => new NPC(map, x.Name, x.Phrases)).ToArray();
                for (int i = 0; i < npcS.Length; i++)
                    npcS[i].Position = new(levelData.Npcs[i].X, levelData.Npcs[i].Y);

                map.AddNPCs(npcS.ToArray());
                map.SetChestsStates(levelData.Chests);
            }
            
            player = Player.GetInstance(map);
        }

        public void SaveChanges()
        {
            if (!File.Exists(savePath)) return;

            var chests = map.Decorations.OfType<Chest>().Select(chest => new ChestData(chest.Position.X, chest.Position.Y, chest.IsOpen)).ToList();

            var levelData = new LevelData(chests, this.levelData.Npcs);
            var jsonStringSave = JsonConvert.SerializeObject(levelData, Formatting.Indented);
            File.WriteAllText(savePath, jsonStringSave);
        }

        public LevelData LoadLevelData()
        {
            var jsonString = File.ReadAllText(savePath);
            var data = JsonConvert.DeserializeObject<LevelData>(jsonString);

            return data;
        }

        public void ClearProgress()
        {
            var jsonString = File.ReadAllText(savePath);
            var data = JsonConvert.DeserializeObject<LevelData>(jsonString);

            data.Chests.Clear();
            jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(savePath, jsonString);
        }

        public override void Exit()
        {
            base.Exit();
            sound?.Stop();
        }
    }
}
