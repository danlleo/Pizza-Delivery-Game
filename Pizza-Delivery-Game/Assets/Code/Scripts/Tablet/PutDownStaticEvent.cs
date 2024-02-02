using System;

namespace Tablet
{
    public static class PutDownStaticEvent
    {
        public static event EventHandler OnAnyTabletPutDown;
        
        public static void Call(object sender)
            => OnAnyTabletPutDown?.Invoke(sender, EventArgs.Empty);
    }
}