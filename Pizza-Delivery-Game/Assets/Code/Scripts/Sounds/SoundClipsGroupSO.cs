using System.Collections.Generic;
using Enums.Sounds;
using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(fileName = "SoundClipsGroup_", menuName = "Scriptable Objects/Sounds/SoundClipsGroup")]
    public class SoundClipsGroupSO : ScriptableObject
    {
        [SerializeField] private List<SoundClipsGroupStruct> _groupItems;

        public List<SoundClipsGroupStruct> GroupItems => _groupItems;

        public bool TryGetSoundGroupByType(SoundGroupType soundGroupType, out SoundClipsGroupStruct? soundClipsGroupStruct)
        {
            foreach (SoundClipsGroupStruct soundClipsGroup in _groupItems)
            {
                if (soundClipsGroup.SoundGroupType != soundGroupType) continue;
                
                soundClipsGroupStruct = soundClipsGroup;
                return true;
            }

            soundClipsGroupStruct = null;
            return false;
        }
    }
}
