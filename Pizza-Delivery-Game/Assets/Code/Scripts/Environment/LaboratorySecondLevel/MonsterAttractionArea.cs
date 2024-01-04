using System.Collections;
using UnityEngine;

namespace Environment.LaboratorySecondLevel
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public class MonsterAttractionArea : MonoBehaviour
    {
        [SerializeField] private AudioClip _shatteredGlassSound;
        
        private bool _hasAttracted;

        private AudioSource _audioSource;

        private void Awake()
        {
            InitializeAudioSource();
        }

        private void OnTriggerEnter(Collider other)
        {
            _audioSource.Play();
            
            if (_hasAttracted) return;
            
            if (!other.TryGetComponent(out Player.Player _))
                return;
            
            AttractedMonsterStaticEvent.Call(this, new AttractedMonsterEventArgs(transform.position));

            StartCoroutine(CooldownTimerRoutine());
        }

        private void InitializeAudioSource()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.spatialBlend = 1f;
            _audioSource.playOnAwake = false;
            _audioSource.clip = _shatteredGlassSound;
        }
        
        private IEnumerator CooldownTimerRoutine()
        {
            _hasAttracted = true;
            yield return new WaitForSeconds(3f);
            _hasAttracted = false;
        }
    }
}
