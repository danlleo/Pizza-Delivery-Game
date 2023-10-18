using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(fileName = "SoundClipsGroup_", menuName = "Scriptable Objects/Sounds/SoundClipsGroup")]
    public class SoundClipsGroupSO : ScriptableObject
    {
        [SerializeField] private SoundClipsGroupStruct[] _soundsClipsGroups;

        public SoundClipsGroupStruct[] SoundClipsGroups => _soundsClipsGroups;
    }
}
