using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public class PipesTriggerArea : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private ParticleSystem _smokeParticleSystem;
        [SerializeField] private BoxCollider _blockWayBoxCollider;

        private AudioSource _audioSource;

        private bool _hasEntered;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_hasEntered) return;
            if (!other.gameObject.TryGetComponent(out Player.Player _)) return;

            GasLeakedStaticEvent.Call(this, new GasLeakedStaticEventArgs(transform.position, _audioSource));
            
            _blockWayBoxCollider.enabled = true;
            _hasEntered = true;
            
            _smokeParticleSystem.Play();
        }
    }
}