using Door;
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
        }

        private void OnDisable()
        {
            DoorOpenStaticEvent.OnDoorOpened -= DoorOpenStaticEvent_OnDoorOpened;
        }
        
        private void DoorOpenStaticEvent_OnDoorOpened(object sender, DoorOpenStaticEventArgs e)
        {
            PlaySoundAtPoint(_doorOpenClip, e.DoorPosition);
        }
    }
}
