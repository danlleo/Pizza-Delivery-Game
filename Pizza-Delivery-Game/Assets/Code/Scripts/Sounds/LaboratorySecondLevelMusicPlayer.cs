using System;
using DG.Tweening;
using Monster;
using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class LaboratorySecondLevelMusicPlayer : MonoBehaviour
    {
        [Header("Music clips")]
        [SerializeField] private MusicClip _roamingAmbientClip;
        [SerializeField] private MusicClip _chasingAmbientClip;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = true;
            _audioSource.clip = _roamingAmbientClip.Clip;
            _audioSource.Play();
        }

        private void OnEnable()
        {
            BeganChaseStaticEvent.OnAnyBeganChase += Monster_OnAnyBeganChase;
            StoppedChaseStaticEvent.OnAnyStoppedChaseStaticEvent += Monster_OnAnyStoppedChaseStaticEvent;
        }

        private void OnDisable()
        {
            BeganChaseStaticEvent.OnAnyBeganChase -= Monster_OnAnyBeganChase;
            StoppedChaseStaticEvent.OnAnyStoppedChaseStaticEvent -= Monster_OnAnyStoppedChaseStaticEvent;
        }

        private void TransitionTo(MusicClip target)
        {
            _audioSource.DOKill();

            Sequence fadeSequence = DOTween.Sequence();
            fadeSequence.Append(_audioSource.DOFade(0f, target.FadeOutTime));
            fadeSequence.AppendCallback(() =>
            {
                _audioSource.clip = target.Clip;
                _audioSource.Play();
            });
            fadeSequence.Append(_audioSource.DOFade(target.Volume, target.FadeInTime));
        }
        
        private void Monster_OnAnyStoppedChaseStaticEvent(object sender, EventArgs e)
        {
            TransitionTo(_roamingAmbientClip);
        }

        private void Monster_OnAnyBeganChase(object sender, EventArgs e)
        {
            TransitionTo(_chasingAmbientClip);
        }
    }
}
