namespace Scientist.StateMachine.ConcreteStates
{
    public class IdleState : State
    {
        public IdleState(Scientist scientist, StateMachine stateMachine) : base(scientist, stateMachine)
        {
            
        }

        public override void EnterState()
        {
            StateMachine.ChangeState(Scientist.StateFactory.Walking());
        }

        public override void ExitState()
        {
            
        }

        public override void FrameUpdate()
        {
            
        }
    }
}
