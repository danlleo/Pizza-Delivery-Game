using System;

namespace Environment.Bedroom.PC
{
    public static class ClickedStaticEvent
    {
        public static event EventHandler OnClicked;
        
        public static void Call(object sender)
            => OnClicked?.Invoke(sender, EventArgs.Empty);
    }
}