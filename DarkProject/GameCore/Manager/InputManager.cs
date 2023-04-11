using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class InputManager
    {
        public Input Input { get; }

        public bool LeftPressed { get; private set; }

        public bool RightPressed { get; private set; }

        public bool JumpPressed { get; private set; }

        public InputManager(Input input = null) 
        {
            Input = input == null ? new() : input;
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            LeftPressed = keyboardState.IsKeyDown(Input.LeftKey);
            RightPressed = keyboardState.IsKeyDown(Input.RightKey);
            JumpPressed = keyboardState.IsKeyDown(Input.Jump);
        }
    }
}
