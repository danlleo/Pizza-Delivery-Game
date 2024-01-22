using Interfaces;
using Misc;
using UnityEngine;

namespace Environment.Bedroom.PC
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]
    public class PCCollider : MonoBehaviour, IInteractable
    {
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        public void Interact()
        {
            _boxCollider.enabled = false;
            CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(false));
            OnAnyStartedUsingPC.Call(this);
        }

        public string GetActionDescription()
        {
            return "Read vacancy";
        }
    }
}
