using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using Utilities.Types;

namespace Managers
{
    /// <summary>
    /// Manages the mask color
    /// </summary>
    public class MaskManager : Singleton<MaskManager>
    {
        [Header("References")]
        [SerializeField] private Camera _mainCamera;
        
        private MaskColor _curMask = MaskColor.Default;
        private Queue<MaskColor> _maskQueue = new();

        /// <summary>
        /// Invoked when the mask color changes
        /// </summary>
        public event Action<MaskColor> onMaskChange;
        /// <summary>
        /// Invoked when mask color enqueued to mask queue
        /// </summary>
        public event Action<MaskColor> onMaskEnqueue;
        /// <summary>
        /// Invoked when mask color dequeued from mask queue
        /// </summary>
        public event Action onMaskDequeue;

        private void Start()
        {
            InputManager.Instance.playerInput.Player.Space.performed += OnInputSpace;
            InputManager.Instance.playerInput.Player.DefaultMask.performed += OnInputDefaultMask;
            InputManager.Instance.playerInput.Player.NumberKey.performed += OnInputNumberKey;
        }

        private void OnDestroy()
        {
            onMaskEnqueue = null;
            onMaskDequeue = null;
        }

        private void OnInputSpace(InputAction.CallbackContext _)
        {
            DequeueMaskColor();
        }

        private void OnInputDefaultMask(InputAction.CallbackContext _)
        {
            ChangeMaskColor(MaskColor.Default);
        }
        
        private void OnInputNumberKey(InputAction.CallbackContext ctx)
        {
            // For debug purposes
            int number = (int)ctx.ReadValue<float>();
            MaskColor maskColor = number switch
            {
                1 => MaskColor.Default,
                2 => MaskColor.Red,
                3 => MaskColor.Green,
                4 => MaskColor.Blue,
                _ => _curMask
            };
            ChangeMaskColor(maskColor);
        }


        /// <summary>
        /// Changes the global mask color to the given color
        /// </summary>
        /// <param name="newColor"></param>
        public void ChangeMaskColor(MaskColor newColor)
        {
            if (newColor == _curMask) return;
            
            _curMask = newColor;
            if (_mainCamera != null)
            {
                _mainCamera.backgroundColor = Color.Lerp(newColor.GetColor(), Color.white, 0.5f);
            }
            
            AudioManager.Instance.PlayMaskChange();

            onMaskChange?.Invoke(newColor);
        }

        /// <summary>
        /// Retrieve the current global mask color
        /// </summary>
        /// <returns></returns>
        public MaskColor GetMaskColor()
        {
            return _curMask;
        }

        /// <summary>
        /// Queues up the given mask color into the mask queue
        /// </summary>
        /// <param name="maskColor"></param>
        public void QueueMaskColor(MaskColor maskColor)
        {
            _maskQueue.Enqueue(maskColor);
            onMaskEnqueue?.Invoke(maskColor);
        }

        /// <summary>
        /// Changes the mask color to the color at the top of the queue, if any
        /// </summary>
        public void DequeueMaskColor()
        {
            if (!_maskQueue.TryDequeue(out MaskColor newColor)) return;
            onMaskDequeue?.Invoke();
            ChangeMaskColor(newColor);
        }
    }
}