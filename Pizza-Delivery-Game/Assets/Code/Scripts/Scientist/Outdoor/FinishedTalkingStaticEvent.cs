using System;

namespace Scientist.Outdoor
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