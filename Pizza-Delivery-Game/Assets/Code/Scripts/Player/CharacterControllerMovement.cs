using System.Collections;
using UnityEngine;
using Utilities;

namespace Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerMovement : MonoBehaviour
    {
        [Header("External References")]
        [SerializeField] private Player _player;
        [SerializeField] private Transform _groundRaycastTransform;

        [Header("Settings")] 
        [SerializeField] private LayerMask _walkableAreaLayerMask;
        
        [SerializeField, Range(0f, 5f)] private float _groundDetectRadius;
        [SerializeField, Range(0f, 2f)] private float _stepTimeDelayInSeconds;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _sprintSpeed;
        [SerializeField] private float _maxStaminaPercent;
        [SerializeField] private float _staminaRecoverDelayInSeconds;
        [SerializeField] private float _transitionBetweenMovementSpeedInSeconds;
        [SerializeField] private bool _sprintEnabled;
        
        private CharacterController _characterController;

        private Coroutine _gainMomentumRoutine;
        private Coroutine _delayStaminaRecoverRoutine;
        private Coroutine _recoverStaminaRoutine;
        
        private float _staminaPercent;
        private float _currentMoveSpeed;
        private float _stepDelayTimer;

        private bool _canSprint;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _staminaPercent = _maxStaminaPercent;
            _currentMoveSpeed = _moveSpeed;
        }

        private void OnEnable()
        {
            _player.MovementEvent.Event += Movement_Event;
        }

        private void OnDisable()
        {
            _player.MovementEvent.Event -= Movement_Event;
        }

        public void Move(Vector2 input)
        {
            // We multiple direction in which player is looking by direction on vertical input,
            // that way we can move him forward or backwards, plus we add player's horizontal direction 
            // by horizontal input, to move him to the right or left
            Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;
            moveDirection.Normalize();

            _player.MovementEvent.Call(_player,
                moveDirection != Vector3.zero ? new MovementEventArgs(true) : new MovementEventArgs(false));

            _characterController.Move(moveDirection * (_currentMoveSpeed * Time.deltaTime));
            _canSprint = input != Vector2.zero;
        }

        public bool IsGrounded()
        {
            return Physics.CheckSphere(_groundRaycastTransform.position, _groundDetectRadius, _walkableAreaLayerMask);
        }
        
        #region Sprint

        public void BeginSprint()
        {
            if (!_sprintEnabled)
                return;

            if (!_canSprint)
                return;
            
            _gainMomentumRoutine = StartCoroutine(SpeedTransitionRoutine(_currentMoveSpeed, _sprintSpeed));
            
            _player.MovementEvent.Call(this, new MovementEventArgs(true, true));
        }

        public void Sprint()
        {
            if (!_sprintEnabled)
                return;

            if (!_canSprint)
                return;
            
            if (_delayStaminaRecoverRoutine != null)
                StopCoroutine(_delayStaminaRecoverRoutine);
            
            if (_staminaPercent <= 0)
            {
                StopSprint();
                return;
            }
            
            DecreaseStaminaOverTimeBy(.20f);
            _player.StaminaEvent.Call(this, new StaminaEventArgs(_staminaPercent));
        }
        
        public void StopSprint()
        {
            if (!_sprintEnabled)
                return;
            
            if (_delayStaminaRecoverRoutine != null)
                StopCoroutine(_delayStaminaRecoverRoutine);
            
            if (_recoverStaminaRoutine != null)
                StopCoroutine(_recoverStaminaRoutine);
            
            if (_gainMomentumRoutine != null)
                StopCoroutine(_gainMomentumRoutine);
            
            _player.MovementEvent.Call(_player, new MovementEventArgs(true, false));

            _gainMomentumRoutine = StartCoroutine(SpeedTransitionRoutine(_currentMoveSpeed, _moveSpeed));
            _delayStaminaRecoverRoutine = StartCoroutine(DelayStaminaRecoverRoutine());
        }
        
        private void DecreaseStaminaOverTimeBy(float decreaseValue)
        {
            _staminaPercent -= decreaseValue;
        }

        private void RecoverStaminaOverTimeBy(float increaseValue)
        {
            if (_staminaPercent >= _maxStaminaPercent)
            {
                _staminaPercent = _maxStaminaPercent;
                return;
            }

            _staminaPercent += increaseValue;
            _player.StaminaEvent.Call(_player, new StaminaEventArgs(_staminaPercent));
        }

        private IEnumerator SpeedTransitionRoutine(float startSpeed, float targetSpeed)
        {
            var elapsedTime = 0f;

            while (elapsedTime <= _transitionBetweenMovementSpeedInSeconds)
            {
                float t = elapsedTime / _transitionBetweenMovementSpeedInSeconds;
                _currentMoveSpeed = Mathf.Lerp(startSpeed, targetSpeed, InterpolateUtils.EaseInQuart(t));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _currentMoveSpeed = targetSpeed;
        }
        
        private IEnumerator RecoverStaminaRoutine()
        {
            while (_staminaPercent < _maxStaminaPercent)
            {
                RecoverStaminaOverTimeBy(.10f);
                yield return null;
            }
        }

        private IEnumerator DelayStaminaRecoverRoutine()
        {
            yield return new WaitForSeconds(_staminaRecoverDelayInSeconds);
            _recoverStaminaRoutine = StartCoroutine(RecoverStaminaRoutine());
        }

        #endregion

        private void MakeStep(float additive)
        {
            _stepDelayTimer += Time.deltaTime + additive;

            if (!(_stepDelayTimer >= _stepTimeDelayInSeconds)) return;
            
            _player.StepEvent.Call(this);
            _stepDelayTimer = 0f;
        }
        
        private void Movement_Event(object sender, MovementEventArgs e)
        {
            if (!IsGrounded()) return;

            if (e.IsSprinting)
            {
                MakeStep(0.15f);
                return;
            }
            
            if (e.IsMoving)
            {
                MakeStep(0.075f);    
            }
        }
    }
}