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
        [SerializeField] private UI _ui;

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
            HideUI();
        }

        private void OnEnable()
        {
            FirstObjectiveSetStaticEvent.OnFirstObjectiveSet += OnFirstObjectiveSet;
            ObjectiveFinishedStaticEvent.OnObjectiveFinished += OnObjectiveFinished;
            ToggleObjectiveWindowStaticEvent.OnObjectiveWindowToggleChanged += OnObjectiveWindowToggleChanged;
            TimeControl.OnAnyGamePaused.Event += OnAnyGamePaused;
            TimeControl.OnAnyGameUnpaused.Event += OnAnyGameUnpaused;
        }
        
        private void OnDisable()
        {
            FirstObjectiveSetStaticEvent.OnFirstObjectiveSet -= OnFirstObjectiveSet;
            ObjectiveFinishedStaticEvent.OnObjectiveFinished -= OnObjectiveFinished;
            ToggleObjectiveWindowStaticEvent.OnObjectiveWindowToggleChanged -= OnObjectiveWindowToggleChanged;
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
            _objectRectWindow.DOLocalMove(_closedObjectiveWindowRectPosition, _objectiveWindowSlideTimeInSeconds);
        }

        private void ShowUI()
        {
            _objectiveUI.SetActive(true);
        }

        private void HideUI()
        {
            _objectiveUI.SetActive(false);
        }

        private void OnAnyGamePaused(object sender, EventArgs e)
        {
            HideUI();
        }
        
        private void OnAnyGameUnpaused(object sender, EventArgs e)
        {
            ShowUI();
        }
        
        private void OnObjectiveWindowToggleChanged(object sender, ToggleObjectiveWindowStaticEventArgs e)
        {
            if (e.IsOpen)
            {
                OpenObjectiveWindow();
                return;
            }

            CloseObjectiveWindow();
        }

        private void OnFirstObjectiveSet(object sender, FirstObjectiveSetStaticEventArgs e)
        {
            UpdateDisplayObjective(e.SetObjective.GetObjectiveSO());
            OpenObjectiveWindow();

            _ui.OnObjectiveUpdated.Call(_ui);
        }

        private void OnObjectiveFinished(object sender, ObjectiveFinishedStaticEventArgs e)
        {
            UpdateDisplayObjective(e.FinishedObjective.GetObjectiveSO());
            _ui.OnObjectiveUpdated.Call(_ui);
        }
    }
}