using UnityEngine;

namespace Sounds.Audio
{
    [DisallowMultipleComponent]
    public class EnvironmentAudio : AudioPlayer
    {
        [SerializeField] private AudioSource _audioSource;
    }
}
