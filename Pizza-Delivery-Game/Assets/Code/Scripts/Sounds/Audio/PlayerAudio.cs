using Player;
using UnityEngine;
using Enums.Environment;

namespace Sounds.Audio
{
    [DisallowMultipleComponent]
    public class PlayerAudio : AudioPlayer
    {
        [SerializeField] private Player.Player _player;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SoundClipsSO _tileStepClipsSO;
        [SerializeField] private SoundClipsSO _woodenStepClipsSO;
        [SerializeField] private SoundClipsSO _metalStepClipsSO;
        [SerializeField] private SoundClipsSO _grassStepClipsSO;
        
        private void OnEnable()
        {
            _player.StepEvent.Event += Step_Event;
        }

        private void OnDisable()
        {
            _player.StepEvent.Event -= Step_Event;
        }

        private void Step_Event(object sender, StepEventArgs e)
        {
            switch (e.Surface)
            {
                case nameof(SurfaceType.Wood):
                    PlaySound(_audioSource, _woodenStepClipsSO.AudioClips);
                    break;
                case nameof(SurfaceType.Tile):
                    PlaySound(_audioSource, _tileStepClipsSO.AudioClips);
                    break;
                case nameof(SurfaceType.Metal):
                    PlaySound(_audioSource, _metalStepClipsSO.AudioClips);
                    break;
                case nameof(SurfaceType.Grass):
                    PlaySound(_audioSource, _grassStepClipsSO.AudioClips);
                    break;
            }
        }
    }
}
