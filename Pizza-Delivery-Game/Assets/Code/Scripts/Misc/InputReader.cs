using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Flashlight = Player.Flashlight;

namespace Misc
{
    [DisallowMultipleComponent]
    public class InputReader : MonoBehaviour, GameInput.IPlayerInputActions
    {
        [Header("External references")] 
        [SerializeField] private UI.UI _ui;
        [SerializeField] private CharacterControllerMovement _movement;
        [SerializeField] private MouseRotation _mouseRotation;
        [SerializeField] private Interact _interact;
        [SerializeField] private Flashlight _flashlight;
        
        private GameInput _gameInput;
        private Vector2 _moveVector;
        
        private bool _sprintButtonHeld;
        private bool _crouchButtonHeld;

        private Vector3 _rotateDirection;

        private void Awake()
        {
            _gameInput = new GameInput();
            _gameInput.PlayerInput.SetCallbacks(this);
        }

        private void OnEnable()
        {
            _gameInput.Enable();
        }

        private void OnDisable()
        {
            _gameInput.Disable();
        }
        
        private void Update()
        {
            if (!InputAllowance.InputEnabled) return;
            
            _movement.Move(_moveVector);
            
            if (_sprintButtonHeld)
                _movement.Sprint();
            
            if (_crouchButtonHeld)
                _movement.Crouch();
        }

        private void LateUpdate()
        {
            if (!InputAllowance.InputEnabled) return;
            
            float mouseX = Input.GetAxisRaw(Axis.MouseX);
            float mouseY = Input.GetAxisRaw(Axis.MouseY);

            _rotateDirection = new Vector2(mouseX, mouseY);
            
            _mouseRotation.RotateTowards(_rotateDirection);
        }

        #region PlayerInputActions

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (!InputAllowance.InputEnabled) return;
            
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    _moveVector = context.ReadValue<Vector2>();
                    break;
                case InputActionPhase.Canceled:
                    _moveVector = Vector2.zero;
                    break;
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (!InputAllowance.InputEnabled) return;
            
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _interact.TryInteract();
                    _ui.InspectableObjectClosingEvent.Call(_ui);
                    break;
            }
        }

        public void OnFlashlight(InputAction.CallbackContext context)
        {
            if (!InputAllowance.InputEnabled) return;

            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _flashlight.ToggleLight();
                    break;
            }
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (!InputAllowance.InputEnabled) return;
            
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _movement.BeginSprint();
                    break;
                case InputActionPhase.Performed:
                    _sprintButtonHeld = true;
                    break;
                case InputActionPhase.Canceled:
                    _sprintButtonHeld = false;
                    _movement.StopSprint();
                    break;
            }
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (!InputAllowance.InputEnabled) return;
            
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _movement.BeginCrouch();
                    break;
                case InputActionPhase.Performed:
                    _crouchButtonHeld = true;
                    break;
                case InputActionPhase.Canceled:
                    _crouchButtonHeld = false;
                    _movement.EndCrouch();
                    break;
            }
        }

        #endregion
    }
}
