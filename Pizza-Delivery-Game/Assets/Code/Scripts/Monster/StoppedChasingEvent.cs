using System;
using UnityEngine;

namespace Monster
{
    public class StoppedChasingEvent : MonoBehaviour
    {
        public event EventHandler Event;
        
        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}