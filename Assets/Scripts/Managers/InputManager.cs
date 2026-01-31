using UnityEngine.InputSystem;
using Utilities;

namespace Managers
{
    public class InputManager : PersistentSingleton<InputManager>
    {
        public PlayerInput playerInput;
        
        protected override void Awake()
        {
            base.Awake();
            if (_otherInstanceExists) return;
            playerInput = new PlayerInput();
            playerInput.Player.Enable();
            playerInput.UI.Enable();
        }
    }
}