using System;

namespace Monster
{
    public static class StoppedChaseStaticEvent
    {
        public static event EventHandler OnAnyStoppedChaseStaticEvent;

        public static void CallStoppedChaseStaticEvent(this Monster monster)
        {
            OnAnyStoppedChaseStaticEvent?.Invoke(monster, EventArgs.Empty);
        }
    }
}