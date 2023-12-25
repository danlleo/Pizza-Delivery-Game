using System;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public class HoveringOverInteractableEvent : MonoBehaviour
    {
        public event EventHandler<HoveringOverInteractableEventArgs> Event;
        
        public void Call(object sender, HoveringOverInteractableEventArgs hoveringOverInteractableEventArgs)
        {
            Event?.Invoke(sender, hoveringOverInteractableEventArgs);
        }
    }

    public class HoveringOverInteractableEventArgs : EventArgs
    {
        public readonly bool IsInteracting;
        public readonly string ActionDescription;

        public HoveringOverInteractableEventArgs(bool isInteracting)
        {
            IsInteracting = isInteracting;
        }
        
        public HoveringOverInteractableEventArgs(bool isInteracting, string actionDescription)
        {
            IsInteracting = isInteracting;
            ActionDescription = actionDescription;
        }
    }
}
