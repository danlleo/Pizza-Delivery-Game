using System;
using System.Collections;
using Common;
using UnityEngine;
using UnityEngine.AI;

namespace Monster.StateMachine.ConcreteStates
{
    public class ChaseState : State
    {
        private const float TIME_TO_LOSE_TARGET_IN_SECONDS = 4f;
        
        private readonly Monster _monster;
        private readonly StateMachine _stateMachine;
        
        private readonly NavMeshAgent _navMeshAgent;
        
        private Transform _transform;
        private Transform _targetTransform;

        private Coroutine _loosingTargetRoutine;

        private float _stoppingDistance = 1f;
        private float _timer;
        
        public ChaseState(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
        {
            _monster = monster;
            _stateMachine = stateMachine;
            _navMeshAgent = monster.NavMeshAgent;
        }

        public override void EnterState()
        {
            _monster.StartedChasingEvent.Call(_monster);
            _monster.CallBeganChaseStaticEvent();
            _transform = _monster.transform;
            _targetTransform = _monster.Player.transform;
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
            ChasePlayer();
        }

        public override void ExitState()
        {
            _monster.CallStoppedChaseStaticEvent();
            _monster.StoppedChasingEvent.Call(_monster);
        }
        
        private void ChasePlayer()
        {
            if (Vector3.Distance(_transform.position, _targetTransform.position) > _stoppingDistance)
            {
                _navMeshAgent.SetDestination(_targetTransform.position);
            }
            else
            {
                OnAnyGameOver.Call(this);
                _monster.DestroySelf();
            }
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
