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
    public enum EnemyType
    {
        Sceleton,
        Goblin
    }

    public class EntityManager
    {
        public Player Player { get; private set; }

        public List<Enemy> enemies;
        public HashSet<Enemy> removedEnemy;
        private List<NPC> npcs;

        public EntityManager()
        {
            enemies = new();
            npcs = new();
            removedEnemy = new();
            Player = Player.GetInstance();
        }

        public Enemy AddEnemy(Map map, EnemyType type)
        {
            Enemy enemy = null;

            switch (type)
            {
                case EnemyType.Sceleton:
                    enemy = new Sceleton(map);
                    break;
                case EnemyType.Goblin:
                    enemy = new Goblin(map);
                    break;
            }

            enemies.Add(enemy);
            return enemy;
        }


        public void AddNPC(NPC npc) => npcs.Add(npc);

        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
            foreach (var enemy in enemies)
                enemy.Draw(spriteBatch);
            foreach (var npc in npcs)
                npc.Draw(spriteBatch);
        }

        public void Update()
        {
            Player?.Update();

            for (int i = 0; i < enemies.Count; i++)
                enemies[i].Update();



            foreach (var npc in npcs)
                npc.Update();

            //foreach (var enemy in enemies)
            //    CheckEntityCollision(Player, enemy);

            for (int i = 0; i < enemies.Count - 1; i++)
                for (int j = i + 1; j < enemies.Count; j++)
                {
                    CheckEntityCollision(enemies[i], enemies[j]);
                }
                    

            foreach (var enemy in removedEnemy)
                enemies.Remove(enemy);

            //if (Player.IsDead())
            //    Player = null;
        }

        private void CheckEntityCollision(Entity entity1, Entity entity2)
        {
            MakeAttack(entity1, entity2);
            MakeAttack(entity2, entity1);

            if (entity2.IsDeadFull() && entity2 is Enemy enemy2)
                removedEnemy.Add(enemy2);

            if (entity1.IsDeadFull() && entity1 is Enemy enemy1)
                removedEnemy.Add(enemy1);
        }

        private void MakeAttack(Entity attackEntity, Entity entity)
        {
            if (attackEntity.IsAttacking && attackEntity.AttackBox.Intersects(entity.HitBox))
                entity.GiveDamage(attackEntity.Damage);
        }
    }
}
