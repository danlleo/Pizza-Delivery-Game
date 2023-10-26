using UnityEngine;

namespace Sounds.Audio
{
    public class RoomAudio : AudioPlayer
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _doorOpen;

        public void PlayDoorOpenSound()
        {
            PlaySound(_audioSource, _doorOpen);
        }
    }
}
