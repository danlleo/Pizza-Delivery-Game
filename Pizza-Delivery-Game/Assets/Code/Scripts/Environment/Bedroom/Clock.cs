using System.Collections;
using UnityEngine;

namespace Environment.Bedroom
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class Clock : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private Transform _handSecondTransform;
        [SerializeField] private AudioClip[] _tickClipsArray;
        
        private float _degreesPerSecond = 6f;
        private float _targetYRotation;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(RotateAndTick());
        }

        private IEnumerator RotateAndTick()
        {
            while (true)
            {
                if (_targetYRotation >= 360f)
                    _targetYRotation = 0f;
                
                _targetYRotation += _degreesPerSecond;
                _handSecondTransform.localRotation = Quaternion.Euler(_targetYRotation, -90f, 90f);
                _audioSource.clip = _tickClipsArray[Random.Range(0, _tickClipsArray.Length)];
                _audioSource.Play();
                
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
