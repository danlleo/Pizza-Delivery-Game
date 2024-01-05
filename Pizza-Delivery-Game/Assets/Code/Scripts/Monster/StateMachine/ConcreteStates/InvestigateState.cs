using System;
using Environment.LaboratorySecondLevel;
using UnityEngine;

namespace Monster.StateMachine.ConcreteStates
{
    public class InvestigateState : State
    {
        private Monster _monster;
        private Transform _monsterTransform;
        private StateMachine _stateMachine;

        private Vector3 _investigatePosition;
        
        private float _stoppingDistance = 1f;

        private bool _hasDetectedPlayer;
        
        public InvestigateState(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
        {
            _monster = monster;
            _stateMachine = stateMachine;
        }

        public override void EnterState()
        {
            _investigatePosition = _monster.InvestigatePosition;
            _monsterTransform = _monster.transform;
            
            _monster.CallMonsterStartedInvestigatingStaticEvent();
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
            MoveToPoint(_investigatePosition);
        }

        public override void ExitState()
        {
            _monster.CallMonsterStoppedInvestigatingStaticEvent(
                new MonsterStoppedInvestigatingEventArgs(_hasDetectedPlayer));
        }

        private void MoveToPoint(Vector3 point)
        {
            _monster.NavMeshAgent.SetDestination(point);

            if (Vector3.Distance(_monsterTransform.position, point) <= _stoppingDistance)
            {
                _stateMachine.ChangeState(_monster.StateFactory.Roam());
            }
        }
        
        #region Events

        private void DetectedTarget_Event(object sender, EventArgs e)
        {
            _hasDetectedPlayer = true;
            _stateMachine.ChangeState(_monster.StateFactory.Chase());
        }
        
        private void OnAnyAttractedMonster(object sender, AttractedMonsterEventArgs e)
        {
            _investigatePosition = e.AttractedPosition;
        }

        #endregion
    }
}