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

        [Space(10)] 
        [SerializeField] private AudioClip _flashLightOnClip;
        [SerializeField] private AudioClip _flashLightOffClip;
        [SerializeField] private AudioClip _flashLightFlickClip;
        
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

        public void PlayFlashLightSwitchSound(bool isOn)
        {
            PlaySoundWithRandomPitch(_audioSource, isOn ? _flashLightOnClip : _flashLightOffClip, 0.9f, 1f, 4f);
        }

        public void PlayFlashLightFlickSound()
        {
            PlaySoundWithRandomPitch(_audioSource, _flashLightFlickClip, 0.8f, 1f);
        }
        
        private void Step_Event(object sender, StepEventArgs e)
        {
            switch (e.Surface)
            {
                case nameof(SurfaceType.Wood):
                    PlaySoundWithRandomPitch(_audioSource,
                        e.WasSprinting ? _woodenStepRunningClipsSO.AudioClips : _woodenStepClipsSO.AudioClips, 0.8f,
                        1.5f);
                    break;
                case nameof(SurfaceType.Tile):
                    PlaySoundWithRandomPitch(_audioSource,
                        e.WasSprinting ? _tileStepRunningClipsSO.AudioClips : _tileStepClipsSO.AudioClips, 0.8f, 1.5f);
                    break;
                case nameof(SurfaceType.Metal):
                    PlaySoundWithRandomPitch(_audioSource,
                        e.WasSprinting ? _metalStepRunningClipsSO.AudioClips : _metalStepClipsSO.AudioClips, 0.8f,
                        1.5f);
                    break;
                case nameof(SurfaceType.Grass):
                    PlaySoundWithRandomPitch(_audioSource,
                        e.WasSprinting ? _grassStepRunningClipsSO.AudioClips : _grassStepClipsSO.AudioClips, 0.8f,
                        1.5f);
                    break;
                case nameof(SurfaceType.Rock):
                    PlaySoundWithRandomPitch(_audioSource,
                        e.WasSprinting ? _rockStepRunningClipsSO.AudioClips : _rockStepClipsSO.AudioClips, 0.8f, 1.5f);
                    break;
            }
        }
        
        private void Landed_Event(object sender, LandedEventArgs e)
        {
            switch (e.Surface)
            {
                case nameof(SurfaceType.Wood):
                    PlaySoundWithRandomPitch(_audioSource, _woodenStepLandedClipsSO.AudioClips, 0.8f, 1.5f);
                    break;
                case nameof(SurfaceType.Tile):
                    PlaySoundWithRandomPitch(_audioSource, _tileStepLandedClipsSO.AudioClips, 0.8f, 1.5f);
                    break;
                case nameof(SurfaceType.Metal):
                    PlaySoundWithRandomPitch(_audioSource, _metalStepLandedClipsSO.AudioClips, 0.8f, 1.5f);
                    break;
                case nameof(SurfaceType.Grass):
                    PlaySoundWithRandomPitch(_audioSource, _grassStepLandedClipsSO.AudioClips, 0.8f, 1.5f);
                    break;
                case nameof(SurfaceType.Rock):
                    PlaySoundWithRandomPitch(_audioSource, _rockStepLandedClipsSO.AudioClips, 0.8f, 1.5f);
                    break;
            }
        }
    }
}
