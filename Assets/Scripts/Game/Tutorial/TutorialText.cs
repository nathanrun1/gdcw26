using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class TutorialText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tutorialText;


        private void Start()
        {
        }

        private void Update()
        {
            
        }
        
        public void SetTutorialMessage(string message)
        {
            _tutorialText.text = message;
        }
    }
}