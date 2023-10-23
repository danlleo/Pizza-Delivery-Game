using UnityEngine;

namespace Utilities
{
    /// <summary>
    ///     Class that provides different variations of interpolation effects
    ///     T should be normalized time
    /// </summary>

    public static class Interpolation
    {
        /// <summary>
        /// This formula produces an interpolation that starts and ends slowly, with a faster motion in the middle.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float EaseInOut(float t) => t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;

        /// <summary>
        /// This formula combines the ease-out and ease-in effects, resulting in a slower motion at the start and end, with a faster motion in the middle.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float EaseOutIn(float t) => t < 0.5f ? (1f - Mathf.Pow(1f - 2f * t, 2f)) / 2f : (Mathf.Pow(2f * t - 1f, 2f) + 1f);

        /// <summary>
        /// This formula creates an interpolation that starts fast and slows down towards the end.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float EaseIn(float t) => t * t;

        /// <summary>
        /// This formula creates an interpolation that starts fast and slows down towards the end.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float EaseOut(float t) => 1f - Mathf.Pow(1f - t, 2f);

        /// <summary>
        /// This formula starts and ends slowly with an accelerated middle section.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float EaseInOutCubic(float t) => t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3f);

        /// <summary>
        /// This formula starts slowly and then rapidly gains speed as the input value increases.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float EaseInCubic(float t) => t * t * t;

        /// <summary>
        /// This formulate starts with a higher speed and gradually slows down as the input value approaches 1
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float EaseOutCubic(float t)
        {
            float f = t - 1f;

            return f * f * f + 1f;
        }

        /// <summary>
        /// This formula starts and ends slowly with an accelerated middle section.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float EaseInOutQuart(float t) => t < 0.5f ? 8f * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 4f) / 2f;

        /// <summary>
        /// This formula starts slowly and gradually accelerates.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float EaseInQuart(float t) => t * t * t * t;

        /// <summary>
        /// This formula starts quickly and gradually decelerates.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float EaseOutQuart(float t) => 1f - Mathf.Pow(1f - t, 4);

        /// <summary>
        /// This formula starts slowly and exponentially accelerates.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float EaseInExponential(float t) => Mathf.Pow(2f, 10f * (t - 1f));

        /// <summary>
        /// This formula starts quickly and exponentially decelerates.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float EaseOutExponential(float t) => 1f - Mathf.Pow(2f, -10f * t);

        /// <summary>
        /// This formula uses a combination of exponential and trigonometric functions to create the elastic effect. It starts slowly, accelerates, and then decelerates with a bouncing motion.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="amplitude"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static float ElasticInOut(float t, float amplitude, float period)
        {
            float s = period / 4;
            float s2 = s * 2;

            if (amplitude < 1)
            {
                amplitude = 1;
                s = period / 4;
            }
            else
            {
                s = period / (2 * Mathf.PI) * Mathf.Asin(1 / amplitude);
            }

            if (t < 0.5f)
            {
                t *= 2;

                return -0.5f * (amplitude * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t - s) * s2 * Mathf.PI / period));
            }

            t = t * 2 - 1;

            return amplitude * Mathf.Pow(2, -10 * t) * Mathf.Sin((t - s) * s2 * Mathf.PI / period) * 0.5f + 1f;
        }

        /// <summary>
        /// This formula creates a bouncing effect, simulating an elastic behavior. It starts with a slight overshoot and settles into the final value.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float ElasticIn(float t)
        {
            const float c4 = (2 * Mathf.PI) / 3;

            return t switch
            {
                0 => 0,
                1 => 1,
                _ => -Mathf.Pow(2, 10 * t - 10) * Mathf.Sin((t * 10 - 10.75f) * c4)
            };
        }

        /// <summary>
        /// This formula creates a smooth elastic motion that starts quickly and slows down as it approaches the end.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float ElasticOut(float t)
        {
            const float amplitude = 1f;
            const float period = 0.3f;

            float s = period / (2 * Mathf.PI) * Mathf.Asin(1f / amplitude);

            return (amplitude * Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - s) * (2 * Mathf.PI) / period) + 1f);
        }
    }
}