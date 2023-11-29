using System;
using UnityEngine;

namespace Sounds.Audio
{
    public class LaboratoryFirstLevelAudio : AudioPlayer
    {
        [SerializeField] private AudioClip _keycardAcceptedClip;
        [SerializeField] private AudioClip _keycardDeclinedClip;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
}
