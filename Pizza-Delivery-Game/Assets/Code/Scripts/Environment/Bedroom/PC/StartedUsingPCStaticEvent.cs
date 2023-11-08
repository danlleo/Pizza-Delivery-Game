using System;

namespace Environment.Bedroom.PC
{
    public static class StartedUsingPCStaticEvent
    {
        public static event EventHandler OnStarted;
        
        public static void Call(object sender)
            => OnStarted?.Invoke(sender, EventArgs.Empty);
    }
}