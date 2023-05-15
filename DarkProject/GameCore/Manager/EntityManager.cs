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

        private List<Entity> enemies;

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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Player.Draw(gameTime, spriteBatch);
            foreach (var entity in enemies)
                entity.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                MakeAttack(Player, enemies[i]);
                MakeAttack(enemies[i], Player);

                if (enemies[i].IsDead())
                    enemies.Remove(enemies[i]);
            }

            if (Player.IsDead())
                Player = null;

            Player?.Update(gameTime);
            foreach(var entity in enemies)
                entity.Update(gameTime);
        }

        private void MakeAttack(Entity attackEntity, Entity entity)
        {
            if (attackEntity.IsAttacking && attackEntity.AttackBox.Intersects(entity.HitBox))
                entity.GiveDamage(attackEntity.Damage);
        }
    }
}
