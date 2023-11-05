namespace Player.StateMachine
{
    public abstract class State
    {
        protected StateMachine CurrentContext;
        protected StateFactory Factory;

        public State(StateMachine currentContext, StateFactory factory)
        {
            CurrentContext = currentContext;
            Factory = factory;
        }
        
        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchState();

        public abstract void InitializeSubState();
        
        private void UpdateStates() { }

        protected void SwitchState(State newState)
        {
            ExitState();
            
            newState.EnterState();
            CurrentContext.CurrentState = newState;
        }
        
        protected void SetSuperState() { }
        
        protected void SetSubState() { }
    }
}
