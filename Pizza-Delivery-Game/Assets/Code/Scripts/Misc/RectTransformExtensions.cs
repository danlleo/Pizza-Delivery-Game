using UnityEngine;

namespace Misc
{
    public static class RectTransformExtensions
    { 
        public static Rect GetWorldRect(this RectTransform rectTransform)
        {
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            Vector3 position = corners[0];

            var size = new Vector2(
                rectTransform.lossyScale.x * rectTransform.rect.size.x,
                rectTransform.lossyScale.y * rectTransform.rect.size.y
            );

            return new Rect(position, size);
        }
    }
}
