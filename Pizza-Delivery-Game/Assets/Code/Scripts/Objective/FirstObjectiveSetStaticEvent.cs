using System;

namespace Objective
{
    public static class FirstObjectiveSetStaticEvent
    {
        public static event EventHandler<FirstObjectiveSetStaticEventArgs> OnFirstObjectiveSet;
        
        public static void Call(object sender, FirstObjectiveSetStaticEventArgs firstObjectiveSetStaticEventArgs)
            => OnFirstObjectiveSet?.Invoke(sender, firstObjectiveSetStaticEventArgs);
    }
    
    public class FirstObjectiveSetStaticEventArgs : EventArgs
    {
        public readonly Objective SetObjective;

        public FirstObjectiveSetStaticEventArgs(Objective setObjective)
        {
            SetObjective = setObjective;
        }
    }
}