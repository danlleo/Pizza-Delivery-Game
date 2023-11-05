using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Flashlight = Player.Flashlight;

namespace Misc
{
    [DisallowMultipleComponent]
    public class KeyboardInput : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private UI.UI _ui;
        [SerializeField] private CharacterControllerMovement _movement;
        [SerializeField] private Interact _interact;
        [SerializeField] private Flashlight _flashlight;

        private float _horizontal;
        private float _vertical;
        
        private GameInput _gameInput;
        private Vector2 _moveVector;

        private void Awake()
        {
            _gameInput = new GameInput();
        }

        private void OnEnable()
        {
            _gameInput.Enable();
            
            _gameInput.Player.Movement.performed += Movement_OnPerformed;
            _gameInput.Player.Movement.canceled += Movement_OnCanceled;
            _gameInput.Player.Sprint.started += Sprint_OnStarted;
            _gameInput.Player.Sprint.performed += Sprint_OnPerformed;
            _gameInput.Player.Sprint.canceled += Sprint_OnCanceled;
            _gameInput.Player.Interact.performed += Interact_OnPerformed;
            _gameInput.Player.Flashlight.performed += Flashlight_OnPerformed;
            _gameInput.Player.Crouch.started += Crouch_OnStarted;
            _gameInput.Player.Crouch.performed += Crouch_OnPerformed;
            _gameInput.Player.Crouch.canceled += Crouch_OnCanceled;
        }

        private void OnDisable()
        {
            _gameInput.Disable();
            
            _gameInput.Player.Movement.performed -= Movement_OnPerformed;
            _gameInput.Player.Movement.canceled -= Movement_OnCanceled;
            _gameInput.Player.Sprint.started -= Sprint_OnStarted;
            _gameInput.Player.Sprint.performed -= Sprint_OnPerformed;
            _gameInput.Player.Sprint.canceled -= Sprint_OnCanceled;
            _gameInput.Player.Interact.performed -= Interact_OnPerformed;
            _gameInput.Player.Flashlight.performed -= Flashlight_OnPerformed;
            _gameInput.Player.Crouch.started -= Crouch_OnStarted;
            _gameInput.Player.Crouch.performed -= Crouch_OnPerformed;
            _gameInput.Player.Crouch.canceled -= Crouch_OnCanceled;
            
        }

        #region Input Events

        private void Movement_OnPerformed(InputAction.CallbackContext obj)
        {
            _moveVector = obj.ReadValue<Vector2>();
        }
        
        private void Movement_OnCanceled(InputAction.CallbackContext obj)
        {
            _moveVector = Vector2.zero;
        }
        
        private void Sprint_OnStarted(InputAction.CallbackContext obj)
        {
            
        }

        private void Sprint_OnPerformed(InputAction.CallbackContext obj)
        {
            
        }
        
        private void Sprint_OnCanceled(InputAction.CallbackContext obj)
        {
            
        }
        
        private void Interact_OnPerformed(InputAction.CallbackContext obj)
        {
            
        }
        
        private void Flashlight_OnPerformed(InputAction.CallbackContext obj)
        {
            
        }

        private void Crouch_OnStarted(InputAction.CallbackContext obj)
        {
            
        }

        private void Crouch_OnPerformed(InputAction.CallbackContext obj)
        {
            
        }

        private void Crouch_OnCanceled(InputAction.CallbackContext obj)
        {
            
        }
        
        #endregion
        
        private void Update()
        {
            if (!InputAllowance.InputEnabled) return;
            
            _movement.Move(_moveVector);
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _movement.BeginSprint();
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _movement.Sprint();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _movement.StopSprint();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _interact.TryInteract();
                _ui.InspectableObjectClosingEvent.Call(_ui);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _flashlight.ToggleLight();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                _movement.BeginCrouch();
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                _movement.Crouch();
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                _movement.EndCrouch();
            }
        }
    }
}
