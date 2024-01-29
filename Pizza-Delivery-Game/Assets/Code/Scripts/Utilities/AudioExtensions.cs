using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Utilities
{
    public static class AudioExtensions
    {
        public static AudioClip GetRandomClip(this AudioClip[] audioClips)
        {
            if (audioClips.Length == 0)
            {
                throw new ArgumentException("Audio clips are empty!");
            }

            int index = UnityEngine.Random.Range(0, audioClips.Length - 1);
            AudioClip randomItem = audioClips[index];
            
            return randomItem;
        }

        public static AudioClip GetRandomClip(this List<AudioClip> audioClips)
        {
            if (audioClips.Count == 0)
            {
                throw new ArgumentException("Audio clips are empty!");
            }

            var random = new Random();
            int index = random.Next(audioClips.Count);
            AudioClip randomItem = audioClips[index];

            return randomItem;
        }
    }
}