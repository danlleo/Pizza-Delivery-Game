namespace Monster.StateMachine
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void Initialize(State startingState)
        {
            CurrentState = startingState;
            CurrentState.EnterState();
            CurrentState.SubscribeToEvents();
        }

        public void ChangeState(State newState)
        {
            CurrentState.ExitState();
            CurrentState.UnsubscribeFromEvents();
            CurrentState = newState;
            CurrentState.EnterState();
            CurrentState.SubscribeToEvents();
        }
    }
}
