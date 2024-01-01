using Sounds;
using UnityEngine;

namespace Monster
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class SurfaceSteps : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Transform _surfaceCheckPoint;
        
        [Space(5)]
        [SerializeField] private AudioClip _woodSurfaceClip;
        [SerializeField] private AudioClip _metalSurfaceClip;
        [SerializeField] private AudioClip _rockSurfaceClip;
        [SerializeField] private AudioClip _grassSurfaceClip;
        
        [Header("Settings")] 
        [SerializeField, Min(0.1f)] private float _distanceSurfaceCheck = 1f;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayStepSoundDependingOnSurface()
        {
            if (!Physics.Raycast(_surfaceCheckPoint.position, Vector3.down, out RaycastHit hit, _distanceSurfaceCheck))
                return;
            
            // I know this is horrible, but for the sake of simplicity, for now I will leave it like this
            switch (hit.collider.tag)
            {
                case "Wood":
                    AudioPlayer.PlaySoundWithRandomPitch(_audioSource, _woodSurfaceClip, 0.8f, 1f);
                    break;
                case "Metal":
                    AudioPlayer.PlaySoundWithRandomPitch(_audioSource, _metalSurfaceClip, 0.8f, 1f);
                    break;
                case "Rock":
                    AudioPlayer.PlaySoundWithRandomPitch(_audioSource, _rockSurfaceClip, 0.8f, 1f);
                    break;
                case "Grass":
                    AudioPlayer.PlaySoundWithRandomPitch(_audioSource, _grassSurfaceClip, 0.8f, 1f);
                    break;
            }
        }
    }
}
