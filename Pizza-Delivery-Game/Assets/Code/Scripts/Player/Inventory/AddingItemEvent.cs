using System;
using Interfaces;
using UnityEngine;

namespace Player.Inventory
{
    [DisallowMultipleComponent]
    public class AddingItemEvent : MonoBehaviour, IEvent<AddingItemEventArgs>
    {
        public event EventHandler<AddingItemEventArgs> Event;
        
        public void Call(object sender, AddingItemEventArgs eventArgs)
        {
            Event?.Invoke(sender, eventArgs);
        }
    }

    public class AddingItemEventArgs : EventArgs
    {
        public readonly ItemSO Item;

        public AddingItemEventArgs(ItemSO item)
        {
            Item = item;
        }
    }
}