using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Flashlight = Player.Flashlight;

namespace Misc
{
    [DisallowMultipleComponent]
    public class KeyboardInput : MonoBehaviour, GameInput.IPlayerActions
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
            _gameInput.Player.SetCallbacks(this);
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
        }

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
                    _movement.Sprint();
                    break;
                case InputActionPhase.Canceled:
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
                    _movement.Crouch();
                    break;
                case InputActionPhase.Canceled:
                    _movement.EndCrouch();
                    break;
            }
        }
    }
}
