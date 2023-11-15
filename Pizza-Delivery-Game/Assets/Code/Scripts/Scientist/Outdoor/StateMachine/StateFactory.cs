using Scientist.Outdoor.StateMachine.ConcreteStates;

namespace Scientist.Outdoor.StateMachine
{
    public class StateFactory
    {
        private readonly Scientist _scientist;
        private readonly StateMachine _currentContext;

        public StateFactory(Scientist scientist, StateMachine currentContext)
        {
            _scientist = scientist;
            _currentContext = currentContext;
        }

        public State Idle()
        {
            return new IdleState(_scientist, _currentContext);
        }

        public State Walking()
        {
            return new WalkingState(_scientist, _currentContext);
        }

        public State Talking()
        {
            return new TalkingState(_scientist, _currentContext);
        }
    }
}
