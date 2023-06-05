using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public static class Sound
    {
        private static ContentManager content;

        private const string playerPath = "Sounds/Player/";

        public static void Initialize(ContentManager content)
        {
            Sound.content = content;
        }

        public static SoundEffect GetPlayerSound(string soundName) =>
            content.Load<SoundEffect>($"{playerPath}{soundName}");
    }
}
