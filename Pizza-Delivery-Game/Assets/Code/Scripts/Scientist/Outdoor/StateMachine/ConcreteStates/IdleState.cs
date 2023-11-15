using System;

namespace Scientist.Outdoor.StateMachine.ConcreteStates
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
            _stateMachine.ChangeState(_scientist.StateFactory.Talking());
        }
    }
}
