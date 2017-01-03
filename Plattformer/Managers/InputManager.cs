﻿using Microsoft.Xna.Framework.Input;
using Plattformer.Screens;

namespace Plattformer.Managers
{
    public class InputManager
    {
        private KeyboardState currentKeyState, previousKeyState;

        private static InputManager instance;

        public static InputManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new InputManager();
                return instance;
            }
        }
        private InputManager()
        {
            
        }

        public void Update()
        {
            previousKeyState = currentKeyState;
            if (!ScreenManager.Instance.IsTransitioning)
                currentKeyState = Keyboard.GetState();
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (currentKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (currentKeyState.IsKeyUp(key) && previousKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }
    }
}
