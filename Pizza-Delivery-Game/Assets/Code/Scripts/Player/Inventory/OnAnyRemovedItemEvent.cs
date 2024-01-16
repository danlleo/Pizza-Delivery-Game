using System;

namespace Player.Inventory
{
    public static class OnAnyRemovedItemEvent
    {
        public static event EventHandler<RemovingItemEventArgs> Event;
        
        public static void Call(object sender, RemovingItemEventArgs eventArgs)
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