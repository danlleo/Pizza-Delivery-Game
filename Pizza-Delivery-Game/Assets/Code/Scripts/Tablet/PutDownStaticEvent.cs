using System;

namespace Tablet
{
    public static class PutDownStaticEvent
    {
        public static event EventHandler OnTabletPutDown;
        
        public static void Call(object sender)
            => OnTabletPutDown?.Invoke(sender, EventArgs.Empty);
    }
}