using System;
using System.Collections;
using Environment.LaboratorySecondLevel;
using Monster;
using UnityEngine;

namespace Sounds.Audio
{
    public class LaboratorySecondLevelAudio : AudioPlayer
    {
        [SerializeField] private AudioClip _attractedMonsterClip;
        [SerializeField] private AudioClip _beganChangeClip;

        private AudioSource _audioSource;
        
        private bool _canPlayAttractedMonsterClip;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _canPlayAttractedMonsterClip = true;
        }

        private void OnEnable()
        {
            AttractedMonsterStaticEvent.OnAnyAttractedMonster += OnAnyAttractedMonster;
            BeganChaseStaticEvent.OnAnyBeganChase += Monster_OnAnyBeganChase;
        }

        private void OnDisable()
        {
            AttractedMonsterStaticEvent.OnAnyAttractedMonster -= OnAnyAttractedMonster;
            BeganChaseStaticEvent.OnAnyBeganChase -= Monster_OnAnyBeganChase;
        }

        private IEnumerator CooldownTimerRoutine()
        {
            _canPlayAttractedMonsterClip = false;
            yield return new WaitForSeconds(_attractedMonsterClip.length);
            _canPlayAttractedMonsterClip = true;
        }
        
        private void OnAnyAttractedMonster(object sender, AttractedMonsterEventArgs e)
        {
            if (!_canPlayAttractedMonsterClip) return;

            PlaySound(_audioSource, _attractedMonsterClip);
            StartCoroutine(CooldownTimerRoutine());
        }
        
        private void Monster_OnAnyBeganChase(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _beganChangeClip);
        }
    }
}
