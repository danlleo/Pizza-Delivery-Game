using System;

namespace DataPersistence
{
    public static class LoadStaticEvent
    {
        public static EventHandler OnLoad;
        
        public static void CallLoadEvent(object sender)
            => OnLoad?.Invoke(sender, EventArgs.Empty);
    }
}