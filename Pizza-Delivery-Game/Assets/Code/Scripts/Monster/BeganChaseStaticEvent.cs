using System;

namespace Monster
{
    public static class BeganChaseStaticEvent
    {
        public static event EventHandler OnAnyBeganChase;

        public static void CallBeganChaseStaticEvent(this Monster monster)
        {
            OnAnyBeganChase?.Invoke(monster, EventArgs.Empty);
        }
    }
}