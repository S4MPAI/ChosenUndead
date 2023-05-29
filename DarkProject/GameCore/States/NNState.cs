using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    internal class NNState : PlayState
    {
        Random random = new Random();

        public NNState(ChosenUndeadGame game, ContentManager content) : base(game, content, 99, null)
        {
        }

        private float allGameTime = 15f;
        private float maxGameTime = 15f;
        private List<Enemy> allEntity => map.entityManager.enemies.Concat(map.entityManager.removedEnemy).ToList();

        public override void Initialize()
        {
            base.Initialize();
            var enemies = map.entityManager.enemies;
            for (int i = 0; i < enemies.Count; i += 2)
            {
                enemies[i].ChangeTarget(enemies[i + 1]);
                enemies[i + 1].ChangeTarget(enemies[i]);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            allGameTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (map.entityManager.enemies.Count <= 30 || allGameTime < 0)
            {
                allGameTime = maxGameTime;

                var nns = NeuralNetworkManager.SortNetworks(allEntity).OrderBy(x => random.NextDouble()).ToList();
                Initialize();
                for (int i = 0; i < allEntity.Count; i++)
                    allEntity[i].brain = nns[i];
            }
        }
    }
}
