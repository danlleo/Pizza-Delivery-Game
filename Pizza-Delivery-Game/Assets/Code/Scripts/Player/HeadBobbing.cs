using DG.Tweening;
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
        [Space(10)]
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
        
        [Space(10)]
        [SerializeField] private bool _headBobbingEnabled = true;

        private float _defaultYPosition;
        private float _defaultXPosition;
        private float _previousCameraYValue;
        private float _timer;

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
            if (!_headBobbingEnabled) return;
            
            HandleHeadBob();   
        }
        private void Movement_Event(object sender, MovementEventArgs e)
        {
            _isMoving = e.IsMoving;
            _isSprinting = e.IsSprinting;

            print(_isSprinting);
            
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
            
            _timer += Time.deltaTime * (_isSprinting ? _sprintBobVerticalFrequency : _walkBobVerticalFrequency);

            _playerCamera.transform.localPosition = new Vector3(
                -(_defaultXPosition + Mathf.Cos(_timer / 2) *
                    ((_isSprinting ? _sprintBobVerticalAmplitude : _walkBobVerticalAmplitude) * 2)),
                _defaultYPosition + Mathf.Sin(_timer) * (_isSprinting ? _sprintBobVerticalAmplitude : _walkBobVerticalAmplitude),
                _playerCamera.transform.localPosition.z
            );
        }
    }
}
