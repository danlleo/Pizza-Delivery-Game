using System;
using UnityEngine;

namespace Scientist
{
    public class StoppedWalkingEvent : MonoBehaviour
    {
        public event EventHandler Event;
        
        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}