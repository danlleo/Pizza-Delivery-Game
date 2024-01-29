using System;
using Common;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    [DisallowMultipleComponent]
    public class LoseItCanvas : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField, ChildrenOnly] private TextMeshProUGUI _loseItText;

        [Header("Settings")]
        [SerializeField] private float _textBlinkTimeInSeconds = .35f;

        private float _timeToFadeInSeconds = .15f;
        
        private Monster.Monster _monster;
        
        private bool _isDisplaying;
        
        [Inject]
        private void Construct(Monster.Monster monster)
        {
            _monster = monster;
        }
        
        private void Awake()
        {
            _loseItText.enabled = false;
        }

        private void Update()
        {
            if (!_isDisplaying) return;
            
            _loseItText.alpha = Mathf.PingPong(Time.time * _textBlinkTimeInSeconds, 1f);
        }
        
        private void OnEnable()
        {
            _monster.StartedChasingEvent.Event += StartedChasing_Event;
            _monster.StoppedChasingEvent.Event += StoppedChasing_Event;
        }

        private void OnDisable()
        {
            _monster.StartedChasingEvent.Event -= StartedChasing_Event;
            _monster.StoppedChasingEvent.Event -= StoppedChasing_Event;
        }

        private void FadeInTextAlpha()
        {
            _loseItText.alpha = 0f;
            _loseItText.DOKill();
            _loseItText.DOFade(1f, _timeToFadeInSeconds)
                .OnComplete(() => _isDisplaying = true);
        }

        private void FadeOutTextAlpha()
        {
            _loseItText.DOKill();
            _loseItText.DOFade(0f, _timeToFadeInSeconds)
                .OnComplete(() =>
                {
                    _isDisplaying = false;
                    _loseItText.enabled = false;
                });
        }
        
        private void StartedChasing_Event(object sender, EventArgs e)
        {
            _loseItText.enabled = true;
            FadeInTextAlpha();
        }
        
        private void StoppedChasing_Event(object sender, EventArgs e)
        {
            FadeOutTextAlpha();
        }
    }
}
