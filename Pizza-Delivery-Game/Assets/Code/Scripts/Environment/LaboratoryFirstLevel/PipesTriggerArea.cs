using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class PipesTriggerArea : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private ParticleSystem _smokeParticleSystem;
        [SerializeField] private BoxCollider _blockWayBoxCollider;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out Player.Player _)) return;
        
            GasLeakedStaticEvent.Call(this, new GasLeakedStaticEventArgs(transform.position));
            
            _blockWayBoxCollider.enabled = true;
            _smokeParticleSystem.Play();
        }
    }
}