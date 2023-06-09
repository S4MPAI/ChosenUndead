﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class StateMachine
    {
        private PlayerState CurrentState;

        public void Initialize(PlayerState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            CurrentState.Draw(spriteBatch);
        }

        public virtual void Update()
        {
            CurrentState.Update();
        }
    }
}
