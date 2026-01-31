using System.ComponentModel;
using UnityEngine;

namespace Utilities.Types
{
    public enum MonitorDirection
    {
        North,
        South,
        West,
        East
    }

    public static class MonitorDirectionExtensions
    {
        public static Vector2 ToVector2(this MonitorDirection monitorDirection)
        {
            return monitorDirection switch
            {
                MonitorDirection.East => new(1, 0),
                MonitorDirection.West => new(-1, 0),
                MonitorDirection.North => new(0, 1),
                MonitorDirection.South => new(0, -1),
                _ => throw new InvalidEnumArgumentException($"Invalid monitor direction: {monitorDirection}")
            };
        }
    }
}