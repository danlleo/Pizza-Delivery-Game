using Interfaces;
using UnityEngine;

namespace Environment.Bedroom.PC
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class Clickable : MonoBehaviour, IClickable
    {
        public virtual void HandleClick() { }
        
        public virtual void OnDestroy()
        {
            // Do something here
            ScreenWorldSpaceCanvas.Instance.RemoveClickableObject(this);
        }
    }
}
