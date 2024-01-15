using System;
using UI.GamePause;
using UnityEngine;
using UI;

namespace Sounds.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class ShareAudio : AudioPlayer
    {
        [SerializeField] private AudioClip _buttonFocusClip;
        [SerializeField] private AudioClip _buttonClickClip;
        
        [Space(5)]
        [SerializeField] private AudioClip _gamePauseClip; 
        [SerializeField] private AudioClip _gameUnpauseClip;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            GamePauseScreen.OnAnyButtonSelected += OnAnyButtonSelected;
            GamePauseScreen.OnAnyButtonPressed += OnAnyButtonPressed;
            PopupWindow.OnAnyButtonSelected += PopupWindow_OnAnyButtonSelected;
            PopupWindow.OnAnyButtonClicked += PopupWindow_OnAnyButtonClicked;
            TimeControl.OnAnyGamePaused.Event += OnAnyGamePaused;
            TimeControl.OnAnyGameUnpaused.Event += OnAnyGameUnpaused;
            SettingsWindow.OnAnyButtonClicked += SettingsWindow_OnAnyButtonClicked;
        }

        private void OnDisable()
        {
            GamePauseScreen.OnAnyButtonSelected -= OnAnyButtonSelected;
            GamePauseScreen.OnAnyButtonPressed -= OnAnyButtonPressed;
            PopupWindow.OnAnyButtonSelected -= PopupWindow_OnAnyButtonSelected;
            PopupWindow.OnAnyButtonClicked -= PopupWindow_OnAnyButtonClicked;
            TimeControl.OnAnyGamePaused.Event -= OnAnyGamePaused;
            TimeControl.OnAnyGameUnpaused.Event -= OnAnyGameUnpaused;
            SettingsWindow.OnAnyButtonClicked -= SettingsWindow_OnAnyButtonClicked;
        }

        private void OnAnyButtonSelected()
        {
            PlaySound(_audioSource, _buttonFocusClip, 0.5f);
        }
        
        private void OnAnyButtonPressed()
        {
            PlaySound(_audioSource, _buttonClickClip, 0.7f);
        }
        
        private void PopupWindow_OnAnyButtonClicked()
        {
            PlaySound(_audioSource, _buttonClickClip, 0.7f);
        }
        
        private void PopupWindow_OnAnyButtonSelected()
        {
            PlaySound(_audioSource, _buttonFocusClip, 0.5f);
        }
        
        private void OnAnyGamePaused(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _gamePauseClip);
        }
        
        private void OnAnyGameUnpaused(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _gameUnpauseClip);
        }
        
        private void SettingsWindow_OnAnyButtonClicked()
        {
            PlaySound(_audioSource, _buttonClickClip, 0.7f);
        }
    }
}
