using Door;
using Environment.LaboratoryFirstLevel;
using UnityEngine;

namespace Sounds.Audio
{
    public class LaboratoryFirstLevelAudio : AudioPlayer
    {
        [SerializeField] private AudioClip _keycardAcceptedClip;
        [SerializeField] private AudioClip _keycardDeclinedClip;
        [SerializeField] private AudioClip _doorOpenClip;
        
        private void OnEnable()
        {
            DoorOpenStaticEvent.OnDoorOpened += DoorOpenStaticEvent_OnDoorOpened;
            KeycardStateStaticEvent.OnKeycardStateChanged += KeycardStateStaticEvent_OnKeycardStateChanged;

        }

        private void OnDisable()
        {
            DoorOpenStaticEvent.OnDoorOpened -= DoorOpenStaticEvent_OnDoorOpened;
            KeycardStateStaticEvent.OnKeycardStateChanged -= KeycardStateStaticEvent_OnKeycardStateChanged;
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
    }
}
