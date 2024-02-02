using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class PipesBreakTriggerArea : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private AudioSource _audioSource;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out Player.Player _)) return;

            GasLeakedStaticEvent.Call(this,
                new GasLeakedStaticEventArgs(_audioSource.transform.position, _audioSource));
            
            Destroy(gameObject);            
        }
    }
}