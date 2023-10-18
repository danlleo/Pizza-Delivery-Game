using Enums.Sounds;
using UnityEngine;

namespace Sounds.Audio
{
    public class EnvironmentAudio : AudioPlayer
    {
        [SerializeField] private SoundClipsGroupSO _soundClipsGroup;
        [SerializeField] private AudioSource _audioSource;
        
        private void Start()
        {
            if (_soundClipsGroup.TryGetSoundGroupByType(SoundGroupType.WoodSurfaceSteps, out SoundClipsGroupStruct? soundClipsGroupStruct))
            {
                print("Found group");
            }
        }
    }
}
