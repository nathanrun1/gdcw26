using UnityEngine;

namespace Utilities
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Returns the Vector2 resulting from rotating this vector by the given angle in radians
        /// </summary>
        public static Vector2 Rotate(this Vector2 vec, float angle)
        {
            float sin = Mathf.Sin(angle);
            float cos = Mathf.Cos(angle);
            float tx = vec.x;
            float ty = vec.y;

            vec.x = cos * tx - sin * ty;
            vec.y = sin * tx + cos * ty;

            return vec;
        }
    }
}