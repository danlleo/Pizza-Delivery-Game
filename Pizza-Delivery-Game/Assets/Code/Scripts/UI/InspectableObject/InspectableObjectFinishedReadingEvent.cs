using System;
using Interfaces;
using UnityEngine;

namespace UI.InspectableObject
{
    public class InspectableObjectFinishedReadingEvent : MonoBehaviour, IEvent<InspectableObjectFinishedReadingEventArgs>
    {
        public event EventHandler<InspectableObjectFinishedReadingEventArgs> Event;
        
        public void Call(object sender, InspectableObjectFinishedReadingEventArgs eventArgs)
        {
            Event?.Invoke(sender, eventArgs);
        }
    }

    public class InspectableObjectFinishedReadingEventArgs : EventArgs
    {
        public readonly bool CanClose;

        public InspectableObjectFinishedReadingEventArgs(bool canClose)
        {
            CanClose = canClose;
        }
    }
}