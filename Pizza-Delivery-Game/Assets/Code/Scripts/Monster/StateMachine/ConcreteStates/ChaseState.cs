using UnityEngine;
using UnityEngine.AI;

namespace Monster.StateMachine.ConcreteStates
{
    public class ChaseState : State
    {
        private Monster _monster;
        private NavMeshAgent _navMeshAgent;
        private Transform _transform;
        private Transform _targetTransform;
        
        public ChaseState(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
        {
            _monster = monster;
            _transform = monster.transform;
            _navMeshAgent = monster.NavMeshAgent;
        }

        public override void EnterState()
        {
            _monster.StartedChasingEvent.Call(_monster);
            _targetTransform = Player.Player.Instance.transform;
        }

        public override void FrameUpdate()
        {
            // if (Vector3.Distance(_transform.position, _targetTransform.position) > 5f)
                _navMeshAgent.SetDestination(_targetTransform.position);
        }

        public override void ExitState()
        {
            _monster.StoppedChasingEvent.Call(_monster);
        }
    }
}
