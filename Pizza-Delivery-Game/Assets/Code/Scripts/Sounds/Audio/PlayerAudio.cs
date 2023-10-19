using System;
using UnityEngine;

namespace Sounds.Audio
{
    public class PlayerAudio : AudioPlayer
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Player.Player _player;
        [SerializeField] private SoundClipsSO _stepClipsSO;
        
        private void OnEnable()
        {
            _player.StepEvent.Event += Step_Event;
        }

        private void OnDisable()
        {
            _player.StepEvent.Event -= Step_Event;
        }

        private void Step_Event(object sender, EventArgs e)
        {
            PlaySound(_audioSource, _stepClipsSO.AudioClips);
        }
    }
}
