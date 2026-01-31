using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Utilities.Types
{
    public static class MaskColorExtensions
    {
        private static readonly Dictionary<MaskColor, Color> maskColorRGB = new Dictionary<MaskColor, Color>
        {
            { MaskColor.White , Color.white},
            { MaskColor.Red, Color.red },
            { MaskColor.Blue, Color.blue }
        };
        
        public static Color GetRGB(this MaskColor maskColor)
        {
            return maskColorRGB[maskColor];
        }
    }
}