using UI.MainMenu;
using UnityEngine;

namespace Sounds.Audio
{
    public class MainMenuAudio : AudioPlayer
    {
        [SerializeField] private AudioClip _buttonPressClip;

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
        }

        private void OnDisable()
        {
            MainMenuScreen.OnAnyNewGameButtonClicked -= MainMenuScreen_OnAnyNewGameButtonClicked;
            MainMenuScreen.OnAnyOptionsButtonClicked -= MainMenuScreen_OnAnyOptionsButtonClicked;
            MainMenuScreen.OnAnyCreditsButtonClicked -= MainMenuScreen_OnAnyCreditsButtonClicked;
            MainMenuScreen.OnAnyQuitButtonClicked -= MainMenuScreen_OnAnyQuitButtonClicked;
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
