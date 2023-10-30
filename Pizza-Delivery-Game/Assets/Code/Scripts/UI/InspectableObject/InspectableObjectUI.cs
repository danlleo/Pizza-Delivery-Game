using System;
using TMPro;
using UnityEngine;

namespace UI.InspectableObject
{
    public class InspectableObjectUI : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private GameObject _inspectableObjectUI;
        [SerializeField] private Player.Player _player;
        [SerializeField] private TextMeshProUGUI _continueText;

        [Header("Settings")] 
        [SerializeField] private float _continueTextBlinkTimeInSeconds;

        private bool _canClose;
        
        private void Awake()
        {
            HideContinueText();
        }

        private void OnEnable()
        {
            _player.InspectableObjectFinishedReadingEvent.Event += InspectableObjectFinishedReading_Event;
            _player.InspectableObjectClosingEvent.Event += InspectableObjectClosing_Event;
        }

        private void OnDisable()
        {
            _player.InspectableObjectFinishedReadingEvent.Event -= InspectableObjectFinishedReading_Event;
            _player.InspectableObjectClosingEvent.Event -= InspectableObjectClosing_Event;
        }
        
        private void InspectableObjectFinishedReading_Event(object sender, InspectableObjectFinishedReadingEventArgs e)
        {
            ShowContinueText();
            _canClose = e.CanClose;
        }

        private void InspectableObjectClosing_Event(object sender, EventArgs e)
        {
            if (!_canClose) return;

            HideUI();
        }
        
        private void Update()
        {
            if (_canClose)
                _continueText.alpha = Mathf.PingPong(Time.time * _continueTextBlinkTimeInSeconds, 1f);
        }
        
        private void ShowUI()
            => _inspectableObjectUI.SetActive(true);

        private void HideUI()
            => _inspectableObjectUI.SetActive(false);
        
        private void ShowContinueText()
            => _continueText.gameObject.SetActive(true);
        
        private void HideContinueText()
            => _continueText.gameObject.SetActive(false);
    }
}
