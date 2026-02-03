using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpeedrunClockUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _allTime;
        [SerializeField] private TextMeshProUGUI _levelTime;

        private void Start()
        {
            UpdateTimers();
        }

        private void Update()
        {
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            _allTime.text = "ALL: " + TimeSpan.FromMilliseconds((Time.unscaledTime - GameManager.Instance.gameStartTime) * 1000f).ToString(@"mm\:ss\:fff");
            _levelTime.text = "LEV: " + TimeSpan.FromMilliseconds((Time.unscaledTime - GameManager.Instance.levelStartTime) * 1000f).ToString(@"mm\:ss\:fff");
        }
    }
}