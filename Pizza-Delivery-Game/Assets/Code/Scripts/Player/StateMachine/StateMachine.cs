using Enums.Gravity;
using UnityEngine;

namespace Player.StateMachine
{
    /// <summary>
    /// Stores the persistence state data that is passed to the active concrete states.
    /// This data is used for their logic and switching between states.
    /// </summary>
    public class StateMachine : MonoBehaviour
    {
        // CHARACTER CONTROLLER MOVEMENT
        [Header("External References")]
        [SerializeField] private Player _player;
        [SerializeField] private Transform _groundRaycastTransform;
        [SerializeField] private Camera _camera;
        
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
        [SerializeField, Range(1, 179)] private float _targetFOV;
        [SerializeField] private float _timeToTransitionToTargetFOVInSeconds;
        
        [Space(10)]
        [SerializeField] private bool _sprintEnabled;
        [SerializeField] private bool _crouchEnabled; 
        
        private CharacterController _characterController;

        private Coroutine _gainMomentumRoutine;
        private Coroutine _delayStaminaRecoverRoutine;
        private Coroutine _recoverStaminaRoutine;
        private Coroutine _standCrouchRoutine;
        
        private float _initialFOV;
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
        
        // GRAVITY PULLDOWN
        public bool IsGrounded { get; private set; }
        [Header("External References")]
        [SerializeField] private CharacterControllerMovement _characterControllerMovement;
        
        [Header("Settings")]
        [SerializeField] private GravityType _gravityType;
        
        private Vector3 _velocity;

        private float _gravityValue;

        private bool _hasLanded;
        
        // HEAD BOBBING
        [Header("External references")] 
        [SerializeField] private Camera _playerCamera;
        
        [Header("Settings")]
        [SerializeField] private float _walkBobVerticalFrequency = 14f;
        [SerializeField] private float _walkBobVerticalAmplitude = 0.05f;
        [SerializeField] private float _walkBobHorizontalFrequency;
        [SerializeField] private float _walkBobHorizontalAmplitude;
        
        [Space(10)]
        [SerializeField] private float _sprintBobVerticalFrequency = 18f;
        [SerializeField] private float _sprintBobVerticalAmplitude = 0.1f;
        [SerializeField] private float _sprintBobHorizontalFrequency;
        [SerializeField] private float _sprintBobHorizontalAmplitude;
        
        [Space(10)]
        [SerializeField] private float _stopBobbingTimeInSeconds = .25f;
        [SerializeField] private float _bobbingSmoothTime = 12.5f;
        
        [Space(10)]
        [SerializeField] private bool _headBobbingEnabled = true;

        private float _defaultYPosition;
        private float _defaultXPosition;
        private float _previousCameraYValue;
        private float _timerX;
        private float _timerY;

        private bool _cameraAtDefaultPosition;
        private bool _reachedPickHeight;
        
        // MOUSE ROTATION
        [Header("Settings")]
        [SerializeField] private float _mouseSensitivity = 5f;
        [SerializeField] private float _verticalClampValueInDegrees = 90f;
        [SerializeField, Range(0f, 1f)] private float _smoothingTime = 0.15f;
        [SerializeField] private bool _useSmoothing;

        private float _horizontalRotation;
        private float _verticalRotation;
        
        // STATE variable
        private State _currentState;
        private StateFactory _states;
        
        public State CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; }
        }

        private void Awake()
        {
            _states = new StateFactory(this);
            _currentState = _states.Grounded();
            _currentState.EnterState();
        }
    }
}
