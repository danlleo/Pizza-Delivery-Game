using System;
using System.Collections;
using Common;
using InspectableObject;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.InspectableObject
{
    [DisallowMultipleComponent]
    public class InspectableObjectUI : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private GameObject _inspectableObjectUI;

        [SerializeField] private GameObject _objectParentContainer;
        [SerializeField] private UI _ui;
        [SerializeField] private TextMeshProUGUI _continueText;
        [SerializeField] private Reader _reader;

        [Header("Settings")] 
        [SerializeField] private float _continueTextBlinkTimeInSeconds;

        private bool _allowedToRead;
        private bool _canClose;

        private Action _onComplete;
        private Coroutine _rotateObjectRoutine;
        private ModelViewCamera _modelViewCamera;
        
        [Inject]
        private void Construct(ModelViewCamera modelViewCamera)
        {
            _modelViewCamera = modelViewCamera;
        }
        
        private void Awake()
        {
            HideUI();
            HideContinueText();

            _allowedToRead = true;
        }

        private void Update()
        {
            if (_canClose)
                _continueText.alpha = Mathf.PingPong(Time.time * _continueTextBlinkTimeInSeconds, 1f);
        }

        private void OnEnable()
        {
            _ui.InspectableObjectOpeningEvent.Event += InspectableObjectOpening_Event;
            _ui.InspectableObjectClosingEvent.Event += InspectableObjectClosing_Event;
            _ui.InspectableObjectCloseEvent.Event += InspectableObjectClose_Event;
            _ui.InspectableObjectFinishedReadingEvent.Event += InspectableObjectFinishedReading_Event;
        }

        private void OnDisable()
        {
            _ui.InspectableObjectOpeningEvent.Event -= InspectableObjectOpening_Event;
            _ui.InspectableObjectClosingEvent.Event -= InspectableObjectClosing_Event;
            _ui.InspectableObjectCloseEvent.Event -= InspectableObjectClose_Event;
            _ui.InspectableObjectFinishedReadingEvent.Event -= InspectableObjectFinishedReading_Event;
        }

        private void InspectableObjectOpening_Event(object sender, InspectableObjectOpeningEventArgs e)
        {
            if (!_allowedToRead) return;

            ShowUI();
            ShowObject(e.InspectableObject);
            ShowReader();

            _allowedToRead = false;
            _onComplete = e.OnComplete;

            _reader.BeginDisplay(e.InspectableObject);
            UIOpenedStaticEvent.Call(_ui);
        }

        private void InspectableObjectClosing_Event(object sender, EventArgs e)
        {
            if (!_canClose) return;

            HideUI();
            HideReader();
            HideContinueText();

            _ui.InspectableObjectCloseEvent.Call(_ui);
        }

        private void InspectableObjectClose_Event(object sender, EventArgs e)
        {
            _canClose = false;
            _allowedToRead = true;

            _onComplete?.Invoke();
            _ui.InspectableObjectConfirmEvent.Call(_ui);
            UIClosedStaticEvent.Call(_ui);
            StopCoroutine(_rotateObjectRoutine);
        }

        private void InspectableObjectFinishedReading_Event(object sender, InspectableObjectFinishedReadingEventArgs e)
        {
            ShowContinueText();
            _canClose = e.CanClose;
        }

        private void ShowObject(InspectableObjectSO inspectableObject)
        {
            GameObject inspectableGameObject =
                Instantiate(inspectableObject.Prefab, _objectParentContainer.transform, true);

            _modelViewCamera.Display(inspectableGameObject);
        }
        
        private void ShowReader()
        {
            _reader.gameObject.SetActive(true);
        }

        private void HideReader()
        {
            _reader.gameObject.SetActive(false);
        }

        private void ShowUI()
        {
            _inspectableObjectUI.SetActive(true);
        }

        private void HideUI()
        {
            _inspectableObjectUI.SetActive(false);
        }

        private void ShowContinueText()
        {
            _continueText.gameObject.SetActive(true);
        }

        private void HideContinueText()
        {
            _continueText.gameObject.SetActive(false);
        }
    }
}