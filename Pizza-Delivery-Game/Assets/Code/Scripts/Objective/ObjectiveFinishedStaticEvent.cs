using System;

namespace Objective
{
    public class ObjectiveFinishedStaticEvent
    {
        public static event EventHandler OnObjectiveFinished;

        public static void Call(Objective objective)
        {
            OnObjectiveFinished?.Invoke(objective, EventArgs.Empty);
        }
    }
}