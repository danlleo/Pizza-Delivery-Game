using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Dialogue
{
    [DisallowMultipleComponent]
    public class DialogueUI : MonoBehaviour
    {
        [Header("External references")] [SerializeField]
        private GameObject _dialogueUI;

        [SerializeField] private UI _ui;
        [FormerlySerializedAs("_reader")] [SerializeField] private DialogueReader _dialogueReader;

        private void Awake()
        {
            HideUI();
        }

        private void OnEnable()
        {
            _ui.DialogueOpeningEvent.Event += DialogueOpening_Event;
            _ui.DialogueClosingEvent.Event += DialogueClosing_Event;
        }

        private void OnDisable()
        {
            _ui.DialogueOpeningEvent.Event -= DialogueOpening_Event;
            _ui.DialogueClosingEvent.Event -= DialogueClosing_Event;
        }

        private void DialogueOpening_Event(object sender, DialogueOpeningEventArgs e)
        {
            ShowUI();
            _dialogueReader.ReadDialogue(e.Dialogue);
        }

        private void DialogueClosing_Event(object sender, EventArgs e)
        {
            HideUI();
        }

        private void ShowUI()
        {
            _dialogueUI.SetActive(true);
        }

        private void HideUI()
        {
            _dialogueUI.SetActive(false);
        }
    }
}