using System.Collections;
using Dialogue;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Dialogue
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class DialogueReader : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private GameObject _dialogueContainer;

        [SerializeField] private UI _ui;
        [SerializeField] private TextMeshProUGUI _dialogueText;

        [Header("Settings")] 
        [SerializeField] private float _waitTimeToMoveToNextLineInSeconds;

        [Space(5)] 
        [SerializeField] [Range(0f, 0.35f)] private float _characterTimeToPrintInSeconds;
        
        private AudioSource _audioSource;

        private DialogueSO _currentDialogue;
        private ActionHolder _onReadAction;
        private Story _currentStory;

        private Coroutine _displayTextRoutine;
        
        private bool _isReading;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void ReadDialogue(DialogueSO dialogue)
        {
            if (_currentDialogue == dialogue)
                return;

            if (_displayTextRoutine != null)
            {
                StopCoroutine(_displayTextRoutine);
                _displayTextRoutine = null;
            }
            
            ClearOnReadAction();
            
            _isReading = true;
            _currentDialogue = dialogue;
            _currentStory = new Story(dialogue.DialogueText.text);
            _onReadAction = _currentDialogue.OnDialogueEnd;

            ShowDialogueContainer();

            _displayTextRoutine =
                StartCoroutine(DisplayTextRoutine(dialogue.OnDialogueEnd.TargetAction, dialogue.Configuration));
        }

        private IEnumerator DisplayTextRoutine(DialogueAction onComplete, ConfigurationSO configuration)
        {
            ClearDialogueText();
            
            _audioSource.volume = configuration.Volume;

            string line = _currentStory.Continue().Trim();

            var characterCount = 0;
            
            while (line.Length >= 1)
            {
                char textCharacter = line[0];

                PlayCharacterTypeSound(characterCount, textCharacter, configuration);

                characterCount++;

                PrintTextCharacter(textCharacter);

                line = line[1..];

                yield return new WaitForSeconds(_characterTimeToPrintInSeconds);
            }

            if (_currentStory.canContinue)
            {
                yield return new WaitForSeconds(_waitTimeToMoveToNextLineInSeconds);
                StartCoroutine(DisplayTextRoutine(onComplete, configuration));
            }
            else
            {
                _isReading = false;
                _currentDialogue = null;
                
                yield return new WaitForSeconds(_waitTimeToMoveToNextLineInSeconds);

                _ui.DialogueClosingEvent.Call(_ui);
                
                if (_onReadAction.TargetAction != null)
                    _onReadAction.TargetAction.Perform();
                    
                HideDialogueContainer();
            }
        }

        private void PlayCharacterTypeSound(int currentDisplayCharacterCount, char character,
            ConfigurationSO configuration)
        {
            if (currentDisplayCharacterCount % configuration.CharacterPlaySoundFrequency != 0)
                return;

            if (configuration.StopCharacterTypeSound)
                _audioSource.Stop();

            AudioClip targetAudioClip;

            if (configuration.MakePredictable)
            {
                int hashCode = character.GetHashCode();
                int predictableIndex = hashCode % configuration.AudioClips.Length;

                targetAudioClip = configuration.AudioClips[predictableIndex];

                var minPitch = (int)(configuration.MinimumPitch * 100);
                var maxPitch = (int)(configuration.MaximumPitch * 100);
                int pitchRange = maxPitch - minPitch;

                if (pitchRange != 0)
                {
                    int predictablePitchInt = hashCode % pitchRange + minPitch;
                    float predictablePitch = predictablePitchInt / 100f;

                    _audioSource.pitch = predictablePitch;
                }
                else
                {
                    _audioSource.pitch = minPitch;
                }
            }
            else
            {
                targetAudioClip = configuration.AudioClips[Random.Range(0, configuration.AudioClips.Length)];

                _audioSource.pitch = Random.Range(configuration.MinimumPitch, configuration.MaximumPitch);
            }

            _audioSource.PlayOneShot(targetAudioClip);
        }

        private void ClearDialogueText()
        {
            _dialogueText.text = "";
        }

        private void ClearOnReadAction()
        {
            _onReadAction = null;
        }
        
        private void PrintTextCharacter(char character)
        {
            _dialogueText.text += character;
        }

        private void ShowDialogueContainer()
        {
            _dialogueContainer.SetActive(true);
        }

        private void HideDialogueContainer()
        {
            _dialogueContainer.SetActive(false);
        }
    }
}