using Game;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.Types;

public class Lv4TutorialMaskManager : MonoBehaviour
{
    [SerializeField] private TutorialText _tutorialTextCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MaskManager.Instance.onLastMask += onLastMask;
    }

    private void onLastMask()
    {
        InputManager.Instance.playerInput.Player.Space.performed += OnInputSpace;
        _tutorialTextCanvas.SetTutorialMessage("Press SPACE again to take off your mask when your queue is empty!");
    }

    private void OnInputSpace(InputAction.CallbackContext _)
    {
        _tutorialTextCanvas.SetTutorialMessage("");
    }
}
