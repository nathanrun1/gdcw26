using Game;
using Managers;
using UnityEngine;
using Utilities.Types;

public class Lv5TutorialMaskManager : MonoBehaviour
{
    [SerializeField] private TutorialText _tutorialTextCanvas;

    void Start()
    {
        MaskManager.Instance.onMaskEnqueue += OnMaskEnqueue;
        MaskManager.Instance.onNonInitialDefaultMask += onNonInitialDefaultMask;
    }

    private void OnMaskEnqueue(MaskColor maskColor)
    {
        _tutorialTextCanvas.SetTutorialMessage("Equip the matching mask to stop a charger in its path!");
    }

    private void onNonInitialDefaultMask()
    {
        _tutorialTextCanvas.SetTutorialMessage("");
    }
}
