using System;
using UnityEngine;

namespace Environment.LaboratorySecondLevel
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public class MonsterAttractionArea : MonoBehaviour
    {
        [SerializeField] private AudioClip _shatteredGlassSound;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            InitializeAudioSource();
        }

        private void OnTriggerEnter(Collider other)
        {
            _audioSource.Play();
            
            if (!other.TryGetComponent(out Player.Player _))
                return;

            AttractedMonsterStaticEvent.Call(this, new AttractedMonsterEventArgs(transform.position));
        }

        private void InitializeAudioSource()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.spatialBlend = 1f;
            _audioSource.playOnAwake = false;
            _audioSource.clip = _shatteredGlassSound;
        }
    }
}
