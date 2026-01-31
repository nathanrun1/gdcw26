using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Utilities.Types;

namespace Managers
{
    /// <summary>
    /// Manages the mask color
    /// </summary>
    public class MaskManager : Singleton<MaskManager>
    {
        public MaskColor currentColor = MaskColor.White;

        public event Action<MaskColor> onColorChange;

        public void ChangeMaskColor(MaskColor newColor)
        {
            currentColor = newColor;
            onColorChange?.Invoke(newColor);
        }
    }
}