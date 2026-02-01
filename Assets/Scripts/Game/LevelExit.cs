using System;
using UnityEngine;
using Managers;

namespace Game
{
    public class LevelExit : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _nextSceneName;
        [SerializeField] private int _nextSceneIndex = -1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject != LevelManager.Instance.playerObject) return;
            AudioManager.Instance.PlayLevelComplete();
            LoadNextScene();
        }

        private void LoadNextScene()
        {
            PlayerPrefs.SetInt("ShowLevelTransition", 1);
            if (_nextSceneIndex >= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(_nextSceneIndex);
            }
            else if (!string.IsNullOrEmpty(_nextSceneName))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(_nextSceneName);
            }
            else
            {
                Debug.LogError("No scene specified!");
            }
        }
    }
}
