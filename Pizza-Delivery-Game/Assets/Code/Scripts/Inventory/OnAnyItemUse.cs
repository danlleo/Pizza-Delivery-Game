using System;
using Interfaces;

namespace Player.Inventory
{
    public static class OnAnyItemUse
    {
        public static event EventHandler<OnAnyItemUseEventArgs> Event;

        public static void CallOnAnyItemUseEvent(this IItemReceiver itemReceiver, OnAnyItemUseEventArgs onAnyItemUseEventArgs)
        {
            Event?.Invoke(itemReceiver, onAnyItemUseEventArgs);
        }
    }

    public class OnAnyItemUseEventArgs : EventArgs
    {
        public IItemReceiver ItemReceiver;
        public ItemSO Item;
        
        public OnAnyItemUseEventArgs(IItemReceiver itemReceiver, ItemSO item)
        {
            ItemReceiver = itemReceiver;
            Item = item;
        }
    }
}
