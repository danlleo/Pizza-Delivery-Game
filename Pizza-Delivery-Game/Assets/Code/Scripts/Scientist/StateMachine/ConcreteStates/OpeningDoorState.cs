namespace Scientist.StateMachine.ConcreteStates
{
    public class OpeningDoorState : State
    {
        private Scientist _scientist;
        private StateMachine _stateMachine;
        
        public OpeningDoorState(Scientist scientist, StateMachine stateMachine) : base(scientist, stateMachine)
        {
            _scientist = scientist;
            _stateMachine = stateMachine;
        }

        public override void EnterState()
        {
            _scientist.StartedOpeningDoorEvent.Call(_scientist);
            _scientist.OpenedDoorEvent.Call(_scientist);
        }
    }
}