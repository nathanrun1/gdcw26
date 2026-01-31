using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Types;

namespace UI
{
    public class MaskQueueUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image _maskPrefab;

        private readonly Queue<Image> _activeMasks = new();
        
        private void Start()
        {
            MaskManager.Instance.onMaskEnqueue += OnMaskEnqueue;
            MaskManager.Instance.onMaskDequeue += OnMaskDequeue;
        }

        // private void OnDestroy()
        // {
        //     MaskManager.Instance.onMaskEnqueue -= OnMaskEnqueue;
        //     MaskManager.Instance.onMaskDequeue -= OnMaskDequeue;
        // }

        private void OnMaskEnqueue(MaskColor maskColor)
        {
            Image newMask = Instantiate(_maskPrefab, transform);
            newMask.color = maskColor.GetSoftColor();
            newMask.transform.SetAsLastSibling();
            _activeMasks.Enqueue(newMask);
        }

        private void OnMaskDequeue()
        {
            Debug.Log("Dequeue");
            if (!_activeMasks.TryDequeue(out Image usedMask))
            {
                Debug.LogError("UI and actual mask queue out of sync!");
                return;
            }
            Destroy(usedMask.gameObject);
        }
    }
}