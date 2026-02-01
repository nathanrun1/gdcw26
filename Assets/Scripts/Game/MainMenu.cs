using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace Game
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private string _firstLevelName = "Level1";
        [SerializeField] private int _firstLevelIndex = 1;
    
        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            {
                StartGame();
            }
        
            if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                StartGame();
            }
        
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                StartGame();
            }
        }
    
        private void StartGame()
        {
            PlayerPrefs.SetInt("ShowLevelTransition", 1);
            SceneManager.LoadScene(_firstLevelIndex);
        }
    }
}