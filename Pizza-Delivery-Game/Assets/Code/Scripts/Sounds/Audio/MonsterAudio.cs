using System;
using UnityEngine;

namespace Sounds.Audio
{
    [RequireComponent(typeof(Monster.Monster))]
    [DisallowMultipleComponent]
    public class MonsterAudio : AudioPlayer
    {
        [SerializeField] private AudioClip _chaseBeganClip;
        
        private Monster.Monster _monster;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _monster = GetComponent<Monster.Monster>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _monster.StartedChasingEvent.Event += StartedChasing_Event;
        }

        private void OnDisable()
        {
            _monster.StartedChasingEvent.Event -= StartedChasing_Event;
        }

        private void StartedChasing_Event(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _chaseBeganClip, 3f);
        }
    }
}
