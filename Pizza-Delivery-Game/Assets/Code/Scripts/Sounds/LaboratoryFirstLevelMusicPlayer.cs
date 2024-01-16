using System;
using System.Collections;
using DG.Tweening;
using Environment.Bedroom;
using Environment.Bedroom.PC;
using UnityEngine;

namespace Sounds
{
    [DisallowMultipleComponent]
    public class LaboratoryFirstLevelMusicPlayer : MonoBehaviour
    {
    [Header("External references")]
        [SerializeField] private AudioClip _ambientMusicClip;
        
        [Header("Settings")]
        [SerializeField] private float _defaultVolume;
        [SerializeField] private float _fadeDurationInSeconds = 0.345f;
        [SerializeField] private float _delayMusicPlayTimeInSeconds = 2f;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0f;
            _audioSource.clip = _ambientMusicClip;
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_delayMusicPlayTimeInSeconds);
            FadeIn();
        }

        private void OnEnable()
        {
            Environment.Bedroom.PC.OnAnyStoppedUsingPC.Event += OnAnyStoppedUsingPC;
            WokeUpStaticEvent.OnWokeUp += OnAnyWokeUp;
            TimeControl.OnAnyGamePaused.Event += OnAnyGamePaused;
            TimeControl.OnAnyGameUnpaused.Event += OnAnyGameUnpaused;
        }
        
        private void OnDisable()
        {
            Environment.Bedroom.PC.OnAnyStoppedUsingPC.Event -= OnAnyStoppedUsingPC;
            WokeUpStaticEvent.OnWokeUp -= OnAnyWokeUp;
            TimeControl.OnAnyGamePaused.Event -= OnAnyGamePaused;
            TimeControl.OnAnyGameUnpaused.Event -= OnAnyGameUnpaused;
        }

        private void FadeIn()
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
            
            _audioSource.DOKill();
            _audioSource.DOFade(_defaultVolume, _fadeDurationInSeconds).SetUpdate(this);
        }

        private void FadeIn(float durationInSeconds)
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
            
            _audioSource.DOKill();
            _audioSource.DOFade(_defaultVolume, durationInSeconds).SetUpdate(this);
        }
        
        private void FadeOut(bool pause)
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
        
        private void OnAnyStoppedUsingPC(object sender, EventArgs e)
        {
            FadeOut(false);
        }
        
        private void OnAnyWokeUp(object sender, EventArgs e)
        {
            FadeIn();
        }
        
        private void OnAnyGamePaused(object sender, EventArgs e)
        {
            FadeOut(true, .35f);
        }

        private void OnAnyGameUnpaused(object sender, EventArgs e)
        {
            FadeIn(.35f);
        }
    }
}
