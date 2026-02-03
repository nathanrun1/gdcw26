using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Utilities;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public event Action onGameCompleted;
            
        public float gameStartTime;
        public float levelStartTime;

        [SerializeField] private string _firstLevelScene;
        [SerializeField] private string _gameCompleteScene;
        
        protected void Start()
        {
            OnLevelLoaded();
            SceneManager.sceneLoaded += (_, _) => OnLevelLoaded();
        }

        private void OnLevelLoaded()
        {
            InputManager.Instance.playerInput.Player.RefreshLevel.performed += OnInputRestartLevel;
            levelStartTime = Time.unscaledTime;
            if (SceneManager.GetActiveScene().name == _firstLevelScene) gameStartTime = Time.unscaledTime;
            if (SceneManager.GetActiveScene().name == _gameCompleteScene) onGameCompleted?.Invoke();
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