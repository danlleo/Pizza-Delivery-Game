using System;
using System.Collections.Generic;
using Environment.LaboratorySecondLevel;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Monster.StateMachine.ConcreteStates
{
    public class RoamState : State
    {
        private Monster _monster;
        private StateMachine _stateMachine;
        private AudioSource _audioSource;

        private List<Transform> _patrolPointList;
        private AudioClip[] _monsterNoiseClipsArray;
        
        private Transform _monsterTransform;
        
        private Transform _currentPatrolPointTransform;
        private int _currentPatrolPointIndex;

        private float _stoppingDistance = .25f;

        private float _timeDelayBeforeProducingSoundInSeconds = 4f;
        private float _passedTimeBeforeProducingSound;
        
        public RoamState(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
        {
            _monster = monster;
            _monsterTransform = monster.transform;
            _stateMachine = stateMachine;
            _audioSource = monster.AudioSource;
            _monsterNoiseClipsArray = monster.MonsterNoiseClipsArray;
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
            PlayRandomNoiseSound();
        }

        public override void ExitState()
        {
            _audioSource.Stop();
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

        private void PlayRandomNoiseSound()
        {
            _passedTimeBeforeProducingSound += Time.deltaTime;

            if (_passedTimeBeforeProducingSound < _timeDelayBeforeProducingSoundInSeconds)
                return;

            float chancePercent = Random.Range(0f, 1f) * 100f;
            float passChanceValuePercent = 50f;
            
            bool canPlay = chancePercent >= passChanceValuePercent;

            if (!canPlay)
            {
                _passedTimeBeforeProducingSound = 0f;
                return;
            }
            
            AudioClip noise = _monsterNoiseClipsArray.GetRandomClip();
            
            _audioSource.clip = noise;
            _audioSource.Play();
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
