namespace Scientist.Outdoor.StateMachine
{
    public class State
    {
        protected Scientist Scientist;
        protected StateMachine StateMachine;

        public State(Scientist scientist, StateMachine stateMachine)
        {
            Scientist = scientist;
            StateMachine = stateMachine;
        }
        
        public virtual void EnterState() { }
        
        public virtual void SubscribeToEvents() {}
        
        public virtual void UnsubscribeFromEvents() {}
        
        public virtual void ExitState() { }
        
        public virtual void FrameUpdate() { }
    }
}
