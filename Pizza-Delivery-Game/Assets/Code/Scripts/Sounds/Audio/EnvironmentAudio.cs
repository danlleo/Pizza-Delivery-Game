using Enums.Sounds;
using UnityEngine;

namespace Sounds.Audio
{
    public class EnvironmentAudio : AudioPlayer
    {
        [SerializeField] private SoundClipsGroupSO _soundClipsGroup;
        [SerializeField] private AudioSource _audioSource;
    }
}
