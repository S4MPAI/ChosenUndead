using ChosenUndead.GameCore.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Level1 : PlayState
    {
        public Level1(ChosenUndeadGame game, ContentManager content) : base(game, content, 1)
        {

        }
    }

    public class Level2 : PlayState
    {
        public Level2(ChosenUndeadGame game, ContentManager content) : base(game, content, 2)
        {

        }
    }

    public class Level3 : PlayState
    {
        public Level3(ChosenUndeadGame game, ContentManager content) : base(game, content, 3)
        {

        }
    }
}
