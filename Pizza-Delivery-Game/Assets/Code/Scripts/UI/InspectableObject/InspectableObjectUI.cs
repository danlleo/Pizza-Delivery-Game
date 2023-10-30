using System;
using TMPro;
using UnityEngine;

namespace UI.InspectableObject
{
    [DisallowMultipleComponent]
    public class InspectableObjectUI : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private GameObject _inspectableObjectUI;
        [SerializeField] private UI _ui;
        [SerializeField] private TextMeshProUGUI _continueText;
        [SerializeField] private Reader _reader;

        [Header("Settings")] 
        [SerializeField] private float _continueTextBlinkTimeInSeconds;

        private bool _canClose;
        
        private void Awake()
        {
            HideUI();
            HideContinueText();
        }
        
       
        private void OnEnable()
        {
            _ui.InspectableObjectOpeningEvent.Event += InspectableObjectOpeningEvent;
            _ui.InspectableObjectClosingEvent.Event += InspectableObjectClosing_Event;
            _ui.InspectableObjectFinishedReadingEvent.Event += InspectableObjectFinishedReading_Event;
        }

        private void OnDisable()
        {
            _ui.InspectableObjectOpeningEvent.Event -= InspectableObjectOpeningEvent;
            _ui.InspectableObjectClosingEvent.Event -= InspectableObjectClosing_Event;
            _ui.InspectableObjectFinishedReadingEvent.Event -= InspectableObjectFinishedReading_Event;
        }
       
        private void InspectableObjectOpeningEvent(object sender, InspectableObjectOpeningEventArgs e)
        {
            ShowUI();
            ShowReader();
            
            _reader.BeginDisplay(e.InspectableObject);
        }
        
        private void InspectableObjectClosing_Event(object sender, EventArgs e)
        {
            if (!_canClose) return;

            HideUI();
            HideReader();
        }
                
        private void InspectableObjectFinishedReading_Event(object sender, InspectableObjectFinishedReadingEventArgs e)
        {
            ShowContinueText();
            _canClose = e.CanClose;
        }
        
        private void Update()
        {
            if (_canClose)
                _continueText.alpha = Mathf.PingPong(Time.time * _continueTextBlinkTimeInSeconds, 1f);
        }

        private void ShowReader()
            => _reader.gameObject.SetActive(true);

        private void HideReader()
            => _reader.gameObject.SetActive(false);
        
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
