using UnityEngine;

namespace Environment.Bedroom.PC
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public abstract class Clickable : MonoBehaviour
    {
        public abstract void HandleClick();
        
        protected virtual void OnDestroy()
        {
            ScreenWorldSpaceCanvas.Instance.RemoveClickableObject(this);
        }
    }
}
