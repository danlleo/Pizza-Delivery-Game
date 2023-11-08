using System;

namespace Environment.Bedroom.PC
{
    public class StoppedUsingPCStaticEvent
    {
        public static event EventHandler OnEnded;
        
        public static void Call(object sender)
            => OnEnded?.Invoke(sender, EventArgs.Empty);
    }
}