using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class PlayState : State
    {
        protected Player player;
        private int levelNumber;

        protected readonly Map map = new();

        public int spawnpointNumber;

        public PlayState(ChosenUndeadGame game, ContentManager content, int levelNumber) : base(game, content)
        {
            this.levelNumber = levelNumber;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.Immediate, transformMatrix: game.camera.Transform);

            map.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            map.Update(gameTime);
            game.camera.Follow(player, map);
        }

        public override void Initialize()
        {
            base.Initialize();
            var mapPath = $"Content/Maps/{levelNumber}.txt";
            using var fileStream = TitleContainer.OpenStream(mapPath);
            map.Generate(fileStream, 24, spawnpointNumber);
            player = Player.GetInstance(map);
        }
    }
}
