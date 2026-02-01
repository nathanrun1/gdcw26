using Game;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.Types;

public class Lv1TutorialMaskManager : MonoBehaviour
{
    [SerializeField] private TutorialText _tutorialTextCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputManager.Instance.playerInput.Player.Space.performed += OnInputSpace;
        MaskManager.Instance.onMaskEnqueue += OnMaskEnqueue;
        // Initial tutorial message
        _tutorialTextCanvas.SetTutorialMessage("Move right to pick up the blue mask!");
    }

    private void OnMaskEnqueue(MaskColor maskColor)
    {
        _tutorialTextCanvas.SetTutorialMessage("You've queued up a mask! Press SPACE to wear it!");
    }

    

    private void OnInputSpace(InputAction.CallbackContext _)
    {
        _tutorialTextCanvas.SetTutorialMessage("Grab your loot!!");
    }
}
