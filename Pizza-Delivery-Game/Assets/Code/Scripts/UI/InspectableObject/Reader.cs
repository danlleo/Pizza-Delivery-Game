using System.Collections;
using TMPro;
using UnityEngine;

namespace UI.InspectableObject
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class Reader : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Player.Player _player;
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        [Header("Settings")]
        [SerializeField, Range(0f, 0.25f)] private float _characterTimeToPrintInSeconds = 0.03f;
        
        private AudioSource _audioSource;

        private string _textToRead;
        private bool _isReading;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
            ClearTexts();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);

            SetHeaderText("Flashlight");
            ReadDescription(
                "Equipped with a durable aluminum alloy body, this flashlight is built to withstand the rigors of game development life. Its ergonomic grip ensures comfortable handling, allowing you to focus on your creativity without any discomfort. The ProGlow 5000 is also water-resistant, making it the ideal tool for rainy outdoor game testing or unexpected studio spills.\n");
        }

        private void ClearTexts()
        {
            _headerText.text = "";
            _descriptionText.text = "";
        }
        
        private void SetHeaderText(string header)
        {
            _headerText.text = header;
        }
        
        private void ReadDescription(string description)
        {
            if (_isReading) return;
            
            _isReading = true;
            _textToRead = description;
            
            StartCoroutine(DisplayTextRoutine());
        }

        private IEnumerator DisplayTextRoutine()
        {
            while (_textToRead.Length > 1)
            {
                char textCharacter = _textToRead[0];
                
                PrintTextCharacter(textCharacter);

                _textToRead = _textToRead[1..];
                yield return new WaitForSeconds(_characterTimeToPrintInSeconds);
            }

            FinishReading();
        }

        private void FinishReading()
        {
            _isReading = false;
            _player.InspectableObjectFinishedReadingEvent.Call(_player, new InspectableObjectFinishedReadingEventArgs(true));
        }
        
        private void PrintTextCharacter(char character)
            => _descriptionText.text += character;
    }
}