using System;

namespace DataPersistence
{
    public class NewGameStaticEvent
    {
        public static EventHandler OnNewGame;
        
        public static void CallNewGameEvent(object sender)
            => OnNewGame?.Invoke(sender, EventArgs.Empty);
    }
}