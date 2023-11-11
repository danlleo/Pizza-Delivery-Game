using System;

namespace Environment.Bedroom
{
    public static class WokeUpStaticEvent
    {
        public static event EventHandler OnWokeUp;

        public static void Call(object sender)
            => OnWokeUp?.Invoke(sender, EventArgs.Empty);
    }
}