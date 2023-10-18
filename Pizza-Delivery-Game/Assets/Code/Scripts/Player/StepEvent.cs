using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class StepEvent : MonoBehaviour, IEvent
    {
        public event EventHandler Event;
        
        public void Call(object sender)
        {
            Event?.Invoke(this, EventArgs.Empty);
        }
    }
}