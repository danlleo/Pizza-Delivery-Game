using UnityEngine;

namespace Player.StateMachine.ConcreteStates
{
    public class IdleState : State
    {
        public IdleState(StateMachine currentContext, StateFactory factory) : base(currentContext, factory)
        {
            
        }
        
        public override void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public override void CheckSwitchState()
        {
            throw new System.NotImplementedException();
        }

        public override void InitializeSubState()
        {
            throw new System.NotImplementedException();
        }
    }
}
