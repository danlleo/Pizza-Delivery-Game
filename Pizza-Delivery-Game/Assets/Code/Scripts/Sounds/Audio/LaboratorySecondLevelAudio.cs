using System;
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
            AttractedMonsterStaticEvent.OnAnyAttractedMonster += Monster_OnAnyAttractedMonster;
            BeganChaseStaticEvent.OnAnyBeganChase += Monster_OnAnyBeganChase;
            StoppedChaseStaticEvent.OnAnyStoppedChaseStaticEvent += OnAnyStoppedChaseStaticEvent;
            MonsterStartedInvestigatingStaticEvent.OnAnyMonsterStartedInvestigating += Monster_OnAnyMonsterStartedInvestigating;
            MonsterStoppedInvestigatingStaticEvent.OnAnyMonsterStoppedInvestigating += Monster_OnAnyMonsterStoppedInvestigating;
        }

        private void OnDisable()
        {
            AttractedMonsterStaticEvent.OnAnyAttractedMonster -= Monster_OnAnyAttractedMonster;
            BeganChaseStaticEvent.OnAnyBeganChase -= Monster_OnAnyBeganChase;
            StoppedChaseStaticEvent.OnAnyStoppedChaseStaticEvent -= OnAnyStoppedChaseStaticEvent;
            MonsterStartedInvestigatingStaticEvent.OnAnyMonsterStartedInvestigating -= Monster_OnAnyMonsterStartedInvestigating;
            MonsterStoppedInvestigatingStaticEvent.OnAnyMonsterStoppedInvestigating -= Monster_OnAnyMonsterStoppedInvestigating;
        }

        private void Monster_OnAnyAttractedMonster(object sender, AttractedMonsterEventArgs e)
        {
            if (!_canPlayAttractedMonsterClip) return;
            
            PlaySound(_audioSource, _attractedMonsterClip, .35f);
        }
        
        private void Monster_OnAnyBeganChase(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _beganChangeClip);
            _canPlayAttractedMonsterClip = false;
        }
        
        private void Monster_OnAnyMonsterStoppedInvestigating(object sender, MonsterStoppedInvestigatingEventArgs e)
        {
            _canPlayAttractedMonsterClip = !e.HasDetectedPlayer;
        }

        private void Monster_OnAnyMonsterStartedInvestigating(object sender, EventArgs e)
        {
            _canPlayAttractedMonsterClip = false;
        }
        
        private void OnAnyStoppedChaseStaticEvent(object sender, EventArgs e)
        {
            _canPlayAttractedMonsterClip = true;
        }
    }
}
