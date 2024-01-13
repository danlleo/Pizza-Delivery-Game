using System;
using System.Collections;
using DG.Tweening;
using Environment.Bedroom;
using Environment.Bedroom.PC;
using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class BedroomMusicPlayer : MonoBehaviour
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
            StoppedUsingPCStaticEvent.OnEnded += OnAnyStoppedUsingPC;
            WokeUpStaticEvent.OnWokeUp += OnAnyWokeUp;
        }

        private void OnDisable()
        {
            StoppedUsingPCStaticEvent.OnEnded -= OnAnyStoppedUsingPC;
            WokeUpStaticEvent.OnWokeUp -= OnAnyWokeUp;
        }

        private void FadeIn()
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
            
            _audioSource.DOKill();
            _audioSource.DOFade(_defaultVolume, _fadeDurationInSeconds);
        }

        private void FadeOut()
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
                
            _audioSource.DOKill();
            _audioSource.DOFade(0f, _fadeDurationInSeconds);
        }
        
        private void OnAnyStoppedUsingPC(object sender, EventArgs e)
        {
            FadeOut();
        }
        
        private void OnAnyWokeUp(object sender, EventArgs e)
        {
            FadeIn();
        }
    }
}
