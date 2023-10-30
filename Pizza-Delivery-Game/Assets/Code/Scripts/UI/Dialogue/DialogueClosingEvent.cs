using System;
using Interfaces;
using UnityEngine;

namespace UI.Dialogue
{
    [DisallowMultipleComponent]
    public class DialogueClosingEvent : MonoBehaviour, IEvent
    {
        public event EventHandler Event;
        
        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}