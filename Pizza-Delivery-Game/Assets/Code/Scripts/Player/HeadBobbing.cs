using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class HeadBobbing : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Player _player;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Camera _playerCamera;
        
        [Header("Settings")]
        [SerializeField] private bool _enabled = true;

        [SerializeField] private float _walkBobSpeed = 14f;
        [SerializeField] private float _walkBobAmount = 0.05f;
        [SerializeField] private float _sprintBobSpeed = 18f;
        [SerializeField] private float _sprintBobAmount = 0.1f;
        [SerializeField] private float _stopBobbingTimeInSeconds = .25f;
        
        private float _defaultYPosition;
        private float _timer;

        private bool _isMoving;
        private bool _isSprinting;
        private bool _cameraAtDefaultPosition;

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
            if (!_characterController.isGrounded) return;
            if (!_isMoving) return;
            
            _timer += Time.deltaTime * (_isSprinting ? _sprintBobSpeed : _walkBobSpeed);
            
            _playerCamera.transform.localPosition = new Vector3(
                _playerCamera.transform.localPosition.x,
                _defaultYPosition + Mathf.Sin(_timer) * (_isSprinting ? _sprintBobAmount : _walkBobAmount),
                _playerCamera.transform.localPosition.z
            );
        }
    }
}
