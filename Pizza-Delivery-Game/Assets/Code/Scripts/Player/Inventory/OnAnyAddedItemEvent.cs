using System;

namespace Player.Inventory
{
    public static class OnAnyAddedItemEvent 
    {
        public static event EventHandler<AddingItemEventArgs> Event;
        
        public static void Call(object sender, AddingItemEventArgs eventArgs)
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