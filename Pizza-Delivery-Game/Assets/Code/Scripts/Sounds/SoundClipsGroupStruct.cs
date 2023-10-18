using UnityEngine;

namespace Sounds
{
    [System.Serializable]
    public struct SoundClipsGroupStruct
    {
        [SerializeField] private string _groupName;
        [SerializeField] private SoundClipsSO[] _soundClipsGroup;

        public string GroupName => _groupName;
        public SoundClipsSO[] SoundClipsGroup => _soundClipsGroup;
    }
}
