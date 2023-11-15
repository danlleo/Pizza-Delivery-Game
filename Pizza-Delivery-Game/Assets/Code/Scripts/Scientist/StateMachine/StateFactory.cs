using Scientist.StateMachine.ConcreteStates;

namespace Scientist.StateMachine
{
    public class StateFactory
    {
        private Scientist _scientist;
        private StateMachine _currentContext;

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
    }
}
