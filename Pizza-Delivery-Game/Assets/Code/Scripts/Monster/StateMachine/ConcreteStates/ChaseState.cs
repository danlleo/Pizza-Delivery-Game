using System;
using UnityEngine;
using UnityEngine.AI;

namespace Monster.StateMachine.ConcreteStates
{
    public class ChaseState : State
    {
        private const float TIME_TO_LOSE_TARGET = 0.7f;
        
        private Monster _monster;
        private StateMachine _stateMachine;
        
        private NavMeshAgent _navMeshAgent;
        
        private Transform _transform;
        private Transform _targetTransform;

        private float _timer;
        
        public ChaseState(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
        {
            _monster = monster;
            _stateMachine = stateMachine;
            _transform = monster.transform;
            _navMeshAgent = monster.NavMeshAgent;
        }

        public override void EnterState()
        {
            _monster.StartedChasingEvent.Call(_monster);
            _targetTransform = Player.Player.Instance.transform;
        }

        public override void SubscribeToEvents()
        {
            _monster.DetectedTargetEvent.Event += DetectedTarget_Event;
            _monster.LostTargetEvent.Event += LostTarget_Event;
        }

        public override void UnsubscribeFromEvents()
        {
            _monster.DetectedTargetEvent.Event -= DetectedTarget_Event;
            _monster.LostTargetEvent.Event -= LostTarget_Event;
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

        private void CountTimeToLooseTarget()
        {
            _timer += Time.deltaTime;
            
            Debug.Log(_timer);
            
            if (_timer >= TIME_TO_LOSE_TARGET)
                _stateMachine.ChangeState(_monster.StateFactory.Roam());
        }
        
        private void ResetTimer()
            => _timer = 0f;

        #region Events
        
        private void LostTarget_Event(object sender, EventArgs e)
        {
            CountTimeToLooseTarget();
        }

        private void DetectedTarget_Event(object sender, EventArgs e)
        {
            ResetTimer();
        }
        
        #endregion
    }
}
