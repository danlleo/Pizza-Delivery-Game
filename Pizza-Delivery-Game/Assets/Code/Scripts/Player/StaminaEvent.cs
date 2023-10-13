using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public class StaminaEvent : MonoBehaviour, IEvent<StaminaEventArgs>
    {
        public event EventHandler<StaminaEventArgs> Event;
        
        public void Call(object sender, StaminaEventArgs staminaEventArgs)
        {
            Event?.Invoke(sender, staminaEventArgs);
        }
    }

    public class StaminaEventArgs : EventArgs
    {
        public readonly float StaminaPercent;

        public StaminaEventArgs(float staminaPercent)
        {
            StaminaPercent = staminaPercent;
        }
    }
}
