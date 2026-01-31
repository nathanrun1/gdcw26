using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Types
{
    public enum MaskColor
    {
        Default,
        Red,
        Blue,
        Green
    }
    
    public static class MaskColorExtensions
    {
        private static readonly Dictionary<MaskColor, Color> maskColorRGB = new()
        {
            { MaskColor.Default , Color.gray},
            { MaskColor.Red, Color.red },
            { MaskColor.Blue, Color.blue },
            { MaskColor.Green, Color.green }
        };
        
        /// <summary>
        /// Retrieves the actual 'Color' value associated with this mask color
        /// </summary>
        public static Color GetColor(this MaskColor maskColor)
        {
            return maskColorRGB[maskColor];
        }

        /// <summary>
        /// Linearly interpolates between this mask color's actual 'Color' value and white 
        /// </summary>
        public static Color GetSoftColor(this MaskColor maskColor)
        {
            return Color.Lerp(maskColor.GetColor(), Color.white, 0.5f);
        }
    }
}