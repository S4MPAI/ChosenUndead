using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class EntityManager
    {
        private static readonly AnimationManager _playerAnimations = new ();
        private static readonly string _playerPath = "Entities/Player/";
        private Map _map { get; }

        public EntityManager(Map map)
        {
            _map = map;
        }

        public static void Initialize(ContentManager content)
        {
            _playerAnimations.AddAnimation("Idle", new Animation(content.Load<Texture2D>($"{_playerPath}Idle"), 128, 64, 8, 0.125f));
            _playerAnimations.AddAnimation("Run", new Animation(content.Load<Texture2D>($"{_playerPath}Run"), 128, 64, 8, 0.125f));
            _playerAnimations.AddAnimation("Jump", new Animation(content.Load<Texture2D>($"{_playerPath}Jump"), 128, 64, 8, 0.125f));
        }

        public Player GetPlayer() => Player.GetInstance(_map,_playerAnimations, 32, 64);
    }
}
