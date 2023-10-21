using System.Collections;
using UnityEngine;
using Ink.Runtime;
using TMPro;

namespace Dialogue
{
    public class Reader : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private GameObject _dialogueContainer;
        [SerializeField] private TextMeshProUGUI _dialogueText;

        [Header("Settings")] 
        [SerializeField, Range(0f, 0.35f)] private float _characterTimeToPrintInSeconds;
        [SerializeField] private float _waitTimeToMoveToNextLineInSeconds;

        private Story _story;
        
        private bool _isReading;
        
        private void OnEnable()
        {
            TriggeredStaticEvent.OnDialogueTriggered += TriggeredStatic_Event;
        }

        private void OnDisable()
        {
            TriggeredStaticEvent.OnDialogueTriggered -= TriggeredStatic_Event;
        }

        private void TriggeredStatic_Event(object sender, DialogueTriggeredEventArgs e)
        {
            ReadDialogue(e.Dialogue);
        }

        private void ReadDialogue(DialogueSO dialogue)
        {
            if (_isReading) return;
            
            _isReading = true;
                              
            _story = new Story(dialogue.DialogueText.text);
            
            ShowDialogueContainer();
            StartCoroutine(DisplayTextRoutine());
        }

        private void ClearDialogueText()
            => _dialogueText.text = "";

        private void PrintTextCharacter(char character)
        {
            _dialogueText.text += character;
        }
        
        private IEnumerator DisplayTextRoutine()
        {
            ClearDialogueText();
            
            string line = _story.Continue();
            
            while (line.Length > 1)
            {
                PrintTextCharacter(line[0]);
                line = line[1..];
                yield return new WaitForSeconds(_characterTimeToPrintInSeconds);
            }

            if (_story.canContinue)
            {
                yield return new WaitForSeconds(_waitTimeToMoveToNextLineInSeconds);
                StartCoroutine(DisplayTextRoutine());
            }
            else
            {
                _isReading = false;
                yield return new WaitForSeconds(_waitTimeToMoveToNextLineInSeconds);
                HideDialogueContainer();
            }
        }

        private void ShowDialogueContainer()
            => _dialogueContainer.SetActive(true);
        
        private void HideDialogueContainer()
            => _dialogueContainer.SetActive(false);
    }
}
