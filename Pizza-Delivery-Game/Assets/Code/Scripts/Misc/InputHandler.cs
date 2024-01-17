using System;
using Environment.Bedroom;
using Environment.Bedroom.PC;
using EventBus;
using Keypad;
using Player;
using Player.Inventory;
using Tablet;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Utilities;

namespace Misc
{
    [DisallowMultipleComponent]
    public class InputHandler : 
        MonoBehaviour, 
        GameInput.IPlayerActions,
        GameInput.IUIActions,
        GameInput.IPCActions, 
        GameInput.IKeypadActions, 
        GameInput.ITabletActions, 
        GameInput.IPauseMenuActions,
        GameInput.IUIConfirmationActions
    {
        [Header("External references")] 
        [SerializeField] private UI.UI _ui;
        [SerializeField] private Player.Player _player;
        [SerializeField] private CharacterControllerMovement _movement;
        [FormerlySerializedAs("_mouseRotation")] [SerializeField] private MouseLook _mouseLook;
        [SerializeField] private Interact _interact;
        [SerializeField] private Flashlight _flashlight;
        
        private GameInput _gameInput;
        private Vector2 _moveVector;
        
        private bool _sprintButtonHeld;
        private bool _crouchButtonHeld;

        private Vector3 _rotateDirection;

        private EventBinding<InteractedWithKeypadEvent> _interactedWithKeypadEventBinding;
        
        private void OnEnable()
        {
            _gameInput = new GameInput();
            _gameInput.Player.SetCallbacks(this);
            _gameInput.UI.SetCallbacks(this);
            _gameInput.PC.SetCallbacks(this);
            _gameInput.Tablet.SetCallbacks(this);
            _gameInput.Keypad.SetCallbacks(this);
            _gameInput.PauseMenu.SetCallbacks(this);
            _gameInput.UIConfirmation.SetCallbacks(this);
            _gameInput.SetDefaultActionMap(nameof(_gameInput.Player));
            
            UIOpenedStaticEvent.OnUIOpen += UIOpenedStaticEvent_OnUIOpen;
            UIClosedStaticEvent.OnUIClose += UIClosedStaticEvent_OnUIClose;
            OnAnyStartedUsingPC.Event += StartedUsingPCStaticEventEvent;
            WokeUpStaticEvent.OnWokeUp += OnAnyWokeUp;
            PickedUpStaticEvent.OnTabletPickedUp += OnAnyTabletPickedUp;
            PutDownStaticEvent.OnTabletPutDown += OnAnyTabletPutDown;
            TimeControl.OnAnyGamePaused.Event += OnAnyGamePaused;
            TimeControl.OnAnyGameUnpaused.Event += OnAnyGameUnpaused;
            Player.Inventory.OnAnyItemUse.Event += OnAnyItemUse;
            Player.Inventory.OnAnyItemUseDeclined.Event += OnAnyItemUseDeclined;
            
            _interactedWithKeypadEventBinding =
                new EventBinding<InteractedWithKeypadEvent>(HandleInteractedWithKeypadEvent);

            EventBus<InteractedWithKeypadEvent>.Register(_interactedWithKeypadEventBinding);
        }
        
        private void OnDisable()
        {
            _gameInput.Disable();
            
            UIOpenedStaticEvent.OnUIOpen -= UIOpenedStaticEvent_OnUIOpen;
            UIClosedStaticEvent.OnUIClose -= UIClosedStaticEvent_OnUIClose;
            OnAnyStartedUsingPC.Event -= StartedUsingPCStaticEventEvent;
            WokeUpStaticEvent.OnWokeUp -= OnAnyWokeUp;
            PickedUpStaticEvent.OnTabletPickedUp -= OnAnyTabletPickedUp;
            PutDownStaticEvent.OnTabletPutDown -= OnAnyTabletPutDown;
            TimeControl.OnAnyGamePaused.Event -= OnAnyGamePaused;
            TimeControl.OnAnyGameUnpaused.Event -= OnAnyGameUnpaused;
            Player.Inventory.OnAnyItemUse.Event -= OnAnyItemUse;
            Player.Inventory.OnAnyItemUseDeclined.Event -= OnAnyItemUseDeclined;
            
            EventBus<InteractedWithKeypadEvent>.Deregister(_interactedWithKeypadEventBinding);
        }
        
        private void Update()
        {
            _movement.Move(_moveVector);
            
            if (_sprintButtonHeld)
                _movement.Sprint();
            
            if (_crouchButtonHeld)
                _movement.Crouch();
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

        #region PC Events

        private void StartedUsingPCStaticEventEvent(object sender, EventArgs e)
        {
            _gameInput.SetDefaultActionMap(nameof(_gameInput.PC));
        }

        #endregion

        #region BedroomEvents

        private void OnAnyWokeUp(object sender, EventArgs e)
        {
            _gameInput.SetDefaultActionMap(nameof(_gameInput.Player));
        }

        #endregion
        
        #region TabletEvents

        private void OnAnyTabletPickedUp(object sender, EventArgs e)
        {
            InputAllowance.DisableInput();
            _gameInput.SetDefaultActionMap(nameof(_gameInput.Tablet));
        }
        
        private void OnAnyTabletPutDown(object sender, EventArgs e)
        {
            InputAllowance.EnableInput();
            _gameInput.SetDefaultActionMap(nameof(_gameInput.Player));
        }

        #endregion

        #region UIConfirmationEvents

        private void OnAnyItemUse(object sender, OnAnyItemUseEventArgs e)
        {
            _gameInput.SetDefaultActionMap(nameof(_gameInput.UIConfirmation));
        }
        
        private void OnAnyItemUseDeclined(object sender, EventArgs e)
        {
            _gameInput.SetDefaultActionMap(nameof(_gameInput.Player));
        }

        #endregion
        
        #region TimeControlEvents

        private void OnAnyGamePaused(object sender, EventArgs e)
        {
            _gameInput.SetDefaultActionMap(nameof(_gameInput.PauseMenu));
        }

        private void OnAnyGameUnpaused(object sender, EventArgs e)
        {
            _gameInput.SetDefaultActionMap(nameof(_gameInput.Player));
        }

        #endregion
        
        #region PlayerInputActions

        public void OnMovement(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    _moveVector = context.ReadValue<Vector2>();
                    break;
                case InputActionPhase.Canceled:
                    _moveVector = Vector2.zero;
                    break;
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                case InputActionPhase.Started:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    _interact.TryInteract();
                    break;
                case InputActionPhase.Started:
                    break;
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                case InputActionPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnFlashlight(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _flashlight.ToggleLight();
                    break;
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                case InputActionPhase.Performed:
                    break;
                case InputActionPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
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
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
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
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    TimeControl.OnAnyGamePaused.Call(this);
                    break;
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                case InputActionPhase.Started:
                    break;
                case InputActionPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            _rotateDirection = context.ReadValue<Vector2>();
            _mouseLook.RotateTowards(_rotateDirection);
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
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                case InputActionPhase.Performed:
                    break;
                case InputActionPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        #endregion

        #region IPCActions

        public void OnClick(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _player.CallClickedStaticEvent();
                    break;
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                case InputActionPhase.Performed:
                    break;
                case InputActionPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region ITabletActions

        public void OnTabletPutDown(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    PutDownStaticEvent.Call(this);
                    break;
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                case InputActionPhase.Performed:
                    break;
                case InputActionPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
        
        #region IKeypadActions

        private void HandleInteractedWithKeypadEvent(InteractedWithKeypadEvent interactedWithKeypadEvent)
        {
            _gameInput.SetDefaultActionMap(nameof(_gameInput.Keypad));
        }
        
        public void OnButtonPress(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    EventBus<ButtonPressedEvent>.Raise(new ButtonPressedEvent());
                    break;
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                case InputActionPhase.Performed:
                    break;
                case InputActionPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region IPauseMenuActions

        public void OnUnpause(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                case InputActionPhase.Started:
                    break;
                case InputActionPhase.Performed:
                    if (!InputAllowance.InputEnabled)
                        return;
                    
                    TimeControl.OnAnyGameUnpaused.Call(this);
                    break;
                case InputActionPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}
