using UnityEngine;

namespace Utilities
{
    public static class Math
    {
        /// <summary>
        /// Maps a value from one range to another
        /// </summary>
        /// <param name="iMin">Minimum threshold</param>
        /// <param name="iMax">Maximum threshold</param>
        /// <param name="oMin">Minimum out value</param>
        /// <param name="oMax">Maximum out value</param>
        /// <param name="value">Depending value</param>
        /// <returns></returns>
        public static Color RemapColor(float iMin, float iMax, Color oMin, Color oMax, float value)
        {
            float t = Mathf.InverseLerp(iMin, iMax, value);
            Color outColor = Color.Lerp(oMin, oMax, t);

            return outColor;
        }
    }
}
