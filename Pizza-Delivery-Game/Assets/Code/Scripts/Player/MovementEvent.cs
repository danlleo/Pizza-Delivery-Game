using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public class MovementEvent : MonoBehaviour, IEvent<MovementEventArgs>
    {
        public event EventHandler<MovementEventArgs> Event;
        
        public void Call(object sender, MovementEventArgs movementEventArgs)
        {
            Event?.Invoke(sender, movementEventArgs);
        }
    }

    public class MovementEventArgs : EventArgs
    {
        public readonly bool IsMoving;
        public readonly bool IsSprinting;

        public MovementEventArgs(bool isMoving)
        {
            IsMoving = isMoving;
        }
        
        public MovementEventArgs(bool isMoving, bool isSprinting)
        {
            IsMoving = isMoving;
            IsSprinting = isSprinting;
        }
    }
}
