using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class EntityManager
    {
        public Player Player { get; private set; }

        private List<Enemy> enemies;
        private List<NPC> npcs;

        public EntityManager()
        {
            enemies = new();
            Player = Player.GetInstance();
        }

        public Sceleton AddSceleton(Map map) 
        {
            var sceleton = new Sceleton(map, 32, 64);
            enemies.Add(sceleton);
            return sceleton;
        } 

        public void AddEntity(Entity entity)
        {
            if (entity is NPC npc)
                npcs.Add(npc);
            else if (entity is Enemy enemy)
                enemies.Add(enemy);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Player.Draw(gameTime, spriteBatch);
            foreach (var entity in enemies)
                entity.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            Player?.Update(gameTime);
            foreach (var entity in enemies)
                entity.Update(gameTime);

            for (int i = 0; i < enemies.Count; i++)
            {
                MakeAttack(Player, enemies[i]);
                MakeAttack(enemies[i], Player);

                if (enemies[i].IsDead())
                    enemies.Remove(enemies[i]);
            }

            //if (Player.IsDead())
            //    Player = null;
        }

        private void MakeAttack(Entity attackEntity, Entity entity)
        {
            if (attackEntity.IsAttacking && attackEntity.AttackBox.Intersects(entity.HitBox))
                entity.GiveDamage(attackEntity.Damage);
        }
    }
}
