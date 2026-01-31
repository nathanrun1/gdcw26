using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Utilities;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        protected void Start()
        {
            InputManager.Instance.playerInput.Player.RefreshLevel.performed += OnInputRestartLevel;
        }

        private static void OnInputRestartLevel(InputAction.CallbackContext _)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}