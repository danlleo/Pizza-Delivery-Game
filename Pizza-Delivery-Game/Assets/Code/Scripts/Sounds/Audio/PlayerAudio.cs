using Player;
using UnityEngine;
using Enums.Environment;

namespace Sounds.Audio
{
    [DisallowMultipleComponent]
    public class PlayerAudio : AudioPlayer
    {
        [Header("External references")]
        [SerializeField] private Player.Player _player;
        [SerializeField] private AudioSource _audioSource;
        
        [Header("Settings")]
        [SerializeField] private SoundClipsSO _tileStepClipsSO;
        [SerializeField] private SoundClipsSO _woodenStepClipsSO;
        [SerializeField] private SoundClipsSO _metalStepClipsSO;
        [SerializeField] private SoundClipsSO _grassStepClipsSO;
        [SerializeField] private SoundClipsSO _rockStepClipsSO;
        
        [Space(10)]
        [SerializeField] private SoundClipsSO _tileStepRunningClipsSO;
        [SerializeField] private SoundClipsSO _woodenStepRunningClipsSO;
        [SerializeField] private SoundClipsSO _metalStepRunningClipsSO;
        [SerializeField] private SoundClipsSO _grassStepRunningClipsSO;
        [SerializeField] private SoundClipsSO _rockStepRunningClipsSO;
        
        [Space(10)]
        [SerializeField] private SoundClipsSO _tileStepLandedClipsSO;
        [SerializeField] private SoundClipsSO _woodenStepLandedClipsSO;
        [SerializeField] private SoundClipsSO _metalStepLandedClipsSO;
        [SerializeField] private SoundClipsSO _grassStepLandedClipsSO;
        [SerializeField] private SoundClipsSO _rockStepLandedClipsSO;
        
        private void OnEnable()
        {
            _player.StepEvent.Event += Step_Event;
            _player.LandedEvent.Event += Landed_Event;
        }

        private void OnDisable()
        {
            _player.StepEvent.Event -= Step_Event;
            _player.LandedEvent.Event -= Landed_Event;
        }
        
        private void Step_Event(object sender, StepEventArgs e)
        {
            switch (e.Surface)
            {
                case nameof(SurfaceType.Wood):
                    PlaySound(_audioSource,
                        e.WasSprinting ? _woodenStepRunningClipsSO.AudioClips : _woodenStepClipsSO.AudioClips);
                    break;
                case nameof(SurfaceType.Tile):
                    PlaySound(_audioSource,
                        e.WasSprinting ? _tileStepRunningClipsSO.AudioClips : _tileStepClipsSO.AudioClips);
                    break;
                case nameof(SurfaceType.Metal):
                    PlaySound(_audioSource,
                        e.WasSprinting ? _metalStepRunningClipsSO.AudioClips : _metalStepClipsSO.AudioClips);
                    break;
                case nameof(SurfaceType.Grass):
                    PlaySound(_audioSource,
                        e.WasSprinting ? _grassStepRunningClipsSO.AudioClips : _grassStepClipsSO.AudioClips);
                    break;
                case nameof(SurfaceType.Rock):
                    PlaySound(_audioSource,
                        e.WasSprinting ? _rockStepRunningClipsSO.AudioClips : _rockStepClipsSO.AudioClips);
                    break;
            }
        }
        
        private void Landed_Event(object sender, LandedEventArgs e)
        {
            switch (e.Surface)
            {
                case nameof(SurfaceType.Wood):
                    PlaySound(_audioSource, _woodenStepLandedClipsSO.AudioClips);
                    break;
                case nameof(SurfaceType.Tile):
                    PlaySound(_audioSource, _tileStepLandedClipsSO.AudioClips);
                    break;
                case nameof(SurfaceType.Metal):
                    PlaySound(_audioSource, _metalStepLandedClipsSO.AudioClips);
                    break;
                case nameof(SurfaceType.Grass):
                    PlaySound(_audioSource, _grassStepLandedClipsSO.AudioClips);
                    break;
                case nameof(SurfaceType.Rock):
                    PlaySound(_audioSource, _rockStepLandedClipsSO.AudioClips);
                    break;
            }
        }
    }
}
