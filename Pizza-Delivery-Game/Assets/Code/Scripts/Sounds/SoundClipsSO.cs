using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(fileName = "SoundClips_", menuName = "Scriptable Objects/Sounds/SoundClips")]
    public class SoundClipsSO : ScriptableObject
    {
        [SerializeField] private AudioClip[] _soundClips;

        public AudioClip[] AudioClips => _soundClips;
    }
}
