using System;

namespace Environment.Bedroom.PC
{
    public static class ClickedStaticEvent
    {
        public static event EventHandler OnClicked;
        
        public static void CallClickedStaticEvent(this Player.Player player)
            => OnClicked?.Invoke(player, EventArgs.Empty);
    }
}