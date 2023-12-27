using EventBus;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{    
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class SmokeEffect : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private ParticleSystem _smokeParticleSystem;

        private AudioSource _audioSource;

        private EventBinding<FixPipesEvent> _fixPipesEventBinding;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = true;
        }

        private void OnEnable()
        {
            GasLeakedStaticEvent.OnAnyGasLeaked += OnAnyGasLeaked;

            _fixPipesEventBinding = new EventBinding<FixPipesEvent>(HandleFixPipesEvent);
            EventBus<FixPipesEvent>.Register(_fixPipesEventBinding);
        }

        private void OnDisable()
        {
            GasLeakedStaticEvent.OnAnyGasLeaked -= OnAnyGasLeaked;
            EventBus<FixPipesEvent>.Deregister(_fixPipesEventBinding);
        }
        
        private void OnAnyGasLeaked(object sender, GasLeakedStaticEventArgs e)
        {
            _audioSource.Play();
            _smokeParticleSystem.Play();
        }

        private void HandleFixPipesEvent(FixPipesEvent fixPipesEvent)
        {
            if (!fixPipesEvent.HasFixed) return;
            
            _audioSource.Stop();
            _smokeParticleSystem.Stop();
        }
    }
}