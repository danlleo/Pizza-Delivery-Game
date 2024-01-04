using System;
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
        
        public InvestigateState(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
        {
            _monster = monster;
            _stateMachine = stateMachine;
        }

        public override void EnterState()
        {
            _investigatePosition = _monster.InvestigatePosition;
            _monsterTransform = _monster.transform;
        }

        public override void SubscribeToEvents()
        {
            _monster.DetectedTargetEvent.Event += DetectedTarget_Event;
        }

        public override void UnsubscribeFromEvents()
        {
            _monster.DetectedTargetEvent.Event -= DetectedTarget_Event;
        }
        
        public override void FrameUpdate()
        {
            MoveToPoint(_investigatePosition);
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
            _stateMachine.ChangeState(_monster.StateFactory.Chase());
        }

        #endregion
    }
}