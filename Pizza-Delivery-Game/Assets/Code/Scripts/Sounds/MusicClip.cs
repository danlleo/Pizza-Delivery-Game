using UnityEngine;

namespace Sounds
{
    [System.Serializable]
    public struct MusicClip
    {
        public AudioClip Clip;
        public float FadeOutTime;
        public float FadeInTime;
        public float Volume;
    }
}