using System;
using Interfaces;
using UnityEngine;

namespace Scientist.Outdoor
{
    public class StartedTalkingEvent : MonoBehaviour, IEvent
    {
        public event EventHandler Event;
        
        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}