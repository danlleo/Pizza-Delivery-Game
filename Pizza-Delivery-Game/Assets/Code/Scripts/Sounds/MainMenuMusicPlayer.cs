using UI.MainMenu;
using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class MainMenuMusicPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            MainMenuScreen.OnAnyNewGameStarted += MainMenuScreen_OnAnyNewGameStarted;
        }

        private void OnDisable()
        {
            MainMenuScreen.OnAnyNewGameStarted -= MainMenuScreen_OnAnyNewGameStarted;
        }

        private void MainMenuScreen_OnAnyNewGameStarted()
        {
            _audioSource.Stop();
        }
    }
}
