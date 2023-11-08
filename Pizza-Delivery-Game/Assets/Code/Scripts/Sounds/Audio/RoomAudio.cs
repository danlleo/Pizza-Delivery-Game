using System;
using Environment.Bedroom.PC;
using UnityEngine;

namespace Sounds.Audio
{
    public class RoomAudio : AudioPlayer
    {
        [Header("External References")]
        [SerializeField] private AudioSource _audioSource;
        
        [Header("Settings")]
        [SerializeField] private AudioClip _doorOpen;
        [SerializeField] private AudioClip _switchOnSound;
        [SerializeField] private AudioClip _lampSwitchSound;
        [SerializeField] private AudioClip _chairPullClip;

        private void OnEnable()
        {
            StartedUsingPCStaticEvent.OnStarted += StartedUsingPCStaticEvent_OnStarted;
        }

        private void OnDisable()
        {
            StartedUsingPCStaticEvent.OnStarted -= StartedUsingPCStaticEvent_OnStarted;
        }

        private void StartedUsingPCStaticEvent_OnStarted(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _chairPullClip);
        }

        public void PlayDoorOpenSound()
        {
            PlaySound(_audioSource, _doorOpen);
        }

        public void PlayRoomLightSwitchSound()
        {
            PlaySound(_audioSource, _switchOnSound);
        }

        public void PlayLampLightSwitchSound()
        {
            PlaySound(_audioSource, _lampSwitchSound);
        }
    }
}
