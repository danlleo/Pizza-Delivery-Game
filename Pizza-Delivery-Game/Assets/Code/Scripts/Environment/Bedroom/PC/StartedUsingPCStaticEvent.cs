using System;

namespace Environment.Bedroom.PC
{
    public static class StartedUsingPCStaticEvent
    {
        public static event EventHandler OnStarted;
        
        public static void CallStartedUsingPC(this Player.Player player)
            => OnStarted?.Invoke(player, EventArgs.Empty);
    }
}