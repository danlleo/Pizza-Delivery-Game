using System;
using Enums.Scientist;

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
        
        public override void SubscribeToEvents()
        {
            _scientist.InteractedWithScientistEvent.Event += InteractedWithScientist_Event;
        }

        public override void UnsubscribeFromEvents()
        {
            _scientist.InteractedWithScientistEvent.Event -= InteractedWithScientist_Event;
        }
        
        private void InteractedWithScientist_Event(object sender, EventArgs e)
        {
            switch (_scientist.ScientistType)
            {
                case ScientistType.Outdoor:
                    _stateMachine.ChangeState(_scientist.StateFactory.Talking());
                    break;
                case ScientistType.LaboratoryEntry:
                    _stateMachine.ChangeState(_scientist.StateFactory.Talking());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
