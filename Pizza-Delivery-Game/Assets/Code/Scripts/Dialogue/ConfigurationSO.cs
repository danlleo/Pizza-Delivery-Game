using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "DialogueConfiguration_", menuName = "Scriptable Objects/Dialogues/Configuration")]
    public class ConfigurationSO : ScriptableObject
    {
        [SerializeField] private AudioClip[] _audioClips;

        [Space(5)] 
        [SerializeField, Range(0, 2f)] private float _volume;
        
        [Space(5)]
        [SerializeField, Range(1, 8)] private int _characterPlaySoundFrequency;
        
        [Space(5)]
        [SerializeField, Range(-3f, 3f)] private float _minimumPitch;
        [SerializeField, Range(-3f, 3f)] private float _maximumPitch;
        
        [Space(5)]
        [SerializeField] private bool _stopCharacterTypeSound = true;
        [SerializeField] private bool _makePredictable;

        public AudioClip[] AudioClips => _audioClips;

        public float Volume => _volume;
        
        public int CharacterPlaySoundFrequency => _characterPlaySoundFrequency;

        public float MinimumPitch => _minimumPitch;
        public float MaximumPitch => _maximumPitch;

        public bool StopCharacterTypeSound => _stopCharacterTypeSound;
        public bool MakePredictable => _makePredictable;
    }
}
