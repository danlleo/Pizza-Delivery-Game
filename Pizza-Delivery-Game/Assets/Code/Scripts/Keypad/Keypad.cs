using System;
using Enums.Scenes;
using Environment.LaboratoryFirstLevel;
using EventBus;
using Interfaces;
using Misc;
using Misc.CursorLockState;
using Misc.Loader;
using Player.Inventory;
using Tablet;
using UI.Crossfade;
using UnityEngine;
using Zenject;

namespace Keypad
{
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class Keypad : MonoBehaviour, IInteractable
    {
        [Header("External References")]
        [SerializeField] private ButtonPress _buttonPress;
        [SerializeField] private ItemSO _flashLightItemSO;

        private Player.Player _player;
        private Crossfade _crossfade;
        private CursorLockState _cursorLockState;
        
        private BoxCollider _keypadBoxCollider;

        private EventBinding<PasswordValidationEvent> _passwordValidationEventBinding;
        private EventBinding<PasswordValidationResponseEvent> _passwordValidationEventResponseEventBinding;

        private bool _playerKnowsPassword;
        private bool _isAvailable;

        [Inject]
        private void Construct(Player.Player player, Crossfade crossfade, CursorLockState cursorLockState)
        {
            _player = player;
            _crossfade = crossfade;
            _cursorLockState = cursorLockState;
        }
        
        private void Awake()
        {
            _keypadBoxCollider = GetComponent<BoxCollider>();
            
            _buttonPress.enabled = false;
            _keypadBoxCollider.enabled = true;
            _isAvailable = true;
        }

        private void OnEnable()
        {
            InteractedWithTabletStaticEvent.OnAnyInteractedWithTablet += OnAnyInteractedWithTablet;
            InspectedForbiddenStaticEvent.OnAnyInspectedForbidden += OnAnyInspectedForbidden;
            TriggeredScreamerStaticEvent.OnAnyTriggeredScreamer += OnAnyTriggeredScreamer;
            
            _passwordValidationEventBinding = new EventBinding<PasswordValidationEvent>(HandlePasswordValidationEvent);
            EventBus<PasswordValidationEvent>.Register(_passwordValidationEventBinding);

            _passwordValidationEventResponseEventBinding =
                new EventBinding<PasswordValidationResponseEvent>(HandlePasswordValidationResponseEvent);
            EventBus<PasswordValidationResponseEvent>.Register(_passwordValidationEventResponseEventBinding);
        }

        private void OnDisable()
        {
            InteractedWithTabletStaticEvent.OnAnyInteractedWithTablet -= OnAnyInteractedWithTablet;
            InspectedForbiddenStaticEvent.OnAnyInspectedForbidden -= OnAnyInspectedForbidden;
            TriggeredScreamerStaticEvent.OnAnyTriggeredScreamer -= OnAnyTriggeredScreamer;
            
            EventBus<PasswordValidationEvent>.Deregister(_passwordValidationEventBinding);
            EventBus<PasswordValidationResponseEvent>.Deregister(_passwordValidationEventResponseEventBinding);
        }

        public void Interact()
        {
            if (!_isAvailable) return;
            
            if (!_playerKnowsPassword)
            {
                PasswordUnknownStaticEvent.Call(this);
                return;
            }
            
            if (!_player.Inventory.HasItem(_flashLightItemSO))
            {
                this.CallNoFlashlightStaticEvent();
                return;
            }
            
            InputAllowance.DisableInput();
            EventBus<InteractedWithKeypadEvent>.Raise(new InteractedWithKeypadEvent());
            CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(false));
            
            _cursorLockState.UnlockCursor();

            _buttonPress.enabled = true;
            _keypadBoxCollider.enabled = false;
        }

        public string GetActionDescription()
        {
            return "Keypad";
        }

        private void SetPlayerKnowsPassword()
            => _playerKnowsPassword = true;
        
        private void ValidatePassword(string password)
        {
            bool isCorrect = password == Password.CorrectPassword;

            EventBus<PasswordValidationResponseEvent>.Raise(new PasswordValidationResponseEvent(isCorrect));
        }
        
        private void HandlePasswordValidationEvent(PasswordValidationEvent passwordValidationEvent)
        {
            ValidatePassword(passwordValidationEvent.Password);
        }

        private void HandlePasswordValidationResponseEvent(PasswordValidationResponseEvent passwordValidationResponseEvent)
        {
            if (!_isAvailable) return;
            
            if (!passwordValidationResponseEvent.IsCorrect)
                return;

            _isAvailable = false;
            _cursorLockState.LockCursor();
            _crossfade.FadeIn(InputAllowance.DisableInput,
                () => Loader.Load(Scene.SecondLaboratoryLevelScene), 1.5f);
        }
        
        private void OnAnyInteractedWithTablet(object sender, EventArgs e)
        {
            SetPlayerKnowsPassword();
        }
        
        private void OnAnyInspectedForbidden(object sender, EventArgs e)
        {
            _isAvailable = false;
        }
        
        private void OnAnyTriggeredScreamer(object sender, EventArgs e)
        {
            _isAvailable = true;
        }
    }
}
