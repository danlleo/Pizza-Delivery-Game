using System;

namespace Environment.Bedroom.PC
{
    public static class StoppedUsingPCStaticEvent
    {
        public static event EventHandler OnEnded;
        
        public static void CallStoppedUsingPC(this Player.Player player)
            => OnEnded?.Invoke(player, EventArgs.Empty);
    }
}