using System;
using System.Collections.Generic;
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

        public event Action<MaskColor> onColorChange;

        /// <summary>
        /// Changes the global mask color to the given color
        /// </summary>
        /// <param name="newColor"></param>
        public void ChangeMaskColor(MaskColor newColor)
        {
            _curMask = newColor;
            _mainCamera.backgroundColor = Color.Lerp(newColor.GetRGB(), Color.white, 0.5f);
            onColorChange?.Invoke(newColor);
            Debug.Log($"New color: {newColor}");
        }

        /// <summary>
        /// Retrieve the current global mask color
        /// </summary>
        /// <returns></returns>
        public MaskColor GetMaskColor()
        {
            return _curMask;
        }
    }
}