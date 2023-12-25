using System;
using System.Collections;
using InspectableObject;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.InspectableObject
{
    [DisallowMultipleComponent]
    public class InspectableObjectUI : MonoBehaviour
    {
        private const string UI_LAYER = "UI";

        [Header("External references")] [SerializeField]
        private GameObject _inspectableObjectUI;

        [SerializeField] private GameObject _objectParentContainer;
        [SerializeField] private UI _ui;
        [SerializeField] private TextMeshProUGUI _continueText;
        [SerializeField] private Reader _reader;

        [Header("Settings")] [SerializeField] private float _continueTextBlinkTimeInSeconds;

        [SerializeField] private float _objectRotationSpeed = 35f;

        private bool _allowedToRead;
        private bool _canClose;

        private Action _onComplete;
        private Coroutine _rotateObjectRoutine;

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
            _ui.ConfirmEvent.Call(_ui);
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
            if (_objectParentContainer.transform.childCount > 0)
                foreach (Transform child in _objectParentContainer.transform)
                    Destroy(child.gameObject);

            GameObject inspectableGameObject =
                Instantiate(inspectableObject.Prefab, _objectParentContainer.transform, true);

            inspectableGameObject.transform.position = _objectParentContainer.transform.position;
            inspectableGameObject.transform.localScale = inspectableObject.Scale;
            inspectableGameObject.transform.AddComponent<RectTransform>().localScale = inspectableObject.RectScale;

            SetLayersFor(inspectableGameObject);

            _rotateObjectRoutine = StartCoroutine(RotateObjectRoutine(inspectableGameObject));
        }

        private IEnumerator RotateObjectRoutine(GameObject targetGameObject)
        {
            while (true)
            {
                targetGameObject.transform.Rotate(Vector3.up * (_objectRotationSpeed * Time.deltaTime));
                targetGameObject.transform.Rotate(Vector3.forward * (_objectRotationSpeed * Time.deltaTime));

                yield return null;
            }
        }

        private void SetLayersFor(GameObject target)
        {
            target.layer = LayerMask.NameToLayer(UI_LAYER);

            foreach (Transform child in target.transform) child.gameObject.layer = LayerMask.NameToLayer(UI_LAYER);
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