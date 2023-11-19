namespace Monster.StateMachine
{
    public class State
    {
       protected Monster Monster;
       protected StateMachine StateMachine;

       protected State(Monster monster, StateMachine stateMachine)
       {
           Monster = monster;
           StateMachine = stateMachine;
       }
       
       public virtual void EnterState() { }
        
       public virtual void SubscribeToEvents() {}
        
       public virtual void UnsubscribeFromEvents() {}
        
       public virtual void ExitState() { }
        
       public virtual void FrameUpdate() { }
    }
}
