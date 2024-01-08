using System;
using Enums.Scenes;
using Environment.LaboratoryFirstLevel;
using EventBus;
using Interfaces;
using Misc;
using Misc.Loader;
using Player.Inventory;
using Tablet;
using UI.Crossfade;
using UnityEngine;

namespace Keypad
{
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class Keypad : MonoBehaviour, IInteractable
    {
        [Header("External References")]
        [SerializeField] private ButtonPress _buttonPress;
        [SerializeField] private ItemSO _flashLightItemSO;
        
        private BoxCollider _keypadBoxCollider;

        private EventBinding<PasswordValidationEvent> _passwordValidationEventBinding;
        private EventBinding<PasswordValidationResponseEvent> _passwordValidationEventResponseEventBinding;

        private bool _playerKnowsPassword;
        private bool _isAvailable;
        
        private void Awake()
        {
            _keypadBoxCollider = GetComponent<BoxCollider>();
            
            _buttonPress.enabled = false;
            _keypadBoxCollider.enabled = true;
        }

        private void OnEnable()
        {
            InteractedWithTabletStaticEvent.OnAnyInteractedWithTablet += OnAnyInteractedWithTablet;
            InspectedForbiddenStaticEvent.OnAnyInspectedForbidden += InspectedForbidden;
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
            InspectedForbiddenStaticEvent.OnAnyInspectedForbidden -= InspectedForbidden;
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

            if (!Player.Player.Instance.TryGetComponent(out Inventory inventory))
                throw new Exception("Didn't get the inventory component from the player");

            if (!inventory.HasItem(_flashLightItemSO))
            {
                this.CallNoFlashlightStaticEvent();
                return;
            }
            
            InputAllowance.DisableInput();
            EventBus<InteractedWithKeypadEvent>.Raise(new InteractedWithKeypadEvent());
            CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(false));
            CursorLockStateChangedStaticEvent.Call(this, new CursorLockStateChangedStaticEventArgs(false));

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
            if (!passwordValidationResponseEvent.IsCorrect)
                return;

            ServiceLocator.ServiceLocator.GetCrossfadeService().FadeIn(InputAllowance.DisableInput,
                () => Loader.Load(Scene.SecondLaboratoryLevelScene));
            
            Destroy(this);
        }
        
        private void OnAnyInteractedWithTablet(object sender, EventArgs e)
        {
            SetPlayerKnowsPassword();
        }
        
        private void InspectedForbidden(object sender, EventArgs e)
        {
            _isAvailable = false;
        }
        
        private void OnAnyTriggeredScreamer(object sender, EventArgs e)
        {
            _isAvailable = true;
        }
    }
}
