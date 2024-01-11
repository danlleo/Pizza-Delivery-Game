using UnityEngine;

namespace StoryComics
{
    [System.Serializable]
    public struct SlideItem
    {
        public string Text;
        [Range(0f, 2f)] public float TimeToDisplayInSeconds;
        public AudioClip Clip;
    }
}