using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class HeadBobbing : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Player _player;
        [SerializeField] private CharacterControllerMovement _characterControllerMovement;
        [SerializeField] private Camera _playerCamera;
        
        [Header("Settings")]
        [SerializeField] private bool _enabled = true;

        [FormerlySerializedAs("_walkBobSpeed")] [SerializeField] private float _walkBobFrequency = 14f;
        [FormerlySerializedAs("_walkBobAmount")] [SerializeField] private float _walkBobAmplitude = 0.05f;
        [FormerlySerializedAs("_sprintBobSpeed")] [SerializeField] private float _sprintBobFrequency = 18f;
        [FormerlySerializedAs("_sprintBobAmount")] [SerializeField] private float _sprintBobAmplitude = 0.1f;
        [SerializeField] private float _stopBobbingTimeInSeconds = .25f;

        private float _defaultYPosition;
        private float _previousCameraYValue;
        private float _timer;

        private bool _isMoving;
        private bool _isSprinting;
        private bool _cameraAtDefaultPosition;
        private bool _reachedPickHeight;

        private void Awake()
        {
            _defaultYPosition = _playerCamera.transform.localPosition.y;
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
            if (!_enabled) return;
            
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
            _cameraAtDefaultPosition = true;
        }
        
        private void HandleHeadBob()
        {
            if (!_isMoving) return;
            if (!_characterControllerMovement.IsGrounded()) return;
            
            _timer += Time.deltaTime * (_isSprinting ? _sprintBobFrequency : _walkBobFrequency);
            
            _playerCamera.transform.localPosition = new Vector3(
                _playerCamera.transform.localPosition.x,
                _defaultYPosition + Mathf.Sin(_timer) * (_isSprinting ? _sprintBobAmplitude : _walkBobAmplitude),
                _playerCamera.transform.localPosition.z
            );
        }
    }
}
