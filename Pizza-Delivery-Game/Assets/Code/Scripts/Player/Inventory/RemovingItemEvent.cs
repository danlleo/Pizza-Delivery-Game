using System;
using Interfaces;
using UnityEngine;

namespace Player.Inventory
{
    [DisallowMultipleComponent]
    public class RemovingItemEvent : MonoBehaviour, IEvent<RemovingItemEventArgs>
    {
        public event EventHandler<RemovingItemEventArgs> Event;
        
        public void Call(object sender, RemovingItemEventArgs eventArgs)
        {
            Event?.Invoke(sender, eventArgs);
        }
    }

    public class RemovingItemEventArgs : EventArgs
    {
        public readonly ItemSO Item;

        public RemovingItemEventArgs(ItemSO item)
        {
            Item = item;
        }
    }
}