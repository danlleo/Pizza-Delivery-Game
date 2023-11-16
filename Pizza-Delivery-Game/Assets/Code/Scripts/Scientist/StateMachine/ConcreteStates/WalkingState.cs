using System;
using Enums.Scientist;
using UnityEngine;

namespace Scientist.StateMachine.ConcreteStates
{
    public class WalkingState : State
    {
        private Scientist _scientist;
        private StateMachine _stateMachine;
        
        private Transform _scientistTransform;
        private Transform _targetTransform;
        
        private float _rotateSpeed = 10f;
        private float _stoppingDistance = .1f;
        private float _moveSpeed = 2f;
        
        public WalkingState(Scientist scientist, StateMachine stateMachine) : base(scientist, stateMachine)
        {
            _scientist = scientist;
            _scientistTransform = scientist.transform;
            _targetTransform = scientist.endWalkingPointTransform;
            _stateMachine = stateMachine;
        }

        public override void EnterState()
        {
            _scientist.StartedWalkingEvent.Call(_scientist);
        }
        
        public override void FrameUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 moveDirection = (_targetTransform.position - _scientistTransform.position).normalized;
            
            if (Vector3.Distance(_targetTransform.position, _scientistTransform.position) > _stoppingDistance)
            {
                _scientistTransform.forward = Vector3.Slerp(_scientistTransform.forward, moveDirection,
                    Time.deltaTime * _rotateSpeed);
                _scientistTransform.position += _moveSpeed * Time.deltaTime * moveDirection;
            }
            else
            {
                switch (_scientist.ScientistType)
                {
                    case ScientistType.Outdoor:
                        _scientist.StoppedWalkingEvent.Call(_scientist);
                        _scientist.DestroySelf();
                        break;
                    case ScientistType.LaboratoryEntry:
                        _scientist.StoppedWalkingEvent.Call(_scientist);
                        _stateMachine.ChangeState(_scientist.StateFactory.OpeningDoor());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
