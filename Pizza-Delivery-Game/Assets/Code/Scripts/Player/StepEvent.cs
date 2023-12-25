using System;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public class StepEvent : MonoBehaviour
    {
        public event EventHandler<StepEventArgs> Event;
        
        public void Call(object sender, StepEventArgs stepEventArgs)
        {
            Event?.Invoke(this, stepEventArgs);
        }
    }

    public class StepEventArgs : EventArgs
    {
        public readonly string Surface;
        public readonly bool WasSprinting;

        public StepEventArgs(string surface, bool wasSprinting)
        {
            Surface = surface;
            WasSprinting = wasSprinting;
        }
    }
}