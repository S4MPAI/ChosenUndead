using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public static class InputManager
    {
        public static Input Input { get; set; } = new();

        public static bool LeftPressed { get; private set; }

        public static bool RightPressed { get; private set; }

        public static bool JumpPressed { get; private set; }

        public static bool InteractionPressed { get; private set; }

        public static bool AttackPressed { get; private set; }

        public static void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            LeftPressed = keyboardState.IsKeyDown(Input.LeftKey);
            RightPressed = keyboardState.IsKeyDown(Input.RightKey);
            JumpPressed = keyboardState.IsKeyDown(Input.JumpKey);
            InteractionPressed = keyboardState.IsKeyDown(Input.InteractionKey);
            AttackPressed = mouseState.LeftButton == ButtonState.Pressed;
            if (AttackPressed == true)
            {

            }
        }
    }
}
