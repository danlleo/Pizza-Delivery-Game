using Monster.StateMachine.ConcreteStates;

namespace Monster.StateMachine
{
    public class StateFactory
    {
        private readonly Monster _monster;
        private readonly StateMachine _stateMachine;

        public StateFactory(Monster monster, StateMachine stateMachine)
        {
            _monster = monster;
            _stateMachine = stateMachine;
        }

        public State Idle()
        {
            return new IdleState(_monster, _stateMachine);
        }

        public State Chase()
        {
            return new ChaseState(_monster, _stateMachine);
        }

        public State Roam()
        {
            return new RoamState(_monster, _stateMachine);
        }

        public State Investigate()
        {
            return new InvestigateState(_monster, _stateMachine);
        }
    }
}
