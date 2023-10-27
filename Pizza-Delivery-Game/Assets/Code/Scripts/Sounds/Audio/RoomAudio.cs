using UnityEngine;

namespace Sounds.Audio
{
    public class RoomAudio : AudioPlayer
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _doorOpen;
        [SerializeField] private AudioClip _switchOnSound;

        public void PlayDoorOpenSound()
        {
            PlaySound(_audioSource, _doorOpen);
        }

        public void PlaySwitchOnSound()
        {
            PlaySound(_audioSource, _switchOnSound);
        }
    }
}
