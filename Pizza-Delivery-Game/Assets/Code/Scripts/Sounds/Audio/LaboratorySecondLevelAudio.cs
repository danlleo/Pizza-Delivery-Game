using System.Collections;
using Environment.LaboratorySecondLevel;
using UnityEngine;

namespace Sounds.Audio
{
    public class LaboratorySecondLevelAudio : AudioPlayer
    {
        [SerializeField] private AudioClip _attractedMonsterClip;

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
        }

        private void OnDisable()
        {
            AttractedMonsterStaticEvent.OnAnyAttractedMonster -= OnAnyAttractedMonster;
        }

        private void OnAnyAttractedMonster(object sender, AttractedMonsterEventArgs e)
        {
            if (!_canPlayAttractedMonsterClip) return;

            PlaySound(_audioSource, _attractedMonsterClip);
            StartCoroutine(CooldownTimerRoutine());
        }

        private IEnumerator CooldownTimerRoutine()
        {
            _canPlayAttractedMonsterClip = false;
            yield return new WaitForSeconds(_attractedMonsterClip.length);
            _canPlayAttractedMonsterClip = true;
        }
    }
}
