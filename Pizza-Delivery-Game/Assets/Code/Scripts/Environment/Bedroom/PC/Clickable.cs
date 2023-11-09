using Interfaces;
using UnityEngine;

namespace Environment.Bedroom.PC
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class Clickable : MonoBehaviour, IClickable
    {
        public virtual void HandleClick() { }
    }
}
