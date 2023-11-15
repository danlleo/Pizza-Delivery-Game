using System;
using Environment.Bedroom.PC;
using Player;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Misc
{
    [DisallowMultipleComponent]
    public class InputReader : MonoBehaviour, GameInput.IPlayerActions, GameInput.IUIActions, GameInput.IPCActions
    {
        [Header("External references")] 
        [SerializeField] private UI.UI _ui;
        [SerializeField] private Player.Player _player;
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
            _gameInput.Player.SetCallbacks(this);
            _gameInput.UI.SetCallbacks(this);
            _gameInput.PC.SetCallbacks(this);            
            _gameInput.SetDefaultActionMap(nameof(_gameInput.Player));
        }
        
        private void OnEnable()
        {
            UIOpenedStaticEvent.OnUIOpen += UIOpenedStaticEvent_OnUIOpen;
            UIClosedStaticEvent.OnUIClose += UIClosedStaticEvent_OnUIClose;
            StartedUsingPCStaticEvent.OnStarted += StartedUsingPCStaticEvent_OnStarted;
            StoppedUsingPCStaticEvent.OnEnded += StoppedUsingPCStaticEvent_OnEnded;
        }
        
        private void OnDisable()
        {
            _gameInput.Disable();
            
            UIOpenedStaticEvent.OnUIOpen -= UIOpenedStaticEvent_OnUIOpen;
            UIClosedStaticEvent.OnUIClose -= UIClosedStaticEvent_OnUIClose;
            StartedUsingPCStaticEvent.OnStarted -= StartedUsingPCStaticEvent_OnStarted;
            StoppedUsingPCStaticEvent.OnEnded -= StoppedUsingPCStaticEvent_OnEnded;
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
        
        #region UI Events

        private void UIClosedStaticEvent_OnUIClose(object sender, EventArgs e)
        {
            _gameInput.SetDefaultActionMap(nameof(_gameInput.Player));
        }

        private void UIOpenedStaticEvent_OnUIOpen(object sender, EventArgs e)
        {
            _gameInput.SetDefaultActionMap(nameof(_gameInput.UI));
        }

        #endregion

        #region PCEvents

        private void StartedUsingPCStaticEvent_OnStarted(object sender, EventArgs e)
        {
            _gameInput.SetDefaultActionMap(nameof(_gameInput.PC));
        }
        
        private void StoppedUsingPCStaticEvent_OnEnded(object sender, EventArgs e)
        {
            _gameInput.SetDefaultActionMap(nameof(_gameInput.Player));
        }

        #endregion

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

        #region IUIInputActions

        public void OnSubmit(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _ui.InspectableObjectClosingEvent.Call(_ui);
                    break;
            }
        }
        
        #endregion

        #region IPCActions

        public void OnClick(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    ClickedStaticEvent.Call(_player);
                    break;
            }
        }

        #endregion
    }
}
