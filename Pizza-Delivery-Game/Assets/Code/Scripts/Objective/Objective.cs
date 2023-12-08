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
            ObjectiveFinishedStaticEvent.Call(this);
        }

        public ObjectiveSO GetObjectiveSO()
            => _objective;
    }
}