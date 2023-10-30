using System;
using Interfaces;
using UnityEngine;

namespace UI.InspectableObject
{
    public class InspectableObjectCloseEvent : MonoBehaviour, IEvent
    {
        public event EventHandler Event;
        
        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}