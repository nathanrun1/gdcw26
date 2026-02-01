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
            OnLevelLoaded();
            SceneManager.sceneLoaded += (_, _) => OnLevelLoaded();
        }

        private static void OnLevelLoaded()
        {
            InputManager.Instance.playerInput.Player.RefreshLevel.performed += OnInputRestartLevel;
        }

        private static void OnInputRestartLevel(InputAction.CallbackContext _)
        {
            RestartLevel();
        }
        
        /// <summary>
        /// Restarts the current level
        /// </summary>
        public static void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// Activates the player loss condition
        /// </summary>
        public static void PlayerLoss()
        {
            AudioManager.Instance.PlayPlayerDeath();
            RestartLevel();
        }
    }
}