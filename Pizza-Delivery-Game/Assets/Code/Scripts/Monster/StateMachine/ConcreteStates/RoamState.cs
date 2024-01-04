using System;
using System.Collections.Generic;
using Environment.LaboratorySecondLevel;
using UnityEngine;

namespace Monster.StateMachine.ConcreteStates
{
    public class RoamState : State
    {
        private Monster _monster;
        private StateMachine _stateMachine;

        private List<Transform> _patrolPointList;

        private Transform _monsterTransform;
        
        private Transform _currentPatrolPointTransform;
        private int _currentPatrolPointIndex;

        private float _stoppingDistance = .25f;
        
        public RoamState(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
        {
            _monster = monster;
            _monsterTransform = monster.transform;
            _stateMachine = stateMachine;
        }

        public override void EnterState()
        {
            _monster.FieldOfView.enabled = true;
            _monster.StartedPatrollingEvent.Call(_monster);
            _patrolPointList = new List<Transform>(_monster.PatrolPointList);
            SetNextPatrolPoint();
        }

        public override void SubscribeToEvents()
        {
            _monster.DetectedTargetEvent.Event += DetectedTarget_Event;
            AttractedMonsterStaticEvent.OnAnyAttractedMonster += OnAnyAttractedMonster;
        }

        public override void UnsubscribeFromEvents()
        {
            _monster.DetectedTargetEvent.Event -= DetectedTarget_Event;
            AttractedMonsterStaticEvent.OnAnyAttractedMonster -= OnAnyAttractedMonster;
        }

        public override void FrameUpdate()
        {
            PatrolToPoint(_currentPatrolPointTransform);
        }

        private void PatrolToPoint(Transform point)
        {
            _monster.NavMeshAgent.SetDestination(point.position);

            if (Vector3.Distance(_monsterTransform.position, point.position) <= _stoppingDistance)
            {
                SetNextPatrolPoint();
            }
        }

        private void SetNextPatrolPoint()
        {
            _currentPatrolPointTransform = GetNextPatrolPoint();
        }
        
        private Transform GetNextPatrolPoint()
        {
            if (_currentPatrolPointIndex >= _patrolPointList.Count)
            {
                _currentPatrolPointIndex = 0;
                return _patrolPointList[0];
            }

            _currentPatrolPointIndex++;

            return _patrolPointList[_currentPatrolPointIndex - 1];
        }
        
        #region Events

        private void DetectedTarget_Event(object sender, EventArgs e)
        {
            _stateMachine.ChangeState(_monster.StateFactory.Chase());
        }
        
        private void OnAnyAttractedMonster(object sender, AttractedMonsterEventArgs attractedMonsterEventArgs)
        {
            _stateMachine.ChangeState(_monster.StateFactory.Investigate());
        }

        #endregion
    }
}
