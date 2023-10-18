using Enums.Sounds;
using UnityEngine;

namespace Sounds
{
    [System.Serializable]
    public struct SoundClipsGroupStruct
    {
        [SerializeField] private SoundGroupType _groupType;
        [SerializeField] private SoundClipsSO[] _soundClipsGroup;

        public SoundGroupType SoundGroupType => _groupType;
        public SoundClipsSO[] SoundClipsGroup => _soundClipsGroup;
    }
}
