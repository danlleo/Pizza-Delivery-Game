using System;
using UnityEngine;

namespace Player.Inventory
{
    [DisallowMultipleComponent]
    public class RemovingItemEvent : MonoBehaviour
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