using System;
using DG.Tweening;
using Objective;
using TMPro;
using UnityEngine;

namespace UI.Objective
{
    [DisallowMultipleComponent]
    public class ObjectiveUI : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private GameObject _objectiveUI;
        [SerializeField] private RectTransform _objectRectWindow;
        [SerializeField] private TextMeshProUGUI _objectiveText;
        
        [Header("Settings")]
        [SerializeField] private float _objectiveWindowSlideTimeInSeconds = .335f;
            
        [SerializeField] private Vector2 _openedObjectiveWindowRectPosition;
        [SerializeField] private Vector2 _closedObjectiveWindowRectPosition;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            FirstObjectiveSetStaticEvent.OnFirstObjectiveSet += OnFirstObjectiveSet;
            ObjectiveFinishedStaticEvent.OnObjectiveFinished += OnObjectiveFinished;
        }

        private void OnDisable()
        {
            FirstObjectiveSetStaticEvent.OnFirstObjectiveSet -= OnFirstObjectiveSet;
            ObjectiveFinishedStaticEvent.OnObjectiveFinished -= OnObjectiveFinished;
        }

        private void UpdateDisplayObjective(ObjectiveSO objective)
        {
            _objectiveText.text = objective.ObjectiveText;
        }

        private void Initialize()
        {
            _objectRectWindow.localPosition = _closedObjectiveWindowRectPosition;
        }
        
        private void OpenObjectiveWindow()
        {
            ShowUI();
            _objectRectWindow.DOLocalMove(_openedObjectiveWindowRectPosition, _objectiveWindowSlideTimeInSeconds);
        }
        
        private void CloseObjectiveWindow()
        {
            _objectRectWindow.DOLocalMove(_closedObjectiveWindowRectPosition, _objectiveWindowSlideTimeInSeconds)
                .OnComplete(HideUI);
        }

        private void ShowUI()
            => _objectiveUI.SetActive(true);

        private void HideUI()
            => _objectiveUI.SetActive(false);
        
        private void OnFirstObjectiveSet(object sender, EventArgs e)
        {
            var objectiveRegistry = (ObjectiveRegistry)sender;
            
            UpdateDisplayObjective(objectiveRegistry.GetCurrentObjective().GetObjectiveSO());
            OpenObjectiveWindow();
        }
        
        private void OnObjectiveFinished(object sender, EventArgs e)
        {
            
        }
    }
}
