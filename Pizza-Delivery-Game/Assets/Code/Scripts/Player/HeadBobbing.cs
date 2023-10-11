using UnityEngine;

namespace Player
{
    public class HeadBobbing : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Camera _playerCamera;
        
        [Header("Settings")]
        [SerializeField] private bool _enabled = true;

        [SerializeField] private float _walkBobSpeed = 14f;
        [SerializeField] private float _walkBobAmount = 0.5f;

        private float _defaultYPosition;
        private float _timer;

        private void Awake()
        {
            _defaultYPosition = _playerCamera.transform.localPosition.y;
        }

        private void Update()
        {
            if (!_enabled) return;
            
            HandleHeadBob();   
        }

        private void HandleHeadBob()
        {
            if (!_characterController.isGrounded) return;
            
            
        }
    }
}
