using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Monster.StateMachine.ConcreteStates
{
    public class ChaseState : State
    {
        private const float TIME_TO_LOSE_TARGET_IN_SECONDS = 2f;
        
        private readonly Monster _monster;
        private readonly StateMachine _stateMachine;
        
        private readonly NavMeshAgent _navMeshAgent;
        
        private Transform _transform;
        private Transform _targetTransform;

        private Coroutine _loosingTargetRoutine;

        private float _stoppingDistance = 5f;
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
            if (Vector3.Distance(_transform.position, _targetTransform.position) > _stoppingDistance)
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
            
            if (_timer >= TIME_TO_LOSE_TARGET_IN_SECONDS)
                _stateMachine.ChangeState(_monster.StateFactory.Roam());
        }
        
        private void ResetTimer()
            => _timer = 0f;

        private IEnumerator LoosingTargetRoutine()
        {
            yield return new WaitForSeconds(TIME_TO_LOSE_TARGET_IN_SECONDS);
            
            _stateMachine.ChangeState(_monster.StateFactory.Roam());
        }
        
        #region Events
        
        private void LostTarget_Event(object sender, EventArgs e)
        {
            if (_loosingTargetRoutine != null)
                return;

            _loosingTargetRoutine = _monster.StartCoroutine(LoosingTargetRoutine());
        }

        private void DetectedTarget_Event(object sender, EventArgs e)
        {
            ResetTimer();

            if (_loosingTargetRoutine == null) return;
            
            _monster.StopCoroutine(_loosingTargetRoutine);
            _loosingTargetRoutine = null;
        }
        
        #endregion
    }
}
