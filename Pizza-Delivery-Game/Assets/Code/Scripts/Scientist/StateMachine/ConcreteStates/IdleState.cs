using System;
using UnityEngine;

namespace Scientist.StateMachine.ConcreteStates
{
    public class IdleState : State
    {
        private Scientist _scientist;
        private StateMachine _stateMachine;
        
        public IdleState(Scientist scientist, StateMachine stateMachine) : base(scientist, stateMachine)
        {
            _scientist = scientist;
            _stateMachine = stateMachine;
        }

        public override void EnterState()
        {
            
        }

        public override void SubscribeToEvents()
        {
            _scientist.InteractedWithScientistEvent.Event += InteractedWithScientist_Event;
        }

        public override void UnsubscribeFromEvents()
        {
            _scientist.InteractedWithScientistEvent.Event -= InteractedWithScientist_Event;
        }

        public override void ExitState()
        {
            
        }

        public override void FrameUpdate()
        {
            
        }
        
        private void InteractedWithScientist_Event(object sender, EventArgs e)
        {
            Debug.Log("Hi!");
            _stateMachine.ChangeState(_scientist.StateFactory.Walking());
        }
    }
}
