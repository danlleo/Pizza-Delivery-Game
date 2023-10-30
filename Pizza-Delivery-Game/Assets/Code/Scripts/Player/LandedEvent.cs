using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public class LandedEvent : MonoBehaviour, IEvent<LandedEventArgs>
    {
        public event EventHandler<LandedEventArgs> Event;
        
        public void Call(object sender, LandedEventArgs eventArgs)
        {
            Event?.Invoke(sender, eventArgs);
        }
    }

    public class LandedEventArgs : EventArgs
    {
        public readonly string Surface;

        public LandedEventArgs(string surface)
        {
            Surface = surface;
        }
    }
}