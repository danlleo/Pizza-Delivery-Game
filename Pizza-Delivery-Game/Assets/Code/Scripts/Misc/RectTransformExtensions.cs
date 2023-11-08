using UnityEngine;

namespace Misc
{
    public static class RectTransformExtensions
    { 
        public static Rect GetWorldRect(this RectTransform rectTransform)
        {
            Rect localRect = rectTransform.rect;

            return new Rect
            {
                min = rectTransform.TransformPoint(localRect.min),
                max = rectTransform.TransformPoint(localRect.max)
            };
        }
    }
}
