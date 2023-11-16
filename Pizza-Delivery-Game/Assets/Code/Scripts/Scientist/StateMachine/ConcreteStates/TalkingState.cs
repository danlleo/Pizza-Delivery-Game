using System;

namespace Scientist.StateMachine.ConcreteStates
{
    public class TalkingState : State
    {
        private Scientist _scientist;
        private StateMachine _stateMachine;
        
        public TalkingState(Scientist scientist, StateMachine stateMachine) : base(scientist, stateMachine)
        {
            _scientist = scientist;
            _stateMachine = stateMachine;
        }

        public override void EnterState()
        {
            _scientist.StartedTalkingEvent.Call(_scientist);
        }

        public override void SubscribeToEvents()
        {
            FinishedTalkingStaticEvent.Event += FinishedTalkingStatic_Event;
        }
        
        public override void UnsubscribeFromEvents()
        {
            FinishedTalkingStaticEvent.Event -= FinishedTalkingStatic_Event;
        }
        
        private void FinishedTalkingStatic_Event(object sender, EventArgs e)
        {
            _stateMachine.ChangeState(_scientist.StateFactory.Walking());
        }
    }
}