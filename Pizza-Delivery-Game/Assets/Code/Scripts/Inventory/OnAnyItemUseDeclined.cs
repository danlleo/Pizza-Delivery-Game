using System;
using Interfaces;

namespace Player.Inventory
{
    public static class OnAnyItemUseDeclined
    {
        public static event EventHandler Event;
    
        public static void CallOnAnyItemFinishedInteraction(this IItemReceiver itemReceiver)
            => Event?.Invoke(itemReceiver, EventArgs.Empty);
    }
}
