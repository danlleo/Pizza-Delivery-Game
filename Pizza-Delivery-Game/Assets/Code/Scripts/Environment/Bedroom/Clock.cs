using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment.Bedroom
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class Clock : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Transform _handHourTransform;
        [SerializeField] private Transform _handSecondTransform;
        [SerializeField] private AudioClip[] _tickClipsArray;
        
        private float _degreesPerSecond = 6f;
        private float _targetYRotation;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            PC.OnAnyStartedUsingPC.Event += OnAnyStartedUsingPC;
        }

        private void OnDisable()
        {
            PC.OnAnyStartedUsingPC.Event -= OnAnyStartedUsingPC;
        }

        private void OnAnyStartedUsingPC(object sender, EventArgs e)
        {
            _handHourTransform.transform.localRotation = Quaternion.Euler(new Vector3(-60f, -90f, 90f));
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
