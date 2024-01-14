using UI.GamePause;
using UnityEngine;

namespace Sounds.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class ShareAudio : AudioPlayer
    {
        [SerializeField] private AudioClip _buttonFocusClip;
        [SerializeField] private AudioClip _buttonClickClip;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            GamePauseScreen.OnAnyButtonSelected += OnAnyButtonSelected;
            GamePauseScreen.OnAnyButtonPressed += OnAnyButtonPressed;
        }

        private void OnDisable()
        {
            GamePauseScreen.OnAnyButtonSelected -= OnAnyButtonSelected;
            GamePauseScreen.OnAnyButtonPressed -= OnAnyButtonPressed;
        }

        private void OnAnyButtonSelected()
        {
            PlaySound(_audioSource, _buttonFocusClip, 0.5f);
        }
        
        private void OnAnyButtonPressed()
        {
            PlaySound(_audioSource, _buttonClickClip, 0.7f);
        }
    }
}
