using System.Collections;
using UnityEngine;
using Enums.Player;

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
        
        [Space(10)]
        [SerializeField] private float _sprintSpeed;
        [SerializeField] private float _delayTimeToMakeSprintAvailableInSeconds;

        [Space(10)]
        [SerializeField] private float _standingHeight = 2f;
        [SerializeField] private float _crouchingHeight = 1f;
        [SerializeField] private float _crouchSpeed;
        [SerializeField] private Vector3 _standingCenterPoint = new Vector3(0f, 1f, 0f);
        [SerializeField] private Vector3 _crouchingCenterPoint = new Vector3(0f, 0.5f, 0f);
        [SerializeField, Range(0.01f, 2f)] private float _timeToTransitionToCrouchInSeconds = 0.165f;
        
        [Space(10)]
        [SerializeField] private float _maxStaminaPercent;
        [SerializeField] private float _staminaRecoverDelayInSeconds;
        [SerializeField, Range(0.01f, 1f)] private float _staminaDecreaseRate;
        [SerializeField, Range(0.01f, 1f)] private float _staminaRecoverRate;
        [SerializeField] private float _transitionBetweenMovementSpeedInSeconds;
        
        [Space(10)]
        [SerializeField] private float _walkingFootstepTimeInSeconds;
        [SerializeField] private float _sprintingFootstepTimeInSeconds;

        [Space(10)]
        [SerializeField] private bool _sprintEnabled;
        [SerializeField] private bool _crouchEnabled; 
        
        private CharacterController _characterController;

        private Coroutine _gainMomentumRoutine;
        private Coroutine _delayStaminaRecoverRoutine;
        private Coroutine _recoverStaminaRoutine;
        private Coroutine _standCrouchRoutine;
        
        private float _staminaPercent;
        private float _currentMoveSpeed;
        private float _stepDelayTimer;
        private float _footstepTimer;

        private bool _isMoving;
        
        private bool _canSprint;
        private bool _isSprinting;
        private bool _stoppedSprinting;

        private bool _isCrouching;
        private bool _isDuringCrouchAnimation;
        
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
            if (_player.State != PlayerState.Exploring)
                return;
            
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

        public void Land()
        {
            Collider[] detectedColliders = Physics.OverlapSphere(_groundRaycastTransform.position, _groundDetectRadius,
                _walkableAreaLayerMask);
            
            // Call the event to notify that player has landed on the ground
            if (detectedColliders.Length > 0)
                _player.LandedEvent.Call(_player, new LandedEventArgs(detectedColliders[0].tag));
        }
        
        #region Sprint

        public void BeginSprint()
        {
            if (!_sprintEnabled) return;
            if (!_canSprint) return;
            if (!_isMoving) return;
            if (!IsGrounded()) return;
            if (_isCrouching) return;
            
            if (_recoverStaminaRoutine != null)
                StopCoroutine(_recoverStaminaRoutine);
            
            _player.SprintStateChangedEvent.Call(_player, new SprintStateChangedEventArgs(true));
            
            _gainMomentumRoutine = StartCoroutine(SpeedTransitionRoutine(_currentMoveSpeed, _sprintSpeed));
            _footstepTimer = _sprintingFootstepTimeInSeconds;
            _stoppedSprinting = false;
            _isSprinting = true;
        }

        public void Sprint()
        {
            if (!_sprintEnabled) return;
            if (!_canSprint) return;
            if (_stoppedSprinting) return;
            if (_isCrouching) return;

            if (!IsGrounded())
            {
                StopSprint();
                return;
            }
            
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
            
            DecreaseStaminaOverTimeBy(_staminaDecreaseRate);
            
            _player.MovementEvent.Call(this, new MovementEventArgs(true, true));
            _player.StaminaEvent.Call(this, new StaminaEventArgs(_staminaPercent, IsStaminaFull(_staminaPercent)));
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
            _player.SprintStateChangedEvent.Call(_player, new SprintStateChangedEventArgs(false));
            
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
            _player.StaminaEvent.Call(_player, new StaminaEventArgs(_staminaPercent, IsStaminaFull(_staminaPercent)));
        }

        private bool IsStaminaFull(float percent)
        {
            return percent >= _maxStaminaPercent;
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
                RecoverStaminaOverTimeBy(_staminaRecoverRate);
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

        #region Crouch

        public void BeginCrouch()
        {
            if (!_crouchEnabled) return;
            if (!IsGrounded()) return;
            if (_isDuringCrouchAnimation) return;

            StartCoroutine(StandCrouchRoutine());
        }

        public void Crouch()
        {
            
        }

        public void EndCrouch()
        {
            if (!_crouchEnabled) return;
            if (_isDuringCrouchAnimation) return;

            StartCoroutine(StandCrouchRoutine());
        }
        
        private IEnumerator StandCrouchRoutine()
        {
            _isDuringCrouchAnimation = true;
            
            float timeElapsed = 0;
            float targetHeight = _isCrouching ? _standingHeight : _crouchingHeight;
            float currentHeight = _characterController.height;

            Vector3 targetCenterPoint = _isCrouching ? _standingCenterPoint : _crouchingCenterPoint;
            Vector3 currentCenterPoint = _characterController.center;

            while (timeElapsed <= _timeToTransitionToCrouchInSeconds)
            {
                float t = timeElapsed / _timeToTransitionToCrouchInSeconds;
                
                // Here we change the actual height of character controller,
                // we interpolate smoothly using lerp functionality
                _characterController.center = Vector3.Lerp(currentCenterPoint, targetCenterPoint, t);
                _characterController.height = Mathf.Lerp(currentHeight, targetHeight, t);
                timeElapsed += Time.deltaTime;

                yield return null;
            }

            // Ensure everything
            _characterController.height = targetHeight;
            _characterController.center = targetCenterPoint;

            _isCrouching = !_isCrouching;
            _isDuringCrouchAnimation = false;
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
                _player.StepEvent.Call(_player, new StepEventArgs(detectedColliders[0].tag, _isSprinting));
            
            _footstepTimer = _isSprinting ? _sprintingFootstepTimeInSeconds : _walkingFootstepTimeInSeconds;
        }

        private void ResetFootstepTimer()
            => _footstepTimer = 0f;
    }
}