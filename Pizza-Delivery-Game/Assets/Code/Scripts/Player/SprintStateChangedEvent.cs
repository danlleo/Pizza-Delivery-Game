using System;
using UnityEngine;

namespace Player
{
    public class SprintStateChangedEvent : MonoBehaviour
    {
        public event EventHandler<SprintStateChangedEventArgs> Event;
        
        public void Call(object sender, SprintStateChangedEventArgs eventArgs)
        {
            Event?.Invoke(sender, eventArgs);
        }
    }

    public class SprintStateChangedEventArgs : EventArgs
    {
        public readonly bool IsSprinting;

        public SprintStateChangedEventArgs(bool isSprinting)
        {
            IsSprinting = isSprinting;
        }
    }
}