using UI.MainMenu;
using UnityEngine;

namespace Sounds.Audio
{
    public class MainMenuAudio : AudioPlayer
    {
        [Header("Audio Clips")]
        [SerializeField] private AudioClip _buttonPressClip;
        [SerializeField] private AudioClip _buttonFocusClip;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            MainMenuScreen.OnAnyNewGameButtonClicked += MainMenuScreen_OnAnyNewGameButtonClicked;
            MainMenuScreen.OnAnyOptionsButtonClicked += MainMenuScreen_OnAnyOptionsButtonClicked;
            MainMenuScreen.OnAnyCreditsButtonClicked += MainMenuScreen_OnAnyCreditsButtonClicked;
            MainMenuScreen.OnAnyQuitButtonClicked += MainMenuScreen_OnAnyQuitButtonClicked;
            MainMenuScreen.OnAnyButtonSelected += MainMenuScreen_OnAnyButtonSelected;
            PopupWindow.OnAnyButtonSelected += PopupWindow_OnAnyButtonSelected;
            PopupWindow.OnAnyButtonClicked += PopupWindow_OnAnyButtonClicked;
        }
        
        private void OnDisable()
        {
            MainMenuScreen.OnAnyNewGameButtonClicked -= MainMenuScreen_OnAnyNewGameButtonClicked;
            MainMenuScreen.OnAnyOptionsButtonClicked -= MainMenuScreen_OnAnyOptionsButtonClicked;
            MainMenuScreen.OnAnyCreditsButtonClicked -= MainMenuScreen_OnAnyCreditsButtonClicked;
            MainMenuScreen.OnAnyQuitButtonClicked -= MainMenuScreen_OnAnyQuitButtonClicked;
            MainMenuScreen.OnAnyButtonSelected -= MainMenuScreen_OnAnyButtonSelected;
            PopupWindow.OnAnyButtonSelected -= PopupWindow_OnAnyButtonSelected;
            PopupWindow.OnAnyButtonClicked -= PopupWindow_OnAnyButtonClicked;
        }

        private void PopupWindow_OnAnyButtonClicked()
        {
            PlaySound(_audioSource, _buttonPressClip, 0.7f);
        }
        
        private void PopupWindow_OnAnyButtonSelected()
        {
            PlaySound(_audioSource, _buttonFocusClip, 0.5f);
        }

        private void MainMenuScreen_OnAnyButtonSelected()
        {
            PlaySound(_audioSource, _buttonFocusClip, 0.5f);
        }

        private void MainMenuScreen_OnAnyNewGameButtonClicked()
        {
            PlaySound(_audioSource, _buttonPressClip, 0.7f);
        }
        
        private void MainMenuScreen_OnAnyOptionsButtonClicked()
        {
            PlaySound(_audioSource, _buttonPressClip, 0.7f);
        }
        
        private void MainMenuScreen_OnAnyCreditsButtonClicked()
        {
            PlaySound(_audioSource, _buttonPressClip, 0.7f);
        }
        
        private void MainMenuScreen_OnAnyQuitButtonClicked()
        {
            PlaySound(_audioSource, _buttonPressClip, 0.7f);
        }
    }
}
