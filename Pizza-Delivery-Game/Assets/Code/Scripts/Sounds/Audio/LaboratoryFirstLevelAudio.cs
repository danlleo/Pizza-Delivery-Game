using System;
using Door;
using Environment.LaboratoryFirstLevel;
using EventBus;
using Keypad;
using Tablet;
using UnityEngine;

namespace Sounds.Audio
{
    public class LaboratoryFirstLevelAudio : AudioPlayer
    {
        [SerializeField] private AudioClip _keycardAcceptedClip;
        [SerializeField] private AudioClip _keycardDeclinedClip;
        
        [Space(5)]
        [SerializeField] private AudioClip _doorOpenClip;
        [SerializeField] private AudioClip _monsterPeekClip;

        [Space(5)] 
        [SerializeField] private AudioClip _gasLeakClip;
        [SerializeField] private AudioClip _gasHissingClip;
        [SerializeField] private AudioClip _gasLeakSpookyClip;
        
        [Space(5)]
        [SerializeField] private AudioClip _buttonPressClip;
        [SerializeField] private AudioClip _keypadDeniedClip;
        [SerializeField] private AudioClip _keypadGrantedClip;

        [Space(5)] 
        [SerializeField] private AudioClip _pickupClip;
        [SerializeField] private AudioClip _putdownClip;
        
        [Space(5)]
        [SerializeField] private AudioClip _screamerClip;
        
        private AudioSource _audioSource;
        
        private EventBinding<DigitRegisteredEvent> _digitRegisteredEventBinding;
        private EventBinding<PasswordValidationResponseEvent> _passwordValidationResponseEventBinding;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            DoorOpenStaticEvent.OnDoorOpened += DoorOpenStaticEvent_OnDoorOpened;
            KeycardStateStaticEvent.OnKeycardStateChanged += KeycardStateStaticEvent_OnKeycardStateChanged;
            MonsterPeekedStaticEvent.OnAnyMonsterPeaked += OnAnyMonsterPeaked;
            GasLeakedStaticEvent.OnAnyGasLeaked += OnAnyGasLeaked;
            PickedUpStaticEvent.OnTabletPickedUp += OnAnyTabletPickedUp;
            PutDownStaticEvent.OnTabletPutDown += OnAnyTabletPutDown;
            TriggeredScreamerStaticEvent.OnAnyTriggeredScreamer += OnAnyTriggeredScreamer;

            _digitRegisteredEventBinding = new EventBinding<DigitRegisteredEvent>(HandleButtonPressedEvent);
            EventBus<DigitRegisteredEvent>.Register(_digitRegisteredEventBinding);

            _passwordValidationResponseEventBinding =
                new EventBinding<PasswordValidationResponseEvent>(HandlePasswordValidationResponseEvent);
            EventBus<PasswordValidationResponseEvent>.Register(_passwordValidationResponseEventBinding);
        }

        private void OnDisable()
        {
            DoorOpenStaticEvent.OnDoorOpened -= DoorOpenStaticEvent_OnDoorOpened;
            KeycardStateStaticEvent.OnKeycardStateChanged -= KeycardStateStaticEvent_OnKeycardStateChanged;
            MonsterPeekedStaticEvent.OnAnyMonsterPeaked -= OnAnyMonsterPeaked;
            GasLeakedStaticEvent.OnAnyGasLeaked -= OnAnyGasLeaked;
            PickedUpStaticEvent.OnTabletPickedUp -= OnAnyTabletPickedUp;
            PutDownStaticEvent.OnTabletPutDown -= OnAnyTabletPutDown;
            TriggeredScreamerStaticEvent.OnAnyTriggeredScreamer -= OnAnyTriggeredScreamer;
            
            EventBus<DigitRegisteredEvent>.Deregister(_digitRegisteredEventBinding);
            EventBus<PasswordValidationResponseEvent>.Deregister(_passwordValidationResponseEventBinding);
        }

        private void KeycardStateStaticEvent_OnKeycardStateChanged(object sender, KeycardStateStaticEventArgs e)
        {
            if (e.AccessGranted)
            {
                PlaySoundAtPoint(_keycardAcceptedClip, e.TerminalPosition);
                return;
            }
            
            PlaySoundAtPoint(_keycardDeclinedClip, e.TerminalPosition);
        }

        private void DoorOpenStaticEvent_OnDoorOpened(object sender, DoorOpenStaticEventArgs e)
        {
            PlaySoundAtPoint(_doorOpenClip, e.DoorPosition);
        }
        
        private void OnAnyMonsterPeaked(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _monsterPeekClip, 2f);
        }
        
        private void OnAnyGasLeaked(object sender, GasLeakedStaticEventArgs e)
        {
            PlaySound(_audioSource, _gasLeakSpookyClip, 2f);
            PlaySoundAtPoint(_gasLeakClip, e.GasLeakedPosition, 4f);
        }

        private void HandleButtonPressedEvent(DigitRegisteredEvent digitRegisteredEvent)
        {
            PlaySoundWithRandomPitch(_audioSource, _buttonPressClip, 0.9f, 1f);
        }

        private void HandlePasswordValidationResponseEvent(
            PasswordValidationResponseEvent passwordValidationResponseEvent)
        {
            AudioClip clip = passwordValidationResponseEvent.IsCorrect ? _keypadGrantedClip : _keypadDeniedClip;
            
            PlaySound(_audioSource, clip);
        }
        
        private void OnAnyTabletPickedUp(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _pickupClip);
        }

        private void OnAnyTabletPutDown(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _putdownClip);
        }
        
        private void OnAnyTriggeredScreamer(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _screamerClip);
        }
    }
}
