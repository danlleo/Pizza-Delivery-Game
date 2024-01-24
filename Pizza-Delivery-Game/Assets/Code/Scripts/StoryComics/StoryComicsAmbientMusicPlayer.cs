using System;
using DG.Tweening;
using UnityEngine;

namespace StoryComics
{
    [RequireComponent(typeof(AudioSource))]
    public class StoryComicsAmbientMusicPlayer : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private StoryComicsPlayer _storyComicsPlayer;
        
        [Header("Settings")]
        [SerializeField] private float _fadeDurationInSeconds = 1f;
        [SerializeField] private float _defaultVolume = 0.025f;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0f;
        }

        private void Start()
        {
            FadeIn();
        }

        private void OnEnable()
        {
            _storyComicsPlayer.OnStoryFinished.Event += StoryComicsPlayer_OnStoryFinished;
        }

        private void OnDisable()
        {
            _storyComicsPlayer.OnStoryFinished.Event -= StoryComicsPlayer_OnStoryFinished;
        }

        private void FadeIn()
        {
            _audioSource.DOFade(_defaultVolume, _fadeDurationInSeconds);
        }

        private void FadeOut()
        {
            _audioSource.DOFade(0f, _fadeDurationInSeconds);
        }
        
        private void StoryComicsPlayer_OnStoryFinished(object sender, EventArgs e)
        {
            FadeOut();
        }
    }
}
