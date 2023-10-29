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
