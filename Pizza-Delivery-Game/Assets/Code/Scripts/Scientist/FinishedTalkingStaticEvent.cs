using System;

namespace Scientist
{
    public static class FinishedTalkingStaticEvent
    {
        public static event EventHandler Event;

        public static void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}