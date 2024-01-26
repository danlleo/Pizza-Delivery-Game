using System;
using Environment.Bedroom.PC;
using UnityEngine;

namespace Sounds.Audio
{
    public class BedroomAudio : AudioPlayer
    {
        public static BedroomAudio Instance;
        
        [Header("External References")]
        [SerializeField] private AudioSource _audioSource;
        
        [Header("Settings")]
        [SerializeField] private AudioClip _doorOpen;
        [SerializeField] private AudioClip _chairPullClip;
        [SerializeField] private AudioClip _clickClip;
        [SerializeField] private AudioClip _doorbellClip;
        [SerializeField] private AudioClip _shirtClip;
        
        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            OnAnyStartedUsingPC.Event += StartedUsingPCStaticEventEvent;
            ClickedStaticEvent.OnClicked += ClickedStaticEvent_OnClicked;
            Environment.Bedroom.PC.OnAnyGotDressed.Event += OnAnyGotDressed;
        }

        private void OnDisable()
        {
            OnAnyStartedUsingPC.Event -= StartedUsingPCStaticEventEvent;
            ClickedStaticEvent.OnClicked -= ClickedStaticEvent_OnClicked;
            Environment.Bedroom.PC.OnAnyGotDressed.Event -= OnAnyGotDressed;
        }
        
        private void StartedUsingPCStaticEventEvent(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _chairPullClip);
        }

        private void ClickedStaticEvent_OnClicked(object sender, EventArgs e)
        {
            PlaySoundWithRandomPitch(_audioSource, _clickClip, 0.9f, 1f, 0.65f);
        }
        
        public void PlayDoorOpenSound()
        {
            PlaySound(_audioSource, _doorOpen);
        }
        
        public void PlayDoorBellSound()
        {
            PlaySoundAtPoint(_doorbellClip, Vector3.zero);
        }
        
        private void OnAnyGotDressed(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _shirtClip);
        }
    }
}
