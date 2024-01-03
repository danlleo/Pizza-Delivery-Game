namespace Objective
{
    public class Objective
    {
        private ObjectiveSO _objective;
        private ObjectiveRegistry _objectiveRegistry;
        
        public Objective(ObjectiveSO objective, ObjectiveRegistry objectiveRegistry)
        {
            _objective = objective;
            _objectiveRegistry = objectiveRegistry;
        }

        public void Finish()
        {
            _objectiveRegistry.SetNextObjective();

            if (_objectiveRegistry.TryGetCurrentObjective(out Objective objective))
            {
                ObjectiveFinishedStaticEvent.Call(this, new ObjectiveFinishedStaticEventArgs(objective));
            }
        }

        public ObjectiveSO GetObjectiveSO()
            => _objective;
    }
}