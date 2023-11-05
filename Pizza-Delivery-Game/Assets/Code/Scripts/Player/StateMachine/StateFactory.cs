using Player.StateMachine.ConcreteStates;

namespace Player.StateMachine
{
    public class StateFactory
    {
        private StateMachine _context;

        public StateFactory(StateMachine currentContext)
        {
            _context = currentContext;
        }

        public State Grounded()
        {
            return new GroundedState(_context, this);
        }

        public State Idle()
        {
            return new IdleState(_context, this);
        }

        public State Walking()
        {
            return new WalkingState(_context, this);
        }
    }
}
