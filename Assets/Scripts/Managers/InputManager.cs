using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Utilities;

namespace Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public PlayerInput playerInput;
        
        protected override void Awake()
        {
            base.Awake();
            if (_otherInstanceExists) return;
            InitPlayerInput();
        }

        private void OnDestroy()
        {
            DeInitPlayerInput();
        }

        private void InitPlayerInput()
        {
            playerInput = new PlayerInput();
            playerInput.Player.Enable();
            playerInput.UI.Enable();
        }

        private void DeInitPlayerInput()
        {
            playerInput.Player.Disable();
            playerInput.UI.Disable();
        }
    }
}