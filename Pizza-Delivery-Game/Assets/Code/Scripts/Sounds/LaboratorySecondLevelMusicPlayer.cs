using System;
using Common;
using DG.Tweening;
using Monster;
using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class LaboratorySecondLevelMusicPlayer : MonoBehaviour
    {
        [Header("Music clips")]
        [SerializeField] private MusicClip _roamingAmbientClip;
        [SerializeField] private MusicClip _chasingAmbientClip;
        [SerializeField] private MusicClip _investigatingAmbientClip;
        
        [Header("Settings")]
        [SerializeField] private float _fadeDurationInSeconds = 0.345f;
        
        private AudioSource _audioSource;
        private float _currentVolume;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = true;
            _audioSource.clip = _roamingAmbientClip.Clip;
            _currentVolume = _audioSource.volume;
        }

        private void Start()
        {
            TransitionTo(_roamingAmbientClip);
        }

        private void OnEnable()
        {
            BeganChaseStaticEvent.OnAnyBeganChase += Monster_OnAnyBeganChase;
            StoppedChaseStaticEvent.OnAnyStoppedChaseStaticEvent += Monster_OnAnyStoppedChaseStaticEvent;
            MonsterStartedInvestigatingStaticEvent.OnAnyMonsterStartedInvestigating += Monster_OnAnyMonsterStartedInvestigating;
            MonsterStoppedInvestigatingStaticEvent.OnAnyMonsterStoppedInvestigating += Monster_OnAnyMonsterStoppedInvestigating;
            TimeControl.OnAnyGamePaused.Event += OnAnyGamePaused;
            TimeControl.OnAnyGameUnpaused.Event += OnAnyGameUnpaused;
            Common.OnAnyGameOver.Event += OnAnyGameOver;
        }

        private void OnDisable()
        {
            BeganChaseStaticEvent.OnAnyBeganChase -= Monster_OnAnyBeganChase;
            StoppedChaseStaticEvent.OnAnyStoppedChaseStaticEvent -= Monster_OnAnyStoppedChaseStaticEvent;
            MonsterStartedInvestigatingStaticEvent.OnAnyMonsterStartedInvestigating -= Monster_OnAnyMonsterStartedInvestigating;
            MonsterStoppedInvestigatingStaticEvent.OnAnyMonsterStoppedInvestigating -= Monster_OnAnyMonsterStoppedInvestigating;
            TimeControl.OnAnyGamePaused.Event -= OnAnyGamePaused;
            TimeControl.OnAnyGameUnpaused.Event -= OnAnyGameUnpaused;
            Common.OnAnyGameOver.Event -= OnAnyGameOver;
        }

        private void TransitionTo(MusicClip target)
        {
            _audioSource.DOKill();

            Sequence fadeSequence = DOTween.Sequence();
            fadeSequence.Append(_audioSource.DOFade(0f, target.FadeOutTime));
            fadeSequence.AppendCallback(() =>
            {
                _audioSource.clip = target.Clip;
                _currentVolume = target.Volume;
                _audioSource.Play();
            });
            fadeSequence.Append(_audioSource.DOFade(target.Volume, target.FadeInTime));
        }
        
        private void FadeIn()
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
            
            _audioSource.DOKill();
            _audioSource.DOFade(_currentVolume, _fadeDurationInSeconds).SetUpdate(this);
        }

        private void FadeIn(float durationInSeconds)
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
            
            _audioSource.DOKill();
            _audioSource.DOFade(_currentVolume, durationInSeconds).SetUpdate(this);
        }
        
        private void FadeOut(bool pause = false)
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
                
            _audioSource.DOKill();
            _audioSource.DOFade(0f, _fadeDurationInSeconds).SetUpdate(this)
                .OnComplete(pause ? _audioSource.Stop : null);
        }
        
        private void FadeOut(bool pause, float durationInSeconds)
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
                
            _audioSource.DOKill();
            _audioSource.DOFade(0f, durationInSeconds).SetUpdate(this).OnComplete(pause ? _audioSource.Stop : null);
        }
        
        private void Monster_OnAnyStoppedChaseStaticEvent(object sender, EventArgs e)
        {
            TransitionTo(_roamingAmbientClip);
        }

        private void Monster_OnAnyBeganChase(object sender, EventArgs e)
        {
            TransitionTo(_chasingAmbientClip);
        }
        
        private void Monster_OnAnyMonsterStartedInvestigating(object sender, EventArgs e)
        {
            TransitionTo(_investigatingAmbientClip);
        }
        
        private void Monster_OnAnyMonsterStoppedInvestigating(object sender, MonsterStoppedInvestigatingEventArgs e)
        {
            if (e.HasDetectedPlayer) return;
            TransitionTo(_roamingAmbientClip);
        }
        
        private void OnAnyGameUnpaused(object sender, EventArgs e)
        {
            FadeIn();
        }

        private void OnAnyGamePaused(object sender, EventArgs e)
        {
            FadeOut(true);
        }
        
        private void OnAnyGameOver(object sender, EventArgs e)
        {
            FadeOut();
        }
    }
}
