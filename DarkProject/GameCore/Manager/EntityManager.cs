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
        private static AnimationManager<object> playerAnimations;

        private static AnimationManager<object> sceletonAnimations;

        private const string playerPath = "Entities/Player/";

        private const string sceletonPath = "Entities/Sceleton/";
        public Player Player { get; private set; }

        private List<Entity> enemies;
        
        private List<Entity> deadEnemies;

        #region Initialize

        public static void Initialize(ContentManager content)
        {
            playerAnimations = GetPlayerAnimations(content);
            sceletonAnimations = GetSceletonAnimations(content);
        }

        public EntityManager()
        {
            enemies = new();
            deadEnemies = new();
        }

        private static AnimationManager<object> GetPlayerAnimations(ContentManager content)
        {
            var playerAnimations = new AnimationManager<object>();

            playerAnimations.AddAnimation(EntityAction.Idle, new Animation(content.Load<Texture2D>($"{playerPath}Idle"), 128, 64, 8, 0.125f));
            playerAnimations.AddAnimation(EntityAction.Run, new Animation(content.Load<Texture2D>($"{playerPath}Run"), 128, 64, 8, 0.125f));
            playerAnimations.AddAnimation(EntityAction.Jump, new Animation(content.Load<Texture2D>($"{playerPath}Jump"), 128, 64, 8, 0.125f));
            playerAnimations.AddAnimation(WeaponAttack.FirstAttack, new Animation(content.Load<Texture2D>($"{playerPath}FirstAttack"), 128, 64, 7, 0.125f));
            playerAnimations.AddAnimation(WeaponAttack.SecondAttack, new Animation(content.Load<Texture2D>($"{playerPath}SecondAttack"), 128, 64, 3, 0.125f));
            playerAnimations.AddAnimation(WeaponAttack.ThirdAttack, new Animation(content.Load<Texture2D>($"{playerPath}ThirdAttack"), 128, 64, 4, 0.125f));
            playerAnimations.AddAnimation(WeaponAttack.FourthAttack, new Animation(content.Load<Texture2D>($"{playerPath}FourthAttack"), 128, 64, 6, 0.125f));

            return playerAnimations;
        }

        private static AnimationManager<object> GetSceletonAnimations(ContentManager content)
        {
            var sceletonAnimations = new AnimationManager<object>();

            sceletonAnimations.AddAnimation(EntityAction.Idle, new Animation(content.Load<Texture2D>($"{sceletonPath}Idle"), 150, 64, 4, 0.2f));
            sceletonAnimations.AddAnimation(EntityAction.Death, new Animation(content.Load<Texture2D>($"{sceletonPath}Death"), 150, 64, 4, 0.8f));
            //playerAnimations.AddAnimation(EntityAction.Run, new Animation(content.Load<Texture2D>($"{playerPath}Run"), 128, 64, 8, 0.125f));
            //playerAnimations.AddAnimation(EntityAction.Jump, new Animation(content.Load<Texture2D>($"{playerPath}Jump"), 128, 64, 8, 0.125f));
            //playerAnimations.AddAnimation(WeaponAttack.FirstAttack, new Animation(content.Load<Texture2D>($"{playerPath}FirstAttack"), 128, 64, 7, 0.125f));
            //playerAnimations.AddAnimation(WeaponAttack.SecondAttack, new Animation(content.Load<Texture2D>($"{playerPath}SecondAttack"), 128, 64, 3, 0.125f));
            //playerAnimations.AddAnimation(WeaponAttack.ThirdAttack, new Animation(content.Load<Texture2D>($"{playerPath}ThirdAttack"), 128, 64, 4, 0.125f));
            //playerAnimations.AddAnimation(WeaponAttack.FourthAttack, new Animation(content.Load<Texture2D>($"{playerPath}FourthAttack"), 128, 64, 6, 0.125f));

            return sceletonAnimations;
        }

        #endregion

        public Player AddPlayer(Level map) 
        {
            var player = Player.GetInstance(map, playerAnimations, 32, 32);
            Player = player;
            return player;
        }

        public Sceleton AddSceleton(Level map) 
        {
            var sceleton = new Sceleton(map, sceletonAnimations, 32, 64);
            enemies.Add(sceleton);
            return sceleton;
        } 

        public void RemoveDeadEnemies()
        {
            deadEnemies = new();
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
                {
                    deadEnemies.Add(enemies[i]);
                    enemies.Remove(enemies[i]);
                }
            }

            if (Player.IsDead())
                Player = null;

            deadEnemies.Clear();

            Player?.Update(gameTime);
            foreach(var entity in enemies)
                entity.Update(gameTime);
        }

        //public void AddEntity(Entity entity)
        //{

        //}

        private void MakeAttack(Entity attackEntity, Entity entity)
        {
            if (attackEntity.IsAttacking && attackEntity.AttackBox.Intersects(entity.HitBox))
                entity.GiveDamage(attackEntity.Damage);
        }
    }
}
