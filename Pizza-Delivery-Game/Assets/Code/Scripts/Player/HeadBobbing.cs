using DG.Tweening;
using Enums.Player;
using Misc;
using UnityEngine;

namespace Player
{
    public class HeadBobbing : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Player _player;
        [SerializeField] private CharacterControllerMovement _characterControllerMovement;
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

        private bool _isMoving;
        private bool _isSprinting;
        private bool _cameraAtDefaultPosition;
        private bool _reachedPickHeight;

        private void Awake()
        {
            _defaultYPosition = _playerCamera.transform.localPosition.y;
            _defaultXPosition = _playerCamera.transform.localPosition.x;
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
            if (_player.State != PlayerState.Exploring) return;
            if (!_headBobbingEnabled) return;
            if (!InputAllowance.InputEnabled) return;
            
            HandleHeadBob();   
        }
        private void Movement_Event(object sender, MovementEventArgs e)
        {
            _isMoving = e.IsMoving;
            _isSprinting = e.IsSprinting;
            
            if (_isMoving)
            {
                _cameraAtDefaultPosition = false;
                return;
            }

            if (_cameraAtDefaultPosition) return;
            
            ResetCameraPosition();
        }

        private void ResetCameraPosition()
        {
            _playerCamera.transform.DOLocalMoveY(_defaultYPosition, _stopBobbingTimeInSeconds);
            _playerCamera.transform.DOLocalMoveX(_defaultXPosition, _stopBobbingTimeInSeconds);
            _cameraAtDefaultPosition = true;
        }
        
        private void HandleHeadBob()
        {
            if (!_isMoving) return;
            if (!_characterControllerMovement.IsGrounded()) return;

            _timerX += Time.deltaTime * (_isSprinting ? _sprintBobHorizontalFrequency : _walkBobHorizontalFrequency);
            _timerY += Time.deltaTime * (_isSprinting ? _sprintBobVerticalFrequency : _walkBobVerticalFrequency);

            Vector3 targetPosition = _playerCamera.transform.localPosition;

            // The amplitude controls the size of it,
            // frequency controls the speed
            
            targetPosition.y = Mathf.Lerp(targetPosition.y, Mathf.Sin(_timerY) * (_isSprinting ? _sprintBobVerticalAmplitude : _walkBobVerticalAmplitude),
                Time.deltaTime * _bobbingSmoothTime);
            targetPosition.x = Mathf.Lerp(targetPosition.x, Mathf.Cos(_timerX / 2) * (_isSprinting ? _sprintBobHorizontalAmplitude : _walkBobHorizontalAmplitude),
                Time.deltaTime * _bobbingSmoothTime);

            _playerCamera.transform.localPosition = targetPosition;
        }
    }
}
