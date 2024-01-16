using System;

namespace DataPersistence
{
    public static class NewGameStaticEvent
    {
        public static EventHandler OnNewGame;
        
        public static void Call(object sender)
            => OnNewGame?.Invoke(sender, EventArgs.Empty);
    }
}