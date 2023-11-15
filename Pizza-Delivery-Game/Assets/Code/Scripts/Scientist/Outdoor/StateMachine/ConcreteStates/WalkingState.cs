using UnityEngine;

namespace Scientist.Outdoor.StateMachine.ConcreteStates
{
    public class WalkingState : State
    {
        private Transform _scientistTransform;
        private Transform _targetTransform;
        
        private float _rotateSpeed = 10f;
        private float _stoppingDistance = .1f;
        private float _moveSpeed = 2f;
        
        public WalkingState(Scientist scientist, StateMachine stateMachine) : base(scientist, stateMachine)
        {
            _scientistTransform = scientist.transform;
            _targetTransform = scientist.CarTransform;
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
                Debug.Log("Reached");
            }
        }
    }
}
