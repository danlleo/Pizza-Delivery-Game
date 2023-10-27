using System.Collections;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using Random = UnityEngine.Random;

namespace Dialogue
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class Reader : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private GameObject _dialogueContainer;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        
        [Header("Settings")] 
        [SerializeField] private float _waitTimeToMoveToNextLineInSeconds;
        
        [Space(5)]
        [SerializeField, Range(0f, 0.35f)] private float _characterTimeToPrintInSeconds;
        
        private Story _story;
        private AudioSource _audioSource;
        
        private bool _isReading;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

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
            StartCoroutine(DisplayTextRoutine(dialogue.Configuration));
        }
        
        private IEnumerator DisplayTextRoutine(ConfigurationSO configuration)
        {
            ClearDialogueText();
            
            string line = _story.Continue();
            
            int characterCount = 0;
            
            while (line.Length > 1)
            {
                char textCharacter = line[0];

                PlayCharacterTypeSound(characterCount, textCharacter, configuration);
                
                characterCount++;
                
                PrintTextCharacter(textCharacter);
                
                line = line[1..];
                yield return new WaitForSeconds(_characterTimeToPrintInSeconds);
            }

            if (_story.canContinue)
            {
                yield return new WaitForSeconds(_waitTimeToMoveToNextLineInSeconds);
                StartCoroutine(DisplayTextRoutine(configuration));
            }
            else
            {
                _isReading = false;
                yield return new WaitForSeconds(_waitTimeToMoveToNextLineInSeconds);
                HideDialogueContainer();
            }
        }
        
        private void PlayCharacterTypeSound(int currentDisplayCharacterCount, char character, ConfigurationSO configuration)
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

                int minPitch = (int)(configuration.MinimumPitch * 100);
                int maxPitch = (int)(configuration.MaximumPitch * 100);
                int pitchRange = maxPitch - minPitch;

                if (pitchRange != 0)
                {
                    int predictablePitchInt = (hashCode % pitchRange) + minPitch;
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
            => _dialogueText.text = "";

        private void PrintTextCharacter(char character)
        {
            _dialogueText.text += character;
        }
        
        private void ShowDialogueContainer()
            => _dialogueContainer.SetActive(true);
        
        private void HideDialogueContainer()
            => _dialogueContainer.SetActive(false);
    }
}
