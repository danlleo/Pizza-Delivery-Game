using System;

namespace Objective
{
    public class ObjectiveFinishedStaticEvent
    {
        public static event EventHandler<ObjectiveFinishedStaticEventArgs> OnObjectiveFinished;

        public static void Call(Objective objective, ObjectiveFinishedStaticEventArgs objectiveFinishedStaticEventArgs)
        {
            OnObjectiveFinished?.Invoke(objective, objectiveFinishedStaticEventArgs);
        }
    }

    public class ObjectiveFinishedStaticEventArgs : EventArgs
    {
        public readonly Objective FinishedObjective;

        public ObjectiveFinishedStaticEventArgs(Objective finishedObjective)
        {
            FinishedObjective = finishedObjective;
        }
    }
}