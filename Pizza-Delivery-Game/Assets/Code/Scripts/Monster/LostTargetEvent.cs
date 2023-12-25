using System;
using UnityEngine;

namespace Monster
{
    public class LostTargetEvent : MonoBehaviour
    {
        public event EventHandler Event;
        
        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}