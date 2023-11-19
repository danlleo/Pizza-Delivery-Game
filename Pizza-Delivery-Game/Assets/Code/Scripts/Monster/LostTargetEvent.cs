using System;
using Interfaces;
using UnityEngine;

namespace Monster
{
    public class LostTargetEvent : MonoBehaviour, IEvent
    {
        public event EventHandler Event;
        
        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}