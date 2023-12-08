using System;

namespace Objective
{
    public static class FirstObjectiveSetStaticEvent
    {
        public static event EventHandler OnFirstObjectiveSet;
        
        public static void Call(object sender)
            => OnFirstObjectiveSet?.Invoke(sender, EventArgs.Empty);
    }
}