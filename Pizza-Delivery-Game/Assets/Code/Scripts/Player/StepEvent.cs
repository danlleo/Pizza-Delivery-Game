using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class StepEvent : MonoBehaviour, IEvent<StepEventArgs>
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

        public StepEventArgs(string surface)
        {
            Surface = surface;
        }
    }
}