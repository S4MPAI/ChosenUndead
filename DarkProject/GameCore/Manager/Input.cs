using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public static class Input
    {
        public static InputKeys KeyboardInput { get; set; } = new();

        public static bool LeftPressed { get; private set; }

        public static bool RightPressed { get; private set; }

        public static bool JumpPressed { get; private set; }

        public static bool InteractionPressed { get; private set; }

        public static bool AttackPressed { get; private set; }

        public static void Update()
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            LeftPressed = keyboardState.IsKeyDown(KeyboardInput.LeftKey);
            RightPressed = keyboardState.IsKeyDown(KeyboardInput.RightKey);
            JumpPressed = keyboardState.IsKeyDown(KeyboardInput.JumpKey);
            InteractionPressed = keyboardState.IsKeyDown(KeyboardInput.InteractionKey);
            AttackPressed = mouseState.LeftButton == ButtonState.Pressed;
            if (AttackPressed == true)
            {

            }
        }
    }
}
