using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sounds
{
    [CreateAssetMenu(fileName = "SoundClipsGroup_", menuName = "Scriptable Objects/Sounds/SoundClipsGroup")]
    public class SoundClipsGroupSO : ScriptableObject
    {
        [FormerlySerializedAs("_soundsClipsGroups")] [SerializeField] private List<SoundClipsGroupStruct> _soundsClipsGroupsList;

        public List<SoundClipsGroupStruct> SoundClipsGroupsList => _soundsClipsGroupsList;
    }
}
