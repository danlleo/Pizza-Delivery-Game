using System.Collections;
using UnityEngine;

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
        
        [Space(10)]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _sprintSpeed;
        [SerializeField] private float _delayTimeToMakeSprintAvailableInSeconds;
        
        [Space(10)]
        [SerializeField] private float _maxStaminaPercent;
        [SerializeField] private float _staminaRecoverDelayInSeconds;
        [SerializeField] private float _transitionBetweenMovementSpeedInSeconds;
        
        [Space(10)]
        [SerializeField] private float _walkingFootstepTimeInSeconds;
        [SerializeField] private float _sprintingFootstepTimeInSeconds;
        
        [Space(10)]
        [SerializeField] private bool _sprintEnabled;
        
        private CharacterController _characterController;

        private Coroutine _gainMomentumRoutine;
        private Coroutine _delayStaminaRecoverRoutine;
        private Coroutine _recoverStaminaRoutine;
        
        private float _staminaPercent;
        private float _currentMoveSpeed;
        private float _stepDelayTimer;
        private float _footstepTimer;

        private bool _canSprint;
        private bool _isMoving;
        private bool _isSprinting;
        private bool _stoppedSprinting;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _staminaPercent = _maxStaminaPercent;
            _currentMoveSpeed = _moveSpeed;
            _canSprint = true;
        }

        private void OnEnable()
        {
            _player.MovementEvent.Event += Movement_Event;
        }

        private void OnDisable()
        {
            _player.MovementEvent.Event -= Movement_Event;
        }

        private void Update()
        {
            if (_isMoving) return;
            
            _player.MovementEvent.Call(_player, new MovementEventArgs(false));
        }

        public void Move(Vector2 input)
        {
            // We multiple direction in which player is looking by direction on vertical input,
            // that way we can move him forward or backwards, plus we add player's horizontal direction 
            // by horizontal input, to move him to the right or left
            Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;
            moveDirection.Normalize();
            
            _isMoving = moveDirection != Vector3.zero;

            if (!_isSprinting)
                _player.MovementEvent.Call(_player, new MovementEventArgs(_isMoving));

            _characterController.Move(moveDirection * (_currentMoveSpeed * Time.deltaTime));
        }

        public bool IsGrounded()
        {
            return Physics.CheckSphere(_groundRaycastTransform.position, _groundDetectRadius, _walkableAreaLayerMask);
        }

        #region Sprint

        public void BeginSprint()
        {
            if (!_sprintEnabled) return;
            if (!_isMoving) return;
            
            _gainMomentumRoutine = StartCoroutine(SpeedTransitionRoutine(_currentMoveSpeed, _sprintSpeed));
            _footstepTimer = _sprintingFootstepTimeInSeconds;
            _stoppedSprinting = false;
        }

        public void Sprint()
        {
            if (!_sprintEnabled) return;
            if (!_canSprint) return;
            if (_stoppedSprinting) return;
            
            if (!_isMoving)
            {
                StopSprint();
                return;
            }
            
            if (_delayStaminaRecoverRoutine != null)
                StopCoroutine(_delayStaminaRecoverRoutine);
            
            if (_staminaPercent <= 0)
            {
                StopSprint();
                StartCoroutine(DelaySprintUsageRoutine());
                return;
            }
            
            DecreaseStaminaOverTimeBy(.20f);
            _player.MovementEvent.Call(this, new MovementEventArgs(true, true));
            _player.StaminaEvent.Call(this, new StaminaEventArgs(_staminaPercent));
        }
        
        public void StopSprint()
        {
            if (!_sprintEnabled) return;
            if (!_canSprint) return;
            if (_stoppedSprinting) return;
            
            if (_delayStaminaRecoverRoutine != null)
                StopCoroutine(_delayStaminaRecoverRoutine);
            
            if (_recoverStaminaRoutine != null)
                StopCoroutine(_recoverStaminaRoutine);
            
            if (_gainMomentumRoutine != null)
                StopCoroutine(_gainMomentumRoutine);
            
            _player.MovementEvent.Call(_player, new MovementEventArgs(_isMoving, false));

            _gainMomentumRoutine = StartCoroutine(SpeedTransitionRoutine(_currentMoveSpeed, _moveSpeed));
            _delayStaminaRecoverRoutine = StartCoroutine(DelayStaminaRecoverRoutine());
            _footstepTimer = _walkingFootstepTimeInSeconds; 
            _isSprinting = false;
            _stoppedSprinting = true;
        }
        
        #endregion

        #region Stamina

        private void DecreaseStaminaOverTimeBy(float decreaseValue)
        {
            if (_staminaPercent < 0f)
            {
                _staminaPercent = 0f;
                return;
            }
            
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
                _currentMoveSpeed = Mathf.Lerp(startSpeed, targetSpeed, t);
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

        private IEnumerator DelaySprintUsageRoutine()
        {
            _canSprint = false;
            yield return new WaitForSeconds(_delayTimeToMakeSprintAvailableInSeconds);
            _canSprint = true;
        }

        #endregion
        
        private void Movement_Event(object sender, MovementEventArgs e)
        {
            if (!IsGrounded()) return;
            
            if (!e.IsMoving)
            {
                ResetFootstepTimer();
                return;
            }
            
            _footstepTimer -= Time.deltaTime;

            if (!(_footstepTimer <= 0)) return;
            
            // Detect surfaces first
            Collider[] detectedColliders = Physics.OverlapSphere(_groundRaycastTransform.position, _groundDetectRadius,
                _walkableAreaLayerMask);

            if (detectedColliders.Length > 0)
                _player.StepEvent.Call(_player, new StepEventArgs(detectedColliders[0].tag));
            
            _footstepTimer = _isSprinting ? _sprintingFootstepTimeInSeconds : _walkingFootstepTimeInSeconds;
        }

        private void ResetFootstepTimer()
            => _footstepTimer = 0f;
    }
}