using System;
using Door;
using Environment.LaboratoryFirstLevel;
using EventBus;
using Keypad;
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
        
        private AudioSource _audioSource;
        private EventBinding<ButtonPressedEvent> _buttonPressedEventBinding;
        
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

            _buttonPressedEventBinding = new EventBinding<ButtonPressedEvent>(HandleButtonPressedEvent);
            EventBus<ButtonPressedEvent>.Register(_buttonPressedEventBinding);
        }
        
        private void OnDisable()
        {
            DoorOpenStaticEvent.OnDoorOpened -= DoorOpenStaticEvent_OnDoorOpened;
            KeycardStateStaticEvent.OnKeycardStateChanged -= KeycardStateStaticEvent_OnKeycardStateChanged;
            MonsterPeekedStaticEvent.OnAnyMonsterPeaked -= OnAnyMonsterPeaked;
            GasLeakedStaticEvent.OnAnyGasLeaked -= OnAnyGasLeaked;
            
            EventBus<ButtonPressedEvent>.Deregister(_buttonPressedEventBinding);
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

        private void HandleButtonPressedEvent(ButtonPressedEvent buttonPressedEvent)
        {
            PlaySoundWithRandomPitch(_audioSource, _buttonPressClip, 0.9f, 1f);
        }
    }
}
