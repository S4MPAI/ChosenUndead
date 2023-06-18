using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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

    public static class EntityManager
    {
        public static Player Player { get; private set; }

        private static List<Enemy> enemies;
        private static HashSet<Enemy> removedEnemy;
        private static List<Bullet> bullets; 
        private static List<NPC> npcs;

        static EntityManager()
        {
            enemies = new();
            npcs = new();
            bullets = new();
            removedEnemy = new();
            Player = Player.GetInstance();
        }

        public static Enemy AddEnemy(Map map, EnemyType type)
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


        public static void AddNPC(NPC npc) => npcs.Add(npc);

        public static void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
            foreach (var enemy in enemies)
                enemy.Draw(spriteBatch);
            foreach (var bullet in bullets)
                bullet.Draw(spriteBatch);
            foreach (var npc in npcs)
                npc.Draw(spriteBatch);
        }

        public static void Update()
        {
            Player?.Update();

            for (int i = 0; i < enemies.Count; i++)
                enemies[i].Update();

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();
                if (bullets[i].FlightTime > 20)
                {
                    bullets.Remove(bullets[i]);
                    i--;
                }

            }

            foreach (var npc in npcs)
                npc.Update();

            foreach (var enemy in enemies)
                CheckEntityCollision(Player, enemy);

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].Rectangle.Intersects(Player.HitBox))
                {
                    Player.AddHp(-bullets[i].Damage, true);
                    bullets.Remove(bullets[i]);
                    i--;
                }
            }

            foreach (var enemy in removedEnemy)
                enemies.Remove(enemy);

            //if (Player.IsDead())
            //    Player = null;
        }

        private static void CheckEntityCollision(Entity entity1, Entity entity2)
        {
            MakeAttack(entity1, entity2);
            MakeAttack(entity2, entity1);

            if (entity2.IsDeadFull() && entity2 is Enemy enemy2)
                removedEnemy.Add(enemy2);

            if (entity1.IsDeadFull() && entity1 is Enemy enemy1)
                removedEnemy.Add(enemy1);
        }

        private static void MakeAttack(Entity attackEntity, Entity entity)
        {
            if (attackEntity.IsAttacking && attackEntity.AttackBox.Intersects(entity.HitBox))
                entity.AddHp(-attackEntity.Damage);
        }

        public static void Clear()
        {
            enemies = new();
            npcs = new();
            removedEnemy = new();
            bullets = new();
            Player = Player.GetInstance();
        }

        internal static void AddBullet(Bullet bullet) => bullets.Add(bullet);
    }
}
