using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public static class EntityManager
    {
        private static readonly AnimationManager<object> playerAnimations = new ();
        private static readonly string _playerPath = "Entities/Player/";
        private static Map map { get; set; }

        public static void Initialize(ContentManager content)
        {
            playerAnimations.AddAnimation(EntityAction.Idle, new Animation(content.Load<Texture2D>($"{_playerPath}Idle"), 128, 64, 8, 0.125f));
            playerAnimations.AddAnimation(EntityAction.Run, new Animation(content.Load<Texture2D>($"{_playerPath}Run"), 128, 64, 8, 0.125f));
            playerAnimations.AddAnimation(EntityAction.Jump, new Animation(content.Load<Texture2D>($"{_playerPath}Jump"), 128, 64, 8, 0.125f));
            playerAnimations.AddAnimation(WeaponAttack.FirstAttack, new Animation(content.Load<Texture2D>($"{_playerPath}FirstAttack"), 128, 64, 7, 0.125f));
            playerAnimations.AddAnimation(WeaponAttack.SecondAttack, new Animation(content.Load<Texture2D>($"{_playerPath}SecondAttack"), 128, 64, 3, 0.125f));
            playerAnimations.AddAnimation(WeaponAttack.ThirdAttack, new Animation(content.Load<Texture2D>($"{_playerPath}ThirdAttack"), 128, 64, 4, 0.125f));
            playerAnimations.AddAnimation(WeaponAttack.FourthAttack, new Animation(content.Load<Texture2D>($"{_playerPath}FourthAttack"), 128, 64, 6, 0.125f));
        }

        public static void SetMap(Map map) => EntityManager.map = map;

        public static Player GetPlayer() => Player.GetInstance(map,playerAnimations, 32, 64);
    }
}
