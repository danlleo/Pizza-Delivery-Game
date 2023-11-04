﻿using System.Collections;
using InspectableObject;
using TMPro;
using UnityEngine;

namespace UI.InspectableObject
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class Reader : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private UI _ui;
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        [Header("Settings")]
        [SerializeField] private AudioClip _characterPrintClip;
        [SerializeField] private int _playCharacterPrintSoundFrequency = 2;
        [SerializeField, Range(0f, 0.25f)] private float _characterTimeToPrintInSeconds = 0.03f;
        
        private AudioSource _audioSource;

        private string _textToRead;
        private bool _isReading;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void BeginDisplay(InspectableObjectSO inspectableObject)
        {
            if (_isReading) return;
            
            ClearTexts();
            SetHeaderText(inspectableObject.HeaderText);
            ReadDescription(inspectableObject.DescriptionText);
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
            int characterCount = 0;
            
            while (_textToRead.Length > 1)
            {
                char textCharacter = _textToRead[0];
                
                PlayCharacterTypeSound(characterCount);

                characterCount++;
                
                PrintTextCharacter(textCharacter);

                _textToRead = _textToRead[1..];
                yield return new WaitForSeconds(_characterTimeToPrintInSeconds);
            }

            FinishReading();
        }

        private void FinishReading()
        {
            _isReading = false;
            _ui.InspectableObjectFinishedReadingEvent.Call(_ui, new InspectableObjectFinishedReadingEventArgs(true));
        }
        
        private void PrintTextCharacter(char character)
            => _descriptionText.text += character;

        private void PlayCharacterTypeSound(int currentDisplayCharacterCount)
        {
            if (currentDisplayCharacterCount % _playCharacterPrintSoundFrequency != 0) 
                return;
            
            _audioSource.PlayOneShot(_characterPrintClip);
        }
    }
}